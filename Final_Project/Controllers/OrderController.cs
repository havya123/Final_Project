using Microsoft.AspNetCore.Mvc;

namespace Sign_in.Controllers
{
    public class OrderController : Controller
    {
        
        SqlConnection con = new SqlConnection("Server = .; Database = Web; User id = ltapp; password = 123;TrustServerCertificate=True");
        SqlCommand com = new SqlCommand();

        public IActionResult Index()
        {

            return View();
        }

    }
}