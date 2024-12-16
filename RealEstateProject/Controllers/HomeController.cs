using BusinessLogicLayer.Services;
using DataAccessLayer.Implementations;
using DataAccessLayer.Interfaces;
using DataAccessLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using RealEstateProject.Models;
using Stripe.Checkout;
using Stripe;
using System.Diagnostics;
using Microsoft.Extensions.Options;
using DataAccessLayer.DomainModels;

namespace RealEstateProject.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHomeRepository _homeRepository;
    private readonly ITypeRepository typeRepository;
    private readonly IChoiceRepository choiceRepository;
    private readonly StripeSettings _stripeSettings;


    public HomeController(ILogger<HomeController> logger, IHomeRepository homeRepository, ITypeRepository typeRepository, IChoiceRepository ChoiceRepository, IOptions<StripeSettings> stripeSettings)
    {
        _logger = logger;
        _homeRepository = homeRepository;
        this.typeRepository = typeRepository;
        this.choiceRepository = ChoiceRepository;
        _stripeSettings = stripeSettings.Value;
    }


    public async Task<IActionResult> Index()
    {
        var model = new PropertyListPlusTypeVM();
        model.types = await typeRepository.TypeGetAll();
        model.choices = await choiceRepository.ChoiceGetAll();
        model.propertyList = await _homeRepository.PropertyList();
        model.propertyTypesCountVM = await _homeRepository.PropertyTypeCount();
        return View(model);
    }

    public IActionResult CreateCheckoutSession(string amount)
    {
        if (string.IsNullOrEmpty(amount) || !decimal.TryParse(amount, out decimal parsedAmount) || parsedAmount <= 0)
        {
            return BadRequest("Invalid amount specified.");
        }

        var currency = "EGP";
        var successUrl = Url.Action("Success", "Home", null, Request.Scheme);
        var cancelUrl = Url.Action("Cancel", "Home", null, Request.Scheme);
        StripeConfiguration.ApiKey = _stripeSettings.SecretKey;

        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = new List<string> { "card" },
            LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = currency,
                            UnitAmount = (long?)parsedAmount,
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Bills",
                            }
                        },
                        Quantity = 1
                    }
                },
            Mode = "payment",
            SuccessUrl = successUrl,
            CancelUrl = cancelUrl
        };

        var service = new Stripe.Checkout.SessionService();
        var session = service.Create(options);

        return Redirect(session.Url);
    }

    public IActionResult Success()
    {
        return View("Index");
    }

    public IActionResult Cancel()
    {
        return View("Index");
    }

    public async Task<IActionResult> IndexForOwner()
    {
        var model = new PropertyListPlusTypeVM();
        model.types = await typeRepository.TypeGetAll();
        model.choices = await choiceRepository.ChoiceGetAll();
        model.propertyList = await _homeRepository.PropertyList();
        model.propertyTypesCountVM = await _homeRepository.PropertyTypeCount();
        return View(model);
    }

    public async Task<IActionResult> ContactUs()
    {
        return View();
    }

    //[HttpPost]
    //public async Task<IActionResult> ContactUs(ContactUsVM model)
    //{
    //    if (ModelState.IsValid)
    //    {

    //        try
    //        {
    //            //var msg = new ContactUs()
    //            //{
    //            //    msg.Name = model.Name,
    //            //    msg.Subject = model.Subject,
    //            //    msg.Message = model.Message,
    //            //    msg.Email = model.Email
    //            //};
                
    //            var successMessage = "Thank you for your message! We will get back to you shortly.";

    //            TempData["SuccessMessage"] = successMessage;
    //            return RedirectToAction("ContactUs");
    //        }
    //        catch (Exception ex)
    //        {
    //            TempData["ErrorMessage"] = "An error occurred while sending your message. Please try again later.";
    //            return RedirectToAction("ContactUs");
    //        }
    //    }
    //    return View(model);
    //}


    [HttpPost]
    public async Task<IActionResult> SearchBox(HomeSearchVM model)
    {
        var results = await _homeRepository.PropertyListOfSearchBar(model);
        return View(results);
    }

    [HttpPost]
    public async Task<IActionResult> SearchBoxForOwner(HomeSearchVM model)
    {
        var results = await _homeRepository.PropertyListOfSearchBar(model);
        return View(results);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
