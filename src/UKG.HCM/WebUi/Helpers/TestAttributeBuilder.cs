namespace WebUi.Helpers;

public static class TestAttributeBuilder
{
    private const string AttributeName = "data-testid";

    public static Dictionary<string, object?> GetAttributeForBlazor(string? name)
        => string.IsNullOrWhiteSpace(name)
            ? new Dictionary<string, object?>()
            : new Dictionary<string, object?> { { AttributeName, name } };
}