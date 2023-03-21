using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace test.Pages
{

    public class DeleteSchoolModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public DeleteSchoolModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

    }
}