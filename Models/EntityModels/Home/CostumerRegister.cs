using HAMDA.Models.EntityModels.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HAMDA.Models.EntityModels.Costumer
{
    public class CostumerRegister : EntityBase
    {

        [Required]
        [Column(TypeName = "NVARCHAR(MAX)")]
        public string Username { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(MAX)")]
        public string Country { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(MAX)")]
        public string Phone { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(MAX)")]
        public string Email { get; set; }
    }
}
