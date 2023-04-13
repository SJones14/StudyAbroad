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

        public RedirectToPageResult OnPost(string school, string major)
        {
            Result += "Program has changed!";
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
    public class changeProgram
    {
        static string originalSchool;
        static string originalMajor;
        public static void setOriginalProgram(string school, string major)
        {
            originalSchool = school;
            originalMajor = major;
        }
        public static string getOriginalSchool()
        {
            return originalSchool;
        }
        public static string getOriginalMajor()
        {
            return originalMajor;
        }
    }

}