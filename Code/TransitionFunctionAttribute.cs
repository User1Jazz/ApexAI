using System;

[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
public sealed class TransitionFunctionAttribute : Attribute
{
    // This is a marker attribute; it doesn't need any implementation
}
