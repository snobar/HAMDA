using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMDA.Models.ViewModels
{
    public class UpdateCostumerModel
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public int Status { get; set; }

        [Required(ErrorMessage = "You must upload at least one image.")]
        [MaxLength(10, ErrorMessage = "You can upload up to 10 images.")]
        [Display(Name = "Payment Images")]
        public List<IFormFile> SaveAttachments { get; set; }

    }
}
