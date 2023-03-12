using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace test.Pages
{

    public class Database
    {
        public static string DBoutput()
        {
            SqlConnection dbconnection = new SqlConnection("Server=tcp:ufstudyabroadserver.database.windows.net,1433;Initial Catalog=ufstudyabroadDB;Persist Security Info=False;User ID=tech;Password=StudyAbroad23;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            dbconnection.Open();
            SqlCommand command;
            SqlDataReader reader;
            String sql, Output = "";
            sql = "Select * From UNIVERSITIES";
            command = new SqlCommand(sql, dbconnection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Output += reader.GetValue(1);
                Output += "| " + reader.GetValue(2) + "| " + reader.GetValue(3) + "\n";
            }
            dbconnection.Close();
            return Output;
        }
    }

    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

    }
}