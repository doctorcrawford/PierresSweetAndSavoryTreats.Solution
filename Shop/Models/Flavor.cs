using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shop.Models;

public class Flavor
{
  public int FlavorId { get; set; }
  [Required(ErrorMessage = "You must give the flavor a type!")]
  public string Type { get; set; }
  public List<FlavorTreat> FlavorTreats { get; set; }
}