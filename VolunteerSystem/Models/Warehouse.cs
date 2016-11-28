using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VolunteerSystem.Models
{
    public class Warehouse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Введіть назву складу")]
        [Display(Name = "Назва складу")]
        public string Alias { get; set; }

        [Required(ErrorMessage = "Виберіть область")]
        [Display(Name = "Область")]
        public string State { get; set; }

        [Required(ErrorMessage = "Виберіть місто")]
        [Display(Name = "Місто")]
        public string City { get; set; }

        [Required(ErrorMessage = "Вкажіть адресу")]
        [Display(Name = "Адреса")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Введіть місткість/об'єм")]
        [Display(Name = "Місткість/об'єм")]
        public float Capacity { get; set; }

        [Required(ErrorMessage = "Вкажіть тип складу")]
        [Display(Name = "Тип ресурсів")]
        public ResTypes StoredResType { get; set; }
    }
}