using BudgetBuddy.Lib.DAL;
using BudgetBuddy.Models;

namespace BudgetBuddy.Services;

public class BudgetService
{
    private readonly CategoriesManager _categoriesManager;
    private readonly BudgetItemManager _budgetItemManager;
    
    public BudgetService(CategoriesManager categoriesManager, BudgetItemManager budgetItemManager)
    {
        _categoriesManager = categoriesManager;
        _budgetItemManager = budgetItemManager;
    }

    public static List<BudgetItem> BudgetItems { get; set; }
    public async Task SeedCategoriesAndBudgetItemsAsync(List<Category> categories, List<BudgetItem> budgetItems)
    {
        if (budgetItems.Count() > 0)
        {
            await _budgetItemManager.CreateBudgetItemAsync(new BudgetItem() { Name = "Lön", Amount = 2000, IsIncome = true, CategoryId = 1 });
            await _budgetItemManager.CreateBudgetItemAsync(new BudgetItem() { Name = "OF", Amount = 1337, IsIncome = true, CategoryId = 1});
            await _budgetItemManager.CreateBudgetItemAsync(new BudgetItem() { Name = "Twitch", Amount = 99, IsIncome = true, CategoryId = 1});
            await  _budgetItemManager.CreateBudgetItemAsync(new BudgetItem() { Name = "Barnbidrag", Amount = 2400, IsIncome = true, CategoryId = 1});
            await  _budgetItemManager.CreateBudgetItemAsync(new BudgetItem() { Name = "Netflix", Amount = 149, IsIncome = false, CategoryId = 3 });
            await _budgetItemManager.CreateBudgetItemAsync(new BudgetItem() { Name = "Pornhub subscription", Amount = 99, IsIncome = false, CategoryId = 3 });
            await  _budgetItemManager.CreateBudgetItemAsync(new BudgetItem() { Name = "Donken", Amount = 99, IsIncome = false, CategoryId = 2 });
            await  _budgetItemManager.CreateBudgetItemAsync(new BudgetItem() { Name = "H&M", Amount = 499, IsIncome = false, CategoryId = 4 });
        }
        if (categories.Count > 0)
        {
            await  _categoriesManager.CreateCategoryAsync(new Category() { Name = "Inkomst",  Id = 1 });
            await  _categoriesManager.CreateCategoryAsync(new Category() { Name = "Mat", BudgetLimit = 5000, Id = 2 });
            await  _categoriesManager.CreateCategoryAsync(new Category() { Name = "Nöjen", BudgetLimit = 2500, Id = 3 });
            await  _categoriesManager.CreateCategoryAsync(new Category() { Name = "Kläder", BudgetLimit = 1500, Id = 4 });
            await  _categoriesManager.CreateCategoryAsync(new Category() { Name = "Övrigt", BudgetLimit = 1000, Id = 5 });
        }
    }

    public async Task AddBudgetItem(BudgetItem budgetItem)
    {
        await _budgetItemManager.CreateBudgetItemAsync(budgetItem);
    }

    public async Task <decimal> GetTotalAmount(bool isIncome)
    {
        BudgetItems = await _budgetItemManager.GetBudgetItemsAsync();
        return BudgetItems
            .Where(x => x.IsIncome == isIncome)
            .Sum(x => x.Amount);
    }
    
    public async Task<decimal> GetNetResult()
    {
        var totalIncome = await GetTotalAmount(true);
        var totalExpenses = await GetTotalAmount(false);
        return totalIncome - totalExpenses;
    }
    

    public async Task<string> AddCategory(Category category)
    {
        await _categoriesManager.CreateCategoryAsync(category);
        // Check where the messages pops out and see if it's necessary.
        return category.Name != "" ? category.Name : "Okänd kategori";
    }
    
    public async Task<string> GetCategoryName(int categoryId)
    {
        var category = await _categoriesManager.GetCategoryByIdAsync(categoryId);
        return category != null ? category.Name : "Okänd kategori";
    }
    
}