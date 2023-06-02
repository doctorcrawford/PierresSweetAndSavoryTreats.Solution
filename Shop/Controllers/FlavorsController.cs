using Microsoft.AspNetCore.Mvc;
using Shop.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Shop.Controllers;

public class FlavorsController : Controller
{
  private readonly ShopContext _db;

  public FlavorsController(ShopContext db)
  {
    _db = db;
  }

  public ActionResult Index()
  {
    return View(_db.Flavors.ToList());
  }

  public ActionResult Create()
  {
    return View();
  }

  [Authorize]
  [HttpPost]
  public ActionResult Create(Flavor flavor)
  {
    if (!ModelState.IsValid)
    {
      return View(flavor);
    }

    _db.Flavors.Add(flavor);
    _db.SaveChanges();

    return RedirectToAction("Index");
  }

  public ActionResult Details(int id)
  {
    Flavor thisFlavor = _db.Flavors
        .Include(f => f.FlavorTreats)
        .ThenInclude(ft => ft.Treat)
        .FirstOrDefault(Flavor => Flavor.FlavorId == id);

    return View(thisFlavor);
  }

  [Authorize]
  public ActionResult AddTreat(int id)
  {
    Flavor thisFlavor = _db.Flavors.FirstOrDefault(f => f.FlavorId == id);
    ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "Name");

    return View(thisFlavor);
  }

  [Authorize]
  [HttpPost]
  public ActionResult AddTreat(Flavor flavor, int treatId)
  {
#nullable enable
    FlavorTreat? flavorTreat = _db.FlavorTreats.FirstOrDefault(ft => (ft.TreatId == treatId && ft.FlavorId == flavor.FlavorId));
#nullable disable

    if (flavorTreat == null && treatId != 0)
    {
      _db.FlavorTreats.Add(new FlavorTreat() { TreatId = treatId, FlavorId = flavor.FlavorId });
      _db.SaveChanges();
    }

    return RedirectToAction("Details", new { id = flavor.FlavorId });
  }

  [Authorize]
  public ActionResult Edit(int id)
  {
    Flavor thisFlavor = _db.Flavors.FirstOrDefault(Flavors => Flavors.FlavorId == id);

    return View(thisFlavor);
  }

  [Authorize]
  [HttpPost]
  public ActionResult Edit(Flavor flavor)
  {
    if (!ModelState.IsValid)
    {
      return View(flavor);
    }

    _db.Flavors.Update(flavor);
    _db.SaveChanges();

    return RedirectToAction("Index");
  }

  [Authorize]
  public ActionResult Delete(int id)
  {
    Flavor thisFlavor = _db.Flavors.FirstOrDefault(Flavors => Flavors.FlavorId == id);

    return View(thisFlavor);
  }

  [Authorize]
  [HttpPost, ActionName("Delete")]
  public ActionResult DeleteConfirmed(int id)
  {
    Flavor thisFlavor = _db.Flavors.FirstOrDefault(Flavors => Flavors.FlavorId == id);
    _db.Flavors.Remove(thisFlavor);
    _db.SaveChanges();

    return RedirectToAction("Index");
  }

  [Authorize]
  [HttpPost]
  public ActionResult DeleteJoin(int ftId)
  {
    FlavorTreat flavorTreat = _db.FlavorTreats.FirstOrDefault(e => e.FlavorTreatId == ftId);
    _db.FlavorTreats.Remove(flavorTreat);
    _db.SaveChanges();

    return RedirectToAction("Index");
  }
}