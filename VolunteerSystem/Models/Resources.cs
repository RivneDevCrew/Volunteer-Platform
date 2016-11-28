using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VolunteerSystem.Models
{
    public class ResourceInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Введіть опис ресурсу")]
        [Display(Name = "Найменування")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Виберіть тип ресурсу")]
        [Display(Name = "Тип ресурсу")]
        public ResTypes ResType { get; set; }

        [Required(ErrorMessage = "Виберіть одиниці вимірювання")]
        [Display(Name = "Одиниці вимірювання")]
        public ResMeasures ResMeasure { get; set; }
    }

    public class ResWarehouses
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Виберіть ресурс")]
        [Display(Name = "Ресурс")]
        public int ResourceInfoId { get; set; }

        [Required(ErrorMessage = "Введіть к-ть ресурсу")]
        [Display(Name = "К-ть")]
        public float Count { get; set; }

        [Required(ErrorMessage = "Виберіть склад")]
        [Display(Name = "Склад")]
        public int WarehouseId { get; set; }
    }

    public class ResourceDisplayModel
    {
        public string Description { get; set; }
        public float Amount { get; set; }
        public ResMeasures Measure { get; set; }

        public override string ToString()
        {
            return Description + " - " + Amount + " " + Measure.ToString();
        }
    }
}