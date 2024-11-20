namespace BudgetBuddy.Models;

public class BudgetItem
{
    public int Id { get; set; }
    public string Name { get; set; } 
    public decimal Amount { get; set; }
    public int CategoryId { get; set; } 
    public DateTime Date { get; set; }
    public bool IsIncome { get; set; }
}