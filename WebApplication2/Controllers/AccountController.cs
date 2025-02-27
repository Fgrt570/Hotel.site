using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using WebApplication2.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization; // Убедитесь, что путь к модели правильный
namespace WebApplication2.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        // GET: AccountController
        public ActionResult Index()
        {
            return View();
        }
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        // GET: Register
        [HttpGet]
        public IActionResult Register()
        {
          
            return View();
        }
        [HttpGet]
        public IActionResult Logg1()
        {
            return View(); // Возвращает пустую форму для входа
        }
        // GET: Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Logg1(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Profile", "Home");
                    }
                    else
                    {

                        ModelState.AddModelError("", "Неверное имя пользователя или пароль.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неверное имя пользователя или пароль.");
                }
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync(); // Удаляем email из сессии
            return RedirectToAction("Index", "Home");
        }
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User); // Получаем текущего аутентифицированного пользователя
            if (user == null)
            {
                return RedirectToAction("Logg1"); // Перенаправление на страницу входа, если пользователь не найден
            }
            Console.WriteLine($"Пользователь: {user.Email}");
            ViewBag.UserEmail = user.Email; // Сохраняем email в ViewBag
            var bookings = _context.Bookinggg.Where(b => b.UserId == user.Id).ToList();
            return View(); // Возвращаем представление профиля
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Profile", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
        // GET: AccountController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AccountController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AccountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccountController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
