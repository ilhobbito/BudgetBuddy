using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BudgetBuddy.Lib.DAL;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BudgetBuddy.Services;
using Budgetbuddy.tests.Interfaces;

namespace BudgetBuddy;

public static class ConfigureService
{
    public static void AddServices(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddScoped<BudgetService>();
        builder.Services.AddScoped<ICategoriesManager, CategoriesManager>();
        builder.Services.AddScoped<IBudgetItemManager, BudgetItemManager>();
    
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
        builder.Services.AddScoped<BudgetItemManager>();
        builder.Services.AddScoped<CategoriesManager>();
    }
}