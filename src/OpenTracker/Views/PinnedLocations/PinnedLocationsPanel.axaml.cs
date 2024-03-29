﻿using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.PinnedLocations;

namespace OpenTracker.Views.PinnedLocations;

public sealed class PinnedLocationsPanel : ReactiveUserControl<PinnedLocationsPanelVM>
{
    public PinnedLocationsPanel()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}