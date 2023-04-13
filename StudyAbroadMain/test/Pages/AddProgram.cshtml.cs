using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace test.Pages
{

    public class AddProgramModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public AddProgramModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public string Result { get; set; }

        public RedirectToPageResult OnPost(string school, string major)
        {
            Result += "Program was added!";
                for (int i = 0; i < school.Length; i++)
            {
                if (school[i] == '~')
                {
                    school = school.Substring(0, i);
                }
            }
            EditPrograms.AddProgram(school, major);
            return RedirectToPage("/AdminOptions");
        }

    }

}