using Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;

namespace Final_Project.Controllers
{
    public class ProfileController: Controller
    {
        public IActionResult Index()
        { 
            return View();

        }
        public IActionResult MainProfile()
        {
            return View();

        }
        public IActionResult ChangePassword()
        {
            return View();

        }
        public IActionResult Order()
        {
            return View();

        }
        private object? getProfile(string email)
        {
            try
            { 
                if (email != null)
                {
                    var db = new WebContext();
                    var ls = db.Profiles.Where(x => x.Email == email).ToList(); 
                    return new
                    {
                        Data = ls
                    };
                }
                return null;    
                
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpPost]
        public IActionResult get_profile(String Email)
        {
            var data = getProfile(Email) ;
            if (data != null)
            {
                var res = new
                {
                    Success = true,
                    Message = "",
                    Data = data
                };
                return Json(res);
            }
            else
            {
                var res = new
                {
                    Success = false,
                    Message = "Lỗi xảy ra !"
                };
                return Json(res);
            }
        }



        public Profiles? checkDB(String email)
        {
            Profiles? res = new Profiles();
            if (email != null)
            {
                string cnStr = "Server = .; Database= Web; User id = admin; password = 123; Encrypt=False";
                SqlConnection cnn = new SqlConnection(cnStr);
                try
                {
                    cnn.Open();
                    SqlCommand cmd = cnn.CreateCommand();
                    cmd.Connection = cnn;

                    string sqlStr = "select * from Profiles where Email = '" + email + "'";  

                    cmd.CommandText = sqlStr;
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        res.Id = int.Parse(reader["Id"].ToString());
                        res.Name = reader["Name"].ToString();
                        res.Email = reader["Email"].ToString(); 
                    }
                    reader.Close();
                    if (!(res.Id > 0))
                        res = null;

                }
                catch (Exception ex)
                {
                    res = null;
                }
                if (cnn.State == System.Data.ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return res;
        }
    } 
    public class Profiles
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? Phone { get; set; }

        public string? Avatar { get; set; }

        public string? Address { get; set; }
    }
}
