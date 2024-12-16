using DataAccessLayer.Interfaces;
using DataAccessLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace RealEstateProject.Controllers;

public class LoginController : Controller
{
    private readonly ILoginRepository _loginRepository;
    private readonly IOwnerRepository _ownerRepository;


    public LoginController(ILoginRepository loginRepository, IOwnerRepository ownerRepository)
    {
        _loginRepository = loginRepository;
        _ownerRepository = ownerRepository;
    }
    public IActionResult SignUp()
    {
        return View();
    }
    [HttpPost]

    public async Task<IActionResult> SignUp(OwnerCreateVM model)
    {

        if (ModelState.IsValid)
        {
            HttpContext.Session.SetObject("SignUpModel", model);

            string otp = _loginRepository.GenerateOTP();

            TempData["otp"] = otp;


            bool isOtpSent = _loginRepository.SendOTP(model.Email, otp);

            if (isOtpSent)
            {
                return RedirectToAction("Otp");
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to send OTP. Please try again.";
                return View(TempData["ErrorMessage"]);

            }
        }

        return View(model);
    }

    public async Task<IActionResult> Otp()
    {
        return View();
    }

    public async Task<IActionResult> ResentOtp()
    {

        var model = HttpContext.Session.GetObject<OwnerCreateVM>("SignUpModel");

        string otp2 = _loginRepository.GenerateOTP();

        TempData["otp"] = otp2;


        bool isOtpSent = _loginRepository.SendOTP(model.Email, otp2);

        if (isOtpSent)
        {
            return RedirectToAction("Otp");
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to send OTP. Please try again.";
            return View(TempData["ErrorMessage"]);
        }

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> VerifyOtp(string enteredOtp)
    {
        string Otp = (string)TempData["otp"];

        if (Otp == enteredOtp)
        {
            var ownerModel = HttpContext.Session.GetObject<OwnerCreateVM>("SignUpModel");

            if (ownerModel != null)
            {
                await _ownerRepository.AddOwner(ownerModel);
                return RedirectToAction("SignUpCompleted");
            }
            else
            {
                TempData["ErrorMessage"] = "SignUp model not found in session";
                return View(TempData["ErrorMessage"]);
            }


        }
        else
        {
            TempData["ErrorMessage"] = "Incorrect OTP. Please try again.";
            return RedirectToAction("Otp");

        }
    }
    public async Task<IActionResult> SignUpCompleted()
    {
        return View();
    }



    public async Task<IActionResult> LogIn()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> LogIn(LoginVM model)
    {
        if (ModelState.IsValid)
        {
            var result = _loginRepository.Login(model);
            if (result != null)
            {
                HttpContext.Session.SetString("FirstName", result.FirstName);

                HttpContext.Session.SetObject("LogInModel", result);

                return RedirectToAction("Dashboard", "Owner", new { area = "" });
            }
            else
            {
                ViewBag.errorMessage = "Invalid email or password";
                return View(model);
            }

        }
        return View();
    }

    public async Task<IActionResult> LogOut()
    {
        HttpContext.Session.Remove("FirstName");
        return RedirectToAction("LogIn", "Login");
    }

}
