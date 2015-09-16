using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using MyApp.Models;
using Microsoft.AspNet.Identity;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApp.Controllers {
  public class AccountController : Controller {


    private readonly MyAppDbContext _dbContext;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController(MyAppDbContext dbContext, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) {
      _dbContext = dbContext;
      _userManager = userManager;
      _signInManager = signInManager;
    }

    // GET: /Account/Login
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login(string returnUrl = null) {
      ViewData["ReturnUrl"] = returnUrl;
      return View();
    }

    //
    // POST: /Account/Login
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginModel model, string returnUrl = null) {
      ViewData["ReturnUrl"] = returnUrl;
      if (ModelState.IsValid) {
        var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, lockoutOnFailure: false);
        if (result.Succeeded) {
          return RedirectToLocal(returnUrl);
        }
      }
      ModelState.AddModelError(string.Empty, "Invalid login attempt.");
      return View(model);
    }

    private IActionResult RedirectToLocal(string returnUrl) {
      if (Url.IsLocalUrl(returnUrl)) {
        return Redirect(returnUrl);
      } else {
        return RedirectToAction(nameof(HomeController.Index), "Home");
      }
    }

    public IActionResult SignOut() {
      _signInManager.SignOutAsync();
      return Redirect("/Account/Login");
    }

    // GET: /<controller>/
    public IActionResult Index() {
      return View();
    }
  }
}
