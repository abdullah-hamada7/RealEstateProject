using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.ViewModels;

public class PropertyDetailsVM
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public decimal Price { get; set; }

        public string Image { get; set; } = null!;

        public int Size { get; set; }

        public int Baths { get; set; }

        public int Rooms { get; set; }
        public string Address { get; set; } = null!;
        public string Choice { get; set; }
        public string Type { get; set; }

        [Display(Name = "Owner Name")]
        public string ownerName { get; set; }

        [Display(Name = "Owner Contact")]
        public string ownerContact { get; set; }

        [Display(Name = "Owner Email")]
        public string ownerEmail { get; set; }
    }

