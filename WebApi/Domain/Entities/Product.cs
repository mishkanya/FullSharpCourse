using System.ComponentModel.DataAnnotations;

namespace WebApi.Domain.Entities
{
    public class Product
    {
        [Required]
        public int Id { get; set; } //Первичный ключ для поиска продукта
        [Required(ErrorMessage ="Это свойство обязательно к заполнению")]
        [Display(Name ="Название продукта")]
        public string Name { get; set; }
        [Display(Name = "Описание продукта")]
        public string Description{ get; set; }
        [Required(ErrorMessage = "Это свойство обязательно к заполнению")]
        [Display(Name = "Цена продукта")]
        public double Price { get; set; }
        [Display(Name = "Изображение продукта, закодированное в base64")]
        public string ImageBase64 { get; set; }
        [Required(ErrorMessage = "Это свойство обязательно к заполнению")]
        [Display(Name = "Тип продукта")]
        public int TypeId { get; set; }
    }
}
