using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace test.Pages
{
    public class ViewModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public ViewModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

    }
}