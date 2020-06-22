using OpenTracker.Models.Enums;
using OpenTracker.Models.Sections;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models
{
    /// <summary>
    /// This is the dungeon data class that contains all mutable data
    /// </summary>
    public class DungeonData
    {
        private readonly Game _game;
        private readonly Location _location;
        private readonly DungeonItemSection _dungeonItemSection;

        public Dictionary<DungeonItemID, DungeonItem> DungeonItems { get; }
        public Dictionary<RequirementNodeID, DungeonNode> RequirementNodes { get; }
        public Dictionary<KeyDoorID, KeyDoor> SmallKeyDoors { get; }
        public Dictionary<KeyDoorID, KeyDoor> BigKeyDoors { get; }
        public List<DungeonItem> BossItems { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">The game data parent class.</param>
        /// <param name="location">The location data parent class.</param>
        /// <param name="dungeonItemSection">The dungeon item section to which this data belongs.</param>
        public DungeonData(Game game, Location location, DungeonItemSection dungeonItemSection)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));
            _location = location ?? throw new ArgumentNullException(nameof(location));
            _dungeonItemSection = dungeonItemSection ??
                throw new ArgumentNullException(nameof(dungeonItemSection));

            DungeonItems = new Dictionary<DungeonItemID, DungeonItem>();
            RequirementNodes = new Dictionary<RequirementNodeID, DungeonNode>();
            SmallKeyDoors = new Dictionary<KeyDoorID, KeyDoor>();
            BigKeyDoors = new Dictionary<KeyDoorID, KeyDoor>();
            BossItems = new List<DungeonItem>();

            DungeonItemID firstItem = DungeonItemID.HCSanctuary;
            DungeonItemID lastItem = DungeonItemID.HCSanctuary;
            RequirementNodeID firstNode = RequirementNodeID.Start;
            RequirementNodeID lastNode = RequirementNodeID.Start;
            KeyDoorID? firstSmallKeyDoor = null;
            KeyDoorID? lastSmallKeyDoor = null;
            KeyDoorID? firstBigKeyDoor = null;
            KeyDoorID? lastBigKeyDoor = null;
            DungeonItemID? firstBossItem = null;
            DungeonItemID? lastBossItem = null;

            switch (_location.ID)
            {
                case LocationID.HyruleCastle:
                    {
                        firstItem = DungeonItemID.HCSanctuary;
                        lastItem = DungeonItemID.HCSecretRoomRight;
                        firstSmallKeyDoor = KeyDoorID.HCEscapeFirstKeyDoor;
                        lastSmallKeyDoor = KeyDoorID.HCSewerRatRoomKeyDoor;
                        firstNode = RequirementNodeID.HCSanctuary;
                        lastNode = RequirementNodeID.HCBack;
                    }
                    break;
                case LocationID.AgahnimTower:
                    {
                        firstItem = DungeonItemID.ATRoom03;
                        lastItem = DungeonItemID.ATDarkMaze;
                        firstSmallKeyDoor = KeyDoorID.ATFirstKeyDoor;
                        lastSmallKeyDoor = KeyDoorID.ATFourthKeyDoor;
                        firstNode = RequirementNodeID.AT;
                        lastNode = RequirementNodeID.ATBoss;
                        firstBossItem = DungeonItemID.ATBoss;
                        lastBossItem = DungeonItemID.ATBoss;
                    }
                    break;
                case LocationID.EasternPalace:
                    {
                        firstItem = DungeonItemID.EPCannonballChest;
                        lastItem = DungeonItemID.EPBoss;
                        firstSmallKeyDoor = KeyDoorID.EPRightWingKeyDoor;
                        lastSmallKeyDoor = KeyDoorID.EPBossKeyDoor;
                        firstBigKeyDoor = KeyDoorID.EPBigChest;
                        lastBigKeyDoor = KeyDoorID.EPBigKeyDoor;
                        firstNode = RequirementNodeID.EP;
                        lastNode = RequirementNodeID.EPBoss;
                        firstBossItem = DungeonItemID.EPBoss;
                        lastBossItem = DungeonItemID.EPBoss;
                    }
                    break;
                case LocationID.DesertPalace:
                    {
                        firstItem = DungeonItemID.DPMapChest;
                        lastItem = DungeonItemID.DPBoss;
                        firstNode = RequirementNodeID.DPFront;
                        lastNode = RequirementNodeID.DPBoss;
                        firstSmallKeyDoor = KeyDoorID.DPRightWingKeyDoor;
                        lastSmallKeyDoor = KeyDoorID.DP2FSecondKeyDoor;
                        firstBigKeyDoor = KeyDoorID.DPBigChest;
                        lastBigKeyDoor = KeyDoorID.DPBigKeyDoor;
                        firstBossItem = DungeonItemID.DPBoss;
                        lastBossItem = DungeonItemID.DPBoss;
                    }
                    break;
                case LocationID.TowerOfHera:
                    {
                        firstItem = DungeonItemID.ToHBasementCage;
                        lastItem = DungeonItemID.ToHBoss;
                        firstNode = RequirementNodeID.ToH;
                        lastNode = RequirementNodeID.ToHBoss;
                        firstSmallKeyDoor = KeyDoorID.ToHKeyDoor;
                        lastSmallKeyDoor = KeyDoorID.ToHKeyDoor;
                        firstBigKeyDoor = KeyDoorID.ToHBigKeyDoor;
                        lastBigKeyDoor = KeyDoorID.ToHBigChest;
                        firstBossItem = DungeonItemID.ToHBoss;
                        lastBossItem = DungeonItemID.ToHBoss;
                    }
                    break;
                case LocationID.PalaceOfDarkness:
                    {
                        firstItem = DungeonItemID.PoDShooterRoom;
                        lastItem = DungeonItemID.PoDBoss;
                        firstNode = RequirementNodeID.PoD;
                        lastNode = RequirementNodeID.PoDBoss;
                        firstSmallKeyDoor = KeyDoorID.PoDFrontKeyDoor;
                        lastSmallKeyDoor = KeyDoorID.PoDBossAreaKeyDoor;
                        firstBigKeyDoor = KeyDoorID.PoDBigChest;
                        lastBigKeyDoor = KeyDoorID.PoDBigKeyDoor;
                        firstBossItem = DungeonItemID.PoDBoss;
                        lastBossItem = DungeonItemID.PoDBoss;
                    }
                    break;
                case LocationID.SwampPalace:
                    {
                        firstItem = DungeonItemID.SPEntrance;
                        lastItem = DungeonItemID.SPBoss;
                        firstNode = RequirementNodeID.SP;
                        lastNode = RequirementNodeID.SPBoss;
                        firstSmallKeyDoor = KeyDoorID.SP1FKeyDoor;
                        lastSmallKeyDoor = KeyDoorID.SPBossRoomKeyDoor;
                        firstBigKeyDoor = KeyDoorID.SPBigChest;
                        lastBigKeyDoor = KeyDoorID.SPBigChest;
                        firstBossItem = DungeonItemID.SPBoss;
                        lastBossItem = DungeonItemID.SPBoss;
                    }
                    break;
                case LocationID.SkullWoods:
                    {
                        firstItem = DungeonItemID.SWBigKeyChest;
                        lastItem = DungeonItemID.SWBoss;
                        firstNode = RequirementNodeID.SWBigChestAreaBottom;
                        lastNode = RequirementNodeID.SWBoss;
                        firstSmallKeyDoor = KeyDoorID.SWFrontLeftKeyDoor;
                        lastSmallKeyDoor = KeyDoorID.SWBackSeceondKeyDoor;
                        firstBigKeyDoor = KeyDoorID.SWBigChest;
                        lastBigKeyDoor = KeyDoorID.SWBigChest;
                        firstBossItem = DungeonItemID.SWBoss;
                        lastBossItem = DungeonItemID.SWBoss;
                    }
                    break;
                case LocationID.ThievesTown:
                    {
                        firstItem = DungeonItemID.TTMapChest;
                        lastItem = DungeonItemID.TTBoss;
                        firstNode = RequirementNodeID.TT;
                        lastNode = RequirementNodeID.TTBoss;
                        firstSmallKeyDoor = KeyDoorID.TTFirstKeyDoor;
                        lastSmallKeyDoor = KeyDoorID.TTBigChestKeyDoor;
                        firstBigKeyDoor = KeyDoorID.TTBigKeyDoor;
                        lastBigKeyDoor = KeyDoorID.TTBigChest;
                        firstBossItem = DungeonItemID.TTBoss;
                        lastBossItem = DungeonItemID.TTBoss;
                    }
                    break;
                case LocationID.IcePalace:
                    {
                        firstItem = DungeonItemID.IPCompassChest;
                        lastItem = DungeonItemID.IPBoss;
                        firstNode = RequirementNodeID.IP;
                        lastNode = RequirementNodeID.IPBoss;
                        firstSmallKeyDoor = KeyDoorID.IP1FKeyDoor;
                        lastSmallKeyDoor = KeyDoorID.IPB6KeyDoor;
                        firstBigKeyDoor = KeyDoorID.IPBigKeyDoor;
                        lastBigKeyDoor = KeyDoorID.IPBigChest;
                        firstBossItem = DungeonItemID.IPBoss;
                        lastBossItem = DungeonItemID.IPBoss;
                    }
                    break;
                case LocationID.MiseryMire:
                    {
                        firstItem = DungeonItemID.MMBridgeChest;
                        lastItem = DungeonItemID.MMBoss;
                        firstNode = RequirementNodeID.MM;
                        lastNode = RequirementNodeID.MMBoss;
                        firstSmallKeyDoor = KeyDoorID.MMB1TopRightKeyDoor;
                        lastSmallKeyDoor = KeyDoorID.MMB2WorthlessKeyDoor;
                        firstBigKeyDoor = KeyDoorID.MMBigChest;
                        lastBigKeyDoor = KeyDoorID.MMBossRoomBigKeyDoor;
                        firstBossItem = DungeonItemID.MMBoss;
                        lastBossItem = DungeonItemID.MMBoss;
                    }
                    break;
                case LocationID.TurtleRock:
                    {
                        firstItem = DungeonItemID.TRCompassChest;
                        lastItem = DungeonItemID.TRBoss;
                        firstNode = RequirementNodeID.TRFront;
                        lastNode = RequirementNodeID.TRBoss;
                        firstSmallKeyDoor = KeyDoorID.TR1FFirstKeyDoor;
                        lastSmallKeyDoor = KeyDoorID.TRB2KeyDoor;
                        firstBigKeyDoor = KeyDoorID.TRBigChest;
                        lastBigKeyDoor = KeyDoorID.TRBossRoomBigKeyDoor;
                        firstBossItem = DungeonItemID.TRBoss;
                        lastBossItem = DungeonItemID.TRBoss;
                    }
                    break;
                case LocationID.GanonsTower:
                    {
                        firstItem = DungeonItemID.GTHopeRoomLeft;
                        lastItem = DungeonItemID.GTMoldormChest;
                        firstNode = RequirementNodeID.GT;
                        lastNode = RequirementNodeID.GTFinalBoss;
                        firstSmallKeyDoor = KeyDoorID.GT1FLeftToRightKeyDoor;
                        lastSmallKeyDoor = KeyDoorID.GT6FSecondKeyDoor;
                        firstBigKeyDoor = KeyDoorID.GTBigChest;
                        lastBigKeyDoor = KeyDoorID.GT7FBigKeyDoor;
                        firstBossItem = DungeonItemID.GTBoss1;
                        lastBossItem = DungeonItemID.GTFinalBoss;
                    }
                    break;
            }

            for (int i = (int)firstNode; i <= (int)lastNode; i++)
                RequirementNodes.Add((RequirementNodeID)i, new DungeonNode(_game, this, (RequirementNodeID)i));

            for (int i = (int)firstItem; i <= (int)lastItem; i++)
                DungeonItems.Add((DungeonItemID)i, new DungeonItem(_game, this, (DungeonItemID)i));

            if (firstSmallKeyDoor.HasValue && lastSmallKeyDoor.HasValue)
            {
                for (int i = (int)firstSmallKeyDoor.Value; i <= (int)lastSmallKeyDoor.Value; i++)
                    SmallKeyDoors.Add((KeyDoorID)i, new KeyDoor(this, (KeyDoorID)i));
            }

            if (firstBigKeyDoor.HasValue && lastBigKeyDoor.HasValue)
            {
                for (int i = (int)firstBigKeyDoor.Value; i <= (int)lastBigKeyDoor.Value; i++)
                    BigKeyDoors.Add((KeyDoorID)i, new KeyDoor(this, (KeyDoorID)i));
            }

            if (firstBossItem.HasValue && lastBossItem.HasValue)
            {
                for (int i = (int)firstBossItem.Value; i <= (int)lastBossItem.Value; i++)
                {
                    if (DungeonItems.ContainsKey((DungeonItemID)i))
                        BossItems.Add(DungeonItems[(DungeonItemID)i]);
                    else
                        BossItems.Add(new DungeonItem(_game, this, (DungeonItemID)i));
                }
            }

            foreach (KeyDoor smallKeyDoor in SmallKeyDoors.Values)
                smallKeyDoor.Initialize();

            foreach (KeyDoor bigKeyDoor in BigKeyDoors.Values)
                bigKeyDoor.Initialize();

            foreach (DungeonNode node in RequirementNodes.Values)
                node.Initialize();
        }

        /// <summary>
        /// Sets the state of all small key doors based on a specified list of unlocked doors.
        /// </summary>
        /// <param name="unlockedDoors">
        /// A list of unlocked doors.
        /// </param>
        public void SetSmallKeyDoorState(List<KeyDoorID> unlockedDoors)
        {
            if (unlockedDoors == null)
            {
                throw new ArgumentNullException(nameof(unlockedDoors));
            }

            foreach (KeyDoor smallKeyDoor in SmallKeyDoors.Values)
            {
                if (unlockedDoors.Contains(smallKeyDoor.ID))
                {
                    smallKeyDoor.Unlocked = true;
                }
                else
                {
                    smallKeyDoor.Unlocked = false;
                }
            }
        }

        /// <summary>
        /// Sets the state of all big key doors to a specified state.
        /// </summary>
        /// <param name="unlocked">
        /// A boolean representing whether the doors are to be unlocked.
        /// </param>
        public void SetBigKeyDoorState(bool unlocked)
        {
            foreach (KeyDoor bigKeyDoor in BigKeyDoors.Values)
            {
                bigKeyDoor.Unlocked = unlocked;
            }
        }

        /// <summary>
        /// Returns the number of keys that are available in all dungeon nodes for free.
        /// </summary>
        /// <returns>
        /// A 32-bit integer representing the number of keys that can be collected for free.
        /// </returns>
        public int GetFreeKeys()
        {
            int freeKeys = 0;

            foreach (DungeonNode node in RequirementNodes.Values)
            {
                if (node.Accessibility >= AccessibilityLevel.SequenceBreak)
                {
                    freeKeys += node.FreeKeysProvided;
                }
            }

            return freeKeys;
        }

        /// <summary>
        /// Returns a list of locked key doors that are accessible.
        /// </summary>
        /// <returns>
        /// A list of locked key doors that are accessible.
        /// </returns>
        public List<KeyDoorID> GetAccessibleKeyDoors()
        {
            List<KeyDoorID> accessibleKeyDoors = new List<KeyDoorID>();

            foreach (KeyDoor keyDoor in SmallKeyDoors.Values)
            {
                if (keyDoor.Accessibility >= AccessibilityLevel.SequenceBreak && !keyDoor.Unlocked)
                {
                    accessibleKeyDoors.Add(keyDoor.ID);
                }
            }

            return accessibleKeyDoors;
        }

        /// <summary>
        /// Returns whether the specified number of collected keys and big key can occur,
        /// based on key logic.
        /// </summary>
        /// <param name="keysCollected">
        /// A 32-bit integer representing the number of small keys collected.
        /// </param>
        /// <param name="bigKeyCollected">
        /// A boolean representing whether the big key is collected.
        /// </param>
        /// <returns>
        /// A boolean representing whether the result can occur.
        /// </returns>
        public bool ValidateSmallKeyLayout(int keysCollected, bool bigKeyCollected)
        {
            if (_dungeonItemSection.SmallKey == 0)
            {
                return true;
            }

            if (_game.Mode.SmallKeyShuffle)
            {
                return true;
            }

            foreach (KeyLayout keyLayout in _dungeonItemSection.KeyLayouts)
            {
                if (!_game.Mode.Validate(keyLayout.ModeRequirement))
                {
                    continue;
                }

                if (_dungeonItemSection.BigKey > 0)
                {
                    if (_game.Mode.BigKeyShuffle)
                    {
                        if (keyLayout.BigKeyLocations.Count > 0)
                        {
                            continue;
                        }
                    }
                    else if (bigKeyCollected)
                    {
                        bool anyBigKeyLocationsAccessible = false;

                        foreach (DungeonItemID iD in keyLayout.BigKeyLocations)
                        {
                            if (DungeonItems[iD].Accessibility >= AccessibilityLevel.SequenceBreak)
                            {
                                anyBigKeyLocationsAccessible = true;
                                break;
                            }
                        }

                        if (!anyBigKeyLocationsAccessible)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        bool allBigKeysAccessible = true;

                        foreach (DungeonItemID iD in keyLayout.BigKeyLocations)
                        {
                            if (DungeonItems[iD].Accessibility < AccessibilityLevel.SequenceBreak)
                            {
                                allBigKeysAccessible = false;
                                break;
                            }
                        }

                        if (allBigKeysAccessible)
                        {
                            continue;
                        }
                    }
                }

                int inaccessibleItems = 0;

                foreach (DungeonItemID item in keyLayout.SmallKeyLocations)
                {
                    if (DungeonItems[item].Accessibility < AccessibilityLevel.SequenceBreak)
                    {
                        inaccessibleItems++;
                    }
                }

                int inaccessibleKeys = Math.Max(0, inaccessibleItems -
                    (keyLayout.SmallKeyLocations.Count - keyLayout.SmallKeyCount));

                if (_dungeonItemSection.SmallKey - keysCollected < inaccessibleKeys)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Returns whether the specified big key collection state can occur, based on key logic.
        /// </summary>
        /// <param name="bigKeyCollected">
        /// A boolean representing whether the big key has been collected.
        /// </param>
        /// <returns>
        /// A boolean representing whether the big key collection state is possible.
        /// </returns>
        public bool ValidateBigKeyPlacement(bool bigKeyCollected)
        {
            if (_dungeonItemSection.BigKey == 0)
            {
                return true;
            }

            if (_game.Mode.BigKeyShuffle)
            {
                return true;
            }

            foreach (BigKeyPlacement placement in _dungeonItemSection.BigKeyPlacements)
            {
                if (!_game.Mode.Validate(placement.ModeRequirement))
                {
                    continue;
                }

                if (bigKeyCollected)
                {
                    foreach (DungeonItemID item in placement.Placements)
                    {
                        if (DungeonItems[item].Accessibility >= AccessibilityLevel.SequenceBreak)
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    foreach (DungeonItemID item in placement.Placements)
                    {
                        if (DungeonItems[item].Accessibility < AccessibilityLevel.SequenceBreak)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Returns a list of the current accessibility of each boss in the dungeon.
        /// </summary>
        /// <returns>
        /// A list of the current accessibility of each boss in the dungeon.
        /// </returns>
        public List<AccessibilityLevel> GetBossAccessibility()
        {
            List<AccessibilityLevel> bossAccessibility = new List<AccessibilityLevel>();

            foreach (DungeonItem bossItem in BossItems)
            {
                bossAccessibility.Add(bossItem.Accessibility);
            }

            return bossAccessibility;
        }

        /// <summary>
        /// Returns the current accessibility and accessible item count based on the specified
        /// number of small keys collected and whether the big key is collected.
        /// </summary>
        /// <param name="smallKeyValue">
        /// A 32-bit integer representing the number of small keys collected.
        /// </param>
        /// <param name="bigKeyValue">
        /// A boolean representing whether the big key is collected.
        /// </param>
        /// <returns>
        /// A tuple of the accessibility of items in the dungeon and a 32-bit integer representing
        /// the number of accessible items in the dungeon.
        /// </returns>
        public (AccessibilityLevel, int) GetItemAccessibility(int smallKeyValue, bool bigKeyValue)
        {
            int inaccessibleItems = 0;
            bool sequenceBreak = false;

            foreach (DungeonItem item in DungeonItems.Values)
            {
                switch (item.Accessibility)
                {
                    case AccessibilityLevel.None:
                        {
                            inaccessibleItems++;
                        }
                        break;
                    case AccessibilityLevel.Inspect:
                    case AccessibilityLevel.SequenceBreak:
                        {
                            sequenceBreak = true;
                        }
                        break;
                }
            }

            if (inaccessibleItems <= 0)
            {
                if (sequenceBreak)
                {
                    return (AccessibilityLevel.SequenceBreak, _dungeonItemSection.Available);
                }
                else
                {
                    return (AccessibilityLevel.Normal, _dungeonItemSection.Available);
                }
            }

            if (!_game.Mode.MapCompassShuffle)
            {
                inaccessibleItems -= _dungeonItemSection.MapCompass;
            }

            if (inaccessibleItems <= 0)
            {
                return (AccessibilityLevel.SequenceBreak, _dungeonItemSection.Available);
            }

            if (!_game.Mode.BigKeyShuffle && !bigKeyValue)
            {
                inaccessibleItems -= _dungeonItemSection.BigKey;
            }

            if (inaccessibleItems <= 0)
            {
                return (AccessibilityLevel.SequenceBreak, _dungeonItemSection.Available);
            }

            if (!_game.Mode.SmallKeyShuffle)
            {
                inaccessibleItems -= _dungeonItemSection.SmallKey - smallKeyValue;
            }

            if (inaccessibleItems <= 0)
            {
                return (AccessibilityLevel.SequenceBreak, _dungeonItemSection.Available);
            }

            if (inaccessibleItems >= _dungeonItemSection.Available)
            {
                return (AccessibilityLevel.None, 0);
            }
            else
            {
                return (AccessibilityLevel.Partial, Math.Max(0,
                    _dungeonItemSection.Available - inaccessibleItems));
            }
        }
    }
}
