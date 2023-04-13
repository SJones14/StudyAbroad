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
            SqlConnection dbconnection = new("Server=tcp:ufstudyabroadserver." +
                "database.windows.net,1433;Initial Catalog=ufstudyabroadDB;" +
                "Persist Security Info=False;User ID=tech;Password=StudyAbroad23" +
                ";MultipleActiveResultSets=False;Encrypt=True;TrustServerCertifi" +
                "cate=False;Connection Timeout=30;");
            dbconnection.Open();
            SqlCommand command;
            SqlDataReader reader;
            string sql = "Select * From UNIVERSITIES";
            command = new SqlCommand(sql, dbconnection);
            reader = command.ExecuteReader();
            List<string> Output = new();
            while (reader.Read())
            {
                Output.Add(reader.GetValue(1) + "~ " + reader.GetValue(2) + "~ " + reader.GetValue(3));
            }
            dbconnection.Close();
            return Output;
        }
        public static List<string> DBAllMajors()
        {
            SqlConnection dbconnection = new("Server=tcp:ufstudyabroadserver." +
                "database.windows.net,1433;Initial Catalog=ufstudyabroadDB;" +
                "Persist Security Info=False;User ID=tech;Password=StudyAbroad" +
                "23;MultipleActiveResultSets=False;Encrypt=True;TrustServerCerti" +
                "ficate=False;Connection Timeout=30;");
            dbconnection.Open();
            SqlCommand command;
            SqlDataReader reader;
            string sql = "SELECT Major from PROGRAMS;";
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
        public static List<string> DBProgramMajors()
        {
            SqlConnection dbconnection = new("Server=tcp:ufstudyabroadserver." +
                "database.windows.net,1433;Initial Catalog=ufstudyabroadDB;" +
                "Persist Security Info=False;User ID=tech;Password=StudyAbroad" +
                "23;MultipleActiveResultSets=False;Encrypt=True;TrustServerCerti" +
                "ficate=False;Connection Timeout=30;");
            dbconnection.Open();
            SqlCommand command;
            SqlDataReader reader;
            string sql = "SELECT DISTINCT Major FROM UNIVERSITIES, UNIVERSITY" +
                "_PROGRAMS, PROGRAMS WHERE UNIVERSITIES.University_ID = UNIVE" +
                "RSITY_PROGRAMS.University_ID AND UNIVERSITY_PROGRAMS.Program" +
                "_ID = PROGRAMS.Program_ID;";
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
            SqlConnection dbconnection = new("Server=tcp:ufstudyabroadserver." +
                "database.windows.net,1433;Initial Catalog=ufstudyabroadDB;Pe" +
                "rsist Security Info=False;User ID=tech;Password=StudyAbroad2" +
                "3;MultipleActiveResultSets=False;Encrypt=True;TrustServerCer" +
                "tificate=False;Connection Timeout=30;");
            dbconnection.Open();
            SqlCommand command;
            SqlDataReader reader;
            string sql = "SELECT DISTINCT Continent FROM UNIVERSITIES, UNIVER" +
                "SITY_PROGRAMS, PROGRAMS WHERE UNIVERSITIES.University_ID = U" +
                "NIVERSITY_PROGRAMS.University_ID AND UNIVERSITY_PROGRAMS.Pro" +
                "gram_ID = PROGRAMS.Program_ID;";
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
            SqlConnection dbconnection = new("Server=tcp:ufstudyabroadserver." +
                "database.windows.net,1433;Initial Catalog=ufstudyabroadDB;Pe" +
                "rsist Security Info=False;User ID=tech;Password=StudyAbroad2" +
                "3;MultipleActiveResultSets=False;Encrypt=True;TrustServerCer" +
                "tificate=False;Connection Timeout=30;");
            dbconnection.Open();
            SqlCommand command;
            SqlDataReader reader;
            string sql = "SELECT DISTINCT Country FROM UNIVERSITIES, UNIVERSI" +
                "TY_PROGRAMS, PROGRAMS WHERE UNIVERSITIES.University_ID = UNI" +
                "VERSITY_PROGRAMS.University_ID AND UNIVERSITY_PROGRAMS.Progr" +
                "am_ID = PROGRAMS.Program_ID;";
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
            
            filterResults.result(Major, ctry, cont);
            
            return RedirectToPage("/results");
        }

    }

    public class filterResults
    {
        public static void result(string[] Major, string[] ctry, string[] cont)
        {
            Filters.setFilters(Major, cont, ctry);
        }
    }
    }


