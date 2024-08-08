using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HAMDA.Models.ViewModels
{
    public class RegisterCostumerModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "You must upload at least one image.")]
        [MaxLength(10, ErrorMessage = "You can upload up to 10 images.")]
        [Display(Name = "Payment Images")]
        public List<IFormFile> Attachments { get; set; }
    }
}
