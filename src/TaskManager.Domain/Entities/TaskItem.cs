namespace TaskManager.Domain.Entities;

public class TaskItem(string description)
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Description { get; set; } = description;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; }

    public bool IsCompleted => CompletedAt.HasValue;

    public void MarkAsCompleted()
    {
        CompletedAt = DateTime.UtcNow;
    }
}