using Final_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;

using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Final_Project.Controllers
{
    public class ProfileController : Controller
    {
        private string _key;
        private readonly IWebHostEnvironment webHostEnvironment;
        public IActionResult Index()
        {
            return View();

        }
        public ProfileController()
        {
            _key = "E546C8DF278CD5931069B522E695D4F2";
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

        [HttpPost]
        public IActionResult get_profile(string Email)
        {
            var data = getProfile(Email);
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
        [HttpPost] 
        public IActionResult update_profile(Profile data)
        {
            var result = updateProfile(data);
            if (data != null)
            {
                var res = new
                {
                    Success = true,
                    Message = "",
                    Data = result
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

        [HttpPost]
        public IActionResult change_pass(string username, string oldPass, string newPass)
        {
            var obj = ChangePass(username, oldPass, newPass);
            return Json(obj); 
        }
        [HttpPost]
        public IActionResult change_email(string email, string password, string username)
        {
            var obj = ChangeEmail(email, password, username);
            return Json(obj);
        }

        [HttpPost]
        public IActionResult get_order(string Id)
        {
            var data = getOrder(Id);
            if (data != null)
            {
                var res = new
                {
                    Success = true,
                    Message = "get order successful",
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
        private object? getProfile(string grp) 
        {
            try
            {
                var db = new WebContext();
                var ls = db.Profiles.Where(x => x.Email == grp).ToList(); 
                return new
                {
                    Data = ls, 
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private object? getOrder(string Id)
        {
            try
            {
                var db = new WebContext();
                var ls = db.Orders.Where(x => x.Id == Int64.Parse(Id)).ToList();
                return new
                {
                    Data = ls,
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private object? updateProfile(Profile c)
        {
            try
            {
                if (c == null)
                    return null;  
                var db = new WebContext();
                var c1 = db.Profiles.Where(x => x.Id == c.Id).FirstOrDefault();
                
                if (c1.Phone != c.Phone)
                    c1.Phone = c.Phone;
                if (c1.Name != c.Name)
                    c1.Name = c.Name;
                if (c1.Gender != c.Gender)
                    c1.Gender = c.Gender;  
                db.Profiles.Update(c1);
                db.SaveChanges();
                return c1;
            }
            catch (Exception ex)
            { 
                return null;
            }
        } 
        private string uploadedFile(Profile profile)
        {
            string uniqueFileName = null;
            if(profile.Avatar != null)
            {
                string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "img");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + profile.Avatar.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    profile.Avatar.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        private object? ChangePass(string uid, string oPass, string nPass)
        {
            try
            {
                var db = new WebContext(); 
                var usr = db.Users.Where(x => x.Username == uid).FirstOrDefault();  
                if (usr != null)
                { 
                    var curPass = DecryptString(usr.Password, _key);
                    if (curPass != oPass)
                    {
                        return new
                        {
                            success = false,
                            message = "Current password inconrect !!!",
                        };
                    }
                    else
                    {
                        var hashPass = EncryptString(nPass, _key);
                        usr.Password = hashPass;
                        db.Users.Update(usr); 
                        db.SaveChanges(); 
                        return new
                        {
                            success = true,
                            message = "Update Password Successful!!!",
                        };

                    }
                }
                else
                {
                    return new
                    {
                        success = false,
                        message = "Didn't exist this user!!!",
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
        private object? ChangeEmail(string email, string password,string username)
            {
            try
            {
                var db = new WebContext(); 
                var usr = db.Users.Where(x => x.Username == username).FirstOrDefault();  
                if (usr != null)
                { 
                    var curPass = DecryptString(usr.Password, _key);
                    if (curPass != password)
                    { 
                        return new
                        {
                            success = false,
                            message = "Current password inconrect !!!",
                        };
                    }
                    else
                    {  
                        if (usr.Email != email)
                            usr.Email = email; 
                        db.Users.Update(usr); 
                        db.SaveChanges(); 
                        return new
                        {
                            success = true,
                            message = "Update Email Successful!!!",
                        }; 
                    }
                }
                else
                {
                    return new
                    {
                        success = false,
                        message = "Didn't exist this user!!!",
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
    }
}
