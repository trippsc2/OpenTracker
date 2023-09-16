using Autofac;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.SequenceBreaks;
using Xunit;

namespace OpenTracker.UnitTests.Models.SequenceBreaks;

public class SequenceBreakTests
{
    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void Ctor_ShouldSetEnabledToExpected(bool expected, bool starting)
    {
        var sut = new SequenceBreak(starting);
            
        Assert.Equal(expected, sut.Enabled);
    }

    [Fact]
    public void Enabled_ShouldRaisePropertyChanged()
    {
        var sut = new SequenceBreak();
            
        Assert.PropertyChanged(sut, nameof(ISequenceBreak.Enabled), () => sut.Enabled = false);
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void Save_ShouldSetSaveDataEnabledToExpected(bool expected, bool enabled)
    {
        var sut = new SequenceBreak {Enabled = enabled};
        var saveData = sut.Save();
            
        Assert.Equal(expected, saveData.Enabled);
    }

    [Fact]
    public void Load_ShouldDoNothing_WhenSaveDataIsNull()
    {
        var sut = new SequenceBreak();
            
        sut.Load(null);
            
        Assert.True(sut.Enabled);
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void Load_ShouldSetEnabledToExpected(bool expected, bool enabled)
    {
        var saveData = new SequenceBreakSaveData {Enabled = enabled};
        var sut = new SequenceBreak();
        sut.Load(saveData);
            
        Assert.Equal(expected, sut.Enabled);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<ISequenceBreak.Factory>();
        var sut = factory();
            
        Assert.NotNull(sut as SequenceBreak);
    }
}