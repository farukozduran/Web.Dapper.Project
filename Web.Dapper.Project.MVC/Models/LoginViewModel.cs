using System.ComponentModel.DataAnnotations;

namespace Web.Dapper.Project.MVC.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Kimlik numarası zorunludur.")]
        [Display(Name = "TC Kimlik No")]
        public string IdentityNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre zorunludur.")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; } = string.Empty;
    }
}