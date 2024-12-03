namespace DataAccessLayer.ViewModels;

public class PropertyVM
{
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public decimal Price { get; set; }

        public string Image { get; set; } = null!;
        //public IFormFile ImageFile { get; set; }

        public int Size { get; set; }

        public int Baths { get; set; }

        public int Rooms { get; set; }
        public string Address { get; set; } = null!;
        public string Choice { get; set; }
        public string type { get; set; }
        public string owner { get; set; }


    }

