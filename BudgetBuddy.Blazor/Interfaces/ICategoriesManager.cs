using BudgetBuddy.Models;

namespace Budgetbuddy.tests.Interfaces;

public interface ICategoriesManager
{
    Task<List<Category>> GetCategoriesAsync();
    Task<HttpResponseMessage> CreateCategoryAsync(Category category);
}