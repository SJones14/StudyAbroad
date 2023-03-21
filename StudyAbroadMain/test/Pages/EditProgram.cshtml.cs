using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace test.Pages
{

    public class EditProgramModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public EditProgramModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public string Result { get; set; }

        public void OnPost(string input1, string input2)
        {
            Result = input1 + " " + input2;
        }
    }

}