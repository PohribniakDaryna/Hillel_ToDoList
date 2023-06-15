using Microsoft.AspNetCore.Mvc;
using System;
using ToDoList;
using ToDoList.Models;

public interface ITaskRegister
{
    List<TaskItem> GetTasks();
    void AddTask(TaskItem taskItem);
    ITaskItem? UpdateTask(int id, [FromBody] CreateTaskItemRequest request);
    bool DeleteTask(int id, string title);
    ITaskItem? GetTaskById(int id);
}