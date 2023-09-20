using System;
using System.Collections.Generic;
using System.Linq;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Sections;
using OpenTracker.Models.Sections.Entrance;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.ViewModels.Markings;

/// <summary>
/// This is the class for creating marking select control ViewModel classes.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class MarkingSelectFactory : IMarkingSelectFactory
{
    private readonly MarkingSelectButtonVM.Factory _buttonFactory;
    private readonly IMarkingSelectVM.Factory _selectFactory;
    private readonly INoteMarkingSelectVM.Factory _noteSelectFactory;

    public MarkingSelectFactory(
        MarkingSelectButtonVM.Factory buttonFactory,
        IMarkingSelectVM.Factory selectFactory,
        INoteMarkingSelectVM.Factory noteSelectFactory)
    {
        _buttonFactory = buttonFactory;
        _selectFactory = selectFactory;
        _noteSelectFactory = noteSelectFactory;
    }

    /// <summary>
    /// Populates the observable collection of marking select button control ViewModel
    /// instances for non-entrance markings.
    /// </summary>
    /// <param name="marking"></param>
    private List<IMarkingSelectItemVMBase> GetNonEntranceMarkingSelect(IMarking marking)
    {
        var markingSelectButtons = new List<IMarkingSelectItemVMBase>();
        
        foreach (var markType in Enum.GetValues<MarkType>())
        {
            switch (markType)
            {
                case MarkType.Sword:
                case MarkType.Shield:
                case MarkType.Mail:
                case MarkType.Boots:
                case MarkType.Gloves:
                case MarkType.Flippers:
                case MarkType.MoonPearl:
                case MarkType.Bow:
                case MarkType.SilverArrows:
                case MarkType.Boomerang:
                case MarkType.RedBoomerang:
                case MarkType.Hookshot:
                case MarkType.Bomb:
                case MarkType.Mushroom:
                case MarkType.FireRod:
                case MarkType.IceRod:
                case MarkType.Bombos:
                case MarkType.Ether:
                case MarkType.Quake:
                case MarkType.TriforcePiece:
                case MarkType.Powder:
                case MarkType.Lamp:
                case MarkType.Hammer:
                case MarkType.Flute:
                case MarkType.Net:
                case MarkType.Book:
                case MarkType.Shovel:
                case MarkType.SmallKey:
                case MarkType.Bottle:
                case MarkType.CaneOfSomaria:
                case MarkType.CaneOfByrna:
                case MarkType.Cape:
                case MarkType.Mirror:
                case MarkType.HalfMagic:
                case MarkType.BigKey:
                    markingSelectButtons.Add(_buttonFactory(marking, markType));
                    break;
                case MarkType.Unknown:
                case MarkType.HCLeft:
                case MarkType.HCFront:
                case MarkType.HCRight:
                case MarkType.EP:
                case MarkType.DPLeft:
                case MarkType.DPFront:
                case MarkType.DPRight:
                case MarkType.DPBack:
                case MarkType.ToH:
                case MarkType.PoD:
                case MarkType.Aga:
                case MarkType.SP:
                case MarkType.SW:
                case MarkType.TT:
                case MarkType.IP:
                case MarkType.MM:
                case MarkType.TRFront:
                case MarkType.TRLeft:
                case MarkType.TRRight:
                case MarkType.TRBack:
                case MarkType.GT:
                case MarkType.Ganon:
                case MarkType.Sanctuary:
                case MarkType.OldWoman:
                case MarkType.Brothers:
                case MarkType.OldManFront:
                case MarkType.OldManBack:
                case MarkType.SpectacleRockTop:
                case MarkType.SpectacleRockBottomLeft:
                case MarkType.SpectacleRockBottomRight:
                case MarkType.ParadoxCaveTop:
                case MarkType.ParadoxCaveBottomLeft:
                case MarkType.ParadoxCaveBottomRight:
                case MarkType.SpiralCaveTop:
                case MarkType.SpiralCaveBottom:
                case MarkType.FairyAscensionTop:
                case MarkType.FairyAscensionBottom:
                case MarkType.SickKid:
                case MarkType.Library:
                case MarkType.Sahasrahla:
                case MarkType.PotionShop:
                case MarkType.MagicBat:
                case MarkType.Blacksmith:
                case MarkType.BigBomb:
                case MarkType.SpikeCave:
                case MarkType.MimicCave:
                case MarkType.Dam:
                case MarkType.MountainCave:
                case MarkType.BumperCave:
                case MarkType.SuperBunnyCaveBottom:
                case MarkType.SuperBunnyCaveTop:
                default:
                    break;
            }
        }

        return markingSelectButtons;
    }

    /// <summary>
    /// Populates the observable collection of marking select button control ViewModel
    /// instances for entrance markings.
    /// </summary>
    /// <param name="marking"></param>
    private List<IMarkingSelectItemVMBase> GetEntranceMarkingSelect(IMarking marking)
    {
        return Enum.GetValues<MarkType>()
            .Select(markType => _buttonFactory(marking, markType))
            .Cast<IMarkingSelectItemVMBase>()
            .ToList();
    }

    /// <summary>
    /// Returns a new marking select popup control ViewModel instance for the specified
    /// marking.
    /// </summary>
    /// <param name="marking">
    /// The marking.
    /// </param>
    /// <returns>
    /// A new marking select popup control ViewModel instance.
    /// </returns>
    private IMarkingSelectVM GetNonEntranceMarkingSelectPopupVM(IMarking marking)
    {
        return _selectFactory(marking, GetNonEntranceMarkingSelect(marking), 238.0, 200.0);
    }

    /// <summary>
    /// Returns a new marking select popup control ViewModel instance for the specified
    /// marking.
    /// </summary>
    /// <param name="marking">
    /// The marking.
    /// </param>
    /// <returns>
    /// A new marking select popup control ViewModel instance.
    /// </returns>
    private IMarkingSelectVM GetEntranceMarkingSelectPopupVM(IMarking marking)
    {
        return _selectFactory(marking, GetEntranceMarkingSelect(marking), 374.0, 320.0);
    }

    /// <summary>
    /// Returns a new marking select popup control ViewModel instance for the specified
    /// section.
    /// </summary>
    /// <param name="section">
    /// The section.
    /// </param>
    /// <returns>
    /// A new marking select popup control ViewModel instance.
    /// </returns>
    public IMarkingSelectVM GetMarkingSelectVM(ISection section)
    {
        if (section is IEntranceSection)
        {
            return GetEntranceMarkingSelectPopupVM(section.Marking!);
        }

        return GetNonEntranceMarkingSelectPopupVM(section.Marking!);
    }

    /// <summary>
    /// Returns a new marking select popup control ViewModel instance.
    /// </summary>
    /// <param name="marking">
    /// The marking.
    /// </param>
    /// <param name="location">
    /// The location.
    /// </param>
    /// <returns>
    /// A new marking select popup control ViewModel instance.
    /// </returns>
    public INoteMarkingSelectVM GetNoteMarkingSelectVM(IMarking marking, ILocation location)
    {
        return _noteSelectFactory(marking, GetNonEntranceMarkingSelect(marking), location);
    }
}