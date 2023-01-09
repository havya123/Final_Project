namespace Final_Project.Models
{
    public class Order
    { 
        public int Id { get; set; } 
        public string? NameProduct { get; set; }
        public string ?Description { get; set; }  

        public int? Number { get; set; }
        public string? ImageProduct { get; set; }
        public int? id_user { get; set; }   
    } 
}
