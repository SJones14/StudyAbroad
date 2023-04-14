using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Reflection.PortableExecutable;

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

        public string? Result { get; set; }

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
            changeProgram.editProgram(school, major);
            return RedirectToPage("/AdminOptions");
        }

    }
    public class changeProgram
    {
        static string? originalSchool;
        static string? originalSchoolID;
        static string? originalMajor;
        static string? originalMajorID;

        public static void setOriginalProgram(string school, string major)
        {
            originalSchool = school.Trim();
            originalMajor = major.Trim();
            setOriginalIDs();
        }
        public static string getOriginalSchool()
        {
            return originalSchool;
        }
        public static string getOriginalMajor()
        {
            return originalMajor;
        }
        public static void editProgram(string school, string major)
        {
            string[] IDs = new string[2];
            IDs = getNewIDS(school, major);
            SqlConnection dbconnection = new("Server=tcp:ufstudyabroadserver.database.windows.net,1433;Initial Catalog=ufstudyabroadDB;Persist Security Info=False;User ID=tech;Password=StudyAbroad23;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlDataAdapter adapter = new();
            dbconnection.Open();
            string sql = "UPDATE UNIVERSITY_PROGRAMS SET University_ID = \'" + IDs[0] + "\', Program_ID =\'" + IDs[1] +
                            "\' WHERE  University_ID = \'" + originalSchoolID + "\' AND Program_ID = \'" + originalMajorID + "\';";
            adapter.InsertCommand = new SqlCommand(sql, dbconnection);
            adapter.InsertCommand.ExecuteNonQuery();
            dbconnection.Close();
        }
        public static string[] getNewIDS(string school, string major)
        {
            string[] IDs = new string[2]; //location 0 = university ID and location 1 = major ID

            SqlConnection dbconnection = new("Server=tcp:ufstudyabroadserver.database.windows.net,1433;Initial Catalog=ufstudyabroadDB;Persist Security Info=False;User ID=tech;Password=StudyAbroad23;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            dbconnection.Open();
            SqlCommand command;
            SqlDataReader reader;

            string sql = "SELECT UNIVERSITIES.University_ID, PROGRAMS.Program_ID, University_Name, Major " +
                           "FROM UNIVERSITIES, UNIVERSITY_PROGRAMS, PROGRAMS " +
                           "WHERE University_Name = \'" + school.Trim() + "\' AND Major = \'" + major.Trim() + "\' ;";

            command = new SqlCommand(sql, dbconnection);
            reader = command.ExecuteReader();

            reader.Read();
            IDs[0] = reader.GetValue(0).ToString();
            IDs[1] = reader.GetValue(1).ToString();

            dbconnection.Close();

            return IDs;
        }
        public static void setOriginalIDs()
        {
            SqlConnection dbconnection = new("Server=tcp:ufstudyabroadserver.database.windows.net,1433;Initial Catalog=ufstudyabroadDB;Persist Security Info=False;User ID=tech;Password=StudyAbroad23;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            dbconnection.Open();
            SqlCommand command;
            SqlDataReader reader;

            string sql = "SELECT UNIVERSITY_PROGRAMS.University_ID, UNIVERSITY_PROGRAMS.Program_ID, University_Name, Major " +
                           "FROM UNIVERSITIES, UNIVERSITY_PROGRAMS, PROGRAMS " +
                           "WHERE UNIVERSITIES.University_ID = UNIVERSITY_PROGRAMS.University_ID AND UNIVERSITY_PROGRAMS.Program_ID = PROGRAMS.Program_ID " +
                           "AND University_Name = \'" + originalSchool + "\' AND Major = \'" + originalMajor + "\' ;";

            command = new SqlCommand(sql, dbconnection);
            reader = command.ExecuteReader();

            reader.Read();
            originalSchoolID = reader.GetValue(0).ToString();
            originalMajorID = reader.GetValue(1).ToString();

            dbconnection.Close();
        }
    }

}