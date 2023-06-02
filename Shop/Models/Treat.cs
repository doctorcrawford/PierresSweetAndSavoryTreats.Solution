using System.Collections.Generic;

namespace Shop.Models;

public class Treat
{
  public int TreatId { get; set; }
  public string Name { get; set; }
  public List<FlavorTreat> FlavorTreats { get; set; }
}