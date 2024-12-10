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
using Microsoft.Identity.Client;
using Xunit;
using static System.DateTime;

namespace Budgetbuddy.tests;

public class CategoriesManagerTests
{
    [Fact]
    public async Task TestIfCanCreateNewCategory()
    {
        var mockHttpMessageHandler = new MockHttpMessageHandler((request, cancellationToken) =>
        {
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
        });

        var client = new HttpClient(mockHttpMessageHandler);
        var sutCategoryItemManager = new CategoriesManager(client);

        var category = new Category()
        {
            Name = "TestCategory",
            BudgetLimit = 10000
        };

        HttpResponseMessage expectedResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK);

        var actualResponse = await sutCategoryItemManager.CreateCategoryAsync(category);

        Assert.Equal(expectedResponse.StatusCode, actualResponse.StatusCode);
    }
    
    [Fact]
    public async Task ShouldReturnConflictForDuplicateCategory()
    {
        var mockHttpHandler = new MockHttpMessageHandler((request, cancellationToken) =>
        {
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.Conflict));
        });

        var client = new HttpClient(mockHttpHandler);
        var sutCategoryManager = new CategoriesManager(client);

        var category = new Category
        {
            Name = "ExistingCategory",
            BudgetLimit = 1000
        };

        var response = await sutCategoryManager.CreateCategoryAsync(category);

        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }
    
    
    [Fact]
    public async Task TestIfReturnErrorOnFailedCreateCategory()
    {
        var mockHttpMessageHandler = new MockHttpMessageHandler((request, cancellationToken) =>
        {
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
        });
        var client = new HttpClient(mockHttpMessageHandler);
        // Act
        var sutCategoryManager = new CategoriesManager(client);
        var response = await sutCategoryManager.CreateCategoryAsync(null);
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    
    
    

    [Theory]
    [InlineData("", 10000, 1)]     
    [InlineData("", -10000, 1)]     
    [InlineData("Mat", 1000, 1)]   
    [InlineData("Kläder", -1000, 1)]     

    public async Task TestIfReturnErrorOnFailedGetCategory(string categoryName, int expectedBudgetLimit, int categoryId)
    {
        // Arrange
        var mockHttpMessageHandler = new MockHttpMessageHandler((request, cancellationToken) =>
        {
            
            if (string.IsNullOrWhiteSpace(categoryName) || expectedBudgetLimit <= 0)
            {
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }

            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
        });

        var client = new HttpClient(mockHttpMessageHandler);
        var sutCategoryManager = new CategoriesManager(client);

        var category = new Category()
        {
            Name = categoryName,
            BudgetLimit = expectedBudgetLimit,
            Id = categoryId
        };

        // Act
        var response = await sutCategoryManager.CreateCategoryAsync(category);

        // Assert
        var expectedStatusCode = string.IsNullOrWhiteSpace(categoryName) || expectedBudgetLimit <= 0
            ? HttpStatusCode.BadRequest
            : HttpStatusCode.OK;

        Assert.Equal(expectedStatusCode, response.StatusCode);
    }
    
    [Theory]
    [InlineData("", 10000)]
    [InlineData("Test", -1000)]   
    [InlineData(null, -1000)]        
    public async Task ShouldReturnBadRequestForInvalidCategoryInputs(string name, int budgetLimit)
    {
        var mockHttpHandler = new MockHttpMessageHandler((request, cancellationToken) =>
        {
            if (string.IsNullOrWhiteSpace(name) || budgetLimit <= 0)
            {
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }

            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
        });

        var client = new HttpClient(mockHttpHandler);
        var sutCategoryManager = new CategoriesManager(client);

        var category = new Category { Name = name, BudgetLimit = budgetLimit };
        var response = await sutCategoryManager.CreateCategoryAsync(category);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        
    }
    
    
    
    
    
    
    
    
    
}