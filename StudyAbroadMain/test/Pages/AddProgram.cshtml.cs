using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace test.Pages
{

    public class AddProgramModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public AddProgramModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public string? Result { get; set; }

        public RedirectToPageResult? OnPost(string school, string major)
        {
            if (school == null || major == null)
            {
                Result += "Enter all program information";
                return null;
            }
            else
            {
                for (int i = 0; i < school.Length; i++)
                {
                    if (school[i] == '~')
                    {
                        school = school.Substring(0, i);
                    }
                }

                bool isNotUnique = checkPrograms.uniqueProgram(school.Trim(), major.Trim());

                if (isNotUnique)
                {
                    Result += "That program already exists.";
                    return null;
                }
                else
                {
                    Result += "";
                    EditPrograms.AddProgram(school.Trim(), major.Trim());
                    return RedirectToPage("/AdminOptions");
                }
            }

        }

    }

    public class checkPrograms
    {
        public static bool uniqueProgram(string school, string major)
        {
            bool isNotUnique = false;

            SqlConnection dbconnection = new("Server=tcp:ufstudyabroadserver.database.windows.net,1433;Initial Catalog=ufstudyabroadDB;Persist Security Info=False;User ID=tech;Password=StudyAbroad23;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            dbconnection.Open();
            SqlCommand command;
            SqlDataReader reader;
            string sql = "SELECT University_Name, Major FROM UNIVERSITIES, PROGRAMS, " +
                "UNIVERSITY_PROGRAMS  WHERE UNIVERSITY_PROGRAMS.University_ID = " +
                "UNIVERSITIES.University_ID AND UNIVERSITY_PROGRAMS.Program_ID = " +
                "PROGRAMS.Program_ID;";
            command = new SqlCommand(sql, dbconnection);
            reader = command.ExecuteReader();
            List<string> dbProgramSchool = new();
            List<string> dbProgramMajor = new();

            while (reader.Read())
            {
                dbProgramSchool.Add(reader.GetValue(0).ToString());
                dbProgramMajor.Add(reader.GetValue(1).ToString());
            }

            for (int i = 0; i < dbProgramSchool.Count; i++)
            {
                string checkSchool = dbProgramSchool[i].Trim();
                string checkMajor = dbProgramMajor[i].Trim();

                if (school == checkSchool && major == checkMajor)
                {
                    isNotUnique = true;
                    return isNotUnique;
                }
            }



            dbconnection.Close();

            return isNotUnique;
        }
    }

}