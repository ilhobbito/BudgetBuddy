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
}