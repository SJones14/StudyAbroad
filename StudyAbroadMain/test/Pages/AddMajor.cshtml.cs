using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace test.Pages
{
    public class AddMajorModel : PageModel
    {
        private readonly ILogger<AddUniversityModel> _logger;

        public AddMajorModel(ILogger<AddUniversityModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public string Failed { get; set; }

        public RedirectToPageResult OnPost(string major)
        {
            bool notUnique;

            if (major == null)
            {
                Failed += "Please enter the name of the major.";
                return null;
            }
            else
            {
                notUnique = AddMajor.checkUniqueMajor(major);
                if (notUnique == false)
                {
                    Failed += "";
                    AddMajor.Add(major);
                    return RedirectToPage("/AddProgram");
                }
                else
                {
                    Failed += "That major has already been added to the list.";
                    return null;
                }
            }
        }
    }

    public class AddMajor
    {
        public static void Add(string major)
        {
            SqlConnection dbconnection = new("Server=tcp:ufstudyabroadserver.database.windows.net,1433;Initial Catalog=ufstudyabroadDB;Persist Security Info=False;User ID=tech;Password=StudyAbroad23;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlDataAdapter adapter = new();
            dbconnection.Open();
            string sql = "INSERT INTO PROGRAMS VALUES (\'" + major + "\');";
            adapter.InsertCommand = new SqlCommand(sql, dbconnection);
            adapter.InsertCommand.ExecuteNonQuery();
            dbconnection.Close();
        }

        public static bool checkUniqueMajor(string major){
            
            bool notUnique = false;

            SqlConnection dbconnection = new("Server=tcp:ufstudyabroadserver.database.windows.net,1433;Initial Catalog=ufstudyabroadDB;Persist Security Info=False;User ID=tech;Password=StudyAbroad23;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            dbconnection.Open();
            SqlCommand command;
            SqlDataReader reader;
            string sql = "SELECT Major FROM PROGRAMS";
            command = new SqlCommand(sql, dbconnection);
            reader = command.ExecuteReader();
            List<string> dbMajors = new();

            while (reader.Read())
            {
                dbMajors.Add(reader.GetValue(0).ToString());
            }

            for (int i = 0; i < dbMajors.Count; i++)
            {
                string check = dbMajors[i].Trim();
                notUnique = major.Equals(check, StringComparison.OrdinalIgnoreCase);

                if (notUnique)
                {
                    return notUnique;
                }
            }

            dbconnection.Close();

            
            return notUnique;
        }
}
}
