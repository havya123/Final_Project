using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace Sign_in.Controllers
{
    public class OrderController : Controller
    {
        private string _key;

        public IActionResult Index()
        {

            return View();
        }

    }
}