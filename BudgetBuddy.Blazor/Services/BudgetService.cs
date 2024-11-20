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
    
    public List<Category> GetCategories() => _categories;

    public void AddCategory(Category category)
    {
        Console.WriteLine($"Adding Category: Name={category.Name}, Limit={category.BudgetLimit}");
        _categories.Add(category);
    }
}