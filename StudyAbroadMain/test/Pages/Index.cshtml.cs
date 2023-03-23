using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace test.Pages
{

    public class Database
    {
        public static List<string> DBoutput()
        {
            SqlConnection dbconnection = new("Server=tcp:ufstudyabroadserver.database.windows.net,1433;Initial Catalog=ufstudyabroadDB;Persist Security Info=False;User ID=tech;Password=StudyAbroad23;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            dbconnection.Open();
            SqlCommand command;
            SqlDataReader reader;
            string sql = "Select * From UNIVERSITIES";
            command = new SqlCommand(sql, dbconnection);
            reader = command.ExecuteReader();
            List<string> Output = new();
            while (reader.Read())
            {
                Output.Add(reader.GetValue(1) + "," + reader.GetValue(2) + "," + reader.GetValue(3));
            }
            dbconnection.Close();
            return Output;
        }
        public static List<string> DBoutputMajors()
        {
            SqlConnection dbconnection = new("Server=tcp:ufstudyabroadserver.database.windows.net,1433;Initial Catalog=ufstudyabroadDB;Persist Security Info=False;User ID=tech;Password=StudyAbroad23;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            dbconnection.Open();
            SqlCommand command;
            SqlDataReader reader;
            string sql = "Select DISTINCT Major From PROGRAMS";
            command = new SqlCommand(sql, dbconnection);
            reader = command.ExecuteReader();
            List<string> Output = new();
            while (reader.Read())
            {
                Output.Add(reader.GetValue(0).ToString());
            }
            dbconnection.Close();
            return Output;
        }
        public static List<string> DBoutputContinents()
        {
            SqlConnection dbconnection = new("Server=tcp:ufstudyabroadserver.database.windows.net,1433;Initial Catalog=ufstudyabroadDB;Persist Security Info=False;User ID=tech;Password=StudyAbroad23;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            dbconnection.Open();
            SqlCommand command;
            SqlDataReader reader;
            string sql = "Select DISTINCT Continent From UNIVERSITIES";
            command = new SqlCommand(sql, dbconnection);
            reader = command.ExecuteReader();
            List<string> Output = new();
            while (reader.Read())
            {
                Output.Add(reader.GetValue(0).ToString());
            }
            dbconnection.Close();
            return Output;
        }
        public static List<string> DBoutputCountries()
        {
            SqlConnection dbconnection = new("Server=tcp:ufstudyabroadserver.database.windows.net,1433;Initial Catalog=ufstudyabroadDB;Persist Security Info=False;User ID=tech;Password=StudyAbroad23;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            dbconnection.Open();
            SqlCommand command;
            SqlDataReader reader;
            string sql = "Select DISTINCT Country From UNIVERSITIES";
            command = new SqlCommand(sql, dbconnection);
            reader = command.ExecuteReader();
            List<string> Output = new();
            while (reader.Read())
            {
                Output.Add(reader.GetValue(0).ToString());
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
        public RedirectToPageResult OnPost(string[] Major, string[] ctry, string[] cont)
        {


            return RedirectToPage("/results");
        }

    }

    public class filterResults
    {

    }

}