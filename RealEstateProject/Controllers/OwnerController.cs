using DataAccessLayer.Interfaces;
using DataAccessLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace RealEstateProject.Controllers;

public class OwnerController : Controller
{
    private readonly IOwnerRepository _ownerRepository;
    public OwnerController(IOwnerRepository ownerRepository)
    {
        _ownerRepository = ownerRepository;
    }


    public async Task<IActionResult> Dashboard()
    {
        if (HttpContext.Session.GetString("FirstName") == null)
        {
            return RedirectToAction("LogIn", "Login");
        }
        else
        {
            return View();
        }
    }

    public async Task<IActionResult> Profile()
    {
        var model = HttpContext.Session.GetObject<OwnerVM>("LogInModel");
        if (model != null)
        {
            var result = await _ownerRepository.OwnerVMGetById(model.Id);

            if (result != null)
            {
                return View(result);
            }
            return View(model);
        }
        else
        {
            return View();
        }
    }


    public async Task<IActionResult> Update(int id)
    {
        return View(await _ownerRepository.OwnerGetById(id));
    }


    [HttpPost]
    public async Task<IActionResult> Update(OwnerUpdateVM model)
    {
        await _ownerRepository.OwnerUpdate(model);
        return RedirectToAction("Profile", "Owner");
    }
}
