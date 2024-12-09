using BudgetBuddy.Models;

namespace Budgetbuddy.tests.Interfaces;

public interface IBudgetItemManager
{
    Task<List<BudgetItem>> GetBudgetItemsAsync();
    Task<BudgetItem> GetBudgetItemByIdAsync(int id);
    Task<HttpResponseMessage> CreateBudgetItemAsync(BudgetItem budgetItem);
}