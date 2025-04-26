namespace TaskManager.API.Installers; 
public static class SwaggerInstaller
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "Task Manager API",
                Version = "v1",
                Description = "API para gerenciamento de tarefas"
            });
        });
        return services;
    }
}