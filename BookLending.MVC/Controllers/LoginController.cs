using BookLending.MVC.DTOs;
using BookLending.MVC.Services.LoginService;
using Microsoft.AspNetCore.Mvc;

namespace BookLending.MVC.Controllers;

public class LoginController : Controller
{
    private readonly ILoginInterface _loginInterface;

    public LoginController(ILoginInterface loginInterface)
    {
        _loginInterface = loginInterface;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
    {
        if (ModelState.IsValid)
        {
            var user = await _loginInterface.UserRegister(userRegisterDto);

            if (user.Status)
            {
                TempData["SuccessMessage"] = user.Message;
            }
            else
            {
                TempData["ErrorMessage"] = user.Message;
                return View(userRegisterDto);
            }

            return RedirectToAction("Index");
        }
        else
        {
            return View(userRegisterDto);
        }
    }
}
