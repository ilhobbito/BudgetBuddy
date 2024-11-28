using BudgetBuddy.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1;

public static class UserEndpoint
{
    public static void AddUserEndpoints(this IEndpointRouteBuilder app)
    {
        const string baseUrl = "/api/User";
        
        app.MapGet(baseUrl, async (BudgetBuddyContext context) =>
        {
            return await context.Users.ToListAsync(); 
        }).WithName("GetUsers").WithOpenApi();

        app.MapPost(baseUrl, async (User user, BudgetBuddyContext context) =>
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user;
        }).WithName("PostUsers").WithOpenApi();

        app.MapDelete($"{baseUrl}" + "/{id}", async (int id, BudgetBuddyContext context) =>
        {
            var user = await context.Users.FindAsync(id);
            context.Users.Remove(user);
            await context.SaveChangesAsync();
        }).WithName("DeleteUsers").WithOpenApi();

        app.MapPut(baseUrl, async (User user, BudgetBuddyContext context) =>
        {
            context.Update(user);
            await context.SaveChangesAsync();
        }).WithName("EditUsers").WithOpenApi();
    }
}