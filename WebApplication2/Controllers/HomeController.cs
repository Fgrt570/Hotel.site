using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        SqlConnection con = new SqlConnection("Server=PC;Database=cyrs;Trusted_Connection=True;MultipleActiveResultSets=true"); // Замените на вашу строку подключения
        SqlCommand com = new SqlCommand();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<MRooms> rooms = new List<MRooms>();
            com.Connection = con;
            con.Open();
            com.CommandText = "SELECT * FROM Rooms";
            SqlDataReader reader = com.ExecuteReader();

            while (reader.Read())
            {
                MRooms room = new MRooms
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

        public IActionResult AddNewreg(string Description, string ImageUrl)
        {
            try
            {
                com.Connection = con;
                con.Open();

                com.CommandText = "Insert into Rooms values('" + Description + "','" + ImageUrl + "')";

                com.ExecuteNonQuery();

                con.Close();

                TempData["message"] = "Data Saved Successfully";
                return RedirectToAction("Index");
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
        public IActionResult Aboutus()
        {
            return View(); // Возвращает представление About.cshtml
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Logg()
        {
            return View();
        }
        public IActionResult Rooms()
        {
            return View();
        }
        public ActionResult RedirectToAnotherPage()
        {
            return RedirectToAction("Logg");
        }
        public ActionResult RedirectToIndex()
        {
            return RedirectToAction("Logg");
        }
        public IActionResult Profile()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
