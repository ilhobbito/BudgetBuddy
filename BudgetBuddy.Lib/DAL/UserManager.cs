using System.Text;
using System.Text.Json;
using BudgetBuddy.Models;

namespace BudgetBuddy.Lib.DAL;

public class UserManager
{
    public static readonly Uri BaseAddress = new Uri("https://localhost:7107/");

    public async Task<List<User>> GetUsersAsync()
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = BaseAddress;
            HttpResponseMessage response = await client.GetAsync("api/Users");

            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                List<User> users = JsonSerializer.Deserialize<List<User>>(responseString);
                return users;
            }

            return new List<User>();
        }
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = BaseAddress;
            HttpResponseMessage response = await client.GetAsync("api/Users");

            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                User user = JsonSerializer.Deserialize<User>(responseString);
                return user;
            }

            return null;
        }
    }

    public async Task<HttpResponseMessage> CreateUserAsync(User user)
    {
        HttpResponseMessage response = null;
        if (user != null)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                var json = JsonSerializer.Serialize(user);
                StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                response = await client.PostAsync("api/Users", httpContent);
            }
        }
        else
        {
            Console.WriteLine("User is null");
        }
        return response;
    }

    public async Task<HttpResponseMessage> UpdateUserAsync(User user)
    {
        HttpResponseMessage response = null;
        if (user != null)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                var json = JsonSerializer.Serialize(user);
                StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                response = await client.PutAsync("api/Users", httpContent);
            }
        }
        else
        {
            Console.WriteLine("User is null");
        }

        return response;
    }

    public async Task<HttpResponseMessage> DeleteUserAsync(User user)
    {
        HttpResponseMessage response = null;
        if (user != null)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                var json = JsonSerializer.Serialize(user);
                StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                response = await client.DeleteAsync("api/Users/" + user.Id);
            }
        }
        else
        {
            Console.WriteLine("Users is null");
        }
        return response;
    }
}