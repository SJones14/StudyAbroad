using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Collections.Specialized;
using System.Data.Common;

namespace test.Pages
{
    public class printresults
    {
        public static List<string> DBoutputPrograms()
        {
            string[] major = Filters.getMajor();
            string[] country = Filters.getCountries();
            string[] Continent = Filters.getContinent();
            string majorSQLCode = "";
            string countrySQLCode = "";
            string continentSQLCode = "";

            if(major.Length > 0) { 
            majorSQLCode = " AND (Major = \'" + major[0] + "\'";
            for (int i = 1; i < major.Length; i++)
            {
                majorSQLCode += " OR Major = \'" + major[i] + "\'";
            }
                majorSQLCode += ") ";
            }

            if(country.Length > 0) { 
            countrySQLCode = " AND (Country = \'" + country[0] + "\'";
            for (int i = 1; i < country.Length; i++)
            {
                countrySQLCode += " OR Country = \'" + country[i] + "\'";
            }
                countrySQLCode += ") ";
            }

            if (Continent.Length > 0)
            {
            continentSQLCode = " AND (Continent = \'" + Continent[0] + "\'";
            for (int i = 1; i < Continent.Length; i++)
            {
                continentSQLCode += " OR Continent = \'" + Continent[i] + "\'";
            }
                continentSQLCode += ") ";
            }
            SqlConnection dbconnection = new("Server=tcp:ufstudyabroadserver.database.windows.net,1433;Initial Catalog=ufstudyabroadDB;Persist Security Info=False;User ID=tech;Password=StudyAbroad23;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            dbconnection.Open();
            SqlCommand command;
            SqlDataReader reader;

            string sql = "SELECT University_Name, Country, Continent, Major FROM UNIVERSITIES, UNIVERSITY_PROGRAMS, PROGRAMS " +
                "WHERE UNIVERSITIES.University_ID = UNIVERSITY_PROGRAMS.University_ID AND UNIVERSITY_PROGRAMS.Program_ID = PROGRAMS.Program_ID " +
                majorSQLCode + 
                countrySQLCode + 
                continentSQLCode + " ;";
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

        private static string[] Major;
        private static string[] continent;
        private static string[] Countries;
        public static void setFilters(string[] m, string[] con, string[] cr)
        {
            Major = new string[m.Length];
            for (int i = 0; i< m.Length; i++)
            {
                Major[i] = m[i];
            }
            continent = new string[con.Length];
            for (int i = 0; i < con.Length; i++)
            {
                continent[i] = con[i];
            }
            Countries = new string[cr.Length];
            for (int i = 0; i < cr.Length; i++)
            {
                Countries[i] = cr[i];
            }
            
        }
        public static string[] getMajor() { return Major; }
        public static string[] getContinent() { return continent;}
        public static string[] getCountries() {  return Countries;}
    }
    
}
