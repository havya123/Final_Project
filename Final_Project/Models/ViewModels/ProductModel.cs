 using System.Diagnostics;

namespace Final_Project.Models.ViewModels
{
    public class ProductModel
    {
        public List<Product> _products { get; set;}

        public List<Product> findAll()
        {
            _products = new List<Product>
            {
                new Product()
                {
                    Id= 1,
                    prtName="PlayStation 5 / PS5 Standard Edition - KOREA [ CFI-1118 ]",
                    prtPrice=15190000,
                    prtImage="ps5-standard-ps-plus-700x700.jpg"
                },

                new Product()
                {
                    Id= 2,
                    prtName="PlayStation 4 Slim 500GB FW 9.00 USED",
                    prtPrice=6272000,
                    prtImage="ps4-slim-cu-unlock-fw-00-700x700.jpg"
                },

                new Product()
                {
                    Id= 3,
                    prtName="New Nintendo Switch with Gray Joy Con",
                    prtPrice=7428000,
                    prtImage="nintendo-switch-2019-gray-00-700x700.jpg"
                },

                new Product()
                {
                    Id= 1,
                    prtName="PlayStation 5 / PS5 Standard Edition - KOREA [ CFI-1118 ]",
                    prtPrice=15190000,
                    prtImage="ps5-standard-ps-plus-700x700.jpg"
                },


                new Product()
                {
                    Id= 4,
                    prtName="Nintendo Switch Lite - Turquoise",
                    prtPrice=4508000,
                    prtImage="nintendo-switch-lite-turquoise-00-700x700.jpg"
                },


                new Product()
                {
                    Id= 5,
                    prtName="DualSense Starlight Blue - PS5 Wireless Game Controller",
                    prtPrice=2180000,
                    prtImage="dualsense-ps5-starlight-blue-00-700x700.jpg"
                },


                new Product()
                {
                    Id= 6,
                    prtName="Logitech G29 Driving Force",
                    prtPrice=8880000,
                    prtImage="logitech-g29-driving-force-00-700x700.jpg"
                },


                new Product()
                {
                    Id= 1,
                    prtName="Nintendo Wii White USED",
                    prtPrice=3400000,
                    prtImage="nintendo-switch-oled-white-mod-chip-700x700.jpg"
                },

                new Product()
                {
                    Id= 8,
                    prtName="DOBE Charging Dock For PS5/Xbox Series/Nintendo Switch Controller",
                    prtPrice=480000,
                    prtImage="xbox-series-x-xbox-game-pass-ultimate-700x700.jpg"
                },

            };
            return _products;

        }

        public Product find(int id)
        {

            List<Product> products= findAll();
            var prod = products.Where(x => x.Id == id).FirstOrDefault();
            return prod;

        }
    }
}
