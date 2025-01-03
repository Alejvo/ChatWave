using System.Reflection;

namespace Application;

public sealed class ApplicationAssemblyReference
{
    internal static readonly Assembly Assembly = typeof(ApplicationAssemblyReference).Assembly;
}
