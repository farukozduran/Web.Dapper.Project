using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Dapper.Project.MVC.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kimlik numarası zorunludur.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Kimlik numarası 11 karakter olmalıdır.")]
        [Display(Name = "TC Kimlik No")]
        public string IdentityNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        [StringLength(50)]
        [Display(Name = "Ad")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Soyad alanı zorunludur.")]
        [StringLength(50)]
        [Display(Name = "Soyad")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre zorunludur.")]
        [Display(Name = "Şifre")]
        public string PasswordHash { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
        [StringLength(100)]
        [Display(Name = "E-Posta")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Kayıt Tarihi")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}