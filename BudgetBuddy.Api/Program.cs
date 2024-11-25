using BudgetBuddy.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication1;

var builder = WebApplication.CreateBuilder(args);

builder.AddServices();

var app = builder.Build();
app.MapControllers();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.AddCategoryEndpoints();
app.AddItemsEndpoints();
app.AddUserEndpoints();
app.UseHttpsRedirection();
app.Run();
