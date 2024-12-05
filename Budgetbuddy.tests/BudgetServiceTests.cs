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
        var totalIncome = await service.GetTotalAmount(true);
        var totalExpense = await service.GetTotalAmount(false);
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
        var netSum = await service.GetNetResult();
        // Assert
        Assert.Equal(15, netSum);
    }
}