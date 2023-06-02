using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shop.Models;

public class Treat
{
  public int TreatId { get; set; }
  [Required(ErrorMessage = "You must give a name for the treat!")]
  public string Name { get; set; }
  public List<FlavorTreat> FlavorTreats { get; set; }
}