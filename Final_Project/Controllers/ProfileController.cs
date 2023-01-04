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

namespace Final_Project.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
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
         
        private object? updateProfile(Profile c)
        {
            try
            {
                if (c == null)
                    return null; 
                string uniqueFileName = uploadedFile(c);
                var db = new WebContext();
                var c1 = db.Profiles.Where(x => x.Id == c.Id).FirstOrDefault();
                
                if (c1.Phone != c.Phone)
                    c1.Phone = c.Phone;
                if (c1.Name != c.Name)
                    c1.Name = c.Name;
                if (c1.Gender != c.Gender)
                    c1.Gender = c.Gender; 
                if (c1.ImageURL != uniqueFileName)
                    c1.ImageURL = uniqueFileName; 
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
    }
}
