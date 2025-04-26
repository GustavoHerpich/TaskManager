using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Extensions;
using TaskManager.Application.Dtos;
using TaskManager.Application.Interfaces;

namespace TaskManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController(ITaskService _taskService) : ControllerBase
{
    [HttpGet("completed/{completed:bool}")]
    public async Task<IActionResult> GetAll(bool completed)
    {
        var result = await _taskService.GetAllAsync(completed);
        return result.ToActionResult(); 
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _taskService.GetByIdAsync(id);
        return result.ToActionResult(); 
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTaskDto dto)
    {
        var result = await _taskService.CreateAsync(dto);
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value)
            : result.ToActionResult(); 
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTaskDto dto)
    {
        var result = await _taskService.UpdateAsync(id, dto);
        return result.ToActionResult(); 
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _taskService.DeleteAsync(id);
        return result.ToActionResult(); 
    }
}