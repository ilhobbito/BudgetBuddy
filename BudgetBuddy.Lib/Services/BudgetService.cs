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
        _budgetItems.Add(new BudgetItem() { Name = "Lön", Amount = 2000, IsIncome = true, CategoryId = 1 });
        _budgetItems.Add(new BudgetItem() { Name = "OF", Amount = 1337, IsIncome = true, CategoryId = 1});
        _budgetItems.Add(new BudgetItem() { Name = "Twitch", Amount = 99, IsIncome = true, CategoryId = 1});
        _budgetItems.Add(new BudgetItem() { Name = "Barnbidrag", Amount = 2400, IsIncome = true, CategoryId = 1});
        _budgetItems.Add(new BudgetItem() { Name = "Netflix", Amount = 149, IsIncome = false, CategoryId = 3 });
        _budgetItems.Add(new BudgetItem() { Name = "Pornhub subscription", Amount = 99, IsIncome = false, CategoryId = 3 });
        _budgetItems.Add(new BudgetItem() { Name = "Donken", Amount = 99, IsIncome = false, CategoryId = 2 });
        _budgetItems.Add(new BudgetItem() { Name = "H&M", Amount = 499, IsIncome = false, CategoryId = 4 });

        _categories.Add(new Category() { Name = "Inkomst",  Id = 1 });
        _categories.Add(new Category() { Name = "Mat", BudgetLimit = 5000, Id = 2 });
        _categories.Add(new Category() { Name = "Nöjen", BudgetLimit = 2500, Id = 3 });
        _categories.Add(new Category() { Name = "Kläder", BudgetLimit = 1500, Id = 4 });
        _categories.Add(new Category() { Name = "Övrigt", BudgetLimit = 1000, Id = 5 });
        
    }

    public decimal GetNetResult()
    {
        var totalIncome = GetTotalAmount(true);
        var totalExpenses = GetTotalAmount(false);
        return totalIncome - totalExpenses;
    }

    public List<Category> GetCategories() => _categories;

    public void AddCategory(Category category)
    {
        Console.WriteLine($"Adding Category: Name={category.Name}, Limit={category.BudgetLimit}");
        _categories.Add(category);
    }
    public string GetCategoryName(int categoryId)
    {
        var category = GetCategories().FirstOrDefault(c => c.Id == categoryId);
        return category != null ? category.Name : "Okänd kategori";
    }
}