﻿using System.ComponentModel.DataAnnotations;

namespace RaumplanungCore.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        

       

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Erinnern?")]
        public bool RememberMe { get; set; }
    }
}
