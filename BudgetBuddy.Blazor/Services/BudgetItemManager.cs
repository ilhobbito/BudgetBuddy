using System.Text;
using System.Text.Json;
using BudgetBuddy.Models;

namespace BudgetBuddy.Lib.DAL;

public class BudgetItemManager
{
    private readonly HttpClient _client;
    public static readonly Uri BaseAddress = new Uri("https://localhost:5231/");

    public BudgetItemManager(HttpClient httpClient)
    {
        _client = httpClient;
        _client.BaseAddress = BaseAddress;
    }

    public async Task<List<BudgetItem>> GetBudgetItemsAsync()
    {
        try
        {
            HttpResponseMessage response = await _client.GetAsync("api/BudgetItems");

            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Fetched JSON: {responseString}");

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                List<BudgetItem> budgetItems = JsonSerializer.Deserialize<List<BudgetItem>>(responseString, options);
                return budgetItems ?? new List<BudgetItem>();
            }
            else
            {
                Console.WriteLine($"API request failed with status: {response.StatusCode}");
                return new List<BudgetItem>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetCategoriesAsync: {ex.Message}");
            return new List<BudgetItem>();
        }
    }


    public async Task<BudgetItem> GetBudgetItemByIdAsync(int id)
    {
        HttpResponseMessage response = await _client.GetAsync("api/BudgetItems");

        if (response.IsSuccessStatusCode)
        {
            string responseString = await response.Content.ReadAsStringAsync();
            BudgetItem budgetItem = JsonSerializer.Deserialize<BudgetItem>(responseString);
            return budgetItem;
        }

        return null;
    }

    public async Task<HttpResponseMessage> CreateBudgetItemAsync(BudgetItem budgetItem)
    {
        HttpResponseMessage response = null;
        if (budgetItem == null)
        {
            throw new ArgumentNullException("BudgetItem is null");
        }

        var json = JsonSerializer.Serialize(budgetItem);
        StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
        response = await _client.PostAsync("api/BudgetItems", httpContent);

        return response;
    }

    public async Task<HttpResponseMessage> UpdateBudgetItemAsync(BudgetItem budgetItem)
    {
        HttpResponseMessage response = null;
        if (budgetItem == null)
        {
            Console.WriteLine("BudgetItem is null");
            return null;
        }

        var json = JsonSerializer.Serialize(budgetItem);
        StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
        response = await _client.PutAsync("api/BudgetItems", httpContent);

        return response;
    }

    public async Task<HttpResponseMessage> DeleteBudgetItemAsync(BudgetItem budgetItem)
    {
        HttpResponseMessage response = null;
        if (budgetItem == null)
        {
            Console.WriteLine("BudgetItem is null");
            return null;
        }

        var json = JsonSerializer.Serialize(budgetItem);
        StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
        response = await _client.DeleteAsync("api/BudgetItems/" + budgetItem.Id);


        return response;
    }
}