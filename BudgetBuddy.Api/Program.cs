using BudgetBuddy.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication1;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BudgetBuddyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/api/BudgetItemsGet", async (BudgetBuddyContext context) =>
{
    return await context.BudgetItems.ToListAsync(); 
}).WithName("GetBudgetItems").WithOpenApi();

app.MapPost("/api/BudgetItemsPost", async (BudgetItem budgetitem, BudgetBuddyContext context) =>
{
    context.BudgetItems.Add(budgetitem);
    await context.SaveChangesAsync();
    return budgetitem;
}).WithName("PostBudgetItem").WithOpenApi();

app.MapDelete("/api/BudgetItemsDelete/{id}", async (int id, BudgetBuddyContext context) =>
{
    var budgetItem = await context.BudgetItems.FindAsync(id);
    context.BudgetItems.Remove(budgetItem);
    await context.SaveChangesAsync();
}).WithName("DeleteBudgetItem").WithOpenApi();

app.MapPut("/api/BudgetItemsPut", async (BudgetItem budgetitem, BudgetBuddyContext context) =>
{
    context.Update(budgetitem);
    await context.SaveChangesAsync();
}).WithName("EditBudgetItem").WithOpenApi();

// ------------------

app.MapGet("/api/UsersGet", async (BudgetBuddyContext context) =>
{
    return await context.Users.ToListAsync(); 
}).WithName("GetUsers").WithOpenApi();

app.MapPost("/api/UsersPost", async (User user, BudgetBuddyContext context) =>
{
    context.Users.Add(user);
    await context.SaveChangesAsync();
    return user;
}).WithName("PostUsers").WithOpenApi();

app.MapDelete("/api/UsersDelete/{id}", async (int id, BudgetBuddyContext context) =>
{
    var user = await context.Users.FindAsync(id);
    context.Users.Remove(user);
    await context.SaveChangesAsync();
}).WithName("DeleteUsers").WithOpenApi();

app.MapPut("/api/UsersPut", async (User user, BudgetBuddyContext context) =>
{
    context.Update(user);
    await context.SaveChangesAsync();
}).WithName("EditUsers").WithOpenApi();

//-----------------

app.MapGet("/api/CategoriesGet", async (BudgetBuddyContext context) =>
{
    return await context.Categories.ToListAsync(); 
}).WithName("GetCategories").WithOpenApi();

app.MapPost("/api/CategoriesPost", async (Category category, BudgetBuddyContext context) =>
{
    context.Categories.Add(category);
    await context.SaveChangesAsync();
    return category;
}).WithName("PostCategories").WithOpenApi();

app.MapDelete("/api/CategoriesDelete/{id}", async (int id, BudgetBuddyContext context) =>
{
    var category = await context.Categories.FindAsync(id);
    context.Categories.Remove(category);
    await context.SaveChangesAsync();
}).WithName("DeleteCategories").WithOpenApi();

app.MapPut("/api/CategoriesPut", async (Category category, BudgetBuddyContext context) =>
{
    context.Update(category);
    await context.SaveChangesAsync();
}).WithName("EditCategories").WithOpenApi();

app.UseHttpsRedirection();

app.Run();
