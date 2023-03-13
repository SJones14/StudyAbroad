using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace test.Pages
{
    public class AddUniversityModel : PageModel
    {
        private readonly ILogger<AddUniversityModel> _logger;

        public AddUniversityModel(ILogger<AddUniversityModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}