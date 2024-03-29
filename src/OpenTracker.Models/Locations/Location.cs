﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Locations.Map;
using OpenTracker.Models.Markings;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.Sections;
using OpenTracker.Models.Sections.Factories;
using OpenTracker.Models.Sections.Item;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Locations;
using OpenTracker.Models.UndoRedo.Notes;
using OpenTracker.Utils.Autofac;
using ReactiveUI;

namespace OpenTracker.Models.Locations;

/// <summary>
/// This class contains location data.
/// </summary>
[DependencyInjection]
public sealed class Location : ReactiveObject, ILocation
{
    private readonly IMarking.Factory _markingFactory;
    private readonly IClearLocation.Factory _clearLocationFactory;
    private readonly IPinLocation.Factory _pinLocationFactory;
    private readonly IUnpinLocation.Factory _unpinLocationFactory;
    private readonly IAddNote.Factory _addNoteFactory;
    private readonly IRemoveNote.Factory _removeNoteFactory;

    public LocationID ID { get; }
    public string Name { get; }

    public IList<IMapLocation> MapLocations { get; }
    public IList<ISection> Sections { get; }
    public ILocationNoteCollection Notes { get; }

    private AccessibilityLevel _accessibility;
    public AccessibilityLevel Accessibility
    {
        get => _accessibility;
        private set => this.RaiseAndSetIfChanged(ref _accessibility, value);
    }

    private int _accessible;
    public int Accessible
    {
        get => _accessible;
        private set => this.RaiseAndSetIfChanged(ref _accessible, value);
    }

    private int _available;
    public int Available
    {
        get => _available;
        private set => this.RaiseAndSetIfChanged(ref _available, value);
    }

    private int _total;
    public int Total
    {
        get => _total;
        private set => this.RaiseAndSetIfChanged(ref _total, value);
    }

    private bool _isActive;
    public bool IsActive
    {
        get => _isActive;
        private set => this.RaiseAndSetIfChanged(ref _isActive, value);
    }

    private bool _shouldBeDisplayed;
    public bool ShouldBeDisplayed
    {
        get => _shouldBeDisplayed;
        private set => this.RaiseAndSetIfChanged(ref _shouldBeDisplayed, value);
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="mapLocationFactory">
    ///     The <see cref="IMapLocationFactory"/>.
    /// </param>
    /// <param name="sectionFactory">
    ///     The <see cref="ISectionFactory"/>.
    /// </param>
    /// <param name="markingFactory">
    ///     An Autofac factory for creating new <see cref="IMarking"/> objects.
    /// </param>
    /// <param name="addNoteFactory">
    ///     An Autofac factory for creating new <see cref="IAddNote"/> objects.
    /// </param>
    /// <param name="clearLocationFactory">
    ///     An Autofac factory for creating new <see cref="IClearLocation"/> objects.
    /// </param>
    /// <param name="pinLocationFactory">
    ///     An Autofac factory for creating new <see cref="IPinLocation"/> objects.
    /// </param>
    /// <param name="removeNoteFactory">
    ///     An Autofac factory for creating new <see cref="IRemoveNote"/> objects.
    /// </param>
    /// <param name="unpinLocationFactory">
    ///     An Autofac factory for creating new <see cref="IUnpinLocation"/> objects.
    /// </param>
    /// <param name="notes">
    ///     A <see cref="ILocationNoteCollection"/>.
    /// </param>
    /// <param name="id">
    ///     The <see cref="LocationID"/>.
    /// </param>
    /// <param name="name">
    ///     A <see cref="string"/> representing the location name.
    /// </param>
    public Location(
        IMapLocationFactory mapLocationFactory, ISectionFactory sectionFactory,
        IMarking.Factory markingFactory, IClearLocation.Factory clearLocationFactory,
        IPinLocation.Factory pinLocationFactory, IUnpinLocation.Factory unpinLocationFactory,
        IAddNote.Factory addNoteFactory, IRemoveNote.Factory removeNoteFactory, ILocationNoteCollection notes,
        LocationID id, string name)
    {
        _markingFactory = markingFactory;
        _clearLocationFactory = clearLocationFactory;
        _pinLocationFactory = pinLocationFactory;
        _unpinLocationFactory = unpinLocationFactory;
        _addNoteFactory = addNoteFactory;
        _removeNoteFactory = removeNoteFactory;

        ID = id;
        Name = name;
            
        MapLocations = mapLocationFactory.GetMapLocations(this);
        Sections = sectionFactory.GetSections(ID);
        Notes = notes;

        foreach (ISection section in Sections)
        {
            section.PropertyChanged += OnSectionChanged;
        }

        UpdateAccessibility();
        UpdateAccessible();
        UpdateAvailable();
        UpdateTotal();
        UpdateIsActive();
        UpdateShouldBeDisplayed();
    }

    public bool CanBeCleared(bool force)
    {
        return Sections.Any(section => section.CanBeCleared(force));
    }

    public IUndoable CreateAddNoteAction()
    {
        return _addNoteFactory(this);
    }

    public IUndoable CreateRemoveNoteAction(IMarking note)
    {
        return _removeNoteFactory(note, this);
    }

    public IUndoable CreateClearLocationAction(bool force = false)
    {
        return _clearLocationFactory(this, force);
    }

    public IUndoable CreatePinLocationAction()
    {
        return _pinLocationFactory(this);
    }

    public IUndoable CreateUnpinLocationAction()
    {
        return _unpinLocationFactory(this);
    }

    public void Reset()
    {
        foreach (ISection section in Sections)
        {
            section.Reset();
        }
    }
        
    public LocationSaveData Save()
    {
        IList<SectionSaveData> sections = Sections.Select(section => section.Save()).ToList();
        IList<MarkType?> markings = Notes.Select(marking => marking.Mark).Select(
            mark => (MarkType?) mark).ToList();

        return new LocationSaveData()
        {
            Sections = sections,
            Markings = markings
        };
    }

    public void Load(LocationSaveData? saveData)
    {
        if (saveData is null)
        {
            return;
        }

        Notes.Clear();
        LoadSections(saveData.Sections);
        LoadMarkings(saveData.Markings);
    }

    /// <summary>
    /// Loads the section save data.
    /// </summary>
    /// <param name="sectionSaveData">
    ///     The section save data.
    /// </param>
    private void LoadSections(IList<SectionSaveData>? sectionSaveData)
    {
        if (sectionSaveData is null)
        {
            return;
        }
            
        for (var i = 0; i < sectionSaveData.Count; i++)
        {
            Sections[i].Load(sectionSaveData[i]);
        }
    }

    /// <summary>
    /// Loads the marking save data.
    /// </summary>
    /// <param name="noteSaveData">
    ///     The note save data.
    /// </param>
    private void LoadMarkings(IList<MarkType?>? noteSaveData)
    {
        if (noteSaveData is null)
        {
            return;
        }

        foreach (var marking in noteSaveData)
        {
            var newMarking = _markingFactory();

            newMarking.Mark = marking ?? MarkType.Unknown;

            Notes.Add(newMarking);
        }
    }

    /// <summary>
    /// Subscribes to the <see cref="ISection.PropertyChanged"/> event.
    /// </summary>
    /// <param name="sender">
    ///     The <see cref="object"/> from which the event is sent.
    /// </param>
    /// <param name="e">
    ///     The <see cref="PropertyChangedEventArgs"/>.
    /// </param>
    private void OnSectionChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(ISection.Accessibility):
                UpdateAccessibility();
                break;
            case nameof(ISection.Available):
                UpdateAccessibility();
                UpdateAvailable();
                break;
            case nameof(IItemSection.Accessible):
                UpdateAccessible();
                break;
            case nameof(ISection.Total):
                UpdateTotal();
                break;
            case nameof(ISection.IsActive):
                UpdateIsActive();
                UpdateAccessibility();
                break;
            case nameof(ISection.ShouldBeDisplayed):
                UpdateShouldBeDisplayed();
                break;
        }
    }

    /// <summary>
    /// Updates the value of the <see cref="Accessibility"/> property.
    /// </summary>
    private void UpdateAccessibility()
    {
        var leastAccessible = AccessibilityLevel.Normal;
        var mostAccessible = AccessibilityLevel.None;

        var available = false;

        foreach (var section in Sections)
        {
            if (!section.IsActive || !section.IsAvailable())
            {
                continue;
            }
                
            available = true;
            var sectionAccessibility = section.Accessibility;

            if (leastAccessible > sectionAccessibility)
            {
                leastAccessible = sectionAccessibility;
            }

            if (mostAccessible < sectionAccessibility)
            {
                mostAccessible = sectionAccessibility;
            }
        }

        if (!available)
        {
            Accessibility = AccessibilityLevel.Cleared;
            return;
        }

        Accessibility = mostAccessible switch
        {
            AccessibilityLevel.None => AccessibilityLevel.None,
            AccessibilityLevel.Inspect => AccessibilityLevel.Inspect,
            AccessibilityLevel.Partial => AccessibilityLevel.Partial,
            AccessibilityLevel.SequenceBreak when leastAccessible <= AccessibilityLevel.Partial =>
                AccessibilityLevel.Partial,
            AccessibilityLevel.SequenceBreak => AccessibilityLevel.SequenceBreak,
            AccessibilityLevel.Normal when leastAccessible <= AccessibilityLevel.Partial =>
                AccessibilityLevel.Partial,
            AccessibilityLevel.Normal when leastAccessible == AccessibilityLevel.SequenceBreak =>
                AccessibilityLevel.SequenceBreak,
            AccessibilityLevel.Normal => AccessibilityLevel.Normal,
            _ => throw new Exception(string.Format(
                CultureInfo.InvariantCulture, "Unknown availability state for location {0}", ID.ToString())),
        };
    }

    /// <summary>
    /// Updates the value of the <see cref="Accessible"/> property.
    /// </summary>
    private void UpdateAccessible()
    {
        var accessible = 0;

        foreach (ISection section in Sections)
        {
            if (section is IItemSection itemSection)
            {
                accessible += itemSection.Accessible;
            }
        }

        Accessible = accessible;
    }

    /// <summary>
    /// Updates the value of the <see cref="Available"/> property.
    /// </summary>
    private void UpdateAvailable()
    {
        var available = 0;

        foreach (var section in Sections)
        {
            if (section is IItemSection itemSection)
            {
                available += itemSection.Available;
            }
        }

        Available = available;
    }

    /// <summary>
    /// Updates the value of the <see cref="Total"/> property.
    /// </summary>
    private void UpdateTotal()
    {
        var total = 0;

        foreach (var section in Sections)
        {
            if (section is IItemSection itemSection)
            {
                total += itemSection.Total;
            }
        }

        Total = total;
    }

    /// <summary>
    /// Updates the value of the <see cref="IsActive"/> property.
    /// </summary>
    private void UpdateIsActive()
    {
        IsActive = Sections.Any(section => section.IsActive);
    }

    /// <summary>
    /// Updates the value of the <see cref="ShouldBeDisplayed"/> property.
    /// </summary>
    private void UpdateShouldBeDisplayed()
    {
        ShouldBeDisplayed = Sections.Any(section => section.ShouldBeDisplayed);
    }
}