using FluentValidation;
using TaskManager.Application.Dtos;
using TaskManager.Application.Interfaces;
using TaskManager.Application.Services;
using TaskManager.Application.Validators;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;
using TaskManager.Infrastructure.Repositories;

namespace TaskManager.API.Installers;

public static class DependencyInjectionInstaller
    {
        public static IServiceCollection AddProjectDependencies(this IServiceCollection services)
        {
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IValidator<CreateTaskDto>, CreateTaskValidator>();
            services.AddScoped<IValidator<UpdateTaskDto>, UpdateTaskValidator>();
            services.AddScoped<IRepository<TaskItem>, TaskRepository>();
            
            return services;
        }
    }