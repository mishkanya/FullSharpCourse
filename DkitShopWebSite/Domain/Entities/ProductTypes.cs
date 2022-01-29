using System.ComponentModel.DataAnnotations;

namespace WebApi.Domain.Entities
{
    public class ProductTypes
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Это свойство обязательно к заполнению")]
        [Display(Name = "Название типа")]
        public string Name { get; set; }
    }
}
