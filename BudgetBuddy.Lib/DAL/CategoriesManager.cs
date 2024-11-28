using System.Text;
using System.Text.Json;
using BudgetBuddy.Models;

namespace BudgetBuddy.Lib.DAL;

public class CategoriesManager
{
    public static readonly Uri BaseAddress = new Uri("https://localhost:7107/");

    public async Task<List<Category>> GetCategoriesAsync()
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = BaseAddress;
            HttpResponseMessage response = await client.GetAsync("api/Categories");

            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                List<Category> categories = JsonSerializer.Deserialize<List<Category>>(responseString);
                return categories;
            }

            return new List<Category>();
        }
    }

    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = BaseAddress;
            HttpResponseMessage response = await client.GetAsync("api/Categories");

            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                Category category = JsonSerializer.Deserialize<Category>(responseString);
                return category;
            }

            return null;
        }
    }

    public async Task<HttpResponseMessage> CreateCategoryAsync(Category category)
    {
        HttpResponseMessage response = null;
        if (category != null)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                var json = JsonSerializer.Serialize(category);
                StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                response = await client.PostAsync("api/Categories", httpContent);
            }
        }
        else
        {
            Console.WriteLine("Category is null");
        }
        return response;
    }

    public async Task<HttpResponseMessage> UpdateCategoryAsync(Category category)
    {
        HttpResponseMessage response = null;
        if (category != null)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                var json = JsonSerializer.Serialize(category);
                StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                response = await client.PutAsync("api/Categories", httpContent);
            }
        }
        else
        {
            Console.WriteLine("BudgetItem is null");
        }

        return response;
    }

    public async Task<HttpResponseMessage> DeleteCategoryAsync(Category category)
    {
        HttpResponseMessage response = null;
        if (category != null)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                var json = JsonSerializer.Serialize(category);
                StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                response = await client.DeleteAsync("api/Category/" + category.Id);
            }
        }
        else
        {
            Console.WriteLine("Category is null");
        }
        return response;
    }
}