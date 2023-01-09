using Final_Project.Models;
using Final_Project.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Final_Project.Controllers
{
   
        public class HomeController : Controller
        {
            Product ProductDb = new Product();
            private readonly ILogger<HomeController> _logger;

            public HomeController(ILogger<HomeController> logger)
            {
                _logger = logger;
            }

            public IActionResult Index()
            {
                ProductModel productmodel = new ProductModel();
                ViewBag.products = productmodel.findAll();

                if (string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
                {
                    return Redirect("Login");
                }
                else
                {
                    var model = new
                    {
                        username = HttpContext.Session.GetString("Username"),
                        fullname = HttpContext.Session.GetString("Fullname"),
                    };
                    return View(model);
                }
            }

            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            public IActionResult Error()
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }




        }
    }