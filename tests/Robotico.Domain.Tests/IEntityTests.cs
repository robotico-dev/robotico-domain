namespace Robotico.Domain.Tests;

/// <summary>IEntity and Entity&lt;TId&gt;: identity, equality by Id.</summary>
public sealed class IEntityTests
{
    [Fact]
    public void Entity_exposes_Id()
    {
        Guid id = Guid.NewGuid();
        SampleEntity entity = new SampleEntity(id, "Test");
        Assert.Equal(id, entity.Id);
    }

    [Fact]
    public void Entity_equality_by_Id_same_Id_are_equal()
    {
        Guid id = Guid.Parse("11111111-1111-1111-1111-111111111111");
        SampleEntity a = new SampleEntity(id, "A");
        SampleEntity b = new SampleEntity(id, "B");
        Assert.Equal(a, b);
        Assert.True(a == b);
        Assert.False(a != b);
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }

    [Fact]
    public void Entity_equality_by_Id_different_Id_not_equal()
    {
        SampleEntity a = new SampleEntity(Guid.NewGuid(), "Same");
        SampleEntity b = new SampleEntity(Guid.NewGuid(), "Same");
        Assert.NotEqual(a, b);
        Assert.False(a == b);
        Assert.True(a != b);
    }

    [Fact]
    public void Entity_equality_operator_both_null_returns_true()
    {
        SampleEntity? left = null;
        SampleEntity? right = null;
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact]
    public void Entity_equality_operator_one_null_returns_false()
    {
        SampleEntity? left = null;
        SampleEntity right = new SampleEntity(Guid.NewGuid(), "X");
        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact]
    public void IEntity_contract_implemented_by_Entity_base()
    {
        Guid id = Guid.NewGuid();
        SampleEntity entity = new SampleEntity(id, "E");
        Assert.Equal(id, entity.Id);
    }
}
