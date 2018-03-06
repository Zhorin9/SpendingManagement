using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SpendingManagement.WebUI.Models.Account
{
    public class LoginViewModel
    {
        [Display(Name = "E-mail")]
        [Required(AllowEmptyStrings =false, ErrorMessage = "Adres e-mail jest wymagany")]
        [DataType( DataType.EmailAddress, ErrorMessage ="Wprowadź poprawny adres e-mail")]
        public string Email { get; set; }

        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false ,ErrorMessage = "Hasło jest wymagane")]
        public string UserPassword { get; set; }

        [Display (Name = "Zapamiętaj mnie")]
        public bool RememberMe { get; set; }
    }
}