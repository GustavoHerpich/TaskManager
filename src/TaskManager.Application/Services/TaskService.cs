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
        _logger.CreateTaskAttempt(dto.Title, dto.Description);
        
        var validation = await _createValidator.ValidateAsync(dto);
        var result = validation.ToResult();
        if (result.IsFailed)
        {
            _logger.TaskCreationFailed(dto.Title, dto.Description);
            return result;
        }

        var task = new TaskItem(dto.Title, dto.Description);
        await _repository.AddAsync(task);

        _logger.TaskCreated(task.Title, task.Id);
        return Result.Ok(ToDto(task));
    }

    public async Task<Result<TaskItemDto>> UpdateAsync(Guid id, UpdateTaskDto dto)
    {
        _logger.UpdateTaskAttempt(id, dto.Title);

        var validation = await _updateValidator.ValidateAsync(dto);
        var result = validation.ToResult();
        if (result.IsFailed)
        {
            _logger.TaskUpdateFailed(id, dto.Title);
            return result;
        }

        var task = await _repository.GetByIdAsync(id);
        if (task == null)
        {
            _logger.TaskNotFound(id);
            return Result.Fail(new NotFoundError("Tarefa não encontrada."));
        }

        task.Description = dto.Description;
        task.Title = dto.Title;

        if (dto.MarkAsCompleted && !task.IsCompleted)
        {
            task.MarkAsCompleted();
        }
        else if (!dto.MarkAsCompleted && task.IsCompleted)
        {
            task.MarkAsActive(); 
        }

        await _repository.UpdateAsync(task);

        _logger.TaskUpdated(task.Id, task.Title);
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

        _logger.TaskFetched(id, task.Title); 
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
        Title = task.Title,
        Description = task.Description,
        CreatedAt = task.CreatedAt,
        CompletedAt = task.CompletedAt
    };
}