using Microsoft.Data.SqlClient;
using System.Data;

namespace Final_Project.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string prtName { get; set; }

        public double prtPrice { get; set; }   

        public string prtImage { get; set; }

    }
}
