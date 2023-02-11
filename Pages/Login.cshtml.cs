using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace test.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public LoginModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}