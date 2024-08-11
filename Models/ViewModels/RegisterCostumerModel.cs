using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HAMDA.Models.ViewModels
{
    public class RegisterCostumerModel
    {
        public int? CostumerCount { get; set; }
        public int? NumberOfSeats { get; set; }
        public string CurrentLanguage { get; set; }

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
    }
}
