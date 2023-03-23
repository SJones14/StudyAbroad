using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace test.Pages
{

    

    public class resultsModel : PageModel
    {
        private readonly ILogger<resultsModel> _logger;

        public resultsModel(ILogger<resultsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
        public RedirectToPageResult OnPost(string uniName, string ctry, string cont)
        {
            
            return RedirectToPage("/Index");
        }
    }
    
}