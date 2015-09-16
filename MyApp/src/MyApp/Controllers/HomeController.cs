using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using MyApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Authorization;
using System.Security.Claims;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApp.Controllers {
  public class HomeController : Controller {

    // GET: /<controller>/
    [Authorize]
    public IActionResult Index() {
      ViewBag.CanAdd = User.FindFirst(match => match.Type == ClaimTypes.Role && match.Value == "CanAdd") != null ? "true" : "false";
      ViewBag.CanRemove = User.FindFirst(match => match.Type == ClaimTypes.Role && match.Value == "CanRemove") != null ? "true" : "false";
      return View();
    }

  }
}
