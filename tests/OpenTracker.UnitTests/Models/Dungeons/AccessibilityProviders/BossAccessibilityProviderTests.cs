using Autofac;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Dungeons.AccessibilityProvider;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.AccessibilityProviders;

public class BossAccessibilityProviderTests
{
    private readonly BossAccessibilityProvider _sut = new();

    [Theory]
    [InlineData(AccessibilityLevel.None, AccessibilityLevel.None)]
    [InlineData(AccessibilityLevel.Inspect, AccessibilityLevel.Inspect)]
    [InlineData(AccessibilityLevel.Partial, AccessibilityLevel.Partial)]
    [InlineData(AccessibilityLevel.SequenceBreak, AccessibilityLevel.SequenceBreak)]
    [InlineData(AccessibilityLevel.Normal, AccessibilityLevel.Normal)]
    [InlineData(AccessibilityLevel.Cleared, AccessibilityLevel.Cleared)]
    public void Accessibility_ShouldMatchExpected(AccessibilityLevel expected, AccessibilityLevel set)
    {
        _sut.Accessibility = set;
            
        Assert.Equal(expected, _sut.Accessibility);
    }

    [Fact]
    public void Accessibility_ShouldRaisePropertyChanged()
    {
        _sut.Accessibility = AccessibilityLevel.None;
            
        Assert.PropertyChanged(_sut, nameof(IBossAccessibilityProvider.Accessibility), () => 
            _sut.Accessibility = AccessibilityLevel.Inspect);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IBossAccessibilityProvider.Factory>();
        var sut = factory();
            
        Assert.NotNull(sut as BossAccessibilityProvider);
    }
}