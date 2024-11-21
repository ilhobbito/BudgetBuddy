using BudgetBuddy.Models;

namespace BudgetBuddy.Services;

public class BudgetService
{
    private readonly List<BudgetItem> _budgetItems = new();
    private readonly List<Category> _categories = new();
    public List<BudgetItem> GetBudgetItems() => _budgetItems;

    public void AddBudgetItem(BudgetItem budgetItem)
    {
        _budgetItems.Add(budgetItem);
    }

    public decimal GetTotalAmount(bool isIncome)
    {
        return _budgetItems
            .Where(x => x.IsIncome == isIncome)
            .Sum(x => x.Amount);
    }
    
    public BudgetService()
    {
        _budgetItems.Add(new BudgetItem() { Name = "Lön", Amount = 2000,IsIncome = true });
        _budgetItems.Add(new BudgetItem() { Name = "OF", Amount = 1337,IsIncome = true });
        _budgetItems.Add(new BudgetItem() { Name = "Twitch", Amount = 99,IsIncome = true });
        _budgetItems.Add(new BudgetItem() { Name = "Barnbidrag", Amount = 2400, IsIncome = true});
        _budgetItems.Add(new BudgetItem() { Name = "Netflix", Amount = 149, IsIncome = false});
        _budgetItems.Add(new BudgetItem() { Name = "Pornhub subscription", Amount = 99, IsIncome = false});
    }
    public decimal GetNetResult()
    {
        var totalIncome = GetTotalAmount(true);  // Assuming this method returns the total income
        var totalExpenses = GetTotalAmount(false);  // Assuming this method returns the total expenses
        return totalIncome - totalExpenses;
    }
    
    public List<Category> GetCategories() => _categories;

    public void AddCategory(Category category)
    {
        Console.WriteLine($"Adding Category: Name={category.Name}, Limit={category.BudgetLimit}");
        _categories.Add(category);
    }
}