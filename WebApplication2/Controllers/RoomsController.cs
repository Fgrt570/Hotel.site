using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class RoomsController : Controller
    {
        private readonly ApplicationDbContext _context;
        SqlConnection con = new SqlConnection("Server=PC;Database=cyrs;Trusted_Connection=True;MultipleActiveResultSets=true"); // Замените на вашу строку подключения
        SqlCommand com = new SqlCommand();
        public RoomsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> TestConnection()
        {
            try
            {
                bool canConnect = await _context.Database.CanConnectAsync();
                if (canConnect)
                {
                    return Content("Подключение к базе данных успешно!");
                }
                else
                {
                    return Content("Не удалось подключиться к базе данных.");
                }
            }
            catch (Exception ex)
            {
                return Content($"Ошибка подключения: {ex.Message}");
            }
        }
        // GET: Rooms
        public IActionResult Rooms()
        {
            List<MRooms> rooms = new List<MRooms>();
            com.Connection = con;
            con.Open();
            com.CommandText = "SELECT * FROM Rooms";
            SqlDataReader reader = com.ExecuteReader();

            while (reader.Read())
            {
                MRooms room = new()
                {
                    id = reader.GetInt32(0),
                    RoomType = reader.GetString(1),
                    Price = reader.GetDecimal(2),
                    
                    // Добавьте другие поля по мере необходимости
                };
                rooms.Add(room);
            }
            ViewBag.emplist = rooms;
            return View(rooms); // Передача списка комнат в представление 
        }

        [HttpPost]

        public IActionResult AddNewRecord(string Description, string ImageUrl)
        {
            try
            {
                com.Connection = con;
                con.Open();
            
                com.CommandText = "Insert into Rooms values('" + Description + "','" + ImageUrl + "')";

                com.ExecuteNonQuery();

                con.Close();

                TempData["message"] = "Data Saved Successfully";
                return RedirectToAction("Rooms");
            }
            catch (Exception ex)
            {
                
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }

                TempData["message"] = "Data Save Failed";
                return RedirectToAction("Rooms");
            }
        }

    }
    
}
