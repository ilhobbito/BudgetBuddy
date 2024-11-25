using BudgetBuddy.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1;

public static class ItemsEndpoint
{
    public static void AddItemsEndpoints(this IEndpointRouteBuilder app)
    {
        const string _baseUrl = "/api/BudgetItems";

        app.MapGet(_baseUrl,
                async (BudgetBuddyContext context) => { return await context.BudgetItems.ToListAsync(); })
            .WithName("GetBudgetItems").WithOpenApi();

        app.MapPost(_baseUrl, async (BudgetItem budgetitem, BudgetBuddyContext context) =>
        {
            context.BudgetItems.Add(budgetitem);
            await context.SaveChangesAsync();
            return budgetitem;
        }).WithName("PostBudgetItem").WithOpenApi();

        app.MapDelete($"{_baseUrl}" + "/{id}", async (int id, BudgetBuddyContext context) =>
        {
            var budgetItem = await context.BudgetItems.FindAsync(id);
            context.BudgetItems.Remove(budgetItem);
            await context.SaveChangesAsync();
        }).WithName("DeleteBudgetItem").WithOpenApi();

        app.MapPut(_baseUrl, async (BudgetItem budgetitem, BudgetBuddyContext context) =>
        {
            context.Update(budgetitem);
            await context.SaveChangesAsync();
        }).WithName("EditBudgetItem").WithOpenApi();
    }
}