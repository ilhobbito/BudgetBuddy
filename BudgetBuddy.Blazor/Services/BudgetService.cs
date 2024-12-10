using BudgetBuddy.Lib.DAL;
using BudgetBuddy.Models;
using Budgetbuddy.tests.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace BudgetBuddy.Services;

public class BudgetService
{
    private readonly ICategoriesManager _categoriesManager;
    private readonly IBudgetItemManager _budgetItemManager;
    
    public BudgetService(ICategoriesManager categoriesManager, IBudgetItemManager budgetItemManager)
    {
        _categoriesManager = categoriesManager;
        _budgetItemManager = budgetItemManager;
    }

    public static List<BudgetItem> BudgetItems { get; set; }
    public async Task SeedCategoriesAndBudgetItemsAsync(List<Category> categories, List<BudgetItem> budgetItems)
    {
        
        if (categories.Count() <= 0)
        {
            await  _categoriesManager.CreateCategoryAsync(new Category() { Name = "Mat", BudgetLimit = 5000});
            await  _categoriesManager.CreateCategoryAsync(new Category() { Name = "Nöjen", BudgetLimit = 2500});
            await  _categoriesManager.CreateCategoryAsync(new Category() { Name = "Kläder", BudgetLimit = 1500});
            await  _categoriesManager.CreateCategoryAsync(new Category() { Name = "Övrigt", BudgetLimit = 1000});
        }
        if (budgetItems.Count() <= 0)
        {
            await _budgetItemManager.CreateBudgetItemAsync(new BudgetItem() { Name = "Lön", Amount = 2000, IsIncome = true});
            await _budgetItemManager.CreateBudgetItemAsync(new BudgetItem() { Name = "Studiebidrag", Amount = 1337, IsIncome = true});
            await _budgetItemManager.CreateBudgetItemAsync(new BudgetItem() { Name = "Twitch streaming", Amount = 99, IsIncome = true});
            await _budgetItemManager.CreateBudgetItemAsync(new BudgetItem() { Name = "Barnbidrag", Amount = 2400, IsIncome = true });
            await _budgetItemManager.CreateBudgetItemAsync(new BudgetItem() { Name = "Netflix", Amount = 149, IsIncome = false, CategoryId = 3 });
            await _budgetItemManager.CreateBudgetItemAsync(new BudgetItem() { Name = "Disney plus", Amount = 99, IsIncome = false, CategoryId = 3 });
            await _budgetItemManager.CreateBudgetItemAsync(new BudgetItem() { Name = "Donken", Amount = 99, IsIncome = false, CategoryId = 2 });
            await _budgetItemManager.CreateBudgetItemAsync(new BudgetItem() { Name = "H&M", Amount = 499, IsIncome = false, CategoryId = 4 });
        }
      
    }

    public decimal GetTotalAmount(List<BudgetItem> budgetItems, bool income)
    {
        if (budgetItems.Select(x => x.Name).ToList().Contains("") ||  budgetItems.Select(x => x.Name).ToList().Contains(null))
        {
            throw new ArgumentException("Items must have a name");
        }
        if (budgetItems.Any(b => b.Amount < 0))
        {
            foreach (var negativeItem in budgetItems.Where(b => b.Amount < 0))
            {
                Console.WriteLine($"{negativeItem.Name}: {negativeItem.Amount}");
            }
            throw new ArgumentOutOfRangeException(nameof(budgetItems), "Budget items cannot be negative");
        }
        return budgetItems
            .Where(x => x.IsIncome == income).Sum(x => x.Amount);
    }

    public decimal GetNetResult(List<BudgetItem> budgetItems)
    {
        
        var income =  GetTotalAmount(budgetItems, true);
        var expense =  GetTotalAmount(budgetItems, false);
        return income - expense;
    }
}