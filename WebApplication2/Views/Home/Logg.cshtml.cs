using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication2.Views.Home
{
    public class LoggModel : PageModel
    {
        private readonly ILogger<LoggModel> _logger;

        public LoggModel(ILogger<LoggModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
