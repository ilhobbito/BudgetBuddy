using System;
using System.Diagnostics;
using System.Net;
using BudgetBuddy.Lib.DAL;
using Xunit; 

namespace Budgetbuddy.tests;

public class ApplicationPerformanceTests
{
    private readonly HttpClient _client;
    public ApplicationPerformanceTests()
    {
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback =
                (message, cert, chain, sslPolicyErrors) => true // Bypass SSL validation
        };
        _client = new HttpClient(handler);
    }

    [Fact]
    public async Task LoadTimeUnderCertainSeconds()
    {
        
        var sut = new BudgetItemManager(_client);
        var stopwatch = Stopwatch.StartNew();

        var items = await sut.GetBudgetItemsAsync();
        stopwatch.Stop();
        
        Assert.True(stopwatch.Elapsed.TotalSeconds <= 6.0,
            $"Applikationens laddningstid överskred 6 sekunder: {stopwatch.Elapsed.TotalSeconds} sekunder.");
    }

    [Fact]
    public async Task LoadCategoriesUnderCertainSeconds()
    {
        var sutCategories = new CategoriesManager(_client);
        var stopwatchCategories = Stopwatch.StartNew();
        
        var itemsCategories = await sutCategories.GetCategoriesAsync();
        stopwatchCategories.Stop();
        
        Assert.True(stopwatchCategories.Elapsed.TotalSeconds <= 6.0,
            $"Hämtningen tog : {stopwatchCategories.Elapsed.TotalSeconds} sekunder");
    }
    [Fact]
    public async Task BudgetItem()
    {
        var sutBudgetItem = new BudgetItemManager(_client);
        var stopwatchBudgetItem = Stopwatch.StartNew();
        
        var itemsBudgetItem = await sutBudgetItem.GetBudgetItemsAsync();
        stopwatchBudgetItem.Stop();
        
        Assert.True(stopwatchBudgetItem.Elapsed.TotalSeconds <= 6.0,
            $"Hämtningen tog : {stopwatchBudgetItem.Elapsed.TotalSeconds} sekunder");
    }
}
