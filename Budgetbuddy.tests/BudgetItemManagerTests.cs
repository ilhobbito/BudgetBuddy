using System.Net;
using BudgetBuddy.Lib.DAL;
using BudgetBuddy.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using WebApplication1;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using Azure.Core;
using Xunit;

namespace Budgetbuddy.tests;

public class BudgetItemManagerTests
{
    [Fact]
    public async Task TestIfCanCreateBudgetItem()
    {
      
        var mockHttpMessageHandler = new MockHttpMessageHandler((request, cancellationToken) =>
            {
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
            });
        
        var client = new HttpClient(mockHttpMessageHandler);
        var sutBudgetItemManager = new BudgetItemManager(client);
        
        // Arrange
        var budgetItem = new BudgetItem()
        {
            Name = "Testitem",
            CategoryId = 1,
            Amount = 200m,
            IsIncome = true,
            Date = DateTime.Now,
        };
        
        HttpResponseMessage expectedResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK); 

        // Act
        var actualResponse = await sutBudgetItemManager.CreateBudgetItemAsync(budgetItem);
        // Assert
        Assert.Equal(expectedResponse.StatusCode, actualResponse.StatusCode);
    }
    [Fact]
    public async Task TestIfCanCreateBudgetItemAgain()
    {
      
        var mockHttpMessageHandler = new MockHttpMessageHandler((request, cancellationToken) =>
        {
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
        });
        
        var client = new HttpClient(mockHttpMessageHandler);
        var sutBudgetItemManager = new BudgetItemManager(client);
        
        // Arrange Added this to push something new
        var budgetItem = new BudgetItem()
        {
            Name = "Testitem",
            CategoryId = 1,
            Amount = 200m,
            IsIncome = true,
            Date = DateTime.Now,
        };
        
        HttpResponseMessage expectedResponse = new HttpResponseMessage(System.Net.HttpStatusCode.Conflict); 

        // Act
        var actualResponse = await sutBudgetItemManager.CreateBudgetItemAsync(budgetItem);
        // Assert
        Assert.Equal(expectedResponse.StatusCode, actualResponse.StatusCode);
    }

   
}