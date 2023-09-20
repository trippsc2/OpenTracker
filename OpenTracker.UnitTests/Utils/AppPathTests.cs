using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using OpenTracker.Utils;
using Xunit;

namespace OpenTracker.UnitTests.Utils;

[ExcludeFromCodeCoverage]
public sealed class AppPathTests
{
    [Fact]
    public void AvaloniaLogFilePath_ShouldReturnExpected()
    {
        var expected = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData, Environment.SpecialFolderOption.Create), "OpenTracker",
            "OpenTracker.Avalonia.log");
            
        Assert.Equal(expected, AppPath.AvaloniaLogFilePath);
        Assert.Equal(expected, AppPath.AvaloniaLogFilePath);
    }
        
    [Fact]
    public void AutoTrackingLogFilePath_ShouldReturnExpected()
    {
        var expected = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData, Environment.SpecialFolderOption.Create), "OpenTracker",
            "OpenTracker.AutoTracking.log");
            
        Assert.Equal(expected, AppPath.AutoTrackingLogFilePath);
        Assert.Equal(expected, AppPath.AutoTrackingLogFilePath);
    }
        
    [Fact]
    public void AppDataThemesPath_ShouldReturnExpected()
    {
        var expected = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData, Environment.SpecialFolderOption.Create), "OpenTracker",
            "Themes");
            
        Assert.Equal(expected, AppPath.AppDataThemesPath);
        Assert.Equal(expected, AppPath.AppDataThemesPath);
    }
        
    [Fact]
    public void AppRootThemesPath_ShouldReturnExpected()
    {
        var expected = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!, "Themes");
            
        Assert.Equal(expected, AppPath.AppRootThemesPath);
        Assert.Equal(expected, AppPath.AppRootThemesPath);
    }
        
    [Fact]
    public void LastThemeFilePath_ShouldReturnExpected()
    {
        var expected = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData, Environment.SpecialFolderOption.Create), "OpenTracker",
            "OpenTracker.theme");
            
        Assert.Equal(expected, AppPath.LastThemeFilePath);
        Assert.Equal(expected, AppPath.LastThemeFilePath);
    }
        
    [Fact]
    public void AppSettingsFilePath_ShouldReturnExpected()
    {
        var expected = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData, Environment.SpecialFolderOption.Create), "OpenTracker",
            "OpenTracker.json");
            
        Assert.Equal(expected, AppPath.AppSettingsFilePath);
        Assert.Equal(expected, AppPath.AppSettingsFilePath);
    }
        
    [Fact]
    public void SequenceBreakPath_ShouldReturnExpected()
    {
        var expected = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData, Environment.SpecialFolderOption.Create), "OpenTracker",
            "sequencebreak.json");
            
        Assert.Equal(expected, AppPath.SequenceBreakPath);
        Assert.Equal(expected, AppPath.SequenceBreakPath);
    }
}