using static Seed;
using static Seed.AccessChecker;
using UnityEngine;

public class CheckIndex
{
    #region area logic
    public static bool StartAccess()
    {
        if (AccessCache["StartAccess"] == true)
        {
            return true;
        }
        if (PlayerPrefs.GetInt("StartLogic") == 0 || (TownAccess() && BushCut()))
        {
            AccessCache["StartAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool RuinsWarpBackdoor()
    {
        if (AccessCache["RuinsWarpBackdoor"] == true)
        {
            return true;
        }
        if ((Balloon1() && GetTier("Ruins Warp") > 0) || (!WarpShuffle() && StartWarpAccess()))
        {
            AccessCache["RuinsWarpBackdoor"] = true;
            return true;
        }
        return false;
    }
    public static bool TownAccess()
    {
        if (AccessCache["TownAccess"] == true)
        {
            return true;
        }
        if (BushCut() || PlayerPrefs.GetInt("StartLogic") == 1 || RuinsWarpBackdoor())
		{
            AccessCache["TownAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool WaterWayPointAccess()
    {
        if (AccessCache["WaterWayPointAccess"] == true)
        {
            return true;
        }
        if (TownAccess() && (Charge1() || Balloon1()))
        {
            AccessCache["WaterWayPointAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool Vision()
    {
        return PlayerPrefs.GetInt("LensLogic") == 1 || Lens();
    }
    public static bool AllowDark()
    {
        return PlayerPrefs.GetInt("LanternLogic") == 1 || GetTier("Lantern") > 0;
    }
    public static bool Traction()
    {
        return PlayerPrefs.GetInt("BootsLogic") == 1 || GetTier("Boots") > 1;
    }
    public static bool AllowMajorSecrets()
    {
        return PlayerPrefs.GetInt("SecretLogic") == 0;
    }
    public static bool WarpShuffle()
    {
        return PlayerPrefs.GetInt("WarpShuffle") == 0;
    }
    public static bool Charge1()
    {
        if (AccessCache["Charge1"] == true)
        {
            return true;
        }
        if (GetTier("Weapon Upgrade") > 0 && GetTier("Vitamins") > 0)
        {
            AccessCache["Charge1"] = true;
            return true;
        }
        return false;
    }
    public static bool Charge2()
    {
        if (AccessCache["Charge2"] == true)
        {
            return true;
        }
        if (GetTier("Weapon Upgrade") > 0 && GetTier("Vitamins") > 1)
        {
            AccessCache["Charge2"] = true;
            return true;
        }
        return false;
    }
    public static bool BushCut()
    {
        if (AccessCache["BushCut"] == true)
        {
            return true;
        }
        if (GetTier("Weapon Upgrade") > 0 || Boomerang1())
        {
            AccessCache["BushCut"] = true;
            return true;
        }
        return false;
    }
    public static bool Weapon()
    {
        if (AccessCache["Weapon"] == true)
        {
            return true;
        }
        if (BushCut() || Grapple1())
        {
            AccessCache["Weapon"] = true;
            return true;
        }
        return false;
    }
    public static bool EasyFlight()
    {
        if (AccessCache["EasyFlight"] == true)
        {
            return true;
        }
        if (GetTier("Grappling Hook") > 0 || Balloon1())
        {
            AccessCache["EasyFlight"] = true;
            return true;
        }
        return false;
    }
    public static bool LongFlight()
    {
        if (AccessCache["LongFlight"] == true)
        {
            return true;
        }
        if (GetTier("Grappling Hook") > 1 || Balloon1())
        {
            AccessCache["LongFlight"] = true;
            return true;
        }
        return false;
    }
    public static bool BasicBurn()
    {
        if (AccessCache["BasicBurn"] == true)
        {
            return true;
        }
        if (GetTier("Lantern") > 0 && BushCut())
        {
            AccessCache["BasicBurn"] = true;
            return true;
        }
        return false;
    }
    public static bool BigBurn()
    {
        if (AccessCache["BigBurn"] == true)
        {
            return true;
        }
        if (GetTier("Lantern") > 1 && BushCut())
        {
            AccessCache["BigBurn"] = true;
            return true;
        }
        return false;
    }
    public static bool ShortRange()
    {
        if (AccessCache["ShortRange"] == true)
        {
            return true;
        }
        if (Grapple1() || Boomerang1())
        {
            AccessCache["ShortRange"] = true;
            return true;
        }
        return false;
    }
    public static bool LongRange()
    {
        if (AccessCache["LongRange"] == true)
        {
            return true;
        }
        if (Grapple2() || Boomerang1())
        {
            AccessCache["LongRange"] = true;
            return true;
        }
        return false;
    }
    public static bool BasicShopAccess()
    {
        if (AccessCache["BasicShopAccess"] == true)
        {
            return true;
        }
        if (TownAccess() && Charge1())
        {
            AccessCache["BasicShopAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool FancyShopAccess()
    {
        if (AccessCache["FancyShopAccess"] == true)
        {
            return true;
        }
        if (TownAccess())
        {
            AccessCache["FancyShopAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool GymAccess()
    {
        if (AccessCache["GymAccess"] == true)
        {
            return true;
        }
        if (TownAccess())
        {
            AccessCache["GymAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool LeftDuplexAccess()
    {
        if (AccessCache["LeftDuplexAccess"] == true)
        {
            return true;
        }
        if (TownAccess())
        {
            AccessCache["LeftDuplexAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool TwoFloorHouseAccess()
    {
        if (AccessCache["TwoFloorHouseAccess"] == true)
        {
            return true;
        }
        if (TownAccess())
        {
            AccessCache["TwoFloorHouseAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool TwoFloorHouseBalconyAccess()
    {
        if (AccessCache["TwoFloorHouseBalconyAccess"] == true)
        {
            return true;
        }
        if (TownAccess())
        {
            AccessCache["TwoFloorHouseBalconyAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool BreakoutCaveAccess()
    {
        if (AccessCache["BreakoutCaveAccess"] == true)
        {
            return true;
        }
        if (TownAccess())
        {
            AccessCache["BreakoutCaveAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool AbandonedHouseAccess()
    {
        if (AccessCache["AbandonedHouseAccess"] == true)
        {
            return true;
        }
        if (TownAccess() && Swim1())
        {
            AccessCache["AbandonedHouseAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool PongCaveAccess()
    {
        if (AccessCache["PongCaveAccess"] == true)
        {
            return true;
        }
        if (TownAccess())
        {
            AccessCache["PongCaveAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool MountainSpikeMazeAccess()
    {
        if (AccessCache["MountainSpikeMazeAccess"] == true)
        {
            return true;
        }
        if (MountainLowerAccess() && Charge1() && AllowDark())
        {
            AccessCache["MountainSpikeMazeAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool MountainCaveSpikeMazeAccess()
    {
        if (AccessCache["MountainCaveSpikeMazeAccess"] == true)
        {
            return true;
        }
        if (MountainLowerAccess() && AllowDark() && Charge1())
        {
            AccessCache["MountainCaveSpikeMazeAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool BushCaveAccess()
    {
        if (AccessCache["BushCaveAccess"] == true)
        {
            return true;
        }
        if (TownAccess() && BushCut() && AllowDark())
        {
            AccessCache["BushCaveAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool WaterDungeonAccess()
    {
        if (AccessCache["WaterDungeonAccess"] == true)
        {
            return true;
        }
        if (WaterWayPointAccess() && Charge1())
        {
            AccessCache["WaterDungeonAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool TownDwarfAccess()
    {
        if (AccessCache["TownDwarfAccess"] == true)
        {
            return true;
        }
        if (TownAccess() && Weapon())
        {
            AccessCache["TownDwarfAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool TownCaveAccess()
    {
        if (AccessCache["TownCaveAccess"] == true)
        {
            return true;
        }
        if ((TownAccess() || SprawlingCaveAccess()) && Charge1() && Vision())
        {
            AccessCache["TownCaveAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool SewerAccess()
    {
        if (AccessCache["SewerAccess"] == true)
        {
            return true;
        }
        if (TownAccess() && Swim1() && AllowDark())
        {
            AccessCache["SewerAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool ThreeFireCaveAccess()
    {
        if (AccessCache["ThreeFireCaveAccess"] == true)
        {
            return true;
        }
        if (TownAccess() && Charge1())
        {
            AccessCache["ThreeFireCaveAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool WaterDwarfAccess()
    {
        if (AccessCache["WaterDwarfAccess"] == true)
        {
            return true;
        }
        if (WaterWayPointAccess() && EasyFlight() && Weapon())
        {
            AccessCache["WaterDwarfAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool NorthOfWaterDungeonAccess()
    {
        if (AccessCache["NorthOfWaterDungeonAccess"] == true)
        {
            return true;
        }
        if (TownAccess() && (Charge1() || Balloon1()))
        {
            AccessCache["NorthOfWaterDungeonAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool RabbitCaveAccess()
    {
        if (AccessCache["RabbitCaveAccess"] == true)
        {
            return true;
        }
        if (TownAccess() && BushCut())
        {
            AccessCache["RabbitCaveAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool IceSpikeMazeAccess()
    {
        if (AccessCache["IceSpikeMazeAccess"] == true)
        {
            return true;
        }
        if (TownAccess() && Charge1() && AllowDark() && Traction())
        {
            AccessCache["IceSpikeMazeAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool WindCaveAccess()
    {
        if (AccessCache["WindCaveAccess"] == true)
        {
            return true;
        }
        if (WaterDwarfAccess())
        {
            AccessCache["WindCaveAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool SwampBackDoorAccess()
    {
        if (AccessCache["SwampBackDoorAccess"] == true)
        {
            return true;
        }
        if (BigBurn() && AllowDark())
        {
            AccessCache["SwampBackDoorAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool SwampAccess()
    {
        if (AccessCache["SwampAccess"] == true)
        {
            return true;
        }
        if (TownAccess() && (Swim1() || Balloon1()))
        {
            AccessCache["SwampAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool WaterWayPointCaveAccess()
    {
        if (AccessCache["WaterWayPointCaveAccess"] == true)
        {
            return true;
        }
        if (WaterWayPointAccess() && Charge1() && Vision())
        {
            AccessCache["WaterWayPointCaveAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool GrottoEastOfWaterWayPointAccess()
    {
        if (AccessCache["GrottoEastOfWaterWayPointAccess"] == true)
        {
            return true;
        }
        if (WaterWayPointAccess() && AllowDark())
        {
            AccessCache["GrottoEastOfWaterWayPointAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool NorthOfLakeAccess()
    {
        if (AccessCache["NorthOfLakeAccess"] == true)
        {
            return true;
        }
        if (TownAccess() && (LongFlight() || BasicBurn() || Swim1()))
        {
            AccessCache["NorthOfLakeAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool SwampCaveAccess()
    {
        if (AccessCache["SwampCaveAccess"] == true)
        {
            return true;
        }
        if (TownAccess() && Swim1() && AllowDark())
        {
            AccessCache["SwampCaveAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool DragonCaveAccess()
    {
        if (AccessCache["DragonCaveAccess"] == true)
        {
            return true;
        }
        if (NorthOfLakeAccess() && BushCut())
        {
            AccessCache["DragonCaveAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool DrakeCaveAccess()
    {
        if (AccessCache["DrakeCaveAccess"] == true)
        {
            return true;
        }
        if (WaterWayPointAccess() && AllowDark())
        {
            AccessCache["DrakeCaveAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool HauntWayPointAccess()
    {
        if (AccessCache["HauntWayPointAccess"] == true)
        {
            return true;
        }
        if (TownAccess() && BasicBurn() && AllowDark())
        {
            AccessCache["HauntWayPointAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool HauntedHouseAccess()
    {
        if (AccessCache["HauntedHouseAccess"] == true)
        {
            return true;
        }
        if (HauntWayPointAccess() && AllowDark())
        {
            AccessCache["HauntedHouseAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool ForestGrottoAccess()
    {
        if (AccessCache["ForestGrottoAccess"] == true)
        {
            return true;
        }
        if ((TownAccess() && BasicBurn() || HauntWayPointAccess() && BushCut()) && AllowDark())
        {
            AccessCache["ForestGrottoAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool EightRoomAccess()
    {
        if (AccessCache["EightRoomAccess"] == true)
        {
            return true;
        }
        if (NorthOfLakeAccess() && BasicBurn() && (EasyFlight() || BigBurn()) && Vision())
        {
            AccessCache["EightRoomAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool EightRoomLensAccess()
    {
        if (AccessCache["EightRoomLensAccess"] == true)
        {
            return true;
        }
        if (EightRoomAccess())
        {
            AccessCache["EightRoomLensAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool EightRoomZigguratAccess()
    {
        if (AccessCache["EightRoomZigguratAccess"] == true)
        {
            return true;
        }
        if (EightRoomAccess())
        {
            AccessCache["EightRoomZigguratAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool EightRoomUnknownAccess()
    {
        if (AccessCache["EightRoomUnknownAccess"] == true)
        {
            return true;
        }
        if (EightRoomAccess() && AllowMajorSecrets())
        {
            AccessCache["EightRoomUnknownAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool EightRoomKickstarterAccess()
    {
        if (AccessCache["EightRoomKickstarterAccess"] == true)
        {
            return true;
        }
        if (EightRoomAccess() && AllowMajorSecrets())
        {
            AccessCache["EightRoomKickstarterAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool EightRoomBannerAccess()
    {
        if (AccessCache["EightRoomBannerAccess"] == true)
        {
            return true;
        }
        if (EightRoomAccess())
        {
            AccessCache["EightRoomBannerAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool EightRoomDwarfAccess()
    {
        if (AccessCache["EightRoomDwarfAccess"] == true)
        {
            return true;
        }
        if (EightRoomAccess() && Weapon())
        {
            AccessCache["EightRoomDwarfAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool ForestDwarfAccess()
    {
        if (AccessCache["ForestDwarfAccess"] == true)
        {
            return true;
        }
        if (DeepWoodsAccess() && EightRoomAccess() && Weapon())
        {
            AccessCache["ForestDwarfAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool SwampDwarfAccess()
    {
        if (AccessCache["SwampDwarfAccess"] == true)
        {
            return true;
        }
        if (SwampAccess() || SwampBackDoorAccess() && Weapon())
        {
            AccessCache["SwampDwarfAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool ZombieGrottoAccess()
    {
        if (AccessCache["ZombieGrottoAccess"] == true)
        {
            return true;
        }
        if (TownAccess() && ((Boomerang1() && EasyFlight()) || Balloon1()) && AllowDark())
        {
            AccessCache["ZombieGrottoAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool ScubaShopVicinityAccess()
    {
        if (AccessCache["ScubaShopVicinityAccess"] == true)
        {
            return true;
        }
        if (NorthOfLakeAccess() && (LongFlight() || (MountainCaveAccessLower() && EasyFlight())))
        {
            AccessCache["ScubaShopVicinityAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool ScubaShopAccess()
    {
        if (AccessCache["ScubaShopAccess"] == true)
        {
            return true;
        }
        if (ScubaShopVicinityAccess())
        {
            AccessCache["ScubaShopAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool MountainCaveAccessLower()
    {
        if (AccessCache["MountainCaveAccessLower"] == true)
        {
            return true;
        }
        if ((NorthOfLakeAccess() || MountainCaveAccess()) && AllowDark())

		{
            AccessCache["MountainCaveAccessLower"] = true;
            return true;
        }
        return false;
    }
    public static bool MountainCaveAccess()
    {
        if (AccessCache["MountainCaveAccess"] == true)
        {
            return true;
        }
        if (MountainAccess() && AllowDark())
        {
            AccessCache["MountainCaveAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool MountainLowerAccess()
    {
        if (AccessCache["MountainLowerAccess"] == true)
        {
            return true;
        }
        if (TownAccess() && Swim1())
        {
            AccessCache["MountainLowerAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool MountainAccess()
    {
        if (AccessCache["MountainAccess"] == true)
        {
            return true;
        }
        if (MountainLowerAccess() && Charge1())
        {
            AccessCache["MountainAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool MountainDwarfAccess()
    {
        if (AccessCache["MountainDwarfAccess"] == true)
        {
            return true;
        }
        if (MountainAccess() && LongFlight() && Traction() && Weapon())
        {
            AccessCache["MountainDwarfAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool WesternWaterfallCaveAccess()
    {
        if (AccessCache["WesternWaterfallCaveAccess"] == true)
        {
            return true;
        }
        if (TownAccess() && Swim1() || (Balloon1() && MountainLowerAccess()))
        {
            AccessCache["WesternWaterfallCaveAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool SmithAccess()
    {
        if (AccessCache["SmithAccess"] == true)
        {
            return true;
        }
        if (MountainAccess())
        {
            AccessCache["SmithAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool ZigguratAccess()
    {
        if (AccessCache["ZigguratAccess"] == true)
        {
            return true;
        }
        if (TownAccess() && (Balloon1() || (EasyFlight() && BasicBurn())))
        {
            AccessCache["ZigguratAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool LowerZigguratAccess()
    {
        if (AccessCache["LowerZigguratAccess"] == true)
        {
            return true;
        }
        if (ZigguratAccess() && Charge1() && Lens() && EasyFlight())
        {
            return true;
        }
        return false;
    }
    public static bool EarlyWoodsAccess()
    {
        if (AccessCache["EarlyWoodsAccess"] == true)
        {
            return true;
        }
        if (TownAccess() && BasicBurn())
        {
            AccessCache["EarlyWoodsAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool DeepWoodsAccess()
    {
        if (AccessCache["DeepWoodsAccess"] == true)
        {
            return true;
        }
        if (TownAccess() && BigBurn() && AllowDark())
        {
            AccessCache["DeepWoodsAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool TPToWoodsAccess()
    {
        if (AccessCache["TPToWoodsAccess"] == true)
        {
            return true;
        }
        if ((GetTier("Woods Potion") > 0 && WarpShuffle()) || (!WarpShuffle() && PotionShopAccess() && ForestMushroomAccess()) || RuinCityBasicAccess() && Charge1() && Swim1() && Vision())
        {
            AccessCache["TPToWoodsAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool SnowyPeaksAccess()
    {
        if (AccessCache["SnowyPeaksAccess"] == true)
        {
            return true;
        }
        if (MountainAccess() && Boots1())
        {
            AccessCache["SnowyPeaksAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool ColosseumAccess()
    {
        if (AccessCache["ColosseumAccess"] == true)
        {
            return true;
        }
        if (MountainAccess() && LongFlight())
        {
            AccessCache["ColosseumAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool BannerHallAccess()
    {
        if (AccessCache["BannerHallAccess"] == true)
        {
            return true;
        }
        if (RuinsWarpBackdoor() || (NorthOfLakeAccess() && Weapon()))
        {
            AccessCache["BannerHallAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool FactoryAccess()
    {
        if (AccessCache["FactoryAccess"] == true)
        {
            return true;
        }
        if (SwampCaveAccess() && Gloves())
        {
            AccessCache["FactoryAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool StartWarpAccess()
    {
        if (AccessCache["StartWarpAccess"] == true)
        {
            return true;
        }
        if (StartAccess() && Charge1())
        {
            AccessCache["StartWarpAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool SprawlingCaveLakeAccess()
    {
        if (AccessCache["SprawlingCaveLakeAccess"] == true)
        {
            return true;
        }
        if (MountainLowerAccess() && ((Boomerang1() && EasyFlight() && BasicBurn()) || Balloon1()) && AllowDark())
        {
            AccessCache["SprawlingCaveLakeAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool SprawlingCaveAccess()
    {
        if (AccessCache["SprawlingCaveAccess"] == true)
        {
            return true;
        }
        if (((SprawlingCaveLakeAccess() && Balloon1()) || (TownAccess() && Charge1()) || (NorthOfLakeAccess() && Balloon1() && Gloves())) && AllowDark())

		{
            AccessCache["SprawlingCaveAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool SprawlingCaveZombieAccess()
    {
        if (AccessCache["SprawlingCaveZombieAccess"] == true)
        {
            return true;
        }
        if (SprawlingCaveAccess() && Balloon1() && Boomerang1())
        {
            AccessCache["SprawlingCaveZombieAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool SwampSecretCaveAccess()
    {
        if (AccessCache["SwampSecretCaveAccess"] == true)
        {
            return true;
        }
        if ((SwampAccess() || SwampBackDoorAccess()) && Charge1() && AllowMajorSecrets())
        {
            AccessCache["SwampSecretCaveAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool SwordDreamAccess()
    {
        if (AccessCache["SwordDreamAccess"] == true)
        {
            return true;
        }
        if (BigBurn() && SwampAccess())
        {
            AccessCache["SwordDreamAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool NightClubAccess()
    {
        if (AccessCache["NightClubAccess"] == true)
        {
            return true;
        }
        if (ColosseumAccess() && Vision())
        {
            AccessCache["NightClubAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool PotionShopAccess()
    {
        if (AccessCache["PotionShopAccess"] == true)
        {
            return true;
        }
        if (SwampBackDoorAccess() || SwampAccess())
        {
            AccessCache["PotionShopAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool ForestMushroomAccess()
    {
        if (AccessCache["ForestMushroomAccess"] == true)
        {
            return true;
        }
        if (PotionShopAccess() && SwampBackDoorAccess())
        {
            AccessCache["ForestMushroomAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool SnowMushroomAccess()
    {
        if (AccessCache["SnowMushroomAccess"] == true)
        {
            return true;
        }
        if (PotionShopAccess() && SwampAccess() && BigBurn() && Vision())
        {
            AccessCache["SnowMushroomAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool SwampIslandGrottoAccess()
    {
        if (AccessCache["SwampIslandGrottoAccess"] == true)
        {
            return true;
        }
        if (TownAccess() && ((EasyFlight() && Boomerang1()) || Balloon1()) && AllowDark())
        {
            AccessCache["SwampIslandGrottoAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool RuinCityBasicAccess()
    {
        if (AccessCache["RuinCityBasicAccess"] == true)
        {
            return true;
        }
        if (RuinsWarpBackdoor() || BannerHallAccess() || (GetTier("Woods Potion") > 0 && Swim1() && Charge1()))
        {
            AccessCache["RuinCityBasicAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool RuinCityBasicWaterAccess()
    {
        if (AccessCache["RuinCityBasicWaterAccess"] == true)
        {
            return true;
        }
        if (RuinCityBasicAccess() && Weapon() && AllowDark())
        {
            AccessCache["RuinCityBasicWaterAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool RuinCityFullWaterAccess()
    {
        if (AccessCache["RuinCityFullWaterAccess"] == true)
        {
            return true;
        }
        if (RuinCityBasicAccess() && Balloon1())
        {
            AccessCache["RuinCityFullWaterAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool RuinCityMidAccess()
    {
        if (AccessCache["RuinCityMidAccess"] == true)
        {
            return true;
        }
        if (RuinCityBasicAccess() && Balloon1())
        {
            AccessCache["RuinCityMidAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool RuinCityFullAccess()
    {
        if (AccessCache["RuinCityFullAccess"] == true)
        {
            return true;
        }
        if (RuinCityMidAccess() && Charge2())
        {
            AccessCache["RuinCityFullAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool RuinCityGymLowerAccess()
    {
        if (AccessCache["RuinCityGymLowerAccess"] == true)
        {
            return true;
        }
        if (RuinCityMidAccess() || RuinCityGymUpperAccess())
        {
            AccessCache["RuinCityGymLowerAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool RuinCityGymUpperAccess()
    {
        if (AccessCache["RuinCityGymUpperAccess"] == true)
        {
            return true;
        }
        if (RuinCityMidAccess() || RuinCityFullAccess())
        {
            AccessCache["RuinCityGymUpperAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool RuinCityEastRooflessAccess()
    {
        if (AccessCache["RuinCityEastRooflessAccess"] == true)
        {
            return true;
        }
        if (RuinCityMidAccess())
        {
            AccessCache["RuinCityEastRooflessAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool RuinCitySouthRooflessAccess()
    {
        if (AccessCache["RuinCitySouthRooflessAccess"] == true)
        {
            return true;
        }
        if (RuinCityMidAccess())
        {
            AccessCache["RuinCitySouthRooflessAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool RuinCityMarketAccess()
    {
        if (AccessCache["RuinCityMarketAccess"] == true)
        {
            return true;
        }
        if (RuinCityMidAccess())
        {
            AccessCache["RuinCityMarketAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool RuinCityLibraryAccess()
    {
        if (AccessCache["RuinCityLibraryAccess"] == true)
        {
            return true;
        }
        if (RuinCityFullAccess())
        {
            AccessCache["RuinCityLibraryAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool RuinCityBoulderHouse1Access()
    {
        if (AccessCache["RuinCityBoulderHouse1Access"] == true)
        {
            return true;
        }
        if (RuinCityFullWaterAccess())
        {
            AccessCache["RuinCityBoulderHouse1Access"] = true;
            return true;
        }
        return false;
    }
    public static bool RuinCityBoulderHouse2Access()
    {
        if (AccessCache["RuinCityBoulderHouse2Access"] == true)
        {
            return true;
        }
        if (RuinCityFullWaterAccess())
        {
            AccessCache["RuinCityBoulderHouse2Access"] = true;
            return true;
        }
        return false;
    }
    public static bool RuinCityHouseWithAccessToWestIslands()
    {
        if (AccessCache["RuinCityHouseWithAccessToWestIslands"] == true)
        {
            return true;
        }
        if (RuinCityFullWaterAccess())
        {
            AccessCache["RuinCityHouseWithAccessToWestIslands"] = true;
            return true;
        }
        return false;
    }
    public static bool RuinCityHouseWithLensChestAccess()
    {
        if (AccessCache["RuinCityHouseWithLensChestAccess"] == true)
        {
            return true;
        }
        if (RuinCityBasicAccess())
        {
            AccessCache["RuinCityHouseWithLensChestAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool ForgeAccess()
    {
        if (AccessCache["ForgeAccess"] == true)
        {
            return true;
        }
        if (GetTier("Gear Piece 1") > 0 && GetTier("Gear Piece 2") > 0 && GetTier("Gear Piece 3") > 0 && RuinCityBasicAccess())
        {
            AccessCache["ForgeAccess"] = true;
            return true;
        }
        return false;
    }
    public static bool FinalDungeonAccess()
    {
        if (AccessCache["FinalDungeonAccess"] == true)
        {
            return true;
        }
        if (RuinsWarpBackdoor() || RuinCityFullAccess())
        {
            AccessCache["FinalDungeonAccess"] = true;
            return true;
        }
        return false;
    }
    #endregion area logic
    #region item shorthand
    public static bool Swim1()
    {
        return GetTier("Snorkel") > 0;
    }
    public static bool Swim2()
    {
        return GetTier("Snorkel") > 1;
    }
    public static bool Lens()
    {
        return GetTier("Lens") > 0;
    }
    public static bool Balloon1()
    {
        return GetTier("Balloon") > 0;
    }
    public static bool Balloon2()
    {
        return GetTier("Balloon") > 1;
    }
    public static bool Boomerang1()
    {
        return GetTier("Boomerang") > 0;
    }
    public static bool Boomerang2()
    {
        return GetTier("Boomerang") > 1;
    }
    public static bool Gloves()
    {
        return GetTier("Gloves") > 0;
    }
    public static bool Grapple1()
    {
        return GetTier("Grappling Hook") > 0;
    }
    public static bool Grapple2()
    {
        return GetTier("Grappling Hook") > 1;
    }
    public static bool Boots1()
    {
        return GetTier("Boots") > 0;
    }
    public static bool Boots2()
    {
        return GetTier("Boots") > 1;
    }
    public static bool Wallet1()
    {
        return Weapon() && GetTier("Wallet Upgrade") > 0;
    }
    public static bool Wallet2()
    {
        return Weapon() && GetTier("Wallet Upgrade") > 1;
    }
    public static bool Wallet3()
    {
        return Weapon() && GetTier("Wallet Upgrade") > 2;
    }
    #endregion item shorthand
    #region individual checks
    public static bool CheckAccess1() //Starting Cave - Master Key Altar
    {
        return StartAccess();
    }
    public static bool CheckAccess2() //Starting Cave - Secret Salvage
    {
        return (StartAccess() && Charge1() && Swim1() && Vision()) || (TownAccess() && Swim2() && Charge2());
    }
    public static bool CheckAccess3() //Starting Cave - Secret Lens Chest
    {
        return StartAccess() && Charge1() && Lens();
    }
    public static bool CheckAccess4() //Near Starting Cave - Lens Chest
    {
        return StartAccess() && BushCut() && Lens();
    }
    public static bool CheckAccess5() //Near Starting Cave - Stump Chest
    {
        return StartAccess() && BigBurn();
    }
    public static bool CheckAccess6() //Near Starting Cave - Flower Circle
    {
        return StartAccess() && BushCut() && Gloves();
    }
    public static bool CheckAccess7() //East of Village - Bush Farmer's Cave
    {
        return BushCaveAccess();
    }
    public static bool CheckAccess8() //Village - Basement Knight's Chest
    {
        return TwoFloorHouseAccess() && AllowDark();
    }
    public static bool CheckAccess9() //Village - Basement Leak Chest
    {
        return TwoFloorHouseAccess() && Gloves() && AllowDark();
    }
    public static bool CheckAccess10() //Village - Dance Club Rooftop
    {
        return TownAccess() && Balloon1();
    }
    public static bool CheckAccess11() //Near Starting Cave - Burn Tree
    {
        return TownAccess() && BigBurn();
    }
    public static bool CheckAccess12() //Southwest Overworld - Caerbannog's Chest
    {
        return TownAccess() && Swim1();
    }
    public static bool CheckAccess13() //South of Village - Big Slime's Flower Circle
    {
        return TownAccess() && Gloves();
    }
    public static bool CheckAccess14() //Gym - Vitamins Table
    {
        return GymAccess() && Weapon();
    }
    public static bool CheckAccess15() //Gym - Treadmill
    {
        return GymAccess();
    }
    public static bool CheckAccess16() //Village - Hidden Cave Chest
    {
        return TownAccess() && Charge1() && Vision();
    }
    public static bool CheckAccess17() //Village - Duplex Chest
    {
        return LeftDuplexAccess() && Charge1();
    }
    public static bool CheckAccess18() //Village - House Rooftop Chest
    {
        return TwoFloorHouseBalconyAccess() && LongFlight();
    }
    public static bool CheckAccess19() //Simple Shop - Bottom Right
    {
        return BasicShopAccess();
    }
    public static bool CheckAccess20() //Simple Shop - Bottom Middle
    {
        return BasicShopAccess();
    }
    public static bool CheckAccess21() //Simple Shop - Bottom Left
    {
        return BasicShopAccess() && Wallet1();
    }
    public static bool CheckAccess22() //Simple Shop - Top Left 
    {
        return false; //As this is planned to remain vanilla, it will always return false.
    }
    public static bool CheckAccess23() //Simple Shop - Top Middle
    {
        return BasicShopAccess();
    }
    public static bool CheckAccess24() //Simple Shop - Top Right
    {
        return false; //As this is planned to remain vanilla, it will always return false.
    }
    public static bool CheckAccess25() //Fancy Shop - Bottom Left
    {
        return false; //As this is planned to remain vanilla, it will always return false.
    }
    public static bool CheckAccess26() //Fancy Shop - Bottom Mid-Left
    {
        return false; //As this is planned to remain vanilla, it will always return false.
    }
    public static bool CheckAccess27() //Fancy Shop - Bottom Mid-Right
    {
        return FancyShopAccess() && Wallet2();
    }
    public static bool CheckAccess28() //Fancy Shop - Bottom Right
    {
        return FancyShopAccess() && Wallet1();
    }
    public static bool CheckAccess29() //Fancy Shop - Top Left
    {
        return FancyShopAccess() && Wallet2();
    }
    public static bool CheckAccess30() //Fancy Shop - Top Second From Left
    {
        return FancyShopAccess() && Wallet2();
    }
    public static bool CheckAccess31() //Fancy Shop - Top Third From Left
    {
        return FancyShopAccess() && Wallet2();
    }
    public static bool CheckAccess32() //Fancy Shop - Top Third From Right
    {
        return FancyShopAccess() && Wallet2();
    }
    public static bool CheckAccess33() //Fancy Shop - Top Second From Right
    {
        return FancyShopAccess() && Wallet3();
    }
    public static bool CheckAccess34() //Fancy Shop - Top Right
    {
        return FancyShopAccess() && Wallet3();
    }
    public static bool CheckAccess35() //West of Village - Behind Rocks
    {
        return TownAccess() && Charge1();
    }
    public static bool CheckAccess36() //West of Village - Lens Chest
    {
        return TownAccess() && Lens() && BushCut();
    }
    public static bool CheckAccess37() //South of Village - Chest Past Giant Slime
    {
        return TownAccess() && BushCut();
    }
    public static bool CheckAccess38() //South of Village - Giant Boulder
    {
        return TownAccess() && (LongFlight() || Swim1()) && Charge2();
    }
    public static bool CheckAccess39() //North of Village - East of Gate
    {
        return TownAccess() && BushCut();
    }
    public static bool CheckAccess40() //Southwest of Village - Crows
    {
        return TownAccess() && (Swim1() || Balloon1());
    }
    public static bool CheckAccess41() //Breakout Cave - Top Chest
    {
        return BreakoutCaveAccess() && Weapon();
    }
    public static bool CheckAccess42() //Breakout Cave - Middle Chest
    {
        return BreakoutCaveAccess() && Weapon();
    }
    public static bool CheckAccess43() //Breakout Cave - Bottom Chest
    {
        return BreakoutCaveAccess() && Weapon();
    }
    public static bool CheckAccess44() //East Overworld - Southeast of Dark Woods
    {
        return TownAccess() && (BushCut() || EasyFlight());
    }
    public static bool CheckAccess45() //Near Starting Cave - Warp Cave
    {
        return StartWarpAccess();
    }
    public static bool CheckAccess46() //Ruined City - Atop the Tallest Left Side Building
    {
        return RuinsWarpBackdoor() || RuinCityFullAccess();
    }
    public static bool CheckAccess47() //West of Village - Fire Guys' Cave
    {
        return ThreeFireCaveAccess() && Weapon();
    }
    public static bool CheckAccess48() //West of Village - Fire Guys' Steam Leak
    {
        return ThreeFireCaveAccess() && Gloves() && BushCut();
    }
    public static bool CheckAccess49() //West Overworld - Abandoned House
    {
        return AbandonedHouseAccess();
    }
    public static bool CheckAccess50() //West Overworld - Bunny Cave
    {
        return RabbitCaveAccess() && Weapon();
    }
    public static bool CheckAccess51() //Southwest Overworld - Across River from Dungeon
    {
        return WaterWayPointAccess() && LongFlight();
    }
    public static bool CheckAccess52() //West Overworld - South of Abandoned House
    {
        return TownAccess() && Charge1();
    }
    public static bool CheckAccess53() //West Overworld - Pong Cave
    {
        return PongCaveAccess() && Weapon();
    }
    public static bool CheckAccess54() //West Overworld - Spike Maze Chest
    {
        return IceSpikeMazeAccess();
    }
    public static bool CheckAccess55() //Southwest Overworld - Behind Reaper
    {
        return WaterWayPointAccess() && Charge1();
    }
    public static bool CheckAccess56() //Southwest Overworld - Hidden Cave Under Waypoint
    {
        return WaterWayPointCaveAccess();
    }
    public static bool CheckAccess57() //Southwest Overworld - Cave East of Dungeon North Chest
    {
        return GrottoEastOfWaterWayPointAccess() && BushCut();
    }
    public static bool CheckAccess58() //Southwest Overworld - Cave East of Dungeon East Chest
    {
        return GrottoEastOfWaterWayPointAccess() && BushCut();
    }
    public static bool CheckAccess59() //Southwest Overworld - Cave East of Dungeon Center Chest
    {
        return GrottoEastOfWaterWayPointAccess() && BushCut() && (Balloon1() || (Grapple1() && Boomerang1()));
    }
    public static bool CheckAccess60() //Southwest Overworld - Wind Cave
    {
        return WindCaveAccess() && BushCut();
    }
    public static bool CheckAccess61() //Southwest Overworld - Wind Cave Secret Room
    {
        return WindCaveAccess() && Charge1() && Vision();
    }
    public static bool CheckAccess62() //Southwest Overworld - Burn Tree West of Dungeon
    {
        return NorthOfWaterDungeonAccess() && BigBurn();
    }
    public static bool CheckAccess63() //Above Starting Cave - Lens Chest
    {
        return WaterWayPointAccess() && Lens();
    }
    public static bool CheckAccess64() //Above Starting Cave - Sparkling Dig Spot
    {
        return WaterWayPointAccess() && Gloves() && Balloon1() && Vision();
    }
    public static bool CheckAccess65() //Above Starting Cave - Unmarked Dig Spot
    {
        return WaterWayPointAccess() && Gloves() && Balloon1() && AllowMajorSecrets();
    }
    public static bool CheckAccess66() //Southwest Overworld - Ledge Chest West of Dungeon
    {
        return (NorthOfWaterDungeonAccess() && LongFlight()) || (WaterWayPointAccess() && EasyFlight());
    }
    public static bool CheckAccess67() //Central Overworld - Lone Island Chest
    {
        return TownAccess() && EasyFlight();
    }
    public static bool CheckAccess68() //Central Overworld - Seven Island Chest
    {
        return NorthOfLakeAccess() && (Balloon1() || (BigBurn() && EasyFlight()));
    }
    public static bool CheckAccess69() //Central Overworld - Four Pillars Salvage
    {
        return NorthOfLakeAccess() && Swim1();
    }
    public static bool CheckAccess70() //Central Overworld - Salvage Between Waterfalls
    {
        return NorthOfLakeAccess() && Swim1() && Vision();
    }
    public static bool CheckAccess71() //Central Overworld - Chest Near Scuba Shop
    {
        return (NorthOfLakeAccess() && Swim1()) || (ScubaShopVicinityAccess() && EasyFlight());
    }
    public static bool CheckAccess72() //Central Overworld - Scuba Shop
    {
        return ScubaShopAccess() && Wallet1();
    }
    public static bool CheckAccess73() //Northwest Overworld - West of Scuba Shop
    {
        return ScubaShopVicinityAccess();
    }
    public static bool CheckAccess74() //Southwest Overworld - Drake Cave Shallows Maze
    {
        return DrakeCaveAccess();
    }
    public static bool CheckAccess75() //Southwest Overworld - Drake Cave Salvage
    {
        return DrakeCaveAccess() && Swim1() && Vision();
    }
    public static bool CheckAccess76() //Southwest Overworld - Drake Cave Block Puzzle
    {
        return DrakeCaveAccess();
    }
    public static bool CheckAccess77() //West Overworld - By Waterfall
    {
        return NorthOfWaterDungeonAccess();
    }
    public static bool CheckAccess78() //West Overworld - South of Waterfall
    {
        return NorthOfWaterDungeonAccess() && BasicBurn();
    }
    public static bool CheckAccess79() //West Overworld - Lens Chest
    {
        return NorthOfWaterDungeonAccess() && (Swim1() || Balloon1()) && Lens();
    }
    public static bool CheckAccess80() //Central Overworld - Across River North of Village
    {
        return TownAccess() && EasyFlight();
    }
    public static bool CheckAccess81() //Dark Woods - Lantern Shop
    {
        return TownAccess() && Weapon();
    }
    public static bool CheckAccess82() //Northeast Overworld - Ravine Chest
    {
        return TownAccess();
    }
    public static bool CheckAccess83() //Central Overworld - Alligator Hills Chest
    {
        return NorthOfLakeAccess() && EasyFlight();
    }
    public static bool CheckAccess84() //West Overworld - Chest by Abandoned House
    {
        return TownAccess() && BasicBurn();
    }
    public static bool CheckAccess85() //East Overworld - Reaper Flower Circle
    {
        return TownAccess() && BushCut() && Gloves();
    }
    public static bool CheckAccess86() //East Overworld - Across the River
    {
        return TownAccess() && LongFlight();
    }
    public static bool CheckAccess87() //Dark Woods - North of Warning Sign
    {
        return TownAccess() && BushCut();
    }
    public static bool CheckAccess88() //Dark Woods - Mushroom Nook
    {
        return TownAccess() && BasicBurn();
    }
    public static bool CheckAccess89() //Dark Woods - Grotto
    {
        return ForestGrottoAccess() && (EasyFlight() || Swim1());
    }
    public static bool CheckAccess90() //Dark Woods - Grotto Arena
    {
        return ForestGrottoAccess() && Weapon();
    }
    public static bool CheckAccess91() //Dark Woods - by Dungeon
    {
        return HauntWayPointAccess() && BigBurn();
    }
    public static bool CheckAccess92() //Dark Woods - Behind Lantern Shop
    {
        return TownAccess() && BigBurn();
    }
    public static bool CheckAccess93() //Dark Woods - North of Ravine
    {
        return TownAccess() && BigBurn();
    }
    public static bool CheckAccess94() //Dark Woods - Tree Circle
    {
        return TownAccess() && BigBurn();
    }
    public static bool CheckAccess95() //Dark Woods - Dead End North of Swamp
    {
        return TownAccess() && BigBurn();
    }
    public static bool CheckAccess96() //Dark Woods - Burn Tree Nook
    {
        return TownAccess() && BigBurn();
    }
    public static bool CheckAccess97() //Dark Woods - Across Valley South
    {
        return TownAccess() && BigBurn() && LongFlight();
    }
    public static bool CheckAccess98() //Dark Woods - Across Valley North
    {
        return TownAccess() && BigBurn() && Balloon1();
    }
    public static bool CheckAccess99() //Dark Woods - Dwarf
    {
        return TownAccess() && BigBurn();
    }
    public static bool CheckAccess100() //Dark Woods - Burn Tree East of Dwarf
    {
        return TownAccess() && BigBurn();
    }
    public static bool CheckAccess101() //Dark Woods - Dwarfless Boulder
    {
        return DeepWoodsAccess() && Balloon1() && Charge2();
    }
    public static bool CheckAccess102() //Dark Woods - Flower Circle
    {
        return DeepWoodsAccess() && Gloves();
    }
    public static bool CheckAccess103() //North Overworld - Zombie Flower Circle
    {
        return NorthOfLakeAccess() && Gloves();
    }
    public static bool CheckAccess104() //North Overworld - Chest Outside Dragon's Lair
    {
        return NorthOfLakeAccess() && (Boomerang2() && LongFlight() || Balloon1());
    }
    public static bool CheckAccess105() //Dragon's Lair - Free Sample
    {
        return DragonCaveAccess();
    }
    public static bool CheckAccess106() //Dragon's Reward - 1 Crystal
    {
        return DragonCaveAccess() && GetTier("Crystal") > 0;
    }
    public static bool CheckAccess107() //Dragon's Reward - 3 Crystals
    {
        return DragonCaveAccess() && GetTier("Crystal") > 2;
    }
    public static bool CheckAccess108() //Dragon's Reward - 5 Crystals
    {
        return DragonCaveAccess() && GetTier("Crystal") > 4;
    }
    public static bool CheckAccess109() //Dragon's Reward - 10 Crystals
    {
        return DragonCaveAccess() && GetTier("Crystal") > 9;
    }
    public static bool CheckAccess110() //Dragon's Reward - 15 Crystals
    {
        return DragonCaveAccess() && GetTier("Crystal") > 14;
    }
    public static bool CheckAccess111() //Dragon's Reward - 20 Crystals
    {
        return DragonCaveAccess() && GetTier("Crystal") > 19;
    }
    public static bool CheckAccess112() //Dragon's Reward - 25 Crystals
    {
        return DragonCaveAccess() && GetTier("Crystal") > 24;
    }
    public static bool CheckAccess113() //Dragon's Reward - 30 Crystals
    {
        return DragonCaveAccess() && GetTier("Crystal") > 29;
    }
    public static bool CheckAccess114() //Dragon's Reward - 40 Crystals
    {
        return DragonCaveAccess() && GetTier("Crystal") > 39;
    }
    public static bool CheckAccess115() //Dragon's Reward - 50 Crystals
    {
        return DragonCaveAccess() && GetTier("Crystal") > 49;
    }
    public static bool CheckAccess116() //Dragon's Reward - 60 Crystals
    {
        return DragonCaveAccess() && GetTier("Crystal") > 59;
    }
    public static bool CheckAccess117() //Dragon's Lair - Waterfall Chest
    {
        return DragonCaveAccess() && Swim2();
    }
    public static bool CheckAccess118() //Mountain - Ice Spike Maze
    {
        return MountainSpikeMazeAccess() && Weapon() && Traction();
    }
    public static bool CheckAccess119() //Central Overworld - Flower Circle West of Lake
    {
        return MountainLowerAccess() && Gloves();
    }
    public static bool CheckAccess120() //Central Overworld - Flower Circle Southwest of Lake
    {
        return TownAccess() && Gloves();
    }
    public static bool CheckAccess121() //Mountain - Chest South of Smithery
    {
        return MountainLowerAccess() && EasyFlight();
    }
    public static bool CheckAccess122() //Mountain - Flower Circle South of Smithery
    {
        return MountainLowerAccess() && Gloves();
    }
    public static bool CheckAccess123() //Smith Shop - Left
    {
        return SmithAccess() && Wallet1();
    }
    public static bool CheckAccess124() //Smith Shop - Middle
    {
        return SmithAccess() && Wallet2();
    }
    public static bool CheckAccess125() //Smith Shop - Right
    {
        return SmithAccess() && Wallet3();
    }
    public static bool CheckAccess126() //Mountain - Above Rubble
    {
        return MountainLowerAccess() && LongFlight();
    }
    public static bool CheckAccess127() //Mountain - Unmarked Dig Spot Above Rubble
    {
        return MountainLowerAccess() && Balloon1() && AllowMajorSecrets();
    }
    public static bool CheckAccess128() //Mountain - Giant Enemy Crab's Flower Circle
    {
        return MountainLowerAccess() && Gloves() && (Swim1() || Balloon1());
    }
    public static bool CheckAccess129() //Mountain - Rubble Lens Chest
    {
        return MountainLowerAccess() && Charge1() && Lens();
    }
    public static bool CheckAccess130() //Mountain - Above Western Waterfall
    {
        return WesternWaterfallCaveAccess() || MountainLowerAccess() && Balloon1();
    }
    public static bool CheckAccess131() //Mountain - Zig Zag Ice Cave
    {
        return MountainCaveAccess() && Traction();
    }
    public static bool CheckAccess132() //Mountain - Cave Boulder
    {
        return MountainCaveAccess() && Charge2();
    }
    public static bool CheckAccess133() //Mountain - Ice Floe Atop Big Waterfall
    {
        return MountainAccess() && (Balloon1() || Swim2());
    }
    public static bool CheckAccess134() //Mountain - Lens Chest West of Waypoint
    {
        return MountainAccess() && Lens() && (BushCut() || Balloon1());
    }
    public static bool CheckAccess135() //Mountain - Wall North of Waypoint
    {
        return MountainAccess() && Balloon1() && Charge1() && Vision();
    }
    public static bool CheckAccess136() //Mountain - Cliff Over Dungeon
    {
        return MountainAccess() && Balloon1();
    }
    public static bool CheckAccess137() //Mountain - Dwarf Boulder Chest
    {
        return MountainDwarfAccess();
    }
    public static bool CheckAccess138() //Mountain - Cliff Above Penguins
    {
        return MountainAccess() && LongFlight();
    }
    public static bool CheckAccess139() //Smith Shop - Forge the Ultimate Sword
    {
        return GetTier("Weapon Upgrade") > 4 && SmithAccess() & EightRoomDwarfAccess() && ForestDwarfAccess() && MountainDwarfAccess() && SwampDwarfAccess() && TownDwarfAccess() && WaterDwarfAccess();
    }
    public static bool CheckAccess140() //Mountains - Cliff Below Waypoint
    {
        return MountainAccess() && LongFlight();
    }
    public static bool CheckAccess141() //Mountains - Cave West of Waypoint
    {
        return MountainCaveSpikeMazeAccess();
    }
    public static bool CheckAccess142() //Swamp - Bramble Island Across Deep Water
    {
        return SwampAccess();
    }
    public static bool CheckAccess143() //??? - Beneath the Warp Hub
    {
        return Lens() && Balloon1() && (TownAccess() || (Charge1() && (HauntWayPointAccess() || WaterWayPointAccess() || MountainAccess() || SwampCaveAccess() || RuinCityFullAccess())));
    }
    public static bool CheckAccess144() //Sewer - Arena
    {
        return SewerAccess() && Weapon();
    }
    public static bool CheckAccess145() //Swamp - Dwarf Boulder
    {
        return SwampDwarfAccess();
    }
    public static bool CheckAccess146() //Swamp - Cavern Donut Salvage
    {
        return SwampCaveAccess() && Swim1();
    }
    public static bool CheckAccess147() //Swamp - Cavern Lens Chest
    {
        return SwampCaveAccess() && Lens() && Swim1();
    }
    public static bool CheckAccess148() //Swamp - Cavern Arena
    {
        return SwampCaveAccess() && Swim1() && Weapon();
    }
    public static bool CheckAccess149() //Swamp - Cavern Boomerang Challenge
    {
        return SwampCaveAccess() && Boomerang2() && Swim1() && EasyFlight();
    }
    public static bool CheckAccess150() //Swamp - Cavern Block Puzzle
    {
        return SwampCaveAccess() && Swim1() && EasyFlight();
    }
    public static bool CheckAccess151() //Swamp - Chest Outside Power Plant
    {
        return SwampAccess() && (BasicBurn() || Swim1() || Balloon1());
    }
    public static bool CheckAccess152() //Swamp - Past Shallow Maze Lever
    {
        return TownAccess() && (Weapon() || Swim1() || Balloon1());
    }
    public static bool CheckAccess153() //Swamp - Boomerang Grotto
    {
        return ZombieGrottoAccess();
    }
    public static bool CheckAccess154() //Swamp - Double Cyclops Reward
    {
        return SwampBackDoorAccess() && Boomerang1() && LongFlight();
    }
    public static bool CheckAccess155() //Swamp - Flower Circle by Hot Spring
    {
        return SwampBackDoorAccess() && Gloves();
    }
    public static bool CheckAccess156() //Swamp - Two Crabs Bramble Chest
    {
        return SwampAccess();
    }
    public static bool CheckAccess157() //Swamp - Unmarked Secret Cave
    {
        return Charge1() && SwampBackDoorAccess() && AllowMajorSecrets();
    }
    public static bool CheckAccess158() //Swamp - Five Rock Lens Chest
    {
        return SwampAccess() && Lens();
    }
    public static bool CheckAccess159() //Swamp - Broken Road Flower Circle
    {
        return SwampAccess() && Gloves();
    }
    public static bool CheckAccess160() //Potion Shop - Right
    {
        return PotionShopAccess() && SnowMushroomAccess();
    }
    public static bool CheckAccess161() //Potion Shop - Left
    {
        return PotionShopAccess() && ForestMushroomAccess();
    }
    public static bool CheckAccess162() //Swamp - Buried Among Trees
    {
        return SwampBackDoorAccess() && Balloon1() && AllowMajorSecrets();
    }
    public static bool CheckAccess163() //Snow Potion Slope - Buried Treasure 1
    {
        return (GetTier("Snow Potion") > 0 || (!WarpShuffle() && PotionShopAccess() && SnowMushroomAccess())) && Gloves();
    }
    public static bool CheckAccess164() //Nightclub - Brawl Chest
    {
        return NightClubAccess() && Weapon();
    }
    public static bool CheckAccess165() //Nightclub - Meal
    {
        return NightClubAccess();
    }
    public static bool CheckAccess166() //Far West Overworld - Ravine Lens Chest
    {
        return NorthOfWaterDungeonAccess() && BigBurn() && Lens();
    }
    public static bool CheckAccess167() //Far West Overworld - Fox Bushes
    {
        return NorthOfWaterDungeonAccess() && BigBurn() && Balloon1() && Gloves();
    }
    public static bool CheckAccess168() //Village - Coffee Stand
    {
        return false; //As this is planned to remain vanilla, it will always return false.
    }
    public static bool CheckAccess169() //Far West Overworld - Broken Sword
    {
        return TownAccess() && Swim2();
    }
    public static bool CheckAccess170() //Far West Overworld - West of Broken Sword
    {
        return TownAccess() && Swim2() && Balloon1();
    }
    public static bool CheckAccess171() //Far West Overworld - Waterfall Chest
    {
        return TownAccess() && Swim2();
    }
    public static bool CheckAccess172() //Eight Room Maze - Lens Path (LUULDRUR)
    {
        return EightRoomLensAccess();
    }
    public static bool CheckAccess173() //Eight Room Maze - Ziggurat Path (ULURRDLU)
    {
        return EightRoomZigguratAccess();
    }
    public static bool CheckAccess174() //Eight Room Maze - Unknown Path (LRDLLDUU)
    {
        return false; //Until someone actually finds the code for this path in game, I'm putting this check completely out of logic. Once it's been found: EightRoomUnknownAccess();
    }
    public static bool CheckAccess175() //Eight Room Maze - Kickstarter Path (RURDLDLU)
    {
        return EightRoomKickstarterAccess();
    }
    public static bool CheckAccess176() //Eight Room Maze - Dwarf Path (RDLURULL)
    {
        return EightRoomDwarfAccess();
    }
    public static bool CheckAccess177() //Eight Room Maze - Banner Path Left Chest (URDLLURU)
    {
        return EightRoomBannerAccess();
    }
    public static bool CheckAccess178() //Eight Room Maze - Banner Path Right Chest (URDLLURU)
    {
        return EightRoomBannerAccess();
    }
    public static bool CheckAccess179() //North of Dark Woods - Near Potion Warp
    {
        return TPToWoodsAccess();
    }
    public static bool CheckAccess180() //North of Dark Woods - Dive in Lake
    {
        return TPToWoodsAccess() && Swim1() && Weapon() || RuinCityBasicAccess() && Charge1();
    }
    public static bool CheckAccess181() //Dream World - Pull the Legendary Sword
    {
        return SwordDreamAccess() && GetTier("Health") > 41;
    }
    public static bool CheckAccess182() //Path to Ruined City - Right Side
    {
        return BannerHallAccess() && Gloves();
    }
    public static bool CheckAccess183() //Path to Ruined City - Left Side
    {
        return BannerHallAccess() && Balloon1() && Weapon();
    }
    public static bool CheckAccess184() //Colosseum - Prize 1
    {
        return ColosseumAccess() && Weapon() && Traction();
    }
    public static bool CheckAccess185() //Colosseum - Prize 2
    {
        return ColosseumAccess() && Weapon() && Traction();
    }
    public static bool CheckAccess186() //Colosseum - Prize 3
    {
        return ColosseumAccess() && Weapon() && Traction();
    }
    public static bool CheckAccess187() //Colosseum - Prize 4
    {
        return ColosseumAccess() && Weapon() && Traction();
    }
    public static bool CheckAccess188() //Colosseum - Grand Prize Left
    {
        return ColosseumAccess() && Weapon() && Traction();
    }
    public static bool CheckAccess189() //Colosseum - Grand Prize Right
    {
        return ColosseumAccess() && Weapon() && Traction();
    }
    public static bool CheckAccess190() //Sprawling Cave Lake - West Chest
    {
        return SprawlingCaveLakeAccess() && Balloon1();
    }
    public static bool CheckAccess191() //Sprawling Cave Lake - Southeast Salvage
    {
        return SprawlingCaveLakeAccess() && Swim1();
    }
    public static bool CheckAccess192() //Sprawling Cave Lake - Northeast Chest
    {
        return SprawlingCaveLakeAccess() && Balloon1() && Boomerang1();
    }
    public static bool CheckAccess193() //Sprawling Cave Lake - Lever Puzzle
    {
        return SprawlingCaveLakeAccess() && Boomerang2() && LongFlight();
    }
    public static bool CheckAccess194() //Sprawling Cave - Beneath Zombie Houses
    {
        return SprawlingCaveZombieAccess();
    }
    public static bool CheckAccess195() //Sprawling Cave - Burrow Under Steam Leaks
    {
        return SprawlingCaveAccess() && Gloves();
    }
    public static bool CheckAccess196() //Sprawling Cave - Island Chest
    {
        return SprawlingCaveAccess() && EasyFlight() && Swim1();
    }
    public static bool CheckAccess197() //Sprawling Cave - Single Grapple Chest
    {
        return SprawlingCaveAccess() && EasyFlight() && Swim1();
    }
    public static bool CheckAccess198() //Sprawling Cave - Chain Grapple Chest
    {
        return SprawlingCaveAccess() && EasyFlight() && Swim1();
    }
    public static bool CheckAccess199() //Sprawling Cave - Boulder Chest
    {
        return SprawlingCaveAccess() && Balloon1() && Swim1() && Charge2();
    }
    public static bool CheckAccess200() //Sprawling Cave - Knight Corridor
    {
        return SprawlingCaveAccess() && Charge1() && Swim1() && Gloves();
    }
    public static bool CheckAccess201() //Sprawling Cave - Northwest of Block Room
    {
        return SprawlingCaveAccess() && Balloon1() && (Gloves() || Swim1());
    }
    public static bool CheckAccess202() //Water Dungeon - Entry Salvage
    {
        return WaterDungeonAccess() && Swim1() && Vision();
    }
    public static bool CheckAccess203() //Water Dungeon - Mushroom Chest
    {
        return WaterDungeonAccess() && BushCut();
    }
    public static bool CheckAccess204() //Water Dungeon - Salvage West of Entrance
    {
        return WaterDungeonAccess() && Swim1() && Vision();
    }
    public static bool CheckAccess205() //Water Dungeon - Map Chest
    {
        return WaterDungeonAccess() && Weapon();
    }
    public static bool CheckAccess206() //Water Dungeon - Box Yourself In
    {
        return WaterDungeonAccess() && Weapon();
    }
    public static bool CheckAccess207() //Water Dungeon - Grappling Hook Chest
    {
        return WaterDungeonAccess() && BushCut() && GetTier("Water Dungeon Key") > 2;
    }
    public static bool CheckAccess208() //Water Dungeon - Easy Grapple Island
    {
        return WaterDungeonAccess() && Weapon() && EasyFlight();
    }
    public static bool CheckAccess209() //Water Dungeon - Chest West of Entrance
    {
        return WaterDungeonAccess() && (ShortRange() || Swim1() && BushCut());
    }
    public static bool CheckAccess210() //Water Dungeon - Spike Room Salvage
    {
        return WaterDungeonAccess() && ShortRange() && Swim1() && Vision();
    }
    public static bool CheckAccess211() //Water Dungeon - Northeast Room Salvage
    {
        return WaterDungeonAccess() && Swim1() && Vision();
    }
    public static bool CheckAccess212() //Water Dungeon - Challenge Grapple Island
    {
        return WaterDungeonAccess() && Weapon() && EasyFlight();
    }
    public static bool CheckAccess213() //Water Dungeon - Spinning Trap Room Chest
    {
        return WaterDungeonAccess() && Weapon() && EasyFlight();
    }
    public static bool CheckAccess214() //Water Dungeon - Spinning Trap Room North Salvage
    {
        return WaterDungeonAccess() && Weapon() && Swim1() && Vision();
    }
    public static bool CheckAccess215() //Water Dungeon - Spinning Trap Room West Salvage
    {
        return WaterDungeonAccess() && Weapon() && Swim1() && Vision();
    }
    public static bool CheckAccess216() //Water Dungeon - Spinning Trap Room South Salvage
    {
        return WaterDungeonAccess() && Weapon() && Swim1() && Vision();
    }
    public static bool CheckAccess217() //Water Dungeon - Waterfall Room
    {
        return WaterDungeonAccess() && Charge1() && EasyFlight() && ShortRange() && Vision();
    }
    public static bool CheckAccess218() //Water Dungeon - Cluttered Box Room
    {
        return WaterDungeonAccess() && Charge1();
    }
    public static bool CheckAccess219() //Water Dungeon - Boss Chest
    {
        return WaterDungeonAccess() && ShortRange() && GetTier("Water Dungeon Key") > 2;
    }
    public static bool CheckAccess220() //Water Dungeon - Atop Waterfall
    {
        return WaterDungeonAccess() && Charge1() && Swim2() && ShortRange() && Vision();
    }
    public static bool CheckAccess221() //Haunted House - Dining Room Ghost
    {
        return HauntedHouseAccess() && Weapon();
    }
    public static bool CheckAccess222() //Haunted House - Lounge Ghost
    {
        return HauntedHouseAccess() && Weapon();
    }
    public static bool CheckAccess223() //Haunted House - Livingroom Ghost
    {
        return HauntedHouseAccess() && Weapon();
    }
    public static bool CheckAccess224() //Haunted House - Basement Ghost
    {
        return HauntedHouseAccess() && Weapon();
    }
    public static bool CheckAccess225() //Haunted House - Basement Middle Chest
    {
        return HauntedHouseAccess() && Weapon();
    }
    public static bool CheckAccess226() //Haunted House - Basement Furnace
    {
        return HauntedHouseAccess() && Weapon();
    }
    public static bool CheckAccess227() //Haunted House - Basement Hidden Room
    {
        return HauntedHouseAccess() && Charge1() && Vision();
    }
    public static bool CheckAccess228() //Haunted House - Kitchen Ghost
    {
        return HauntedHouseAccess() && Weapon() && GetTier("Light Bulb") > 3;
    }
    public static bool CheckAccess229() //Haunted House - Bedroom Chest
    {
        return HauntedHouseAccess() && GetTier("Light Bulb") > 2;
    }
    public static bool CheckAccess230() //Haunted House - Bedroom Ghost
    {
        return HauntedHouseAccess() && Weapon() && GetTier("Light Bulb") > 2;
    }
    public static bool CheckAccess231() //Haunted House - Balcony Ghost
    {
        return HauntedHouseAccess() && Weapon() && GetTier("Light Bulb") > 5;
    }
    public static bool CheckAccess232() //Haunted House - Treadmill
    {
        return HauntedHouseAccess() && GetTier("Light Bulb") > 5;
    }
    public static bool CheckAccess233() //Haunted House - Lantern Chest
    {
        return HauntedHouseAccess() && Weapon() && GetTier("Light Bulb") > 5;
    }
    public static bool CheckAccess234() //Haunted House - Stump Chest
    {
        return HauntedHouseAccess() && BigBurn() && GetTier("Light Bulb") > 5;
    }
    public static bool CheckAccess235() //Haunted House - Make a Block Table
    {
        return HauntedHouseAccess();
    }
    public static bool CheckAccess236() //Haunted House - Sick Room Ghos
    {
        return HauntedHouseAccess() && Weapon();
    }
    public static bool CheckAccess237() //Haunted House - Block Bedroom Ghost
    {
        return HauntedHouseAccess() && BigBurn() && GetTier("Light Bulb") > 4;
    }
    public static bool CheckAccess238() //Haunted House - 9 Ghost Door
    {
        return HauntedHouseAccess() && GetTier("Light Bulb") > 8;
    }
    public static bool CheckAccess239() //Haunted House - Boss Chest
    {
        return HauntedHouseAccess() && Weapon() && GetTier("Light Bulb") > 6;
    }
    public static bool CheckAccess240() //Haunted House - Atop Waterfall
    {
        return HauntedHouseAccess() && Weapon() && GetTier("Light Bulb") > 5 && Swim2();
    }
    public static bool CheckAccess241() //Snowy Peaks - Map Arena Chest
    {
        return SnowyPeaksAccess() && Weapon() && Gloves();
    }
    public static bool CheckAccess242() //Snowy Peaks - Ice Puzzle East of Entrance
    {
        return SnowyPeaksAccess();
    }
    public static bool CheckAccess243() //Snowy Peaks - Ice Moat Island
    {
        return SnowyPeaksAccess() && Weapon() && GetTier("Snowy Peaks Key") > 0;
    }
    public static bool CheckAccess244() //Snowy Peaks - Boots Arena Chest
    {
        return SnowyPeaksAccess() && GetTier("Snowy Peaks Key") > 0 && Weapon();
    }
    public static bool CheckAccess245() //Snowy Peaks - Ice Puzzle north of Ice Moat
    {
        return SnowyPeaksAccess() && GetTier("Snowy Peaks Key") > 0 && Traction();
    }
    public static bool CheckAccess246() //Snowy Peaks - Glove Chest
    {
        return SnowyPeaksAccess() && Weapon() && GetTier("Snowy Peaks Key") > 1 && (Gloves() || Balloon1() || Swim1()) && Traction();
    }
    public static bool CheckAccess247() //Snowy Peaks - Buried Near Furnace
    {
        return SnowyPeaksAccess() && Weapon() && Gloves() & GetTier("Snowy Peaks Key") > 1 && Vision() && Traction();
    }
    public static bool CheckAccess248() //Snowy Peaks - Buried in Eastmost point
    {
        return SnowyPeaksAccess() && GetTier("Snowy Peaks Key") > 0 && Gloves() && Vision() && Traction();
    }
    public static bool CheckAccess249() //Snowy Peaks - Buried in Nine Square room
    {
        return SnowyPeaksAccess() && GetTier("Snowy Peaks Key") > 0 && Gloves() && Traction();
    }
    public static bool CheckAccess250() //Snowy Peaks - Frozen Chest
    {
        return SnowyPeaksAccess() && Weapon() && GetTier("Snowy Peaks Key") > 1 && Traction();
    }
    public static bool CheckAccess251() //Snowy Peaks - Hidden Room Lens Chest
    {
        return SnowyPeaksAccess() && Charge1() && Lens() && (Gloves() || Balloon1()) && GetTier("Snowy Peaks Key") > 2 && Vision() && Traction();
    }
    public static bool CheckAccess252() //Snowy Peaks - Ice Floe Salvage
    {
        return SnowyPeaksAccess() && Weapon() && GetTier("Snowy Peaks Key") > 2 && Swim1() && Vision() && Traction();
    }
    public static bool CheckAccess253() //Snowy Peaks - Buried East of White Birds
    {
        return SnowyPeaksAccess() && Weapon() && GetTier("Snowy Peaks Key") > 2 && Gloves() && Vision() && Traction();
    }
    public static bool CheckAccess254() //Snowy Peaks - Buried East of Ascension
    {
        return SnowyPeaksAccess() && Weapon() && GetTier("Snowy Peaks Key") > 2 && Gloves() && Vision() && Traction();
    }
    public static bool CheckAccess255() //Snowy Peaks - Across Ravine
    {
        return SnowyPeaksAccess() && Weapon() && GetTier("Snowy Peaks Key") > 2 && Gloves() && EasyFlight() && Traction();
    }
    public static bool CheckAccess256() //Snowy Peaks - Boss Chest
    {
        return SnowyPeaksAccess() && Weapon() && GetTier("Snowy Peaks Key") > 2 && Gloves() && Traction();
    }
    public static bool CheckAccess257() //Factory - Southwest Nine Bulb Puzzle
    {
        return FactoryAccess() && Weapon();
    }
    public static bool CheckAccess258() //Factory - Map Arena Chest
    {
        return FactoryAccess() && Weapon() && GetTier("Factory Pass") > 8;
    }
    public static bool CheckAccess259() //Factory - East Nine Bulb Puzzle
    {
        return FactoryAccess() && Weapon() && Gloves();
    }
    public static bool CheckAccess260() //Factory - Twin Laser Four Block Puzzle
    {
        return FactoryAccess() && Gloves();
    }
    public static bool CheckAccess261() //Factory - Center Room Right Chest
    {
        return FactoryAccess() && Charge1();
    }
    public static bool CheckAccess262() //Factory - Center Room Left Chest
    {
        return FactoryAccess() && Charge1() && Boomerang2() && GetTier("Factory Pass") > 8;
    }
    public static bool CheckAccess263() //Factory - Northwest Nine Bulb Puzzle
    {
        return FactoryAccess() && (ShortRange() && Gloves() || Boomerang2()) && GetTier("Factory Pass") > 7;
    }
    public static bool CheckAccess264() //Factory - Flying Spike and Electricity Room Chest
    {
        return FactoryAccess() && (ShortRange() && Gloves() || Boomerang2()) && GetTier("Factory Pass") > 8;
    }
    public static bool CheckAccess265() //Factory - Flying Spike and Electricity Room Inner Salvage
    {
        return FactoryAccess() && GetTier("Factory Pass") > 8 && Swim1() && (Boomerang2() || Grapple2()) && Gloves();
    }
    public static bool CheckAccess266() //Factory - Flying Spike and Electricity Room Outer Salvage
    {
        return FactoryAccess() && GetTier("Factory Pass") > 8 && Swim1() && Boomerang2() && Gloves();
    }
    public static bool CheckAccess267() //Factory - Bat Room
    {
        return FactoryAccess() && ShortRange() && Gloves() && GetTier("Factory Pass") > 8;
    }
    public static bool CheckAccess268() //Factory - Buried Under a Block
    {
        return FactoryAccess();
    }
    public static bool CheckAccess269() //Factory - Laser Dig Maze South Chest
    {
        return FactoryAccess() && Gloves() && GetTier("Factory Pass") > 7;
    }
    public static bool CheckAccess270() //Factory - Laser Dig Maze North Chest
    {
        return FactoryAccess() && Gloves() && GetTier("Factory Pass") > 7;
    }
    public static bool CheckAccess271() //Factory - Laser Obstruction Room
    {
        return FactoryAccess() && Gloves() && GetTier("Factory Pass") > 7 && Weapon();
    }
    public static bool CheckAccess272() //Factory - East Boomerang Chest
    {
        return FactoryAccess() && Gloves() && GetTier("Factory Pass") > 8 && Weapon();
    }
    public static bool CheckAccess273() //Factory - Northwest Knight
    {
        return FactoryAccess() && Gloves() && Boomerang2() && Charge1() && Vision();
    }
    public static bool CheckAccess274() //Factory - Northwest Chest
    {
        return FactoryAccess() && Gloves() && Boomerang2() && Charge1() && Vision();
    }
    public static bool CheckAccess275() //Factory - Southeast Chest
    {
        return FactoryAccess() && Gloves() && Boomerang1();
    }
    public static bool CheckAccess276() //Factory - West Boomerang Chest
    {
        return FactoryAccess() && GetTier("Factory Pass") > 8 && Boomerang2();
    }
    public static bool CheckAccess277() //Factory - North of West Boomerang Chest
    {
        return FactoryAccess() && Boomerang2() && GetTier("Factory Pass") > 8;
    }
    public static bool CheckAccess278() //Factory - Boss Chest
    {
        return FactoryAccess() && Boomerang2() && GetTier("Factory Pass") > 8;
    }
    public static bool CheckAccess279() //Factory - Gamer Room
    {
        return FactoryAccess() && Boomerang2() && GetTier("Factory Pass") > 8 && Balloon1();
    }
    public static bool CheckAccess280() //South Overworld - Giant Slime
    {
        return TownAccess() && Weapon();
    }
    public static bool CheckAccess281() //West Overworld - Giant Enemy Crab
    {
        return MountainLowerAccess() && Weapon();
    }
    public static bool CheckAccess282() //Mountain - Giant Penguin
    {
        return MountainAccess() && Weapon() && Traction();
    }
    public static bool CheckAccess283() //Dark Woods - Giant Reaper
    {
        return DeepWoodsAccess();
    }
    public static bool CheckAccess284() //Swamp - Giant Crocodile
    {
        return SwampAccess() && Swim1() && Weapon();
    }
    public static bool CheckAccess285() //Ruined City - Giant Dark Slime
    {
        return RuinCityFullAccess() && Weapon();
    }
    public static bool CheckAccess286() //Southwest Overworld - Caerbannog
    {
        return TownAccess() && Swim1() && Weapon();
    }
    public static bool CheckAccess287() //Ziggurat - Pillar Chest
    {
        return ZigguratAccess();
    }
    public static bool CheckAccess288() //Ziggurat - Lower Lens Chest
    {
        return ZigguratAccess() && (EasyFlight() || Swim1());
    }
    public static bool CheckAccess289() //Ziggurat - West of Entrance
    {
        return ZigguratAccess() && EasyFlight();
    }
    public static bool CheckAccess290() //Ziggurat - Maze Southwest Chest
    {
        return ZigguratAccess() && EasyFlight() && Vision();
    }
    public static bool CheckAccess291() //Ziggurat - Map Chest
    {
        return ZigguratAccess() && EasyFlight() && Vision();
    }
    public static bool CheckAccess292() //Ziggurat - Maze Hidden Room Left Chest
    {
        return ZigguratAccess() && EasyFlight() && Charge1() && Vision();
    }
    public static bool CheckAccess293() //Ziggurat - Maze Hidden Room Right Chest
    {
        return ZigguratAccess() && EasyFlight() && Charge1() && Vision();
    }
    public static bool CheckAccess294() //Ziggurat - Very Bottom
    {
        return LowerZigguratAccess();
    }
    public static bool CheckAccess295() //Ziggurat - Second Floor Rubble Northeast Chest
    {
        return ZigguratAccess() && Lens() && Charge1();
    }
    public static bool CheckAccess296() //Ziggurat - Second Floor Rubble Southeast Chest
    {
        return ZigguratAccess() && Lens() && Charge1();
    }
    public static bool CheckAccess297() //Ziggurat - Button Code
    {
        return ZigguratAccess() && Vision() && GetTier("Ziggurat Key") > 0;
    }
    public static bool CheckAccess298() //Ziggurat - Upper Lens Chest
    {
        return ZigguratAccess() && Lens() && Weapon() && GetTier("Ziggurat Key") > 1;
    }
    public static bool CheckAccess299() //Ziggurat - Top Floor Left
    {
        return ZigguratAccess() && Lens() && GetTier("Ziggurat Key") > 0 && Weapon();
    }
    public static bool CheckAccess300() //Ziggurat - Top Floor Right
    {
        return ZigguratAccess() && Lens() && GetTier("Ziggurat Key") > 0 && Weapon();
    }
    public static bool CheckAccess301() //Ziggurat - Warp Secret
    {
        return ZigguratAccess();
    }
    public static bool CheckAccess302() //Ziggurat - Warp Wall Secret
    {
        return ZigguratAccess();
    }
    public static bool CheckAccess303() //Ruined City - Balloon Table
    {
        return (RuinCityBasicAccess() && (EasyFlight() || Charge1())) || RuinCityMidAccess();
    }
    public static bool CheckAccess304() //Ruined City - Treadmill
    {
        return RuinCityGymLowerAccess();
    }
    public static bool CheckAccess305() //Ruined City - East Roofless Hut
    {
        return RuinCityEastRooflessAccess();
    }
    public static bool CheckAccess306() //Ruined City - South Roofless Hut
    {
        return RuinCitySouthRooflessAccess();
    }
    public static bool CheckAccess307() //Ruined City - Rooftop Bar
    {
        return RuinCityMarketAccess();
    }
    public static bool CheckAccess308() //Ruined City - Vitamins Table
    {
        return RuinCityGymUpperAccess();
    }
    public static bool CheckAccess309() //Ruined City - Hidden East Island
    {
        return RuinCityMidAccess() && Vision();
    }
    public static bool CheckAccess310() //Ruined City - Southeast Cliffside
    {
        return RuinCityBasicAccess() && Gloves();
    }
    public static bool CheckAccess311() //Ruined City - Hidden Southeast Island
    {
        return RuinCityMidAccess() && Gloves() && Vision();
    }
    public static bool CheckAccess312() //Ruined City - Supermarket Block Puzzle
    {
        return RuinCityMarketAccess();
    }
    public static bool CheckAccess313() //Ruined City - Boulder House
    {
        return RuinCityBoulderHouse1Access();
    }
    public static bool CheckAccess314() //Ruined City - West Waterways
    {
        return RuinCityFullWaterAccess();
    }
    public static bool CheckAccess315() //Ruined City - Close West Island
    {
        return RuinCityFullAccess();
    }
    public static bool CheckAccess316() //Ruined City - Drop Through Roof
    {
        return RuinCityMidAccess();
    }
    public static bool CheckAccess317() //Ruined City - Atop the Tallest Right Side Building
    {
        return RuinCityFullAccess();
    }
    public static bool CheckAccess318() //Ruined City - Library
    {
        return RuinCityLibraryAccess();
    }
    public static bool CheckAccess319() //Chest by Giant Dark Slime
    {
        return RuinCityLibraryAccess();
    }
    public static bool CheckAccess320() //Library Arena
    {
        return RuinCityLibraryAccess() && Weapon();
    }
    public static bool CheckAccess321() //Ruined City - West Island Lens Chest
    {
        return RuinCityFullAccess() && Lens();
    }
    public static bool CheckAccess322() //Ruined City - Fox Island
    {
        return RuinCityFullAccess() && Gloves() && Boomerang1();
    }
    public static bool CheckAccess323() //Ruined City - House with Lens Chest
    {
        return RuinCityHouseWithLensChestAccess() && Lens();
    }
    public static bool CheckAccess324() //Ruined City - Map Chest
    {
        return RuinCityMidAccess();
    }
    public static bool CheckAccess325() //Ruined City - House with Rubble Accessible Through Sewers
    {
        return RuinCityBoulderHouse2Access();
    }
    public static bool CheckAccess326() //Forge - Blocks and Steam
    {
        return ForgeAccess() && Gloves();
    }
    public static bool CheckAccess327() //Forge - Magma Frog Arena
    {
        return ForgeAccess() && Balloon1() && Weapon();
    }
    public static bool CheckAccess328() //Forge - Four Boulders
    {
        return ForgeAccess() && Balloon1() && GetTier("Forge Pass") > 1;
    }
    public static bool CheckAccess329() //Forge - Fan Zig Zag South Ches
    {
        return ForgeAccess() && Balloon2();
    }
    public static bool CheckAccess330() //Forge - Fan Zig Zag North Chest
    {
        return ForgeAccess() && Balloon2();
    }
    public static bool CheckAccess331() //Forge - Bottom Floor Northeast
    {
        return ForgeAccess() && Balloon1() && Gloves() && GetTier("Forge Pass") > 1;
    }
    public static bool CheckAccess332() //Forge - Bottom Floor South Right Side
    {
        return ForgeAccess() && Boomerang2() && Balloon1() && Gloves() && GetTier("Forge Pass") > 1;
    }
    public static bool CheckAccess333() //Forge - Bottom Floor South Left Side
    {
        return ForgeAccess() && Boomerang2() && ((Gloves() && Balloon2() && GetTier("Forge Pass") > 1) || (Balloon1() && GetTier("Forge Pass") > 10));
    }
    public static bool CheckAccess334() //Forge - Around the Hollow Tower
    {
        return ForgeAccess() && Balloon2() && Boomerang2() && GetTier("Forge Pass") > 1;
    }
    public static bool CheckAccess335() //Forge - Bottom Floor Southeast
    {
        return ForgeAccess() && Balloon1() && Gloves() && GetTier("Forge Pass") > 1;
    }
    public static bool CheckAccess336() //Forge - Beneath the Centurion Blueprints
    {
        return ForgeAccess() && Charge1() && Balloon1() && Vision();
    }
    public static bool CheckAccess337() //Forge - Piston Trove Upper Chest
    {
        return ForgeAccess() && Balloon2() && GetTier("Forge Pass") > 10;
    }
    public static bool CheckAccess338() //Forge - Piston Trove Middle Chest
    {
        return ForgeAccess() && Balloon2() && GetTier("Forge Pass") > 10;
    }
    public static bool CheckAccess339() //Forge - Piston Trove Lower Chest
    {
        return ForgeAccess() && Balloon2() && GetTier("Forge Pass") > 10;
    }
    public static bool CheckAccess340() //Forge - Run Against the Fans
    {
        return ForgeAccess() && Balloon1() && GetTier("Forge Pass") > 6;
    }
    public static bool CheckAccess341() //Forge - Button and Boomerang
    {
        return ForgeAccess() && Balloon1() && Boomerang2() && GetTier("Forge Pass") > 7;
    }
    public static bool CheckAccess342() //Forge - 2 Block Drop Down
    {
        return ForgeAccess() && Balloon1() && GetTier("Forge Pass") > 4;
    }
    public static bool CheckAccess343() //Forge - 4 Block Drop Down
    {
        return ForgeAccess() && Balloon2() && GetTier("Forge Pass") > 9;
    }
    public static bool CheckAccess344() //Forge - Northwest Piston Gauntlet
    {
        return ForgeAccess() && Balloon2() && GetTier("Forge Pass") > 9;
    }
    public static bool CheckAccess345() //Forge - Double Drop Down
    {
        return ForgeAccess() && Balloon1() && GetTier("Forge Pass") > 9;
    }
    public static bool CheckAccess346() //Forge - Multikey Trove Upper Chest
    {
        return ForgeAccess() && Balloon1() && GetTier("Forge Pass") > 10;
    }
    public static bool CheckAccess347() //Forge - Multikey Trove Middle Chest
    {
        return ForgeAccess() && Balloon1() && GetTier("Forge Pass") > 10;
    }
    public static bool CheckAccess348() //Forge - Multikey Trove Lower Chest
    {
        return ForgeAccess() && Balloon1() && GetTier("Forge Pass") > 10;
    }
    public static bool CheckAccess349() //Forge - Block Sentry's Buttons
    {
        return ForgeAccess() && Balloon1() && GetTier("Forge Pass") > 10;
    }
    public static bool CheckAccess350() //Forge - Boss Left Chest
    {
        return ForgeAccess() && Balloon2() && Weapon() && GetTier("Forge Pass") > 10;
    }
    public static bool CheckAccess351() //Forge - Boss Right Chest
    {
        return ForgeAccess() && Balloon2() && Weapon() && GetTier("Forge Pass") > 10;
    }
    public static bool CheckAccess352() //Final Dungeon - Jail Crystal
    {
        return FinalDungeonAccess() && Balloon1();
    }
    public static bool CheckAccess353() //Final Dungeon - Rubble Path
    {
        return FinalDungeonAccess() && Balloon1() && Charge1();
    }
    public static bool CheckAccess354() //Final Dungeon - Southwest Big Chest
    {
        return FinalDungeonAccess() && Balloon1() && Weapon() && GetTier("Water Treasure") > 0;
    }
    public static bool CheckAccess355() //Final Dungeon - Northeast Big Chest
    {
        return FinalDungeonAccess() && Balloon1() && Weapon() && GetTier("House Treasure") > 0;
    }
    public static bool CheckAccess356() //Final Dungeon - Northwest Big Chest
    {
        return FinalDungeonAccess() && Balloon1() && Weapon() && GetTier("Ice Treasure") > 0;
    }
    public static bool CheckAccess357() //Final Dungeon - Southeast Big Chest
    {
        return FinalDungeonAccess() && Balloon1() && Weapon() && GetTier("Lightning Treasure") > 0;
    }
    public static bool CheckAccess358() //Final Dungeon - Library
    {
        return FinalDungeonAccess() && Balloon1() && Weapon();
    }
    public static bool CheckAccess359() //Final Dungeon - Swimming Pool
    {
        return FinalDungeonAccess() && Balloon1() && Weapon() && Swim1();
    }
    public static bool CheckAccess360() //Final Dungeon - Left Drop Down Chest
    {
        return FinalDungeonAccess() && Balloon1() && Weapon();
    }
    public static bool CheckAccess361() //Final Dungeon - Right Drop Down Chest
    {
        return FinalDungeonAccess() && Balloon1() && Weapon() && (Vision() || Balloon2());
    }
    public static bool CheckAccess362() //Final Dungeon - Final Boss Reward
    {
        return false;//For now this will always return false as I am keeping it vanilla, but in case I change it later, it would be: FinalDungeonAccess() && Balloon1() && Gloves() && Weapon() && Boots1();
    }
    public static bool CheckAccess363() //Swamp - Shallow Maze Island
    {
        return SwampAccess() && Balloon1();
    }
    public static bool CheckAccess364() //Water Dungeon - Double Salvage 1
    {
        return WaterDungeonAccess() && Weapon() && Swim1() && Vision();
    }
    public static bool CheckAccess365() //Water Dungeon - Double Salvage 2
    {
        return WaterDungeonAccess() && Weapon() && Swim1() && Vision();
    }
    public static bool CheckAccess366() //Sprawling Cave - Chest near Village Cave
    {
        return SprawlingCaveAccess() || TownCaveAccess() && Charge1() && Vision();
	}
	public static bool CheckAccess367() //Dark Woods - Grotto Salvage
	{
        return ForestGrottoAccess() && Swim1() && Vision();
	}
    public static bool CheckAccess368() //Mountain - Overlooking Capybaras
    {
        return MountainAccess() && Balloon1();
    }
    public static bool CheckAccess369() //Path to Ruined City - Buried Under Sentinel
    {
        return BannerHallAccess() && Vision() && Gloves();    
    }
    public static bool CheckAccess370() //Snow Potion Slope - Buried Treasure 2
    {
		return (GetTier("Snow Potion") > 0 && (!WarpShuffle() && PotionShopAccess() && SnowMushroomAccess())) && Gloves() && AllowMajorSecrets();
	}
	public static bool CheckAccess371() //Snow Potion Slope - Buried Treasure 3
	{
		return (GetTier("Snow Potion") > 0 && (!WarpShuffle() && PotionShopAccess() && SnowMushroomAccess())) && Gloves() && AllowMajorSecrets();
	}
	public static bool CheckAccess372() //Snow Potion Slope - Buried Treasure 4
	{
		return (GetTier("Snow Potion") > 0 && (!WarpShuffle() && PotionShopAccess() && SnowMushroomAccess())) && Gloves() && AllowMajorSecrets();
	}
	public static bool CheckAccess373() //Snow Potion Slope - Buried Treasure 5
	{
		return (GetTier("Snow Potion") > 0 && (!WarpShuffle() && PotionShopAccess() && SnowMushroomAccess())) && Gloves() && AllowMajorSecrets();
	}
    public static bool CheckAccess374() //Forge - Bottom Floor Northwest
    {
        return ForgeAccess() && Grapple1() && GetTier("ForgePass") > 9;
    }
    public static bool CheckAccess375() //Swamp - Behind the Waterfall
    {
        return SwampCaveAccess() && Swim1();
    }
    public static bool CheckAccess376() //Near Starting Cave - Unmarked Dig Spot Near Boulder
    {
        return TownAccess() && EasyFlight() && Swim1() && Gloves() && AllowMajorSecrets();
    }
    public static bool CheckAccess377() //Mountain - Unmarked Dig Spot on Clifftop Behind Smith Shop
    {
        return MountainAccess() && Balloon1() && Gloves() && AllowMajorSecrets();
    }
    public static bool CheckAccess378() //Mountain - Unmarked Hidden Cave North of Waypoint
	{
        return MountainAccess() && Charge1() && Balloon1() && AllowMajorSecrets();
	}
	public static bool CheckAccess379() //Dream World - Hidden Chest Next to Arrow Math Stone
	{
		return GetTier("Snow Potion") > 0 && Lens() && Balloon1() && AllowMajorSecrets();
	}
	public static bool CheckAccess380() //Dream World - Hidden Chest Northwest of Arrow Math Ston
	{
		return GetTier("Snow Potion") > 0 && Lens() && Balloon1() && AllowMajorSecrets();
	}
	public static bool CheckAccess381() //Dream World - Buried North of Arrow Math Stone
	{
		return GetTier("Snow Potion") > 0 && Lens() && Balloon1() && AllowMajorSecrets() && Boomerang1() && Gloves();
	}
	#endregion individual checks
}