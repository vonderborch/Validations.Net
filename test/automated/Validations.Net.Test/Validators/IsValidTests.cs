using Validations.Net.OLD;
using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsValidTests
{
    #region Test Models

    private class ValidPerson
    {
        [ValidateIsNotNull]
        public string Name { get; set; } = "John";

        [ValidateIsNotNull]
        public string Email { get; set; } = "john@example.com";
    }

    private class InvalidPerson
    {
        [ValidateIsNotNull]
        public string? Name { get; set; }

        [ValidateIsNotNull]
        public string? Email { get; set; }
    }

    private class NestedModel
    {
        [ValidateIsNotNull]
        public string Title { get; set; } = "Test";

        [ValidateNested]
        [ValidateIsNotNull]
        public ValidPerson? Person { get; set; } = new();
    }

    private class CollectionModel
    {
        [ValidateEachIsValid]
        [ValidateIsNotNull]
        public List<ValidPerson>? Items { get; set; } = new();
    }

    private class MixedValidityModel
    {
        [ValidateIsNotNull]
        public string? RequiredField { get; set; } = "present";

        [ValidateIsNull]
        public string? MustBeNull { get; set; }
    }

    private class ThrowingGetterModel
    {
        [ValidateIsNotNull]
        public string DangerousValue => throw new InvalidOperationException("Boom");
    }

    #endregion

    #region CheckIsValid Tests

    [Test]
    public void CheckIsValid_WithNullValue_ReturnsFalse()
    {
        ValidPerson? person = null;
        var result = person.CheckIsValid();
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsValid_WithValidObject_ReturnsTrue()
    {
        var person = new ValidPerson { Name = "John", Email = "john@example.com" };
        var result = person.CheckIsValid();
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsValid_WithInvalidObject_ReturnsFalse()
    {
        var person = new InvalidPerson { Name = null, Email = null };
        var result = person.CheckIsValid();
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsValid_WithPartiallyInvalidObject_ReturnsFalse()
    {
        var person = new InvalidPerson { Name = "John", Email = null };
        var result = person.CheckIsValid();
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsValid_WithMixedValidityAllValid_ReturnsTrue()
    {
        var model = new MixedValidityModel { RequiredField = "present", MustBeNull = null };
        var result = model.CheckIsValid();
        Assert.That(result, Is.True);
    }

    #endregion

    #region ValidateIsValid Tests

    [Test]
    public void ValidateIsValid_WithValidObject_ReturnsIsValid()
    {
        var person = new ValidPerson { Name = "John", Email = "john@example.com" };
        var result = person.ValidateIsValid();
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.Failures, Is.Empty);
    }

    [Test]
    public void ValidateIsValid_WithNullValue_ReturnsFailure()
    {
        ValidPerson? person = null;
        var result = person.ValidateIsValid();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures, Has.Count.GreaterThan(0));
    }

    [Test]
    public void ValidateIsValid_WithInvalidObject_ReturnsAllFailures()
    {
        var person = new InvalidPerson { Name = null, Email = null };
        var result = person.ValidateIsValid();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures, Has.Count.EqualTo(2));
    }

    [Test]
    public void ValidateIsValid_WithPartiallyInvalid_ReturnsOnlyFailedMembers()
    {
        var person = new InvalidPerson { Name = "John", Email = null };
        var result = person.ValidateIsValid();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures, Has.Count.EqualTo(1));
        Assert.That(result.Failures[0].MemberPath, Is.EqualTo("Email"));
    }

    [Test]
    public void ValidateIsValid_ToDictionary_MapsPathsToMessages()
    {
        var person = new InvalidPerson { Name = null, Email = null };
        var result = person.ValidateIsValid();
        var dict = result.ToDictionary();
        Assert.That(dict, Contains.Key("Name"));
        Assert.That(dict, Contains.Key("Email"));
    }

    [Test]
    public void ValidateIsValid_WithThrowingGetter_ReportsFailureOnMember()
    {
        var model = new ThrowingGetterModel();
        var result = model.ValidateIsValid();

        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures.Any(f => f.MemberPath == "DangerousValue"), Is.True);
    }

    #endregion

    #region EnsureIsValid Tests

    [Test]
    public void EnsureIsValid_WithValidObject_DoesNotThrow()
    {
        var person = new ValidPerson { Name = "John", Email = "john@example.com" };
        Assert.DoesNotThrow(() => person.EnsureIsValid());
    }

    [Test]
    public void EnsureIsValid_WithValidObject_ReturnsValue()
    {
        var person = new ValidPerson { Name = "John", Email = "john@example.com" };
        var returned = person.EnsureIsValid();
        Assert.That(returned, Is.SameAs(person));
    }

    [Test]
    public void EnsureIsValid_WithInvalidObject_ThrowsValidationException()
    {
        var person = new InvalidPerson { Name = null, Email = null };
        Assert.Throws<ValidationException>(() => person.EnsureIsValid());
    }

    [Test]
    public void EnsureIsValid_WithNullValue_ThrowsValidationException()
    {
        ValidPerson? person = null;
        Assert.Throws<ValidationException>(() => person.EnsureIsValid());
    }

    #endregion

    #region Nested Validation Tests

    [Test]
    public void ValidateIsValid_WithValidNestedObject_Succeeds()
    {
        var model = new NestedModel
        {
            Title = "Test",
            Person = new ValidPerson { Name = "John", Email = "john@example.com" }
        };
        var result = model.ValidateIsValid();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsValid_WithInvalidNestedMember_ReportsNestedPath()
    {
        var model = new NestedModel
        {
            Title = "Test",
            Person = new ValidPerson { Name = null!, Email = "john@example.com" }
        };
        var result = model.ValidateIsValid();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures.Any(f => f.MemberPath == "Person.Name"), Is.True);
    }

    [Test]
    public void ValidateIsValid_WithNullNestedObject_ReportsOnMember()
    {
        var model = new NestedModel { Title = "Test", Person = null };
        var result = model.ValidateIsValid();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures.Any(f => f.MemberPath == "Person"), Is.True);
    }

    #endregion

    #region Collection Validation Tests

    [Test]
    public void ValidateIsValid_WithValidCollection_Succeeds()
    {
        var model = new CollectionModel
        {
            Items = new List<ValidPerson>
            {
                new() { Name = "Alice", Email = "alice@example.com" },
                new() { Name = "Bob", Email = "bob@example.com" }
            }
        };
        var result = model.ValidateIsValid();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsValid_WithInvalidCollectionItem_ReportsIndexedPath()
    {
        var model = new CollectionModel
        {
            Items = new List<ValidPerson>
            {
                new() { Name = "Alice", Email = "alice@example.com" },
                new() { Name = null!, Email = "bob@example.com" }
            }
        };
        var result = model.ValidateIsValid();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Failures.Any(f => f.MemberPath == "Items[1].Name"), Is.True);
    }

    #endregion
}
