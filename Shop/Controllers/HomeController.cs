using Microsoft.AspNetCore.Mvc;
using Shop.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Shop.Controllers
{
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
        return View();
      }
    }
}