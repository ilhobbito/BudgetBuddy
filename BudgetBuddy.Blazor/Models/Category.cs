using System.ComponentModel.DataAnnotations;

namespace BudgetBuddy.Models;

public class Category
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Kategorinamn är obligatoriskt.")]
    public string Name { get; set; }
    
    [Range(1, double.MaxValue, ErrorMessage = "Gränsen måste vara större än 0.")]
    public decimal BudgetLimit { get; set; }
}