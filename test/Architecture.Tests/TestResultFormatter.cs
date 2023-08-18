using NetArchTest.Rules;

namespace Architecture.Tests;
internal static class TestResultFormatter
{
    public static string FormatFailingTypes(TestResult result)
        => result.FailingTypeNames != null ?
            string.Join(", ", result.FailingTypeNames) :
            string.Empty;
}
