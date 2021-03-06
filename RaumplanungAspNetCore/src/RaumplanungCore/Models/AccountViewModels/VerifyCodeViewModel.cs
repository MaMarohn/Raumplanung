﻿using System.ComponentModel.DataAnnotations;

namespace RaumplanungCore.Models.AccountViewModels
{
    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        public string Code { get; set; }

        public string ReturnUrl { get; set; }

        [Display(Name = "Diesen Browser speichern?")]
        public bool RememberBrowser { get; set; }

        [Display(Name = "Erinnern?")]
        public bool RememberMe { get; set; }
    }
}
