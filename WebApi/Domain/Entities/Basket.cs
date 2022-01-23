using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Domain.Entities
{
    public class Basket
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Идентификатор покупателя")]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "Идентификатор продукта")]
        public int ProductId { get; set; }

        [Required]
        [Display(Name = "Время заказа")]
        public DateTime OrderTime { get; set; }
    }
}
