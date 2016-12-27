using System.ComponentModel.DataAnnotations;

namespace RaumplanungCore.Models.AccountViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Das {0} muss mindestens {2} und darf maximal {1} Zeichen lang sein.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bestätige Passwort")]
        [Compare("Password", ErrorMessage = "Das Passwort und das Bestätigungpasswort stimmen nicht überein.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
