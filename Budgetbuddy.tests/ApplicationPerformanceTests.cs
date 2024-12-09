using System;
using System.Diagnostics;
using System.Net;
using BudgetBuddy.Lib.DAL;
using Xunit; 

namespace Budgetbuddy.tests;

public class ApplicationPerformanceTests
{
    public static readonly HttpClient _client; 
    [Fact]
    public async Task LoadTimeUnderThreeSeconds()
    {
        
        var client = new HttpClient();
        var sut = new BudgetItemManager(client);
        var stopwatch = Stopwatch.StartNew();

        var items = await sut.GetBudgetItemsAsync();
        stopwatch.Stop();
        
        Assert.True(stopwatch.Elapsed.TotalSeconds <= 3.0,
            $"Applikationens laddningstid överskred 3 sekunder: {stopwatch.Elapsed.TotalSeconds} sekunder.");
    }

    [Fact]
    public async Task LoadCategoriesUnderThreeSeconds()
    {
        var client = new HttpClient();
        var sutCategories = new CategoriesManager(client);
        var stopwatchCategories = Stopwatch.StartNew();
        
        var itemsCategories = await sutCategories.GetCategoriesAsync();
        stopwatchCategories.Stop();
        
        Assert.True(stopwatchCategories.Elapsed.TotalSeconds <= 3.0,
            $"Hämtningen tog : {stopwatchCategories.Elapsed.TotalSeconds} sekunder");
    }
}
