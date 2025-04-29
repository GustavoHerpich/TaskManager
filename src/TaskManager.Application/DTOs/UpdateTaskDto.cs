namespace TaskManager.Application.Dtos;

public class UpdateTaskDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool MarkAsCompleted { get; set; }
    }
