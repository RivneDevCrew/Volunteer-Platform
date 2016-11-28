using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VolunteerSystem.Models
{
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Власник")]
        public string DriverId { get; set; }

        [Required(ErrorMessage = "Введіть держномер")]
        [Display(Name = "Держномер")]
        public string Plate { get; set; }

        [Required(ErrorMessage = "Вкажіть вантажопідйомність")]
        [Display(Name = "Вантажопідйомність(в кг)")]
        public float CargoMaxWeight { get; set; }

        [Required(ErrorMessage = "Вкажіть модель та марку авто")]
        [Display(Name = "Марка та модель")]
        public string ModelDescription { get; set; }
    }
}