using Microsoft.AspNetCore.Mvc;
using Shop.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;


namespace Shop.Controllers;

public class TreatsController : Controller
{
  private readonly ShopContext _db;

  public TreatsController(ShopContext db)
  {
    _db = db;
  }

  public ActionResult Index()
  {
    return View(_db.Treats.ToList());
  }

  [Authorize]
  public ActionResult Create()
  {
    return View();
  }

  [Authorize]
  [HttpPost]
  public ActionResult Create(Treat treat)
  {
    if (!ModelState.IsValid)
    {
      return View(treat);
    }

    _db.Treats.Add(treat);
    _db.SaveChanges();

    return RedirectToAction("Index");
  }

  [Authorize]
  public ActionResult Details(int id)
  {
    Treat thisTreat = _db.Treats
        .Include(t => t.FlavorTreats)
        .ThenInclude(ft => ft.Flavor)
        .FirstOrDefault(Treat => Treat.TreatId == id);

    return View(thisTreat);
  }

  [Authorize]
  public ActionResult AddFlavor(int id)
  {
    Treat thisTreat = _db.Treats.FirstOrDefault(t => t.TreatId == id);
    ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Type");

    return View(thisTreat);
  }

  [Authorize]
  [HttpPost]
  public ActionResult AddFlavor(Treat treat, int flavorId)
  {
#nullable enable
    FlavorTreat? flavorTreat = _db.FlavorTreats.FirstOrDefault(ft => (ft.FlavorId == flavorId && ft.TreatId == treat.TreatId));
#nullable disable

    if (flavorTreat == null && flavorId != 0)
    {
      _db.FlavorTreats.Add(new FlavorTreat() { FlavorId = flavorId, TreatId = treat.TreatId });
      _db.SaveChanges();
    }

    return RedirectToAction("Details", new { id = treat.TreatId });
  }

  [Authorize]
  public ActionResult Edit(int id)
  {
    Treat thisTreat = _db.Treats.FirstOrDefault(Treats => Treats.TreatId == id);

    return View(thisTreat);
  }

  [Authorize]
  [HttpPost]
  public ActionResult Edit(Treat treat)
  {
    if (!ModelState.IsValid)
    {
      return View(treat);
    }

    _db.Treats.Update(treat);
    _db.SaveChanges();

    return RedirectToAction("Index");
  }

  [Authorize]
  public ActionResult Delete(int id)
  {
    Treat thisTreat = _db.Treats.FirstOrDefault(Treats => Treats.TreatId == id);

    return View(thisTreat);
  }

  [Authorize]
  [HttpPost, ActionName("Delete")]
  public ActionResult DeleteConfirmed(int id)
  {
    Treat thisTreat = _db.Treats.FirstOrDefault(Treats => Treats.TreatId == id);

    _db.Treats.Remove(thisTreat);
    _db.SaveChanges();

    return RedirectToAction("Index");
  }

  [Authorize]
  [HttpPost]
  public ActionResult DeleteJoin(int ftId)
  {
    FlavorTreat flavorTreat = _db.FlavorTreats.FirstOrDefault(ft => ft.FlavorTreatId == ftId);

    _db.FlavorTreats.Remove(flavorTreat);
    _db.SaveChanges();

    return RedirectToAction("Index");
  }
}