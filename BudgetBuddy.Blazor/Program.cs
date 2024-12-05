using BudgetBuddy;
using BudgetBuddy.Lib.DAL;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BudgetBuddy.Services;
using Budgetbuddy.tests.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<BudgetService>();
builder.Services.AddScoped<ICategoriesManager, CategoriesManager>();
builder.Services.AddScoped<IBudgetItemManager, BudgetItemManager>();



builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<BudgetItemManager>();
builder.Services.AddScoped<CategoriesManager>();


await builder.Build().RunAsync();


