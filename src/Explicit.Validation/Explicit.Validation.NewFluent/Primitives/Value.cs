﻿using Explicit.Validation.NewFluent.Fluent;

namespace Explicit.Validation.NewFluent;

public sealed class Value<TValue, TMethod> : IValidate<Value<TValue, TMethod>>
    where TValue : notnull
    where TMethod : IValidationRule<TValue>
{
    private readonly TValue _value;

    public Value(TValue value)
    {
        _value = value;
    }

    public TValue GetValue()
    {
        return _value;
    }

    public static void SetupValidation(FluentValidator<Value<TValue, TMethod>> validator)
    {
        var rule = validator.RuleFor(x => x._value);
        var builder = new RuleBuilder<Value<TValue, TMethod>, TValue>(rule);

        TMethod.SetupRule(builder);
    }
}
