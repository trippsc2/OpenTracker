using OpenTracker.Models.Markings;
using System;

namespace OpenTracker.Models.SaveLoad.Converters
{
    /// <summary>
    /// This is the class for converting saved markings between versions.
    /// </summary>
    public static class MarkingConverter
    {
        /// <summary>
        /// Returns the new value for the specified marking post-1.4.1.
        /// </summary>
        /// <param name="mark">
        /// The saved marking.
        /// </param>
        /// <returns>
        /// The post-1.4.1 marking.
        /// </returns>
        private static MarkType? GetPost141Value(MarkType? mark)
        {
            if (mark.HasValue)
            {
                switch (mark.Value)
                {
                    case MarkType.Unknown:
                    case MarkType.HCRight:
                    case MarkType.EP:
                        {
                            return mark;
                        }
                    case MarkType.HCLeft:
                        {
                            return MarkType.HCFront;
                        }
                    case MarkType.HCFront:
                        {
                            return MarkType.HCLeft;
                        }
                    case MarkType.DPLeft:
                        {
                            return MarkType.ToH;
                        }
                    case MarkType.DPFront:
                        {
                            return MarkType.PoD;
                        }
                    case MarkType.DPRight:
                        {
                            return MarkType.SP;
                        }
                    case MarkType.DPBack:
                        {
                            return MarkType.SW;
                        }
                    case MarkType.ToH:
                        {
                            return MarkType.DPFront;
                        }
                    case MarkType.PoD:
                        {
                            return MarkType.DPLeft;
                        }
                    case MarkType.Aga:
                        {
                            return MarkType.DPRight;
                        }
                    case MarkType.SP:
                        {
                            return MarkType.DPBack;
                        }
                    case MarkType.SW:
                        {
                            return MarkType.TT;
                        }
                    case MarkType.TT:
                        {
                            return MarkType.IP;
                        }
                    case MarkType.IP:
                        {
                            return MarkType.MM;
                        }
                    case MarkType.MM:
                        {
                            return MarkType.TRFront;
                        }
                    case MarkType.TRFront:
                        {
                            return MarkType.Sword;
                        }
                    case MarkType.TRLeft:
                        {
                            return MarkType.Shield;
                        }
                    case MarkType.TRRight:
                        {
                            return MarkType.Mail;
                        }
                    case MarkType.TRBack:
                        {
                            return MarkType.Boots;
                        }
                    case MarkType.GT:
                        {
                            return MarkType.Gloves;
                        }
                    case MarkType.Ganon:
                        {
                            return MarkType.Flippers;
                        }
                    case MarkType.Sanctuary:
                        {
                            return MarkType.MoonPearl;
                        }
                    case MarkType.OldWoman:
                        {
                            return MarkType.TRLeft;
                        }
                    case MarkType.Brothers:
                        {
                            return MarkType.Bow;
                        }
                    case MarkType.OldManFront:
                        {
                            return MarkType.SilverArrows;
                        }
                    case MarkType.OldManBack:
                        {
                            return MarkType.Boomerang;
                        }
                    case MarkType.SpectacleRockTop:
                        {
                            return MarkType.RedBoomerang;
                        }
                    case MarkType.SpectacleRockBottomLeft:
                        {
                            return MarkType.Hookshot;
                        }
                    case MarkType.SpectacleRockBottomRight:
                        {
                            return MarkType.Bomb;
                        }
                    case MarkType.ParadoxCaveTop:
                        {
                            return MarkType.Mushroom;
                        }
                    case MarkType.ParadoxCaveBottomLeft:
                        {
                            return MarkType.TRRight;
                        }
                    case MarkType.ParadoxCaveBottomRight:
                        {
                            return MarkType.FireRod;
                        }
                    case MarkType.Sword:
                        {
                            return MarkType.IceRod;
                        }
                    case MarkType.Shield:
                        {
                            return MarkType.Bombos;
                        }
                    case MarkType.Mail:
                        {
                            return MarkType.Ether;
                        }
                    case MarkType.Boots:
                        {
                            return MarkType.Quake;
                        }
                    case MarkType.Gloves:
                        {
                            return MarkType.TriforcePiece;
                        }
                    case MarkType.Flippers:
                        {
                            return MarkType.Aga;
                        }
                    case MarkType.MoonPearl:
                        {
                            return MarkType.Powder;
                        }
                    case MarkType.SpiralCaveTop:
                        {
                            return MarkType.TRBack;
                        }
                    case MarkType.SpiralCaveBottom:
                        {
                            return MarkType.Lamp;
                        }
                    case MarkType.FairyAscensionTop:
                        {
                            return MarkType.Hammer;
                        }
                    case MarkType.FairyAscensionBottom:
                        {
                            return MarkType.Flute;
                        }
                    case MarkType.Bow:
                        {
                            return MarkType.Net;
                        }
                    case MarkType.SilverArrows:
                        {
                            return MarkType.Book;
                        }
                    case MarkType.Boomerang:
                        {
                            return MarkType.Shovel;
                        }
                    case MarkType.RedBoomerang:
                        {
                            return MarkType.SmallKey;
                        }
                    case MarkType.Hookshot:
                        {
                            return MarkType.GT;
                        }
                    case MarkType.Bomb:
                        {
                            return MarkType.Bottle;
                        }
                    case MarkType.Mushroom:
                        {
                            return MarkType.CaneOfSomaria;
                        }
                    case MarkType.SickKid:
                        {
                            return MarkType.CaneOfByrna;
                        }
                    case MarkType.Library:
                        {
                            return MarkType.Cape;
                        }
                    case MarkType.Sahasrahla:
                        {
                            return MarkType.Mirror;
                        }
                    case MarkType.PotionShop:
                        {
                            return MarkType.HalfMagic;
                        }
                    case MarkType.FireRod:
                        {
                            return MarkType.BigKey;
                        }
                    case MarkType.IceRod:
                        {
                            return MarkType.Ganon;
                        }
                    default:
                        {
                            throw new ArgumentOutOfRangeException(nameof(mark));
                        }
                }
            }

            return null;
        }

        /// <summary>
        /// Converts the section save data to post-1.4.1 values.
        /// </summary>
        /// <param name="saveData">
        /// The location save data.
        /// </param>
        private static void ConvertFrom141(SectionSaveData saveData)
        {
            if (saveData == null)
            {
                throw new ArgumentNullException(nameof(saveData));
            }

            saveData.Marking = GetPost141Value(saveData.Marking);
        }

        /// <summary>
        /// Converts the location save data to post-1.4.1 values.
        /// </summary>
        /// <param name="saveData">
        /// The location save data.
        /// </param>
        public static void ConvertFrom141(LocationSaveData saveData)
        {
            if (saveData == null)
            {
                throw new ArgumentNullException(nameof(saveData));
            }

            for (int i = 0; i < saveData.Markings.Count; i++)
            {
                saveData.Markings[i] = GetPost141Value(saveData.Markings[i]);
            }

            foreach (var section in saveData.Sections)
            {
                ConvertFrom141(section);
            }
        }
    }
}
