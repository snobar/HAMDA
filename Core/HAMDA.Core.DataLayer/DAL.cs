using Dapper;
using HAMDA.Models.SystemCore;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace HAMDA.Models.DataLayer
{
    public class DAL : Disposer
    {
        private string ConnectionString { get; set; }

        public DynamicParameters dynamicParameters { get; set; } = new DynamicParameters();

        public DAL()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            ConnectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }

        public int ExecuteNonQuery(string procedureName)
        {
            using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
            {
                sqlCon.Open();

                dynamicParameters.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                sqlCon.Execute(procedureName, dynamicParameters, commandType: CommandType.StoredProcedure);

                int returnValue = dynamicParameters.Get<int>("@ReturnValue");

                return returnValue;
            }
        }
        public T ExecuteScalar<T>(string procedureName)
        {
            using (SqlConnection SqlCon = new SqlConnection(ConnectionString))
            {
                SqlCon.Open();
                return (T)Convert.ChangeType(SqlCon.ExecuteScalar(procedureName, dynamicParameters, commandType: CommandType.StoredProcedure), typeof(T));
            }
        }


        //DapperORM.ReturnList<EmployeeModel> <=  IEnumerable<EmployeeModel>
        public IEnumerable<T> ExecutQuery<T>(string procedureName)
        {
            using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
            {
                sqlCon.Open();
                return sqlCon.Query<T>(procedureName, dynamicParameters, commandType: CommandType.StoredProcedure);
            }

        }
    }
}
