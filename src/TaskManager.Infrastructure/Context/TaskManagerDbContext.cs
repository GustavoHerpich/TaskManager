using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Mappings;

namespace TaskManager.Infrastructure.Context;

public class TaskManagerDbContext(
    DbContextOptions<TaskManagerDbContext> _options) : DbContext(_options)
{
    public DbSet<TaskItem> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TaskItemMap());
       
        base.OnModelCreating(modelBuilder);
    }
}