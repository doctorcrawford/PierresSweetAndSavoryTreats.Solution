using Microsoft.AspNetCore.Mvc;
using Shop.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Shop.Controllers
{
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

    public ActionResult Create()
    {
      return View();
    }

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

    public ActionResult Details(int id)
    {
      Treat thisTreat = _db.Treats
          // .Include(Treat => Treat.JoinEntities)
          // .ThenInclude(join => join.Flavor)
          .FirstOrDefault(Treat => Treat.TreatId == id);
      return View(thisTreat);
    }

    // public ActionResult AddFlavor(int id)
    // {
    //   Treat thisTreat = _db.Treats.FirstOrDefault(Treats => Treats.TreatId == id);
    //   ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Description");
    //   return View(thisTreat);
    // }

    // [HttpPost]
    // public ActionResult AddFlavor(Treat Treat, int FlavorId)
    // {
    //   #nullable enable
    //   FlavorTreat? joinEntity = _db.FlavorTreats.FirstOrDefault(join => (join.FlavorId == FlavorId && join.TreatId == Treat.TreatId));
    //   #nullable disable
    //   if (joinEntity == null && FlavorId != 0)
    //   {
    //     _db.FlavorTreats.Add(new FlavorTreat() { FlavorId = FlavorId, TreatId = Treat.TreatId });
    //     _db.SaveChanges();
    //   }
    //   return RedirectToAction("Details", new { id = Treat.TreatId });
    // }

    public ActionResult Edit(int id)
    {
      Treat thisTreat = _db.Treats.FirstOrDefault(Treats => Treats.TreatId == id);
      return View(thisTreat);
    }

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

    // public ActionResult Delete(int id)
    // {
    //   Treat thisTreat = _db.Treats.FirstOrDefault(Treats => Treats.TreatId == id);
    //   return View(thisTreat);
    // }

    // [HttpPost, ActionName("Delete")]
    // public ActionResult DeleteConfirmed(int id)
    // {
    //   Treat thisTreat = _db.Treats.FirstOrDefault(Treats => Treats.TreatId == id);
    //   _db.Treats.Remove(thisTreat);
    //   _db.SaveChanges();
    //   return RedirectToAction("Index");
    // }

    // [HttpPost]
    // public ActionResult DeleteJoin(int joinId)
    // {
    //   FlavorTreat joinEntry = _db.FlavorTreats.FirstOrDefault(entry => entry.FlavorTreatId == joinId);
    //   _db.FlavorTreats.Remove(joinEntry);
    //   _db.SaveChanges();
    //   return RedirectToAction("Index");
    // }
  }
}