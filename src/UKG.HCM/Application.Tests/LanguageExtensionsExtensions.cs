using FluentAssertions.Primitives;
using LanguageExt;

namespace UKG.HCM.Application.Tests;

using static FluentAssertions.AssertionExtensions;

public static class LanguageExtensionsExtensions
{
    public static AndWhichConstraint<ObjectAssertions, TLeft> ShouldBeLeft<TLeft, TRight>(
        this Either<TLeft, TRight> either)
    {
        either.IsLeft.Should().BeTrue();
        return either.Match(_ => throw new Exception("Expected left value"), x => x)
            .Should()
            .BeAssignableTo<TLeft>();
    }
    
    public static AndWhichConstraint<ObjectAssertions, TRight> ShouldBeRight<TLeft, TRight>(
        this Either<TLeft, TRight> either)
    {
        either.IsRight.Should().BeTrue();
        return either.Match(x => x, _ => throw new Exception("Expected right value"))
            .Should()
            .BeOfType<TRight>();
    }
    
    public static AndWhichConstraint<ObjectAssertions, T> ShouldBeSome<T>(
        this Option<T> some)
    {
        some.IsSome.Should().BeTrue();
        return some
            .Match(x => x, () => throw new Exception("Expected Some value"))
            .Should()
            .BeOfType<T>();
    }
    
    public static void ShouldBeNone<T>(
        this Option<T> none)
    {
        none.IsNone.Should().BeTrue();
    }
}