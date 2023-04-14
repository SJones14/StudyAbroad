using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace test.Pages
{
    public class AddUniversityModel : PageModel
    {
        private readonly ILogger<AddUniversityModel> _logger;

        public AddUniversityModel(ILogger<AddUniversityModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public string? InsertStatus { get; set; }

        public RedirectToPageResult? OnPost(string uniName, string ctry, string cont)
        {
            if (AddSchool.checkUnique(uniName, ctry))
            {
                InsertStatus += "University was added";
                AddSchool.Add(uniName, ctry, cont);
                return RedirectToPage("/AddProgram");
            }
            else
            {
                InsertStatus += "This University already Exists";
                return null;
            }
            
            
        }
    }

    public class AddSchool
    {
        public static void Add(string name, string ctry, string cont)
        {
            
            
            SqlConnection dbconnection = new("Server=tcp:ufstudyabroadserver.database.windows.net,1433;Initial Catalog=ufstudyabroadDB;Persist Security Info=False;User ID=tech;Password=StudyAbroad23;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlDataAdapter adapter = new();
            dbconnection.Open();

            
                string sql = "INSERT INTO UNIVERSITIES VALUES (\'" + name + "\', \'" + ctry + "\', \'" + cont + "\');";
                adapter.InsertCommand = new SqlCommand(sql, dbconnection);
                adapter.InsertCommand.ExecuteNonQuery();
            
            
            dbconnection.Close();
        }
        public static bool checkUnique(string name, string ctry){
            
            bool Unique = true;

            SqlConnection dbconnection = new("Server=tcp:ufstudyabroadserver.database.windows.net,1433;Initial Catalog=ufstudyabroadDB;Persist Security Info=False;User ID=tech;Password=StudyAbroad23;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            dbconnection.Open();
            SqlCommand command;
            SqlDataReader reader;
            string sql = "SELECT University_Name, Country FROM UNIVERSITIES";
            command = new SqlCommand(sql, dbconnection);
            reader = command.ExecuteReader();
            List<string> dbNames = new();
            List<string> dbCountries = new();

            while (reader.Read())
            {
                dbNames.Add(reader.GetValue(0).ToString());
                dbCountries.Add(reader.GetValue(1).ToString());
            }

            for (int i = 0; i < dbNames.Count; i++)
            {
                if (name == dbNames[i].Trim() && ctry == dbCountries[i].Trim())
                {
                    Unique = false;
                }
            }
            dbconnection.Close();

            
            return Unique;
        }
}
}
