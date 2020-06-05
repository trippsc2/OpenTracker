using OpenTracker.Models.Enums;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models
{
    public class DungeonData
    {
        private readonly Game _game;
        private readonly Location _location;

        public Dictionary<DungeonItemID, DungeonItem> DungeonItems { get; }
        public Dictionary<RequirementNodeID, DungeonNode> RequirementNodes { get; }
        public Dictionary<KeyDoorID, KeyDoor> SmallKeyDoors { get; }
        public Dictionary<KeyDoorID, KeyDoor> BigKeyDoors { get; }
        public List<DungeonItem> BossItems { get; }

        public DungeonData(Game game, Location location)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));
            _location = location ?? throw new ArgumentNullException(nameof(location));

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
                    SmallKeyDoors.Add((KeyDoorID)i, new KeyDoor(_game, this, (KeyDoorID)i));
            }

            if (firstBigKeyDoor.HasValue && lastBigKeyDoor.HasValue)
            {
                for (int i = (int)firstBigKeyDoor.Value; i <= (int)lastBigKeyDoor.Value; i++)
                    BigKeyDoors.Add((KeyDoorID)i, new KeyDoor(_game, this, (KeyDoorID)i));
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
    }
}
