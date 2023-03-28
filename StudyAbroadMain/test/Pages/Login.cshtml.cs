using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;


namespace test.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(ILogger<LoginModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public string Failed { get; set; }

        public RedirectToPageResult OnPost(string uname, string pswd)
        {
            if (uname == null || pswd == null)
            {
                Failed += "Username or Password is incorrect. Please try again.";
                return null;
            }
            else if (CheckPassword.ComparePassword(uname, pswd))
            {
                Failed += "";
                return RedirectToPage("/AdminOptions");
            }
            else
            {
                Failed += "Username or Password is incorrect. Please try again.";
                return null;
            }
        }
    }

    public class CheckPassword
    {

        public static bool ComparePassword(string username, string password)
        {
            bool result;
            string dbUser = GetUser(username);
            string salt = GetSalt(username);
            string dbPassword = GetPassword(username);
            string saltAndPassword;


            saltAndPassword = salt + password;
            saltAndPassword = Hash.GetHashSha512(saltAndPassword);

            if (username.Equals(dbUser) && saltAndPassword.Equals(dbPassword))
            {
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }

        public static string GetPassword(string username)
        {
            SqlConnection dbconnection = new("Server=tcp:ufstudyabroadserver.database.windows.net,1433;Initial Catalog=ufstudyabroadDB;Persist Security Info=False;User ID=tech;Password=StudyAbroad23;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            dbconnection.Open();
            SqlCommand command;
            SqlDataReader reader;
            String sql, Output = "";
            sql = "SELECT * FROM ADMINS WHERE UserName = '" + username + "';";
            command = new SqlCommand(sql, dbconnection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Output = (string)reader.GetValue(2);
            }
            dbconnection.Close();
            return Output;
        }

        public static string GetUser(string username)
        {
            SqlConnection dbconnection = new("Server=tcp:ufstudyabroadserver.database.windows.net,1433;Initial Catalog=ufstudyabroadDB;Persist Security Info=False;User ID=tech;Password=StudyAbroad23;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            dbconnection.Open();
            SqlCommand command;
            SqlDataReader reader;
            String sql, Output = "";
            sql = "SELECT * FROM ADMINS WHERE UserName = '" + username + "';";
            command = new SqlCommand(sql, dbconnection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Output = (string)reader.GetValue(1);
            }
            Output = Output.Trim();
            dbconnection.Close();
            return Output;
        }

        public static string GetSalt(string username)
        {
            SqlConnection dbconnection = new("Server=tcp:ufstudyabroadserver.database.windows.net,1433;Initial Catalog=ufstudyabroadDB;Persist Security Info=False;User ID=tech;Password=StudyAbroad23;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            dbconnection.Open();
            SqlCommand command;
            SqlDataReader reader;
            String sql, Output = "";
            sql = "SELECT * FROM ADMINS WHERE UserName = '" + username + "';";
            command = new SqlCommand(sql, dbconnection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Output = (string)reader.GetValue(3);
            }
            dbconnection.Close();
            return Output;
        }
    }

    public class Hash
    {
        public static string GetHashSha512(string text)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(text);
            SHA512Managed hashstring = new();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }
    }
}
