using System.Text;
using System.Text.Json;
using BudgetBuddy.Models;

namespace BudgetBuddy.Lib.DAL;


//TODO: Build methods for USERS in this application
public class UserManager
{
    public static readonly Uri BaseAddress = new Uri("https://localhost:5231/");
    private readonly HttpClient _client;
    
    public UserManager(HttpClient httpClient)
    {
        _client = httpClient;
        _client.BaseAddress = BaseAddress;
    }
    
    public async Task<List<User>> GetUsersAsync()
    {
        HttpResponseMessage response = await _client.GetAsync("api/Users");

        if (response.IsSuccessStatusCode)
        {
            string responseString = await response.Content.ReadAsStringAsync();
            List<User> users = JsonSerializer.Deserialize<List<User>>(responseString);
            return users;
        }
        return new List<User>();
    }
    public async Task<User> GetUserByIdAsync(int id)
    {
        HttpResponseMessage response = await _client.GetAsync("api/Users");

        if (response.IsSuccessStatusCode)
        {
            string responseString = await response.Content.ReadAsStringAsync();
            User user = JsonSerializer.Deserialize<User>(responseString);
            return user;
        }
        return null;
    }

    public async Task<HttpResponseMessage> CreateUserAsync(User user)
    {
        HttpResponseMessage response;
        if (true)
        {
            var json = JsonSerializer.Serialize(user);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            response = await _client.PostAsync("api/Users", httpContent);
        }
        return response;
    }

    public async Task<HttpResponseMessage> UpdateUserAsync(User user)
    {
        HttpResponseMessage response;
        if (true)
        {
            var json = JsonSerializer.Serialize(user);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            response = await _client.PutAsync("api/Users", httpContent);
        }
        return response;
    }

    public async Task<HttpResponseMessage> DeleteUserAsync(User user)
    {
        HttpResponseMessage response;
        if (true)
        {
            var json = JsonSerializer.Serialize(user);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            response = await _client.DeleteAsync("api/Users/" + user.Id);
        }
        return response;
    }
}