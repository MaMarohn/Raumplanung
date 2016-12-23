using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RaumplanungCore.Models.AccountViewModels
{
    public class RegisterViewModel
    {

        [Required]
        [Display(Name = "Nachname")]
        public string Nachname { get; set; }

        [Required]
        [Display(Name = "Admin")]
        public bool Admin { get; set; }

        [Required]
        [Display(Name = "Vorname")]
        public string Vorname { get; set; }

        [Required]
        [Display(Name = "Anrede")]
        public string Anrede { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Das {0} muss mindestens {2} und darf maximal {1} Zeichen lang sein.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Passwort")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bestätige Passwort")]
        [Compare("Password", ErrorMessage = "Das Passwort und das Bestätigungpasswort stimmen nicht überein.")]
        public string ConfirmPassword { get; set; }
    }
}
