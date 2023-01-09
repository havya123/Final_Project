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
                    prtPrice=string.Format("{0:n0}",15190000),
                    prtImage="ps5-standard-ps-plus-700x700.jpg"
                },

                new Product()
                {
                    Id= 2,
                    prtName="PlayStation 4 Slim 500GB FW 9.00 USED",
                    prtPrice=string.Format("{0:n0}",6272000),
                    prtImage="ps4-slim-cu-unlock-fw-00-700x700.jpg"
                },

                new Product()
                {
                    Id= 3,
                    prtName="New Nintendo Switch with Gray Joy Con",
                    prtPrice=string.Format("{0:n0}",7428000),
                    prtImage="nintendo-switch-2019-gray-00-700x700.jpg"
                },

                new Product()
                {
                    Id= 4,
                    prtName="Nintendo Switch Lite - Turquoise",
                    prtPrice=string.Format("{0:n0}",4508000),
                    prtImage="nintendo-switch-lite-turquoise-00-700x700.jpg"
                },


                new Product()
                {
                    Id= 5,
                    prtName="DualSense Starlight Blue - PS5 Wireless Game Controller",
                    prtPrice=string.Format("{0:n0}",2180000),
                    prtImage="dualsense-ps5-starlight-blue-00-700x700.jpg"
                },


                new Product()
                {
                    Id= 6,
                    prtName="Logitech G29 Driving Force",
                    prtPrice=string.Format("{0:n0}",8880000),
                    prtImage="logitech-g29-driving-force-00-700x700.jpg"
                },


                new Product()
                {
                    Id= 7,
                    prtName="Nintendo Wii White USED",
                    prtPrice=string.Format("{0:n0}",3400000),
                    prtImage="nintendo-switch-oled-white-mod-chip-700x700.jpg"
                },

                new Product()
                {
                    Id= 8,
                    prtName="DOBE Charging Dock For PS5/Xbox Series/Nintendo Switch Controller",
                    prtPrice=string.Format("{0:n0}",480000),
                    prtImage="xbox-series-x-xbox-game-pass-ultimate-700x700.jpg"
                },

                 new Product()
                {
                    Id= 9,
                    prtName="Xbox Series S 512GB",
                    prtPrice=string.Format("{0:n0}",8624000),
                    prtImage="xbox-series-s-xbox-game-pass-ultimate-700x700.jpg"
                },

                new Product()
                {
                    Id= 10,
                    prtName="Máy chơi game Pandora's Box 2 Joysticks 4780 Games",
                    prtPrice=string.Format("{0:n0}",3200000),
                    prtImage="pandora-2-joysticks-4780-in-1-00-700x700.jpg"
                },

                new Product()
                {
                    Id= 11,
                    prtName="Xbox Series X 1TB",
                    prtPrice=string.Format("{0:n0}",15190000),
                    prtImage="xbox-series-x-xbox-game-pass-ultimate-700x700.jpg"
                },

                new Product()
                {
                    Id= 12,
                    prtName="PS4 Lỗi Không Nhận Đĩa",
                    prtPrice=string.Format("{0:n0}",900000),
                    prtImage="PS4 ko nhận đĩa-700x700.jpg"
                },


                new Product()
                {
                    Id= 13,
                    prtName="PS4 Lỗi Không Lên Nguồn Điện, Có Mùi Khét",
                    prtPrice=string.Format("{0:n0}",2200000),
                    prtImage="PS4 lỗi nguồn, khét-700x700.jpg"
                },


                new Product()
                {
                    Id= 14,
                    prtName="New Nintendo Switch with Neon Red Blue Joy Con - BH 3 tháng",
                    prtPrice=string.Format("{0:n0}",7862000),
                    prtImage="nintendo-switch-2019-neon-00-700x700.jpg"
                },


                new Product()
                {
                    Id= 15,
                    prtName="New Nintendo Switch - Animal Crossing New Horizons Special Edition",
                    prtPrice=string.Format("{0:n0}",8728000),
                    prtImage="nintendo-switch-animal-crossing-horizon-00-700x700.jpg"
                },

                new Product()
                {
                    Id= 16,
                    prtName="Nintendo Switch OLED model with White Joy Con Mod Chip + MicroSD 128GB",
                    prtPrice=string.Format("{0:n0}",100000),
                    prtImage="nintendo-switch-oled-white-mod-chip-700x700.jpg"
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
