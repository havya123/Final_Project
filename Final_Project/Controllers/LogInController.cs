using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;
using Final_Project.Models;
using Microsoft.Data.SqlClient;

namespace Final_Project.Controllers
{
    public class LogInController : Controller
    {
        private string _key;

        public LogInController()
        {
            _key = "E546C8DF278CD5931069B522E695D4F2";
        }
        public IActionResult Index()
        {
            if ((HttpContext.Session.GetString("Username")) == null)
            {
                return View();
            }
            else
                return Redirect("home");
        }

        [HttpPost]
        public IActionResult doLogin(LoginData data)
        {
            Users? usr = checkLogin(data);
            if (usr == null)
            {
                var res = new
                {
                    Success = false,
                    Message = "Tìm không thấy User !"
                };
                return Json(res);
            }
            else
            {
                var pass = data.Password;
                var passHashed = EncryptString(pass, _key);
                var decryptedPass = DecryptString(usr.Password, _key);
                if (pass == decryptedPass)
                {
                    var res = new
                    {
                        Success = true,
                        Message = "Hello",
                        User = usr
                    };
                    HttpContext.Session.SetString("Username", usr.Username);
                    HttpContext.Session.SetString("Fullname", usr.Fullname);
                    return Json(res);
                }
                else
                {
                    var res = new
                    {
                        Success = false,
                        Message = "Sai password !",
                    };
                    return Json(res);
                }
            }
        }

        [HttpPost]
        public IActionResult Register(IFormCollection fc)
        {
            User usr = new User();
            usr.Username = fc["floatingInputUsername"].ToString();
            usr.Fullname = fc["floatingInputFullname"].ToString();
            usr.Password = fc["floatingPassword"].ToString();
            usr.Email = fc["floatingInputEmail"];
            var obj = CreateUser(usr);
            return Redirect("/");
        }

        [HttpPost]
        public IActionResult signOut()
        {
            HttpContext.Session.Remove("Username");
            HttpContext.Session.Remove("Fullname");
            var res = new { Success = true, Massage = "" };
            return Json(res);

        }

        public Users? checkLogin(LoginData data)
        {
            Users? res = new Users();
            if (data != null)
            {
                string cnStr = "Server = .; Database = Web; User id = duc; password = 123456;TrustServerCertificate=True";
                SqlConnection cnn = new SqlConnection(cnStr);
                try
                {
                    cnn.Open();
                    SqlCommand cmd = cnn.CreateCommand();
                    cmd.Connection = cnn;

                    string sqlStr = "select * from Users ";
                    sqlStr += "where Username= '" + data.Username + "'";
                    //sqlStr += "and [Password]= '" + data.Password + "'";

                    cmd.CommandText = sqlStr;
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        res.Id = int.Parse(reader["Id"].ToString());
                        res.Username = reader["Username"].ToString();
                        res.Password = reader["Password"].ToString();
                        if (reader["LastLogin"] != null && reader["LastLogin"].ToString() != "")
                        {
                            res.LastLogin = DateTime.Parse(reader["LastLogin"].ToString());
                        }
                        res.Fullname = reader["Fullname"].ToString();
                    }
                    reader.Close();
                    if (!(res.Id > 0))
                    {
                        res = null;
                    }
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

        [HttpPost]
        private string EncryptString(string text, string keyString)
        {
            var key = Encoding.UTF8.GetBytes(keyString);

            using (var aesAlg = Aes.Create())
            {
                using (var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV))
                {
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(text);
                        }

                        var iv = aesAlg.IV;

                        var decryptedContent = msEncrypt.ToArray();

                        var result = new byte[iv.Length + decryptedContent.Length];

                        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                        Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

                        return Convert.ToBase64String(result);
                    }
                }
            }
        }

        private string DecryptString(string cipherText, string keyString)
        {
            var fullCipher = Convert.FromBase64String(cipherText);

            var iv = new byte[16];
            var cipher = new byte[16];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, iv.Length);
            var key = Encoding.UTF8.GetBytes(keyString);

            using (var aesAlg = Aes.Create())
            {
                using (var decryptor = aesAlg.CreateDecryptor(key, iv))
                {
                    string result;
                    using (var msDecrypt = new MemoryStream(cipher))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                result = srDecrypt.ReadToEnd();
                            }
                        }
                    }

                    return result;
                }
            }
        }

        private object? CreateUser(User u)
        {
            try
            {
                WebContext db = new WebContext();
                var usr = db.Users.Where(x => x.Username.Equals(u.Username)).FirstOrDefault();
                if (usr != null)
                {
                    // ton tai user
                    return new
                    {
                        success = false,
                        message = "Da ton tai username !!!",
                    };
                }
                else
                {
                    var hashPass = EncryptString(u.Password, _key);
                    u.Password = hashPass;
                    db.Users.Update(u);
                    db.SaveChanges();
                    return new
                    {
                        success = true,
                        message = "Tao thanh cong!!!",
                        data = u
                    };
                }
            }
            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    message = ex.Message,
                };
            }
        }
    }

    public class LoginData
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class Users
    {
        public int? Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public DateTime? LastLogin { get; set; }
        public string Email { get; set; }
    }
}
