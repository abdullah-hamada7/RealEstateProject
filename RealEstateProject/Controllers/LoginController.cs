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
            // stores model for storing in db after varification
            HttpContext.Session.SetObject("SignUpModel", model);

            // Generate a random 4-digit OTP
            string otp = _loginRepository.GenerateOTP();

            //stores otp for sending to otp varification action
            TempData["otp"] = otp;


            // Send OTP to the provided email address
            bool isOtpSent = _loginRepository.SendOTP(model.Email, otp);

            if (isOtpSent)
            {
                // Redirect to OTP validation action with email and OTP
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

    //Action for otp vaification of 4 digits
    public async Task<IActionResult> Otp()
    {
        return View();
    }

    //Action for otp vaification of 4 digits after click resent otp
    public async Task<IActionResult> ResentOtp()
    {

        //retrive signupModel from signUp action
        var model = HttpContext.Session.GetObject<OwnerCreateVM>("SignUpModel");

        // Generate a random 4-digit OTP
        string otp2 = _loginRepository.GenerateOTP();

        //stores otp for sending to otp varification action
        TempData["otp"] = otp2;


        // Send OTP to the provided email address
        bool isOtpSent = _loginRepository.SendOTP(model.Email, otp2);

        if (isOtpSent)
        {
            // Redirect to OTP validation action with email and OTP
            return RedirectToAction("Otp");
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to send OTP. Please try again.";
            return View(TempData["ErrorMessage"]);
        }

    }

    // POST: /Login/VerifyOtp
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> VerifyOtp(string enteredOtp)
    {
        string Otp = (string)TempData["otp"];

        if (Otp == enteredOtp)
        {
            //first stores signup data in db
            var ownerModel = HttpContext.Session.GetObject<OwnerCreateVM>("SignUpModel");

            if (ownerModel != null)
            {
                await _ownerRepository.AddOwner(ownerModel);
                // OTP is valid, proceed with account Createion or further actions
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



    //Actions for LogIn process
    public async Task<IActionResult> LogIn()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> LogIn(LoginVM model)
    {
        if (ModelState.IsValid)
        {
            //Retrive owner model from db throug login model and store in session
            var result = _loginRepository.Login(model);
            if (result != null)
            {
                //first name store in session for dashboard
                HttpContext.Session.SetString("FirstName", result.FirstName);

                //model store in session for profile
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

    //Actions for LogOut process
    public async Task<IActionResult> LogOut()
    {
        HttpContext.Session.Remove("FirstName");
        return RedirectToAction("LogIn", "Login");
    }

}
