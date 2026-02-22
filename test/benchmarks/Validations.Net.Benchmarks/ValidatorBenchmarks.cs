using BenchmarkDotNet.Attributes;
using Validations.Net.ValidationAttributes;
using Validations.Net.ValidationSets;
using Validations.Net.Validators;

namespace Validations.Net.Benchmarks;

[MemoryDiagnoser]
[ShortRunJob]
public class ValidatorBenchmarks
{
    private string _nonNullString = "hello";
    private int _positiveInt = 42;
    private int[] _sortedArray = null!;
    private List<int> _listOf100 = null!;
    private DecoratedModel _validModel = null!;
    private DecoratedModel _invalidModel = null!;
    private ValidationSet<DecoratedModel> _validationSet = null!;

    [GlobalSetup]
    public void Setup()
    {
        _sortedArray = Enumerable.Range(0, 100).ToArray();
        _listOf100 = Enumerable.Range(0, 100).ToList();
        _validModel = new DecoratedModel { Name = "John", Email = "john@example.com" };
        _invalidModel = new DecoratedModel { Name = null!, Email = null! };
        _validationSet = ValidationSet.For<DecoratedModel>()
            .AddIsNotNull()
            .AddIsNotNullOrEmpty(m => m.Name)
            .AddIsNotNullOrEmpty(m => m.Email)
            .Build();
    }

    #region Single Validator -- Success Path

    [Benchmark(Baseline = true)]
    public bool Check_IsNotNull_Success() => _nonNullString.CheckIsNotNull();

    [Benchmark]
    public ValidationResult Validate_IsNotNull_Success() => _nonNullString.ValidateIsNotNull();

    [Benchmark]
    public bool Check_IsPositive_Success() => _positiveInt.CheckIsPositive();

    [Benchmark]
    public bool Check_IsInRange_Success() => _positiveInt.CheckIsInRange(0, 100);

    #endregion

    #region Single Validator -- Failure Path

    [Benchmark]
    public ValidationResult Validate_IsNotNull_Failure()
    {
        string? nullStr = null;
        return nullStr.ValidateIsNotNull();
    }

    #endregion

    #region Collection Validators

    [Benchmark]
    public bool Check_IsSorted_100Elements() => _sortedArray.CheckIsSorted();

    [Benchmark]
    public bool Check_DoesContain_InList100() => _listOf100.CheckDoesContain(99);

    [Benchmark]
    public bool Check_IsDistinct_100Elements() => _listOf100.CheckIsDistinct();

    #endregion

    #region Type-Level IsValid

    [Benchmark]
    public bool Check_IsValid_ShallowSuccess() => _validModel.CheckIsValid();

    [Benchmark]
    public bool Check_IsValid_ShallowFailure() => _invalidModel.CheckIsValid();

    [Benchmark]
    public AggregateValidationResult Validate_IsValid_ShallowFailure() => _invalidModel.ValidateIsValid();

    #endregion

    #region ValidationSet

    [Benchmark]
    public bool ValidationSet_Check_Success() => _validationSet.Check(_validModel);

    [Benchmark]
    public AggregateValidationResult ValidationSet_Execute_Success() => _validationSet.Execute(_validModel);

    [Benchmark]
    public AggregateValidationResult ValidationSet_Execute_Failure() => _validationSet.Execute(_invalidModel);

    #endregion

    public class DecoratedModel
    {
        [ValidateIsNotNull]
        public string? Name { get; set; }

        [ValidateIsNotNull]
        public string? Email { get; set; }
    }
}
