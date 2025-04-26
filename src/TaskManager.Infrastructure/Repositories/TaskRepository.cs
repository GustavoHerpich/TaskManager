using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;
using TaskManager.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Infrastructure.Repositories;

public class TaskRepository(TaskManagerDbContext _context) : IRepository<TaskItem>
{
    public async Task AddAsync(TaskItem entity)
    {
        _context.Tasks.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TaskItem entity)
    {
        _context.Tasks.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TaskItem entity)
    {
        _context.Tasks.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<TaskItem> GetByIdAsync(Guid id)
    {
        return await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<List<TaskItem>> GetAllAsync()
    {
        return await _context.Tasks.ToListAsync();
    }
}
