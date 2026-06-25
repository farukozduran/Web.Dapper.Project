using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Dapper.Project.MVC.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Hesap")]
        public int AccountId { get; set; }

        [Required(ErrorMessage = "Tutar zorunludur.")]
        [Display(Name = "Tutar")]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "İşlem Tipi")]
        public string TransactionType { get; set; } = string.Empty; // Deposit, Withdrawal, Transfer

        [Display(Name = "İşlem Tarihi")]
        public DateTime TransactionDate { get; set; } = DateTime.Now;
    }
}