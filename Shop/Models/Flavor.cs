using System.Collections.Generic;

namespace Shop.Models;

public class Flavor
{
  public int FlavorId { get; set; }
  public string Type { get; set; }
  public List<FlavorTreat> FlavorTreats { get; set; }
}