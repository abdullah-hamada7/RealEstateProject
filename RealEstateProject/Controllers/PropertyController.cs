using BusinessLogicLayer.Services;
using DataAccessLayer.Interfaces;
using DataAccessLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.BillingPortal;
using Stripe.Checkout;
using SessionCreateOptions = Stripe.Checkout.SessionCreateOptions;

namespace RealEstateProject.Controllers
{
    public class PropertyController : Controller
    {
        private readonly ITypeRepository typeRepository;
        private readonly IChoiceRepository choiceRepository;
        private readonly IPropertyRepository propertyRepository;
        private readonly IHomeRepository homeRepository;
        private readonly IWebHostEnvironment _hostEnvironment;

        public PropertyController(ITypeRepository typeRepository, IChoiceRepository choiceRepository, IPropertyRepository propertyRepository, IWebHostEnvironment webHostEnvironment, IHomeRepository homeRepository)
        {
            this.typeRepository = typeRepository;
            this.choiceRepository = choiceRepository;
            this.propertyRepository = propertyRepository;
            _hostEnvironment = webHostEnvironment;
            this.homeRepository = homeRepository;
        }
        public async Task<IActionResult> AddProperty()
        {
            var viewModel = new PropertyCreateVM();
            viewModel.types = await typeRepository.TypeGetAll();
            viewModel.choices = await choiceRepository.ChoiceGetAll();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddProperty(PropertyCreateVM model)
        {
            var result = HttpContext.Session.GetObject<OwnerVM>("LogInModel");

            if (result != null)
            {
                model.OwnerId = result.Id;
            }

            if (!await propertyRepository.IsImageFile(model.ImageFile))
            {
                ViewBag.ErrorMessage = "Only image files are allowed.";
                return RedirectToAction("AddProperty", "Property");
            }

            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
            string extension = Path.GetExtension(model.ImageFile.FileName);
            model.Image = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwRootPath + "/images/", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await model.ImageFile.CopyToAsync(fileStream);
            }

            if (await propertyRepository.AddProperty(model))
            {
                ViewBag.ErrorMessage = "Property added successfully.";
                TempData["SuccessMessage"] = "Property added successfully!";
            }
            else
            {
                ViewBag.ErrorMessage = "something went wrong";
            }

            return View();

        }

        public async Task<IActionResult> PropertyGetAll()
        {
            var properties = await homeRepository.PropertyList();
            return View(properties);
        }

        public async Task<IActionResult> PropertyGetAllForOwner()
        {
            var properties = await homeRepository.PropertyList();
            return View(properties);
        }

        public async Task<IActionResult> PropertyTypeGetAll()
        {
            var types = await homeRepository.PropertyTypeCount();
            return View(types);
        }



        public async Task<IActionResult> ShowOwnerPropertyList()
        {
            var result = HttpContext.Session.GetObject<OwnerVM>("LogInModel");

            var proprtyList = await propertyRepository.PropertyListForOwner(result.Id);
            return View(proprtyList);
        }

        public async Task<IActionResult> ApartmentList()
        {
            int id = 1; //id for apartment
            var proprtyList = await propertyRepository.PropertyListForType(id);
            if (proprtyList == null)
            {
                ViewBag.ErrorMessage = "No listing yet";
                return View(proprtyList);
            }
            else
            {
                return View(proprtyList);
            }

        }
        public async Task<IActionResult> ApartmentListForOwner()
        {
            int id = 1;
            var proprtyList = await propertyRepository.PropertyListForType(id);
            if (proprtyList == null)
            {
                ViewBag.ErrorMessage = "No listing yet";
                return View(proprtyList);
            }
            else
            {
                return View(proprtyList);
            }

        }

        public async Task<IActionResult> villaList()
        {
            int id = 2;
            var proprtyList = await propertyRepository.PropertyListForType(id);
            return View(proprtyList);
        }

        public async Task<IActionResult> villaListForOwner()
        {
            int id = 2;
            var proprtyList = await propertyRepository.PropertyListForType(id);
            return View(proprtyList);
        }

        public async Task<IActionResult> HomeList()
        {
            int id = 3;
            var proprtyList = await propertyRepository.PropertyListForType(id);
            return View(proprtyList);
        }

        public async Task<IActionResult> HomeListForOwner()
        {
            int id = 3;
            var proprtyList = await propertyRepository.PropertyListForType(id);
            return View(proprtyList);
        }

        public async Task<IActionResult> TownHouseList()
        {
            int id = 4;
            var proprtyList = await propertyRepository.PropertyListForType(id);
            return View(proprtyList);
        }

        public async Task<IActionResult> TownHouseListForOwner()
        {
            int id = 4;
            var proprtyList = await propertyRepository.PropertyListForType(id);
            return View(proprtyList);
        }

        public async Task<IActionResult> BuildingList()
        {
            int id = 5;
            var proprtyList = await propertyRepository.PropertyListForType(id);
            return View(proprtyList);
        }

        public async Task<IActionResult> BuildingListForOwner()
        {
            int id = 5;
            var proprtyList = await propertyRepository.PropertyListForType(id);
            return View(proprtyList);
        }

        public async Task<IActionResult> OfficeList()
        {
            int id = 6;
            var proprtyList = await propertyRepository.PropertyListForType(id);
            return View(proprtyList);
        }

        public async Task<IActionResult> OfficeListForOwner()
        {
            int id = 6; //id for office
            var proprtyList = await propertyRepository.PropertyListForType(id);
            return View(proprtyList);
        }

        public async Task<IActionResult> SaleList()
        {
            int id = 1; //id for sale
            var proprtyList = await propertyRepository.PropertyListForChoice(id);
            return View(proprtyList);
        }

        public async Task<IActionResult> SaleListForOwner()
        {
            int id = 1; //id for sale
            var proprtyList = await propertyRepository.PropertyListForChoice(id);
            return View(proprtyList);
        }

        public async Task<IActionResult> RentList()
        {
            int id = 2; //id for rent   
            var proprtyList = await propertyRepository.PropertyListForChoice(id);
            return View(proprtyList);
        }

        public async Task<IActionResult> RentListForOwner()
        {
            int id = 2; //id for rent
            var proprtyList = await propertyRepository.PropertyListForChoice(id);
            return View(proprtyList);
        }


        public async Task<IActionResult> PropertyDetails(int id)
        {
            var result = await propertyRepository.PropertyDetails(id);
            return View(result);
        }

        public async Task<IActionResult> PropertyDetailsForOwner(int id)
        {
            var result = await propertyRepository.PropertyDetails(id);
            return View(result);
        }

        public async Task<IActionResult> PropertyDelete(int id)
        {
            if (await propertyRepository.PropertyDelete(id))
            {
                return RedirectToAction("ShowOwnerPropertyList");
            }
            else
            {
                return RedirectToAction("ShowOwnerPropertyList");
            }
        }

    }
}
