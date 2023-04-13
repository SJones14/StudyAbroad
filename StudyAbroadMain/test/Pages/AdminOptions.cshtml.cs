using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace test.Pages
{
    public class AdminOptionsModel : PageModel
    {
        public void OnGet()
        {
        }

        public RedirectToPageResult OnPostDelete(string school, string major)
        {
            for (int i = 0; i < school.Length; i++)
            {
                if (school[i] == '~')
                {
                    school = school.Substring(0, i);
                }
            }
            EditPrograms.DeleteProgram(school, major);
            return RedirectToPage("/AdminOptions");
        }
        public void OnPostEdit()
        {
            Console.WriteLine("it works");
        }
    }

    public class EditPrograms
    {
        public static void DeleteProgram(string school, string major)
        {
            SqlConnection dbconnection = new("Server=tcp:ufstudyabroadserver.database.windows.net,1433;Initial Catalog=ufstudyabroadDB;Persist Security Info=False;User ID=tech;Password=StudyAbroad23;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlDataAdapter adapter = new();
            dbconnection.Open();
            string sql = "DELETE FROM UNIVERSITY_PROGRAMS " +
                " WHERE University_ID = (SELECT University_ID FROM UNIVERSITIES WHERE University_Name = \'" + school + "\') " +
                " AND Program_ID = (SELECT Program_ID FROM PROGRAMS WHERE Major = \'" + major + "\');";

            adapter.InsertCommand = new SqlCommand(sql, dbconnection);
            adapter.InsertCommand.ExecuteNonQuery();
            dbconnection.Close();
        }

        public static void AddProgram(string school, string major)
        {
            SqlConnection dbconnection = new("Server=tcp:ufstudyabroadserver.database.windows.net,1433;Initial Catalog=ufstudyabroadDB;Persist Security Info=False;User ID=tech;Password=StudyAbroad23;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlDataAdapter adapter = new();
            dbconnection.Open();
            string sql = "INSERT INTO UNIVERSITY_PROGRAMS Select University_ID, Program_ID " +
                "           From UNIVERSITIES, PROGRAMS " +
                "           Where UNIVERSITIES.University_Name = \'" + school + "\' And PROGRAMS.Major = \'" + major + "\';";
            adapter.InsertCommand = new SqlCommand(sql, dbconnection);
            adapter.InsertCommand.ExecuteNonQuery();
            dbconnection.Close();
        }
    }

    public class table
    {
        public static string DBoutputSQLCode()
        {
            string[] major = Filters.getMajor();
            string[] country = Filters.getCountries();
            string[] Continent = Filters.getContinent();




            string sql = "SELECT University_Name, Country, Continent, Major FROM UNIVERSITIES, UNIVERSITY_PROGRAMS, PROGRAMS " +
                "WHERE UNIVERSITIES.University_ID = UNIVERSITY_PROGRAMS.University_ID AND UNIVERSITY_PROGRAMS.Program_ID = PROGRAMS.Program_ID;";

            return sql;
        }
        public static List<string> DBoutputProgramUniversityName()
        {

            SqlConnection dbconnection = new("Server=tcp:ufstudyabroadserver.database.windows.net,1433;Initial Catalog=ufstudyabroadDB;Persist Security Info=False;User ID=tech;Password=StudyAbroad23;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            dbconnection.Open();
            SqlCommand command;
            SqlDataReader reader;

            string sql = DBoutputSQLCode();
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
        public static List<string> DBoutputProgramCountry()
        {

            SqlConnection dbconnection = new("Server=tcp:ufstudyabroadserver.database.windows.net,1433;Initial Catalog=ufstudyabroadDB;Persist Security Info=False;User ID=tech;Password=StudyAbroad23;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            dbconnection.Open();
            SqlCommand command;
            SqlDataReader reader;

            string sql = DBoutputSQLCode();
            command = new SqlCommand(sql, dbconnection);
            reader = command.ExecuteReader();
            List<string> Output = new();
            while (reader.Read())
            {
                Output.Add(reader.GetValue(1).ToString());
            }
            dbconnection.Close();

            return Output;
        }
        public static List<string> DBoutputProgramContinent()
        {

            SqlConnection dbconnection = new("Server=tcp:ufstudyabroadserver.database.windows.net,1433;Initial Catalog=ufstudyabroadDB;Persist Security Info=False;User ID=tech;Password=StudyAbroad23;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            dbconnection.Open();
            SqlCommand command;
            SqlDataReader reader;

            string sql = DBoutputSQLCode();
            command = new SqlCommand(sql, dbconnection);
            reader = command.ExecuteReader();
            List<string> Output = new();
            while (reader.Read())
            {
                Output.Add(reader.GetValue(2).ToString());
            }
            dbconnection.Close();

            return Output;
        }

        public static List<string> DBoutputProgramMajor()
        {

            SqlConnection dbconnection = new("Server=tcp:ufstudyabroadserver.database.windows.net,1433;Initial Catalog=ufstudyabroadDB;Persist Security Info=False;User ID=tech;Password=StudyAbroad23;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            dbconnection.Open();
            SqlCommand command;
            SqlDataReader reader;

            string sql = DBoutputSQLCode();

            command = new SqlCommand(sql, dbconnection);
            reader = command.ExecuteReader();
            List<string> Output = new();
            while (reader.Read())
            {
                Output.Add(reader.GetValue(3).ToString());
            }
            dbconnection.Close();

            return Output;
        }
    }
}

