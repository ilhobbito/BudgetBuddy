using WebApplication1;

var builder = WebApplication.CreateBuilder(args);

builder.AddServices();

var app = builder.Build();

app.UseCors("AllowBlazorApp");
Console.WriteLine("CORS Policy Applied");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();
app.MapControllers();

// app.UseEndpoints(endpoints =>
// {
//     endpoints.MapControllers();
// });

app.AddCategoryEndpoints();
app.AddItemsEndpoints();
app.AddUserEndpoints();
app.UseHttpsRedirection();

app.Run();
