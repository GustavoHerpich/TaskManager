using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;
using TaskManager.Application.Interfaces;
using FluentResults;
using TaskManager.Application.Dtos;
using FluentValidation;
using TaskManager.Application.Extensions;
using Microsoft.Extensions.Logging;
using TaskManager.Application.Logging;

namespace TaskManager.Application.Services;

public class TaskService(
    IRepository<TaskItem> _repository,  
    IValidator<CreateTaskDto> _createValidator,
    IValidator<UpdateTaskDto> _updateValidator,
    ILogger<TaskService> _logger) : ITaskService
{
   public async Task<Result<TaskItemDto>> CreateAsync(CreateTaskDto dto)
    {
        _logger.CreateTaskAttempt(dto.Description);
        
        var validation = await _createValidator.ValidateAsync(dto);
        var result = validation.ToResult();
        if (result.IsFailed)
        {
            _logger.TaskCreationFailed(dto.Description);
            return result;
        }

        var task = new TaskItem(dto.Description);
        await _repository.AddAsync(task);

        _logger.TaskCreated(task.Id);
        return Result.Ok(ToDto(task));
    }

    public async Task<Result<TaskItemDto>> UpdateAsync(Guid id, UpdateTaskDto dto)
    {
        _logger.UpdateTaskAttempt(id);

        var validation = await _updateValidator.ValidateAsync(dto);
        var result = validation.ToResult();
        if (result.IsFailed)
        {
            _logger.TaskUpdateFailed(id);
            return result;
        }

        var task = await _repository.GetByIdAsync(id);
        if (task == null)
        {
            _logger.TaskNotFound(id);
            return Result.Fail(new NotFoundError("Tarefa não encontrada."));
        }

        task.Description = dto.Description;
        if (dto.MarkAsCompleted && !task.IsCompleted)
            task.MarkAsCompleted();

        await _repository.UpdateAsync(task);

        _logger.TaskUpdated(task.Id);
        return Result.Ok(ToDto(task));
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        _logger.DeleteTaskAttempt(id);

        var task = await _repository.GetByIdAsync(id);
        if (task == null)
        {
            _logger.TaskNotFound(id);
            return Result.Fail(new NotFoundError("Tarefa não encontrada."));
        }

        await _repository.DeleteAsync(task);

        _logger.TaskDeleted(task.Id);
        return Result.Ok();
    }

    public async Task<Result<TaskItemDto>> GetByIdAsync(Guid id)
    {
        _logger.FetchTaskByIdAttempt(id);

        var task = await _repository.GetByIdAsync(id);
        if (task == null)
        {
            _logger.TaskNotFound(id);
            return Result.Fail(new NotFoundError("Tarefa não encontrada."));
        }

        _logger.TaskFetched(id); 
        return Result.Ok(ToDto(task));
    }

    public async Task<Result<List<TaskItemDto>>> GetAllAsync(bool completed)
    {
        _logger.FetchAllTasksAttempt(completed);

        var list = await _repository.GetAllAsync();

        var filtered = list
            .Where(t => t.IsCompleted == completed)
            .Select(t => ToDto(t))
            .ToList();

        _logger.AllTasksFetched(filtered.Count, completed);
        return Result.Ok(filtered);
    }

    private static TaskItemDto ToDto(TaskItem task) => new()
    {
        Id = task.Id,
        Description = task.Description,
        CreatedAt = task.CreatedAt,
        CompletedAt = task.CompletedAt
    };
}