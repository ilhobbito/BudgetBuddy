using System.ComponentModel.DataAnnotations;

namespace BudgetBuddy.Models;

public class BudgetItem
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Inkomstkälla är obligatoriskt.")]
    public string Name { get; set; }
    
    [Range(1, double.MaxValue, ErrorMessage = "Beloppet måste vara större än 0.")]
    public decimal Amount { get; set; }
    
    public int CategoryId { get; set; } 
    public DateTime Date { get; set; } = DateTime.Now;
    public bool IsIncome { get; set; }
}