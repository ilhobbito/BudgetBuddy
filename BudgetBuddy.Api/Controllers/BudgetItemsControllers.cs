using BudgetBuddy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers;
/*
[ApiController]
[Route("api/BudgetItemsTest")]

public class BudgetItemsController : Controller
{
    private readonly BudgetBuddyContext _context;

    public BudgetItemsController(BudgetBuddyContext context)
    {
        _context = context;
    }

    [HttpPost]
    public void CreateBudgetItem([FromBody]BudgetItem budgetItem)
    {
        _context.BudgetItems.Add(budgetItem);
        _context.SaveChanges();

    }

    [HttpGet]
    public IActionResult GetBudgetItems()
    {
        var budgetItems = _context.BudgetItems.ToList();
        return Ok(budgetItems);
    }
} */
