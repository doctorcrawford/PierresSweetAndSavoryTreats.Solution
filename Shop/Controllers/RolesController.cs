using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Shop.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Shop.Controllers;

public class RolesController : Controller
{
  private RoleManager<IdentityRole> _roleManager;
  private UserManager<ApplicationUser> _userManager;

  public RolesController(RoleManager<IdentityRole> roleMgr, UserManager<ApplicationUser> userMgr)
  {
    _roleManager = roleMgr;
    _userManager = userMgr;
  }

  public ViewResult Index() => View(_roleManager.Roles);

  private void Errors(IdentityResult result)
  {
    foreach (var error in result.Errors)
      ModelState.AddModelError("", error.Description);
  }

  public IActionResult Create() => View();

  [HttpPost]
  public async Task<IActionResult> Create([Required] string name)
  {
    if (ModelState.IsValid)
    {
      IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
      if (result.Succeeded)
        return RedirectToAction("Index");
      else
        Errors(result);
    }
    return View(name);
  }

  public async Task<IActionResult> Update(string id)
  {
    var role = await _roleManager.FindByIdAsync(id);

    var members = new List<ApplicationUser>();
    var nonMembers = new List<ApplicationUser>();
    var allUsers = await _userManager.Users.ToListAsync();

    foreach (var user in allUsers)
    {
      if (await _userManager.IsInRoleAsync(user, role.Name))
      {
        members.Add(user);
        break;
      }

      nonMembers.Add(user);
    }

    return View(new RoleEdit
    {
      Role = role,
      Members = members,
      NonMembers = nonMembers
    });
  }

  // public async Task<IActionResult> Update(string id)
  // {
  //   IdentityRole role = await _roleManager.FindByIdAsync(id);
  //       List<ApplicationUser> allUsers = await _userManager.Users.ToListAsync();

  //   List<ApplicationUser> members = allUsers
  //       .Where(u => _userManager.IsInRoleAsync(u, role.Name).Result)
  //       .ToList();

  //   List<ApplicationUser> nonMembers = allUsers.Except(members).ToList();
  //   return View(new RoleEdit
  //   {
  //     Role = role,
  //     Members = members,
  //     NonMembers = nonMembers
  //   });
  // }

  [HttpPost]
  public async Task<IActionResult> Update(RoleModification model)
  {
    IdentityResult result;
    if (ModelState.IsValid)
    {
      foreach (string userId in model.AddIds ?? new string[] { })
      {
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
          result = await _userManager.AddToRoleAsync(user, model.RoleName);
          if (!result.Succeeded)
            Errors(result);
        }
      }
      foreach (string userId in model.DeleteIds ?? new string[] { })
      {
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
          result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
          if (!result.Succeeded)
            Errors(result);
        }
      }
    }

    if (ModelState.IsValid)
      return RedirectToAction(nameof(Index));
    else
      return await Update(model.RoleId);
  }

  [HttpPost]
  public async Task<IActionResult> Delete(string id)
  {
    var role = await _roleManager.FindByIdAsync(id);
    if (role != null)
    {
      var result = await _roleManager.DeleteAsync(role);
      if (result.Succeeded)
        return RedirectToAction("Index");
      else
        Errors(result);
    }
    else
      ModelState.AddModelError("", "No role found");
    return View("Index", _roleManager.Roles);
  }
}