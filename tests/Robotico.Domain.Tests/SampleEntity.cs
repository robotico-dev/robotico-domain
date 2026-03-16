namespace Robotico.Domain.Tests;

/// <summary>Sample entity for tests.</summary>
public sealed class SampleEntity : Entity<Guid>
{
    public string Name { get; init; } = string.Empty;

    public SampleEntity(Guid id, string name) : base(id)
    {
        Name = name;
    }
}
