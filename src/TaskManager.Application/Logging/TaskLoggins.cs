using Microsoft.Extensions.Logging;

namespace TaskManager.Application.Logging;

public static partial class LoggingExtension
{
    [LoggerMessage(LogLevel.Information, "Tentando criar tarefa: {Title} - {TaskDescription}.")]
    public static partial void CreateTaskAttempt(
        this ILogger logger,
        string title,
        string taskDescription);

    [LoggerMessage(LogLevel.Information, "Tarefa {Title} criada com sucesso: {TaskId}.")]
    public static partial void TaskCreated(
        this ILogger logger,
        string title,
        Guid taskId);

    [LoggerMessage(LogLevel.Error, "Falha ao criar tarefa: {Title} - {TaskDescription}.")]
    public static partial void TaskCreationFailed(
        this ILogger logger,
        string title,
        string taskDescription);

    [LoggerMessage(LogLevel.Information, "Tentando atualizar tarefa: {TaskId} - {Title}.")]
    public static partial void UpdateTaskAttempt(
        this ILogger logger,
        Guid taskId,
        string title);

    [LoggerMessage(LogLevel.Information, "Tarefa atualizada com sucesso: {TaskId} - {Title}.")]
    public static partial void TaskUpdated(
        this ILogger logger,
        Guid taskId,
        string title);

    [LoggerMessage(LogLevel.Error, "Falha ao atualizar tarefa: {TaskId} - {Title}.")]
    public static partial void TaskUpdateFailed(
        this ILogger logger,
        Guid taskId,
        string title);

    [LoggerMessage(LogLevel.Information, "Tentando deletar tarefa: {TaskId}.")]
    public static partial void DeleteTaskAttempt(
        this ILogger logger,
        Guid taskId);

    [LoggerMessage(LogLevel.Information, "Tarefa deletada com sucesso: {TaskId}.")]
    public static partial void TaskDeleted(
        this ILogger logger,
        Guid taskId);

    [LoggerMessage(LogLevel.Error, "Falha ao deletar tarefa: {TaskId} - {Title}.")]
    public static partial void TaskDeletionFailed(
        this ILogger logger,
        Guid taskId,
        string title);

    [LoggerMessage(LogLevel.Information, "Buscando tarefa com ID: {TaskId}.")]
    public static partial void FetchTaskByIdAttempt(
        this ILogger logger,
        Guid taskId);

    [LoggerMessage(LogLevel.Warning, "Tarefa não encontrada: {TaskId}.")]
    public static partial void TaskNotFound(
        this ILogger logger,
        Guid taskId);

    [LoggerMessage(LogLevel.Information, "Tarefa encontrada: {TaskId} - {Title}.")]
    public static partial void TaskFetched(
        this ILogger logger,
        Guid taskId,
        string title);

    [LoggerMessage(LogLevel.Information, "Buscando todas as tarefas com status de conclusão: {Completed}.")]
    public static partial void FetchAllTasksAttempt(
        this ILogger logger,
        bool completed);

    [LoggerMessage(LogLevel.Information, "{TaskCount} tarefas encontradas com status de conclusão: {Completed}.")]
    public static partial void AllTasksFetched(
        this ILogger logger,
        int taskCount,
        bool completed);
}