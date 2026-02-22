namespace Validations.Net.Test;

[TestFixture]
public class ValidationContextTests
{
    [Test]
    public void SetValue_WithCallerDictionary_DoesNotMutateCallerDictionary()
    {
        var source = new Dictionary<string, object?> { ["existing"] = 1 };
        var context = new ValidationContext(source);

        context.SetValue("newKey", 2);

        Assert.That(source.ContainsKey("newKey"), Is.False);
        Assert.That(source["existing"], Is.EqualTo(1));
    }

    [Test]
    public void ClearBlackboard_WithCallerDictionary_DoesNotClearCallerDictionary()
    {
        var source = new Dictionary<string, object?> { ["existing"] = 1 };
        var context = new ValidationContext(source);

        context.ClearBlackboard();

        Assert.That(source.ContainsKey("existing"), Is.True);
        Assert.That(source.Count, Is.EqualTo(1));
    }
}
