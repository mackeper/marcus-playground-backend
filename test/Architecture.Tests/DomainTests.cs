using FluentAssertions;
using NetArchTest.Rules;
using SharedKernel.DDD;

namespace Architecture.Tests;
public class DomainTests
{ 
    [Fact]
    public void ValueObjects_Should_Be_Immutable()
    {
        // Arrange
        var assembly = typeof(Domain.AssemblyReference).Assembly;

        // Act
        var result = Types.InAssembly(assembly)
            .That()
            .Inherit(typeof(ValueObject))
            .Should()
            .BeImmutable()
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue(TestResultFormatter.FormatFailingTypes(result));
    }
}
