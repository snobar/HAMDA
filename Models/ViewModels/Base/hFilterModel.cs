using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMDA.Models.ViewModels.Base
{
    public class hFilterModel
    {
        public int totalCount { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int totalPages => (int)Math.Ceiling(totalCount / (double)pageSize);
        public bool HasPreviousPage => pageNumber > 1;
        public bool HasNextPage => pageNumber < totalPages;
        public int NumberOfSeats { get; set; }
        public string hSearch { get; set; }
    }
}
