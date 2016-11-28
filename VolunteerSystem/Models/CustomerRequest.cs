using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VolunteerSystem.Models
{
    public enum RequestStatus { Відкрита = 1, [Display(Name = "Очікує на виконання")]Очікує_виконання, Виконується,
        [Display(Name = "Очікує підтвердження")]Очікує_підтвердження, Закрита }

    public class CustomerRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Виберіть ресурс")]
        [Display(Name = "Ресурс")]
        public int ResourceInfoId { get; set; }

        [Required(ErrorMessage = "Вкажіть к-ть/об'єм")]
        [Display(Name = "К-ть/об'єм")]
        public float Count { get; set; }

        [Display(Name = "Замовник")]
        public string CustomerId { get; set; }

        [Display(Name = "Водій")]
        public string DriverId { get; set; }

        [Required(ErrorMessage = "Виберіть склад")]
        [Display(Name = "Склад")]
        public int WarehouseId { get; set; }

        [Required(ErrorMessage = "Вкажіть область")]
        [Display(Name = "Область")]
        public string DestinationState { get; set; }

        [Required(ErrorMessage = "Вкажіть місто")]
        [Display(Name = "Місто призначення")]
        public string DestinationCity { get; set; }

        [Display(Name = "Дата та час створення")]
        public DateTime CreationTime { get; set; }

        [Display(Name = "Дата та час завершення")]
        public DateTime DestinationTime { get; set; }

        [Display(Name = "Час виконання")]
        public TimeSpan TransitTime { get; set; }

        [Display(Name = "Статус")]
        public RequestStatus Status { get; set; } 
    }
}