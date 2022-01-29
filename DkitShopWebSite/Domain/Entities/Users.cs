using System.ComponentModel.DataAnnotations;

namespace WebApi.Domain.Entities
{
    public class Users
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Это свойство обязательно к заполнению")]
        [Display(Name = "Логин пользователя")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Это свойство обязательно к заполнению")]
        [Display(Name = "Пароль пользователя")]
        public string Password { get; set; }

        public bool IsAdmin { get; set; } = false;

        [Display(Name = "Имя пользователя")]
        public string Name { get; set; }

        [Display(Name = "Аватарка пользователя")]
        public string AvatarBase64 { get; set; }
    }
}
