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

        public RedirectToPageResult OnPost(string radAnswer)
        {
            string[] uniInfo = radAnswer.Split(',');
            for (int i = 0; i < uniInfo.Length; i++)
            {
                uniInfo[i] = uniInfo[i].Trim();
            }
            string uniName = uniInfo[0];
            DeleteSchool.Delete(uniName);
            return RedirectToPage("/ViewPrograms");
        }
    }

    public class DeleteSchool
    {
        public static void Delete(string uniName)
        {
            SqlConnection dbconnection = new("Server=tcp:ufstudyabroadserver.database.windows.net,1433;Initial Catalog=ufstudyabroadDB;Persist Security Info=False;User ID=tech;Password=StudyAbroad23;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlDataAdapter adapter = new();
            dbconnection.Open();
            string sql = "DELETE FROM UNIVERSITIES WHERE University_Name = \'" + uniName + "\';";
            adapter.InsertCommand = new SqlCommand(sql, dbconnection);
            adapter.InsertCommand.ExecuteNonQuery();
            dbconnection.Close();
        }
    }
}