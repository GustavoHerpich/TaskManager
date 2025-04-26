using Microsoft.Extensions.Logging;

namespace TaskManager.Application.Logging;

public static partial class LoggingExtension
{
    [LoggerMessage(LogLevel.Information, "Attempting to create task: {TaskDescription}.")]
    public static partial void CreateTaskAttempt(
        this ILogger logger,
        string taskDescription);

    [LoggerMessage(LogLevel.Information, "Task successfully created: {TaskId}.")]
    public static partial void TaskCreated(
        this ILogger logger,
        Guid taskId);

    [LoggerMessage(LogLevel.Error, "Failed to create task: {TaskDescription}.")]
    public static partial void TaskCreationFailed(
        this ILogger logger,
        string taskDescription);

    [LoggerMessage(LogLevel.Information, "Attempting to update task {TaskId}.")]
    public static partial void UpdateTaskAttempt(
        this ILogger logger,
        Guid taskId);

    [LoggerMessage(LogLevel.Information, "Task {TaskId} successfully updated.")]
    public static partial void TaskUpdated(
        this ILogger logger,
        Guid taskId);

    [LoggerMessage(LogLevel.Error, "Failed to update task {TaskId}.")]
    public static partial void TaskUpdateFailed(
        this ILogger logger,
        Guid taskId);

    [LoggerMessage(LogLevel.Information, "Attempting to delete task {TaskId}.")]
    public static partial void DeleteTaskAttempt(
        this ILogger logger,
        Guid taskId);

    [LoggerMessage(LogLevel.Information, "Task {TaskId} successfully deleted.")]
    public static partial void TaskDeleted(
        this ILogger logger,
        Guid taskId);

    [LoggerMessage(LogLevel.Error, "Failed to delete task {TaskId}.")]
    public static partial void TaskDeletionFailed(
        this ILogger logger,
        Guid taskId);

    [LoggerMessage(LogLevel.Information, "Fetching task with ID: {TaskId}.")]
    public static partial void FetchTaskByIdAttempt(
        this ILogger logger,
        Guid taskId);

    [LoggerMessage(LogLevel.Warning, "Task with ID {TaskId} not found.")]
    public static partial void TaskNotFound(
        this ILogger logger,
        Guid taskId);

    [LoggerMessage(LogLevel.Information, "Fetched task with ID: {TaskId}.")]
    public static partial void TaskFetched(
        this ILogger logger,
        Guid taskId);

    [LoggerMessage(LogLevel.Information, "Fetching all tasks with completion status: {Completed}.")]
    public static partial void FetchAllTasksAttempt(
        this ILogger logger,
        bool completed);

    [LoggerMessage(LogLevel.Information, "Fetched {TaskCount} tasks with completion status: {Completed}.")]
    public static partial void AllTasksFetched(
        this ILogger logger,
        int taskCount,
        bool completed);
}
