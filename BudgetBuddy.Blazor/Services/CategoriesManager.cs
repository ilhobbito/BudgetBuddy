using System.Text;
using System.Text.Json;
using BudgetBuddy.Models;
using Budgetbuddy.tests.Interfaces;
using System.Net.Http;
using System.Net;

namespace BudgetBuddy.Lib.DAL;

public class CategoriesManager : ICategoriesManager
{
    private readonly HttpClient _client;
    public static readonly Uri BaseAddress = new Uri("https://localhost:5231/");

    public CategoriesManager(HttpClient httpClient)
    {
        _client = httpClient;
        _client.BaseAddress = BaseAddress;
    }
    //TODO: DO tests for these
    //Right?
    public async Task<List<Category>> GetCategoriesAsync()
    {
        try
        {
            HttpResponseMessage response = await _client.GetAsync("api/Categories");

            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Fetched JSON: {responseString}");

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                List<Category> categories = JsonSerializer.Deserialize<List<Category>>(responseString, options);
                return categories ?? new List<Category>();
            }
            else
            {
                Console.WriteLine($"API request failed with status: {response.StatusCode}");
                return new List<Category>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetCategoriesAsync: {ex.Message}");
            return new List<Category>();
        }
    }

    public async Task<Category> GetCategoryByIdAsync(int id)
    {
            HttpResponseMessage response = await _client.GetAsync($"api/Categories/{id}");

            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                Category category = JsonSerializer.Deserialize<Category>(responseString);
                return category;
            }

            return null;
        
    }

    //public async Task<HttpResponseMessage> CreateCategoryAsync(Category category)
    //{
    //    HttpResponseMessage response = null;
    //    if (category != null)
    //    {
    //        using (var client = new HttpClient())
    //        {
    //            client.BaseAddress = BaseAddress;
    //            var json = JsonSerializer.Serialize(category);
    //            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
    //            response = await client.PostAsync("api/Categories", httpContent);
    //        }
    //    }
    //    else
    //    {
    //        Console.WriteLine("Category is null");
    //    }
    //    return response;
    //}

    public async Task<HttpResponseMessage> CreateCategoryAsync(Category category)
    {

        if (category == null)
        {
            Console.WriteLine("Category is null");
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        var json = JsonSerializer.Serialize(category);
        var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("api/Categories", httpContent);
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