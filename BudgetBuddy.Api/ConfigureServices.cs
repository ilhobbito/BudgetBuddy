using Microsoft.EntityFrameworkCore;

namespace WebApplication1;

public static class ConfigureServices
{
    public static void AddServices(this WebApplicationBuilder builder)
    {
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<BudgetBuddyContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddControllers();
        
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowBlazorApp", builder =>
            {
                builder.WithOrigins("https://localhost:5231") // URL of your Blazor WebAssembly app
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
    }
}