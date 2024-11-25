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
    }
}