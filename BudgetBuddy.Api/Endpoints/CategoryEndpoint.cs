using BudgetBuddy.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1;

public static class CategoryEndpoint
{
    public static void AddCategoryEndpoints(this IEndpointRouteBuilder app)
    {
        const string baseUrl = "/api/Categories";
        
        app.MapGet(baseUrl, async (BudgetBuddyContext context) =>
        {
            return await context.Categories.ToListAsync(); 
        }).WithName("GetCategories").WithOpenApi();

        app.MapPost(baseUrl, async (Category category, BudgetBuddyContext context) =>
        {
            context.Categories.Add(category);
            await context.SaveChangesAsync();
            return category;
        }).WithName("PostCategories").WithOpenApi();

        app.MapDelete($"{baseUrl}" + "/{id}", async (int id, BudgetBuddyContext context) =>
        {
            var category = await context.Categories.FindAsync(id);
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
        }).WithName("DeleteCategories").WithOpenApi();

        app.MapPut(baseUrl, async (Category category, BudgetBuddyContext context) =>
        {
            context.Update(category);
            await context.SaveChangesAsync();
        }).WithName("EditCategories").WithOpenApi();

    }
}