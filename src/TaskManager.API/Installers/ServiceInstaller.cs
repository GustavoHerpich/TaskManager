namespace TaskManager.API.Installers; 

public static class ServiceInstaller
    {
        public static void AddServiceConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwagger();
            services.AddProjectDependencies();
            services.AddDatabase(configuration);
            services.AddCorsPolicy();
            services.AddSignalR();
            services.AddControllers();
        }
    }