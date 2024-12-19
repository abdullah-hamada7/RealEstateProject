using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.ViewModels;

public class PropertyCreateVM
    {
        [Display(Name = "Title")]
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; } = null!;


        [Display(Name = "Price")]
        [Required(ErrorMessage = "Price is required.")]
        [Range(1, 10000, ErrorMessage = "Price must be between 1 EGP and 10000 lacs.")]
        public decimal Price { get; set; }


        public string Image { get; set; } = null!;


        [Display(Name = "Upload Image")]
        [Required(ErrorMessage = "Image is required.")]

        public IFormFile ImageFile { get; set; }


        [Display(Name = "Size")]
        [Required(ErrorMessage = "Size is required.")]
        [Range(1, 1000, ErrorMessage = "Size must be between 1 and 1000 metre")]
        public int Size { get; set; }


        [Display(Name = "Baths")]
        [Required(ErrorMessage = "Baths is required.")]
        [Range(1, 1000, ErrorMessage = "No. of baths must be between 0 and 1000")]
        public int Baths { get; set; }


        [Display(Name = "Rooms")]
        [Required(ErrorMessage = "Rooms is required.")]
        [Range(1, 1000, ErrorMessage = "No. of rooms must be between 0 and 1000")]
        public int Rooms { get; set; }


        [Display(Name = "Choice")]
        [Required(ErrorMessage = "choice is required.")]
        public int? ChoiceId { get; set; }


        [Display(Name = "Type")]
        [Required(ErrorMessage = "Type is required.")]
        public int? TypeId { get; set; }


        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address is required.")]

        public string Address { get; set; } = null!;


        [Display(Name = "Owner")]
        [Required(ErrorMessage = "Owner is required.")]
        public int OwnerId { get; set; }

        public List<TypeVM> types { get; set; }
        public List<ChoiceVM> choices { get; set; }

}

