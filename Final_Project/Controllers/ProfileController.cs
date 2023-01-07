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
        private object? getProfile(string email)
        {
            try
            { 
                if (email != null)
                {
                    var db = new WebContext();
                    //var ls = db.Profiles.Where(x => x.Email == email).ToList();
                    return new
                    {
                        Data = "",
                    };
                }
                return null;    
                
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
