using TaskManager.Application.Dtos;
using FluentResults;

namespace TaskManager.Application.Interfaces;

public interface ITaskService
{
    Task<Result<TaskItemDto>> CreateAsync(CreateTaskDto dto);
    Task<Result<TaskItemDto>> UpdateAsync(Guid id, UpdateTaskDto dto);
    Task<Result> DeleteAsync(Guid id);
    Task<Result<TaskItemDto>> GetByIdAsync(Guid id);
    Task<Result<List<TaskItemDto>>> GetAllAsync(bool completed);
}