using System;
using System.ComponentModel;
using OpenTracker.Models.Accessibility;
using ReactiveUI;

namespace OpenTracker.Models.Requirements;

/// <summary>
/// This base class contains non-boolean requirement data.
/// </summary>
public abstract class AccessibilityRequirement : ReactiveObject, IRequirement
{
    public event EventHandler? ChangePropagated;

    private AccessibilityLevel _accessibility;
    public AccessibilityLevel Accessibility
    {
        get => _accessibility;
        private set
        {
            if (_accessibility == value)
            {
                return;
            }
                
            this.RaiseAndSetIfChanged(ref _accessibility, value);
            ChangePropagated?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool Met => Accessibility > AccessibilityLevel.None;
        
    /// <summary>
    /// Constructor
    /// </summary>
    protected AccessibilityRequirement()
    {
        PropertyChanged += OnPropertyChanged;
    }

    /// <summary>
    /// Subscribes to the <see cref="IRequirement.PropertyChanged"/> event on this object.
    /// </summary>
    /// <param name="sender">
    ///     The <see cref="object"/> from which the event is sent.
    /// </param>
    /// <param name="e">
    ///     The <see cref="PropertyChangedEventArgs"/>.
    /// </param>
    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Accessibility))
        {
            this.RaisePropertyChanged(nameof(Met));
        }
    }

    /// <summary>
    /// Returns the current accessibility.
    /// </summary>
    /// <returns>
    ///     The <see cref="AccessibilityLevel"/>.
    /// </returns>
    protected abstract AccessibilityLevel GetAccessibility();

    /// <summary>
    /// Updates the <see cref="Accessibility"/> property value.
    /// </summary>
    protected void UpdateValue()
    {
        Accessibility = GetAccessibility();
    }
}