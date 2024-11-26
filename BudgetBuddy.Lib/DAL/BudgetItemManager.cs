using System.Text.Json;
using BudgetBuddy.Models;

namespace BudgetBuddy.Lib.DAL;

public class BudgetItemManager
{
    private static readonly Uri BaseAddress = new Uri("https://localhost:44346/");
    
    public async Task<List<BudgetItem>> GetBudgetItemsAsync()
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = BaseAddress;
            HttpResponseMessage response = await client.GetAsync("api/BudgetItems");

            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                List<BudgetItem> budgetItems = JsonSerializer.Deserialize<List<BudgetItem>>(responseString);
                return budgetItems;
            }

            return new List<BudgetItem>();
        }
    }

    public async Task<BudgetItem> GetBudgetItemByIdAsync(int id)
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = BaseAddress;
            HttpResponseMessage response = await client.GetAsync("api/BudgetItems");

            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                BudgetItem budgetItem = JsonSerializer.Deserialize<BudgetItem>(responseString);
                return budgetItem;
            }

            return null;
    }
}