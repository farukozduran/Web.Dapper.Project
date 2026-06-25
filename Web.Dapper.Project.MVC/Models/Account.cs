using System.ComponentModel.DataAnnotations;

namespace Web.Dapper.Project.MVC.Models
{
    public class Account
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Müşteri")]
        public int CustomerId { get; set; }

        [Required]
        [Display(Name = "Şube")]
        public int BranchId { get; set; }

        [Required(ErrorMessage = "Hesap numarası zorunludur.")]
        [StringLength(20)]
        [Display(Name = "Hesap Numarası")]
        public string AccountNumber { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Bakiye")]
        public decimal Balance { get; set; } = 0.00m;

        [Required]
        [StringLength(3)]
        [Display(Name = "Para Birimi")]
        public string Currency { get; set; } = "TRY";

        [Display(Name = "Aktif mi?")]
        public bool IsActive { get; set; } = true;
    }
}