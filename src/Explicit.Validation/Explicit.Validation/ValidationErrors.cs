﻿namespace Explicit.Validation;

public sealed record ValidationErrors
{
    public IReadOnlyCollection<string> ErrorMessages { get; }

    public string Message => $"ValidationErrors: {string.Join(", ", ErrorMessages)}";

    public ValidationErrors(IReadOnlyCollection<string> errorMessages)
    {
        ErrorMessages = errorMessages;
    }
    
    public ValidationErrors(IReadOnlyCollection<ValidationErrors> errors)
    {
        ErrorMessages = errors.SelectMany(x => x.ErrorMessages).ToArray();
    }

    public ValidationErrors(params string[] errorMessages)
    {
        ErrorMessages = errorMessages;
    }

    public static ValidationErrors Null(string propertyName)
    {
        return new ValidationErrors($"Property: {propertyName} is null");
    }

    public ExplicitValidationException ToException()
    {
        return new ExplicitValidationException(this);
    }
}

public class ExplicitValidationException : Exception
{
    public ExplicitValidationException(ValidationErrors errors) : base($"ValidationException: {errors.Message}")
    {
    }
}