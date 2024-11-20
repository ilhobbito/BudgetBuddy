using System.ComponentModel.DataAnnotations;

namespace BudgetBuddy.Models;

public class Category
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Kategorinamn är obligatoriskt.")]
    public string Name { get; set; }
    
    public decimal BudgetLimit { get; set; }
}