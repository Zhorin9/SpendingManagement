using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SpendingManagement.WebUI.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Nazwa użytkownika")]
        [Required(AllowEmptyStrings = false, ErrorMessage ="Wymagana nazwa użytkownika")]
        public string UserName { get; set; }

        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage ="Podaj poprawny E-Mail")]
        public string Email { get; set; }

        [Display(Name ="Rok urodzin")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Hasło jest wymagane")]
        [MinLength(6,ErrorMessage = "Hasło jest za krótkie. Minimum 6 znaków.")]
        public string Password { get; set; }

    }
}