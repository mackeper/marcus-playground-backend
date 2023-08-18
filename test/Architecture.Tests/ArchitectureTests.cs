using FluentAssertions;
using NetArchTest.Rules;

namespace Architecture.Tests;

public class ArchitectureTests
{
    private const string DomainNamespace = "Domain";
    private const string ApplicationNamespace = "Application";
    private const string SharedKernalNamespace = "SharedKernel.DDD";
    private const string PresentationNamespace = "Web";
    private const string InfrastructureNamespace = "Infrastructure";

    [Fact]
    public void Domain_Should_Not_ReferenceAnything()
    {
        // Arrange
        var assembly = typeof(Domain.AssemblyReference).Assembly;

        // Act
        var result = Types.InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(ApplicationNamespace, InfrastructureNamespace, PresentationNamespace)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue(TestResultFormatter.FormatFailingTypes(result));
    }

    [Fact]
    public void Application_Should_Not_ReferenceAnything()
    {
        // Arrange
        var assembly = typeof(Application.AssemblyReference).Assembly;

        // Act
        var result = Types.InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(InfrastructureNamespace, PresentationNamespace, SharedKernalNamespace)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue(TestResultFormatter.FormatFailingTypes(result));
    }


    [Fact]
    public void Infrastructure_Should_Not_ReferenceAnything()
    {
        // Arrange
        var assembly = typeof(Infrastructure.AssemblyReference).Assembly;

        // Act
        var result = Types.InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(DomainNamespace, SharedKernalNamespace, PresentationNamespace)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue(TestResultFormatter.FormatFailingTypes(result));
    }

    [Fact]
    public void Presentation_Should_Not_ReferenceAnything()
    {
        // Arrange
        var assembly = typeof(Web.AssemblyReference).Assembly;

        // Act
        var result = Types.InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(DomainNamespace, ApplicationNamespace, SharedKernalNamespace, PresentationNamespace)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue(TestResultFormatter.FormatFailingTypes(result));
    }
}