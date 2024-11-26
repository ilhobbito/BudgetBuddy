using System.Text;
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

    public async Task<HttpResponseMessage> CreateBudgetItemAsync(BudgetItem budgetItem)
    {
        HttpResponseMessage response = null;
        if (budgetItem != null)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                var json = JsonSerializer.Serialize(budgetItem);
                StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                response = await client.PostAsync("api/BudgetItems", httpContent);
            }
        }
        else
        {
            Console.WriteLine("BudgetItem is null");
        }
        return response;
    }

    public async Task<HttpResponseMessage> UpdateBudgetItemAsync(BudgetItem budgetItem)
    {
        HttpResponseMessage response = null;
        if (budgetItem != null)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                var json = JsonSerializer.Serialize(budgetItem);
                StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                response = await client.PutAsync("api/BudgetItems", httpContent);
            }
        }
        else
        {
            Console.WriteLine("BudgetItem is null");
        }

        return response;
    }

    public async Task<HttpResponseMessage> DeleteBudgetItemAsync(BudgetItem budgetItem)
    {
        HttpResponseMessage response = null;
        if (budgetItem != null)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                var json = JsonSerializer.Serialize(budgetItem);
                StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                response = await client.DeleteAsync("api/BudgetItems/" + budgetItem.Id);
            }
        }
        else
        {
            Console.WriteLine("BudgetItem is null");
        }
        return response;
    }
}