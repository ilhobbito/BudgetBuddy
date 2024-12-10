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
using static System.DateTime;

namespace Budgetbuddy.tests;

public class BudgetItemManagerTests
{
    [Fact]
    public async Task TestIfCanCreateBudgetItem()
    {
        // Arrange
        var mockHttpMessageHandler = new MockHttpMessageHandler((request, cancellationToken) =>
            {
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
            });
        
        var client = new HttpClient(mockHttpMessageHandler);
        var sutBudgetItemManager = new BudgetItemManager(client);
        
        var budgetItem = new BudgetItem()
        {
            Name = "Inkomst",
            CategoryId = 1,
            Amount = 200m,
            IsIncome = true,
            Date = Now
        };
        
        HttpResponseMessage expectedResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK); 

        // Act
        var actualResponse = await sutBudgetItemManager.CreateBudgetItemAsync(budgetItem);
        
        // Assert
        Assert.Equal(expectedResponse.StatusCode, actualResponse.StatusCode);
    }
    
    [Fact]
    public async Task TestIfReturnsErrorOnFailedCreation()
    {
        // Arrange
        var mockHttpMessageHandler = new MockHttpMessageHandler((request, cancellationToken) =>
        {
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
        });
        var client = new HttpClient(mockHttpMessageHandler);
        // Act
        var sutBudgetItemManager = new BudgetItemManager(client);
        var response = await sutBudgetItemManager.CreateBudgetItemAsync(null);
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [InlineData( "", 1, 10.0, true )]
    [InlineData( "Inkomst", 1, -10.0, true )]
    public async Task TestIfReturnsErrorOnWrongInputs(string name, int categoryId, decimal amount,  bool isIncome)
    {
        // Arrange
        var mockHttpMessageHandler = new MockHttpMessageHandler((request, cancellationToken) =>
        {
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
        });
        var client = new HttpClient(mockHttpMessageHandler);
        
        var sutBudgetItemManager = new BudgetItemManager(client);
        var budgetItem = new BudgetItem()
        {
            Name = name,
            CategoryId = categoryId,
            Amount = amount,
            IsIncome = isIncome,
            Date = Now
        };
        // Act
        var response = await sutBudgetItemManager.CreateBudgetItemAsync(budgetItem);
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}

