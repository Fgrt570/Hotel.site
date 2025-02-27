using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
   
    public class MRooms
    {
        public int id { get; set; }
        public string RoomType { get; set; } // Тип номера (например, "Стандарт", "Люкс")
        [Column(TypeName = "decimal(18, 0)")]
        public decimal Price { get; set; } // Цена за номер

    }
}
