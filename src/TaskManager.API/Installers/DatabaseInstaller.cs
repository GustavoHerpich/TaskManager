using Microsoft.EntityFrameworkCore;
using TaskManager.Infrastructure.Context;

namespace TaskManager.API.Installers; 
public static class DatabaseInstaller
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
           services.AddEntityFrameworkSqlServer()
                .AddDbContext<TaskManagerDbContext>(
                    options => options.UseSqlServer(configuration.GetConnectionString("DataBase"),
                    sqlOptions => sqlOptions.EnableRetryOnFailure())
                );
            
            return services;
        }
    }