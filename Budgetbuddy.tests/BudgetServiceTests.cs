using BudgetBuddy.Models;
using BudgetBuddy.Services;
using Budgetbuddy.tests.Interfaces;
using Moq;

namespace Budgetbuddy.tests;

public class BudgetServiceTests
{
    [Fact]
    public async Task TestIfReturnsCorrectTotalSum()
    {
        // Arrange
        var mockBudgetItemManager = new Mock<IBudgetItemManager>();
        var budgetItems = new List<BudgetItem>
        {
            new BudgetItem() { Name = "Test 1", Amount = 10, IsIncome = true },
            new BudgetItem() { Name = "Test 2", Amount = 10, IsIncome = true },
            new BudgetItem() { Name = "Test 3", Amount = 10, IsIncome = true },
            new BudgetItem() { Name = "NegativeTest 1", Amount = 5, IsIncome = false },
            new BudgetItem() { Name = "NegativeTest 2", Amount = 5, IsIncome = false },
            new BudgetItem() { Name = "NegativeTest 3", Amount = 5, IsIncome = false }
            
        };
        mockBudgetItemManager
            .Setup(m => m.GetBudgetItemsAsync())
            .ReturnsAsync(budgetItems);
        
        var service = new BudgetService(null, mockBudgetItemManager.Object);
        // Act
        var totalIncome =  service.GetTotalAmount(budgetItems, true);
        var totalExpense =  service.GetTotalAmount(budgetItems, false);
        // Assert
        Assert.Equal(30, totalIncome);
        Assert.Equal(15, totalExpense);
    }
    [Fact]
    public async Task TestIfReturnsCorrectNetSum()
    {
        // Arrange
        var mockBudgetItemManager = new Mock<IBudgetItemManager>();
        var budgetItems = new List<BudgetItem>
        {
            new BudgetItem() { Name = "Test 1", Amount = 10, IsIncome = true },
            new BudgetItem() { Name = "Test 2", Amount = 10, IsIncome = true },
            new BudgetItem() { Name = "Test 3", Amount = 10, IsIncome = true },
            new BudgetItem() { Name = "NegativeTest 1", Amount = 5, IsIncome = false },
            new BudgetItem() { Name = "NegativeTest 2", Amount = 5, IsIncome = false },
            new BudgetItem() { Name = "NegativeTest 3", Amount = 5, IsIncome = false }
        };
        mockBudgetItemManager
            .Setup(m => m.GetBudgetItemsAsync())
            .ReturnsAsync(budgetItems);
        
        var service = new BudgetService(null, mockBudgetItemManager.Object);
        // Act
        var netSum =  service.GetNetResult(budgetItems);
        // Assert
        Assert.Equal(15, netSum);
    }
    
    [Theory]
    [InlineData(10, 5, 5)]
    [InlineData(-10, 5, 5)]
    [InlineData(0, 5, -5)]
    [InlineData(10, -5, 15)]
    [InlineData(10, 0, 10)]
    [InlineData(-10, -5, 15)]
    [InlineData(0, 0, 0)]
    public async Task TestIfFailOnNegativeInput(decimal income, decimal expense, decimal total)
    {
        // Arrange
        var mockBudgetItemManager = new Mock<IBudgetItemManager>();
        var budgetItems = new List<BudgetItem>
        {
            new BudgetItem() { Name = "Test 1", Amount = income, IsIncome = true },
            new BudgetItem() { Name = "NegativeTest 1", Amount = expense, IsIncome = false },
        };
        mockBudgetItemManager
            .Setup(m => m.GetBudgetItemsAsync())
            .ReturnsAsync(budgetItems);
        
        var service = new BudgetService(null, mockBudgetItemManager.Object);

        // Act & Assert
        if (income < 0 || expense < 0)
        {
            var exception =  Assert.Throws<ArgumentOutOfRangeException>(() => service.GetNetResult(budgetItems));
            Assert.Equal("budgetItems", exception.ParamName);
            Assert.Contains("Budget items cannot be negative", exception.Message);
        }
        else
        {
            var netSum =  service.GetNetResult(budgetItems);
            Assert.Equal(total, netSum);
        }
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task TestIfNameCanBeEmptyOrNull(string name)
    {
        // Arrange
        var mockBudgetItemManager = new Mock<IBudgetItemManager>();
        var budgetItems = new List<BudgetItem>
        {
            new BudgetItem() { Name = name, Amount = 50, IsIncome = true },
            new BudgetItem() { Name = name, Amount = 25, IsIncome = false },
        };
        mockBudgetItemManager
            .Setup(m => m.GetBudgetItemsAsync())
            .ReturnsAsync(budgetItems);
        
        var service = new BudgetService(null, mockBudgetItemManager.Object);
        // Act
        var exception = Assert.Throws<ArgumentException>(() => service.GetNetResult(budgetItems));
        
        // Assert
        Assert.Contains("Items must have a name", exception.Message);
    }
}