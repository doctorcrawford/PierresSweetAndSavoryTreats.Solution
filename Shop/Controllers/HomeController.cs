using Microsoft.AspNetCore.Mvc;
using Shop.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Shop.Controllers;

public class HomeController : Controller
{
  private readonly ShopContext _db;
  private readonly UserManager<ApplicationUser> _userManager;

  public HomeController(UserManager<ApplicationUser> userManager, ShopContext db)
  {
    _userManager = userManager;
    _db = db;
  }

  [HttpGet("/")]
  public ActionResult Index()
  {
    var databaseInfo = new DatabaseInfo {
      Flavors = _db.Flavors,
      Treats = _db.Treats
    };
    return View(databaseInfo);
  }

}

public class DatabaseInfo
{
  public IEnumerable<Flavor> Flavors { get; set; }
  public IEnumerable<Treat> Treats { get; set; }
  public IEnumerable<FlavorTreat> FlavorTreats { get; set; }
}