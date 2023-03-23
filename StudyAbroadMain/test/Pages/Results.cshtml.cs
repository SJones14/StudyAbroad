using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace test.Pages
{
    public class printresults
    {
        public static List<string> DBoutputPrograms()
        {
            SqlConnection dbconnection = new("Server=tcp:ufstudyabroadserver.database.windows.net,1433;Initial Catalog=ufstudyabroadDB;Persist Security Info=False;User ID=tech;Password=StudyAbroad23;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            dbconnection.Open();
            SqlCommand command;
            SqlDataReader reader;
            string sql = "Select University_Name, Country, Continent, Major From UNIVERSITIES, UNIVERSITY_PROGRAMS, PROGRAMS " +
                "WHERE UNIVERSITIES.University_ID = UNIVERSITY_PROGRAMS.University_ID AND UNIVERSITY_PROGRAMS.Program_ID = PROGRAMS.Program_ID" +
                ";";
            command = new SqlCommand(sql, dbconnection);
            reader = command.ExecuteReader();
            List<string> Output = new();
            while (reader.Read())
            {
                Output.Add(reader.GetValue(0) + "," + reader.GetValue(1) + "," + reader.GetValue(2) + "," + reader.GetValue(3));
            }
            dbconnection.Close();
            return Output;
        }
    }
    

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
    public class Filters
    {
        
        public static void setFilters(string m = "", string con = "", string cr = "")
        {
            string major = m;
            string continent = con;
            string Countries = cr;
        }
        
    }
    
}
