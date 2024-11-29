using WebApplication1;

var builder = WebApplication.CreateBuilder(args);

builder.AddServices();

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name:"AllowAllOrigins",
        policy =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
        });
});

var app = builder.Build();
//TODO: Maybe not allow all to access?
app.UseCors("AllowAllOrigins");

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

app.Run();
