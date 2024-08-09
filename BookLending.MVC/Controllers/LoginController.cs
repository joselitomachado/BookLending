using BookLending.MVC.DTOs;
using BookLending.MVC.Services.LoginService;
using BookLending.MVC.Services.SessionService;
using Microsoft.AspNetCore.Mvc;

namespace BookLending.MVC.Controllers;

public class LoginController : Controller
{
    private readonly ILoginInterface _loginInterface;
    private readonly ISessionInterface _sessionInterface;

    public LoginController(ILoginInterface loginInterface, ISessionInterface sessionInterface)
    {
        _loginInterface = loginInterface;
        _sessionInterface = sessionInterface;
    }

    public IActionResult Login()
    {
        var user = _sessionInterface.GetSession();

        if (user != null)
        {
            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    public IActionResult Logout()
    {
        _sessionInterface.RemoveSession();
        return RedirectToAction("Login");
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

    [HttpPost]
    public async Task<IActionResult> Login(UserLoginDto userLoginDto)
    {
        if (ModelState.IsValid)
        {
            var user = await _loginInterface.Login(userLoginDto);

            if (user.Status)
            {
                TempData["SuccessMessage"] = user.Message;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["ErrorMessage"] = user.Message;
                return View(userLoginDto);
            }
        }
        else
        {
            return View(userLoginDto);
        }
    }
}
