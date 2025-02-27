using Microsoft.AspNetCore.Identity;
namespace WebApplication2.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Добавьте кастомные свойства при необходимости
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
