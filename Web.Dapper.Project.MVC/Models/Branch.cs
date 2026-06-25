using System.ComponentModel.DataAnnotations;

namespace Web.Dapper.Project.MVC.Models
{
    public class Branch
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Şube adı zorunludur.")]
        [StringLength(100)]
        [Display(Name = "Şube Adı")]
        public string BranchName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şehir zorunludur.")]
        [StringLength(50)]
        [Display(Name = "Şehir")]
        public string City { get; set; } = string.Empty;
    }
}