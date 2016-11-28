using System.ComponentModel.DataAnnotations;

namespace VolunteerSystem.Models
{
    public class FeedbackModel
    {
        [Display(Name = "Ім'я")]
        public string Name { get; set; }

        [Display(Name = "Прізвище")]
        public string Surname { get; set; }

        [EmailAddress(ErrorMessage = "Неправильний формат e-mail")]
        [Required(ErrorMessage = "Введіть e-mail")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введіть повідомлення")]
        [Display(Name = "Повідомлення")]
        public string Message { get; set; }
    }
}