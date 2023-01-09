using Microsoft.AspNetCore.Mvc;
using Final_Project.Helpers;
using Final_Project.Models;
using Final_Project.Models.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.CodeAnalysis;
using Final_Project.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Text.RegularExpressions;

namespace Sign_in.Controllers
{
    public class OrderController : Controller
    {
        SqlConnection con = new SqlConnection("Server = .; Database = Web; User id = ltapp; password = 123;TrustServerCertificate=True");
        SqlCommand com = new SqlCommand();
        


        public List<Item> cart
        {
            get
            {

                var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                if (cart == default(List<Item>))
                {
                    cart = new List<Item>();
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                }
                return cart;
            }
        }


        public IActionResult Index()
        {
            
            ViewBag.Cart = cart;
            if (cart == null)
            {
                ViewBag.total = 0;
            }
            else
            {
                ViewBag.total = cart.Sum(item => Decimal.Parse(item.Product.prtPrice) * item.Quantity);
                ViewBag.total= string.Format("{0:n0}",ViewBag.total);
            }

            return View();
        }

        private int isExist(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.Id.Equals(id))
                {
                    return i;
                }

            }
            return -1;
        }

        public IActionResult Buy(int id)
        {

            ProductModel productModel = new ProductModel();

            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item { Product = productModel.find(id), Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

            }
            else
            {
                List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Item { Product = productModel.find(id), Quantity = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            List<Item> cart = new List<Item>();
            cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");

        }

        public IActionResult Increase(int id)
        {
            List<Item> cart = new List<Item>();
            cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(id);
            if (index != -1)
            {
                cart[index].Quantity++;
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");

        }

        public IActionResult Decrease(int id)
        {
            List<Item> cart = new List<Item>();
            cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(id);
            if (index != -1)
            {
                cart[index].Quantity--;
                if (cart[index].Quantity == 0)
                {
                    cart.RemoveAt(index);
                }
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");

        }


        public IActionResult AddNewRecord()
        {
            List<Item> carts = cart;

            foreach (var i in carts)
            {
                string fullname = Request.Form["fullname"];
                int phoneNumber = Int32.Parse(Request.Form["phoneNumber"]);
                string street = Request.Form["street"];
                string districts = Request.Form["districts"];
                string city = Request.Form["City"];
                string province = Request.Form["Province"];
                string email = Request.Form["Email"];
                string payment = Request.Form["payment"];
                string product = i.Product.prtName;
                int quantity = i.Quantity;
                decimal total = Decimal.Parse(i.Product.prtPrice) * i.Quantity;
                try
                {
                    con.Open();
                    com.Connection = con;
                    com.CommandText = "insert into OrderDetail(fullname,phoneNumber,street,districts,city,province,email,payment,product,quantity,total) values('" + fullname + "','" + phoneNumber + "','" + street + "','" + districts + "','" + city + "','" + province + "','" + email + "','" + payment + "','" + product + "','" + quantity + "','"+total+"')";
                    com.ExecuteNonQuery();
                    con.Close();

                }
                catch (Exception ex)
                {

                    if (con.State == System.Data.ConnectionState.Open)
                    {

                        con.Close();
                    }
                    TempData["message"] = "Data Saved Failed";


                }

            }

            return RedirectToAction("Index");
        }

    }

    
}

