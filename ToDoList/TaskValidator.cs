﻿using FluentValidation;
using FluentValidation.Results;

namespace ToDoList
{
    public class TaskValidator : AbstractValidator<CreateTaskItemRequest>, ITaskValidator
    {
        public TaskValidator()
        {
            RuleFor(t => t.Title).MinimumLength(10).MaximumLength(100).NotNull();
            RuleFor(t => t.DeadLine).NotNull();
        }

        public bool ValidateTask(CreateTaskItemRequest task)
        {
            TaskValidator valid = new ();
            ValidationResult results = valid.Validate(task);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
                return false;
            }
            return true;
        }
    }
}