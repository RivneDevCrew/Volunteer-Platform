using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace VolunteerSystem.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Код")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Запам'ятати цей браузер?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "E-mail")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запам'ятати?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Не введено e-mail")]
        [EmailAddress(ErrorMessage = "Неправильний формат e-mail")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не введено пароль")]
        [StringLength(100, ErrorMessage = "Пароль повинен містити принаймні 6 символів", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Підтвердити пароль")]
        [Compare("Password", ErrorMessage = "Паролі не співпадають")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Не введено ПІБ")]
        [Display(Name = "ПІБ")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не вказано дату народження")]
        [DataType(DataType.Date, ErrorMessage = "Невірний формат дати")]
        [Display(Name = "Дата народження")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Не введено телефон")]
        [Display(Name = "Телефон")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0]{1})\)?([0-9]{9})$", 
            ErrorMessage = "Неправильний формат телефону")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Не вибрано область")]
        [Display(Name = "Область")]
        public string State { get; set; }

        [Required(ErrorMessage = "Не вибрано місто")]
        [Display(Name = "Місто")]
        public string City { get; set; }

        [Required(ErrorMessage = "Не введено адресу")]
        [Display(Name = "Адреса")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Не вибрано роль")]
        [Display(Name = "Роль")]
        public Roles Role { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Не введено e-mail")]
        [EmailAddress(ErrorMessage = "Недійсний формат e-mail")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не введено пароль")]
        [StringLength(100, ErrorMessage = "Пароль повинен містити принаймні 6 символів", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Новий пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Підтвердити новий пароль")]
        [Compare("Password", ErrorMessage = "Паролі не співпадають")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Не введено e-mail")]
        [EmailAddress(ErrorMessage = "Недійсний формат e-mail")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}
