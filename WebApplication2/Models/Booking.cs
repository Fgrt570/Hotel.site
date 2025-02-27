using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph.Models;
using System.ComponentModel.DataAnnotations;
namespace WebApplication2.Models
{
    public class Bookingg
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Имя обязательно")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Фамилия обязательна")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Номер телефона обязателен")]
        [Phone(ErrorMessage = "Некорректный номер телефона")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Дата заезда обязательна")]
        public DateTime CheckInDate { get; set; }

        [Required(ErrorMessage = "Дата выезда обязательна")]
        public DateTime CheckOutDate { get; set; }

        [Required(ErrorMessage = "Номер обязателен")]
        public int RoomId { get; set; }

        public string UserId { get; set; } // Связь с пользователем
        public virtual IdentityUser User { get; set; } // Навигационное свойство
        public virtual MRooms Room { get; set; } // Навигационное свойство
    }
}
