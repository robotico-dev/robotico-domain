# Robotico.Domain

DDD building blocks for .NET 8 and .NET 10. **Zero package dependencies.** Provides `ValueObject`, `Entity<TId>`, and `IEntity<TId>` for domain-driven design.

[![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/download/dotnet/10.0)
[![C#](https://img.shields.io/badge/C%23-12-239120?logo=csharp)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![GitHub Packages](https://img.shields.io/badge/GitHub%20Packages-Robotico.Domain-blue?logo=github)](https://github.com/robotico-dev/robotico-domain/packages)
[![Build](https://github.com/robotico-dev/robotico-domain/actions/workflows/publish.yml/badge.svg)](https://github.com/robotico-dev/robotico-domain/actions/workflows/publish.yml)

## Features

- **ValueObject** — Abstract base for value objects. Override `GetEqualityComponents()`; equality and hashing are based on those components. Null-safe `==` and `!=`.
- **IEntity&lt;TId&gt;** — Interface for entities with an identifier of type `TId` (e.g. `Guid`, `int`). Identity is the Id.
- **Entity&lt;TId&gt;** — Abstract base implementing `IEntity<TId>` with equality and hashing by `Id`. Use when you want structural equality by identity.

## Installation

```bash
dotnet add package Robotico.Domain
```

## Quick start

### Value object

```csharp
using Robotico.Domain;

public sealed class Money : ValueObject
{
    public decimal Amount { get; init; }
    public string Currency { get; init; } = "USD";

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }
}

Money a = new() { Amount = 10m, Currency = "USD" };
Money b = new() { Amount = 10m, Currency = "USD" };
Assert.True(a == b);  // equal by components
```

### Entity

```csharp
using Robotico.Domain;

public sealed class Order : Entity<Guid>
{
    public string Number { get; init; } = string.Empty;

    public Order(Guid id, string number) : base(id)
    {
        Number = number;
    }
}

Order o1 = new Order(id, "ORD-1");
Order o2 = new Order(id, "ORD-2");
Assert.True(o1 == o2);  // same Id ⇒ same entity
```

## When to use

- **ValueObject** — For values defined by their attributes (Money, Address, DateRange). Same components ⇒ equal.
- **IEntity&lt;TId&gt;** / **Entity&lt;TId&gt;** — For objects with stable identity (Order, User). Same Id ⇒ same entity; other attributes can change.

See the design doc (`docs/design.adoc`) for full guidance.

## Documentation

Full contract and design: see **`docs/design.adoc`**. Detailed design docs (AsciiDoc) are in the `docs/` folder:

- **Design** (`docs/design.adoc`) — When to use ValueObject vs Entity, GetEqualityComponents, identity.

To build HTML from the AsciiDoc sources (e.g. with Asciidoctor):

```bash
asciidoctor docs/index.adoc -o docs/index.html
asciidoctor docs/design.adoc -o docs/design.html
```

## Versioning

We follow [Semantic Versioning](https://semver.org/). Version **1.0.0** is the first stable release. No breaking changes in minor/patch versions.

## Building, testing, and benchmarks

From the repo root:

```bash
dotnet restore
dotnet build -c Release
dotnet test tests/Robotico.Domain.Tests/Robotico.Domain.Tests.csproj -c Release
```

With coverage (Coverlet):

```bash
dotnet test tests/Robotico.Domain.Tests/Robotico.Domain.Tests.csproj -c Release --collect:"XPlat Code Coverage"
```

Optional CI gate (fail if line coverage below threshold):

```bash
dotnet test tests/Robotico.Domain.Tests/Robotico.Domain.Tests.csproj -c Release --collect:"XPlat Code Coverage" /p:CollectCoverage=true /p:Threshold=90 /p:ThresholdType=line
```

Run benchmarks (BenchmarkDotNet). **Recommended: run benchmarks in CI to catch performance regressions.**

```bash
dotnet run --project benchmarks/Robotico.Domain.Benchmarks/Robotico.Domain.Benchmarks.csproj -c Release -f net8.0 -- --filter "*"
```

Or open the solution in your IDE and build from there.

## License

See repository license file.
