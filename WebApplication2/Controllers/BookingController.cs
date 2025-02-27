using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using WebApplication2.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering; // Убедитесь, что путь к модели правильный

namespace WebApplication2.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public IActionResult RedirectToBooking()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Если пользователь авторизован, перенаправляем на страницу создания бронирования
                return RedirectToAction("Create");
            }
            else
            {
                // Если пользователь не авторизован, перенаправляем на страницу профиля
                return RedirectToAction("Profile", "Account");
            }
        }
        public BookingController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Booking/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var rooms = await _context.Rooms
                .Select(r => new SelectListItem
                {
                    Value = r.id.ToString(),
                    Text = r.RoomType
                })
                .ToListAsync();

            ViewBag.Rooms = rooms;
            ViewBag.RoomPrices = await _context.Rooms
                .ToDictionaryAsync(r => r.id.ToString(), r => r.Price);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DateTime checkInDate, DateTime checkOutDate, int roomId, string firstName, string lastName, string phoneNumber)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                TempData["message"] = "Пользователь не аутентифицирован.";
                return RedirectToAction("Index");
            }

            bool isRoomAvailable = await IsRoomAvailable(roomId, checkInDate, checkOutDate);
            if (!isRoomAvailable)
            {
                TempData["message"] = "Номер недоступен на выбранные даты.";
                return RedirectToAction("Create");
            }

            var room = await _context.Rooms.FindAsync(roomId);
            if (room == null)
            {
                TempData["message"] = "Номер не найден.";
                return RedirectToAction("Create");
            }

            var booking = new Bookingg
            {
                UserId = userId,
                CheckInDate = checkInDate,
                CheckOutDate = checkOutDate,
                RoomId = roomId,
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber
            };

            _context.Bookinggg.Add(booking);
            await _context.SaveChangesAsync();

            TempData["message"] = "Бронирование успешно создано.";
            return RedirectToAction("MyBookings");
        }

        private async Task<bool> IsRoomAvailable(int roomId, DateTime checkInDate, DateTime checkOutDate)
        {
            var overlappingBookings = await _context.Bookinggg
                .Where(b => b.RoomId == roomId &&
                            !(b.CheckOutDate <= checkInDate || b.CheckInDate >= checkOutDate))
                .ToListAsync();

            return !overlappingBookings.Any();
        }
        // Просмотр бронирований пользователя
        public async Task<IActionResult> MyBookings()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Index");
            }

            var bookings = await _context.Bookinggg
                .Include(b => b.Room)
                .Where(b => b.UserId == userId)
                .ToListAsync();

            return View(bookings);
        }
        //ОТМЕНА БРОНИРОВАНИЯ
        [HttpPost]
        public async Task<IActionResult> Cancel(int id)
        {
            var booking = await _context.Bookinggg.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            // Проверяем, что текущий пользователь является владельцем бронирования
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (booking.UserId != userId)
            {
                return Forbid(); // Запрещаем доступ, если пользователь не владелец
            }

            _context.Bookinggg.Remove(booking);
            await _context.SaveChangesAsync();

            TempData["message"] = "Бронирование успешно отменено.";
            return RedirectToAction("MyBookings");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var booking = await _context.Bookinggg
                .Include(b => b.Room)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (booking.UserId != userId)
            {
                return Forbid();
            }

            // Заполняем список номеров для выпадающего списка
            ViewBag.Rooms = await _context.Rooms
                .Select(r => new SelectListItem
                {
                    Value = r.id.ToString(),
                    Text = r.RoomType
                })
                .ToListAsync();

            return View(booking);
        }
        [HttpPost]
[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Bookingg updatedBooking)
        {
            if (id != updatedBooking.Id)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var existingBooking = await _context.Bookinggg
                .FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);

            if (existingBooking == null)
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Обновляем данные бронирования
                    existingBooking.CheckInDate = updatedBooking.CheckInDate;
                    existingBooking.CheckOutDate = updatedBooking.CheckOutDate;
                    existingBooking.RoomId = updatedBooking.RoomId;
                    existingBooking.FirstName = updatedBooking.FirstName;
                    existingBooking.LastName = updatedBooking.LastName;
                    existingBooking.PhoneNumber = updatedBooking.PhoneNumber;

                    // Помечаем объект как изменённый
                    _context.Bookinggg.Update(existingBooking);
                    await _context.SaveChangesAsync();

                    TempData["message"] = "Бронирование успешно обновлено.";
                    return RedirectToAction("MyBookings");
                }
                catch (Exception ex)
                {
                    // Логируем исключение
                    Console.WriteLine(ex.Message);
                    TempData["message"] = "Произошла ошибка при сохранении изменений.";
                    return View(updatedBooking);
                }
            }

            // Если данные не прошли валидацию, перезагружаем список номеров
            ViewBag.Rooms = await _context.Rooms
                .Select(r => new SelectListItem
                {
                    Value = r.id.ToString(),
                    Text = r.RoomType
                })
                .ToListAsync();

            return View(updatedBooking);
        }
    }
}
