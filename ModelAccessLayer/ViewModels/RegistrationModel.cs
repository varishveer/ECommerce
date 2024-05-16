using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelAccessLayer.ViewModels
{
    public class RegistrationModel
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        [Phone]
        public string? PhoneNumber { get; set; }
        [EmailAddress]
        [Required]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Display(Name = "ConfirmPassword")]
        [Required]
        [Compare("Password", ErrorMessage = "Password and Confirm Password not matched.")]
        public string? ConfirmPassword { get; set; }
        [Required]
        public DateOnly DOB {  get; set; }
        [Required]
        public string? Gender { get; set; }
    }
}
