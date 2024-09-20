using System;
using System.Collections.Generic;
using static ItemCheatSheet;
using static CheckClass;
using System.Reflection;
using static MasterKeyRandomizer.MKLogger;
using UnityEngine;
using System.Linq;
using System.IO;
using MonoMod.Utils;


public class Seed
{
	public const long BASE_ID = 12011993;
    public static List<CheckData> Checks = new();
    public static System.Random rand = new();
    public static bool DebugCheckListings = true;
    public static int seed;
    public static List<string> ProgressionNames = new List<string> { "Weapon Upgrade",
            "Wallet Upgrade", "Lantern", "Grappling Hook", "Boomerang",
            "Health Container", "Crystal", "Half Container", "Triple Crystal",
            "Light Bulb", "Water Dungeon Key", "Snowy Peaks Key", "Factory Pass",
            "Ziggurat Key", "Forge Pass", "Water Treasure", "House Treasure",
            "Ice Treasure", "Lightning Treasure", "Gear Piece 1", "Gear Piece 2",
            "Gear Piece 3", "Vitamins", "Boots", "Gloves", "Lens", "Balloon", "Snorkel",
            "Ruins Warp", "Woods Potion", "Snow Potion" };
    public static void GenerateSeed()
    {
        string saveSlot = GameObject.FindGameObjectWithTag("Player").GetComponent<FoxMove>().saveslot;
        if (PlayerPrefs.GetInt(saveSlot + "maxArgent", 0) == 0)
		{
            if (PlayerPrefs.GetInt("DoSeedPreset") == 0)
                seed = PlayerPrefs.GetInt("SeedPreset", 34);
            else
                seed = rand.Next();
			rand = new System.Random(seed);
			LogInfo("Beginning Generation...");
			LogInfo(CheckLookup.Locations.Count.ToString());
            Checks.AddRange(CheckLookup.Locations.Values);
			DirectoryInfo spoilerPath = Directory.CreateDirectory(Application.persistentDataPath + "\\SpoilerLogs");
			LogInfo(Checks.Count.ToString());
            if (PlayerPrefs.GetInt(saveSlot + "TrapRate", 0) == -1)
            {
                PlayerPrefs.SetInt(saveSlot + "TrapRate", rand.Next(1, 100));
            }
            ItemRandomizer.PopulatePrecollected();
			ItemRandomizer.AssumeFill(Checks);
			string path = Application.persistentDataPath + "\\SpoilerLogs\\SpoilerLogForSeed" + seed + ".txt";
            StreamWriter SpoilerLog = new StreamWriter(path, true);
            SpoilerLog.WriteLine("Spoiler Log for Seed " + seed.ToString() + "\n------------------------------------------\n");
            SpoilerLog.WriteLine("Seed Settings: \n\nStarting Location - " + (PlayerPrefs.GetInt("StartLogic") == 0 ? "Starting Cave" : "Town"));
            SpoilerLog.WriteLine("Lantern Logic - " + (PlayerPrefs.GetInt("LanternLogic") == 0 ? "Need in the Dark" : "Minimum"));
			SpoilerLog.WriteLine("Lens Logic - " + (PlayerPrefs.GetInt("LensLogic") == 0 ? "Visibility Required" : "Minimum"));
			SpoilerLog.WriteLine("Boots Logic - " + (PlayerPrefs.GetInt("BootsLogic") == 0 ? "Ice Traction" : "Minimum"));
			SpoilerLog.WriteLine("Warp Shuffle - " + (PlayerPrefs.GetInt("WarpShuffle") == 0 ? "On" : "Off"));
			SpoilerLog.WriteLine("Dream Pedestal - " + (PlayerPrefs.GetInt("DreamLogic") == 0 ? "In Logic" : "Vanilla"));
            SpoilerLog.WriteLine("Secret Logic - " + (PlayerPrefs.GetInt("SecretLogic") == 0 ? "All in Logic" : "Exclude Sneakiest"));
            SpoilerLog.WriteLine("Traps - " + (PlayerPrefs.GetInt(saveSlot + "TrapRate", 0) > 0 ? "Enabled" : "Disabled"));
            if (PlayerPrefs.GetInt(saveSlot + "TrapRate", 0) > 0)
            {
                SpoilerLog.WriteLine("Percentage of Traps in Junk - " + PlayerPrefs.GetInt(saveSlot + "TrapRate", 0) + "%");
                SpoilerLog.WriteLine("Trap Intensity - " + PlayerPrefs.GetInt(saveSlot + "TrapIntensity", 3) + "/10");
            }
            SpoilerLog.WriteLine("\nLocations and their items\n------------------------------------------\n");
            foreach (CheckData check in CheckLookup.Locations.Values)
            {
                try
                {
                    PlayerPrefs.SetString(saveSlot + check.LocationName, check.CheckItem.Name);
                    //LogInfo($"{check.CheckItem.Name}");
                }
                catch (NullReferenceException)
                {
                    LogError("There was an error with the item at " + check.LocationName + ".");
                }
                try
                {
                    SpoilerLog.WriteLine(check.LocationName + " has " + check.CheckItem.Name);
                }
                catch (Exception)
                {
                    LogError(check.LocationName + " has <ERROR>");
                    SpoilerLog.WriteLine(check.LocationName + " has <ERROR>");
                }
            }
			foreach (KeyValuePair<string, int> keyValuePair in AccessChecker.Access.ToList())
				AccessChecker.Access[keyValuePair.Key] = 0;

            //print to the spoiler log the intended path for collection
            SpoilerLog.WriteLine("\n\nLogical Path\n------------------------------------------\n");
            int sphere = 0;
            List<CheckData> SphereStash = new List<CheckData>();
            while (true)
            {
                //increase the sphere count by 1
                sphere++;
                foreach (CheckData check in Checks.ToList())
				{
                    //iterate through every check and see if it can be collected with what we have.
					string checkField = "CheckAccess" + check.LocationID.ToString();
					MethodInfo CheckAccessMethod = typeof(CheckIndex).GetMethod(checkField, BindingFlags.Public | BindingFlags.Static);
					bool canAccess = (bool)CheckAccessMethod.Invoke(null, null);
                    if (canAccess)
                    {
                        //The item is accessible, add it to the spoiler log, and to the stash of items we collected in this Sphere.
                        SphereStash.Add(check);
                        Checks.Remove(check);
                    }
				}

                if (SphereStash.Count == 0) 
                {
                    //We can no longer collect any more items in this seed.
                    SpoilerLog.WriteLine("\n----------------------------------------------------------------------------------------------------------------------------\nEnd of Logic. Remaining checks (which should only be out of logic junk, or hardcoded locations) are:\n");
                    foreach (CheckData check in Checks.ToList())
					{
						//Hopefully we're in this block because there are either no more items remaining, or the ones that are are at checks excluded from the settings.
						//just in case we aren't, we're going to list every item we can't collect.
						try
						{
							SpoilerLog.WriteLine(check.CheckItem.Name + " located at " + check.LocationName);
						}
						catch (Exception)
						{
							LogError(check.LocationName + " has <ERROR>");
							SpoilerLog.WriteLine("<ERROR> located at " + check.LocationName);
						}
					}
                    break;
                }
                else
				{
					SpoilerLog.WriteLine("\nSphere " + sphere.ToString() + "\n---------");
					foreach (CheckData check in SphereStash.ToList())
					{
                        try
                        {
                            //we have obtained all the items available in this sphere, now we add them to the spoiler log's inventory.
                            if (ProgressionNames.Contains(check.CheckItem.Name))
                                ItemRandomizer.AccessAdd(check.CheckItem.Name, AccessChecker.Access);

                            SpoilerLog.WriteLine("Collect " + check.CheckItem.Name + " from " + check.LocationName);
                        }
                        catch (Exception)
                        {
                            LogError(check.LocationName + " has <ERROR>");
							SpoilerLog.WriteLine("Collect <ERROR> from " + check.LocationName);
						}
					SphereStash.Remove(check);

				    }
                }
            }
			SpoilerLog.Flush();
            PlayerPrefs.SetInt(saveSlot + "randoSeed", seed);
			LogInfo("A new randomized world has been made with seed " + seed + ".");
            if (PlayerPrefs.GetInt("StartLogic") == 1)
            {
                PlayerPrefs.SetString(saveSlot + "respawn", "village");
                PlayerPrefs.SetInt(saveSlot + "IntroFinie", 1);
            }
            else
            {
                PlayerPrefs.SetInt(saveSlot + "IntroFinie", 0);
            }
        }
        else
        {
            seed = PlayerPrefs.GetInt(saveSlot + "randoSeed", 0);

			foreach (CheckData check in CheckLookup.Locations.Values)
            {
				try
				{
					check.CheckItem = ItemCheatSheet.GetData(PlayerPrefs.GetString(saveSlot + check.LocationName, "Error"));
				}
				catch (NullReferenceException)
				{
					LogError("There was an error with the item at " + check.LocationName + ".");
				}
			}
            LogInfo("This world has already been randomized with seed " + seed + ".");
        }
    }

    private static ItemData GetRandomItem(int seed)
    {
        return ItemLookup.TranslatedItemNames.ElementAt(rand.Next(0, ItemLookup.TranslatedItemNames.Count)).Value;
    }
    public class AccessChecker
    {
        public static Dictionary<string, int> Access = new()
        {
            { "Weapon Upgrade", 0 },
            { "Wallet Upgrade", 0 },
            { "Health", 0 },
            { "Gloves", 0 },
            { "Grappling Hook", 0 },
            { "Boomerang", 0 },
            { "Vitamins", 0 },
            { "Boots", 0 },
            { "Balloon", 0 },
            { "Lantern", 0 },
            { "Lens", 0 },
            { "Crystal", 0 },
            { "Snorkel", 0 },
            { "Water Treasure", 0 },
            { "House Treasure", 0 },
            { "Ice Treasure", 0 },
            { "Lightning Treasure", 0 },
            { "Water Dungeon Key", 0 },
            { "Light Bulb", 0 },
            { "Snowy Peaks Key", 0 },
            { "Factory Pass", 0 },
            { "Ziggurat Key", 0 },
            { "Forge Pass", 0 },
            { "Woods Potion", 0 },
            { "Snow Potion", 0 },
            { "Ruins Warp", 0 },
            { "Start Warp", 0 },
            { "Gear Piece 1", 0 },
            { "Gear Piece 2", 0 },
            { "Gear Piece 3", 0 },
            { "Armor", 0 },
            { "Half Container", 0 },
            { "Health Container", 0 },
            { "Triple Crystal", 0}
        };
        public static Dictionary<string, bool> AccessCache = new()
        {
            { "StartAccess", false },
            { "RuinsWarpBackdoor", false },
            { "TownAccess", false },
            { "WaterWayPointAccess", false },
            { "Charge1", false },
            { "Charge2", false },
            { "BushCut", false },
            { "Weapon", false },
            { "EasyFlight", false },
            { "LongFlight", false },
            { "BasicBurn", false },
            { "BigBurn", false },
            { "ShortRange", false },
            { "LongRange", false },
            { "BasicShopAccess", false },
            { "FancyShopAccess", false },
            { "GymAccess", false },
            { "LeftDuplexAccess", false },
            { "TwoFloorHouseAccess", false },
            { "TwoFloorHouseBalconyAccess", false },
            { "BreakoutCaveAccess", false },
            { "AbandonedHouseAccess", false },
            { "PongCaveAccess", false },
            { "MountainSpikeMazeAccess", false },
            { "MountainCaveSpikeMazeAccess", false },
            { "BushCaveAccess", false },
            { "WaterDungeonAccess", false },
            { "TownDwarfAccess", false },
            { "TownCaveAccess", false },
            { "SewerAccess", false },
            { "ThreeFireCaveAccess", false },
            { "WaterDwarfAccess", false },
            { "NorthOfWaterDungeonAccess", false },
            { "RabbitCaveAccess", false },
            { "IceSpikeMazeAccess", false },
            { "WindCaveAccess", false },
            { "SwampBackDoorAccess", false },
            { "SwampAccess", false },
            { "WaterWayPointCaveAccess", false },
            { "GrottoEastOfWaterWayPointAccess", false },
            { "NorthOfLakeAccess", false },
            { "SwampCaveAccess", false },
            { "DragonCaveAccess", false },
            { "DrakeCaveAccess", false },
            { "HauntWayPointAccess", false },
            { "HauntedHouseAccess", false },
            { "ForestGrottoAccess", false },
            { "EightRoomAccess", false },
            { "EightRoomLensAccess", false },
            { "EightRoomZigguratAccess", false },
            { "EightRoomUnknownAccess", false },
            { "EightRoomKickstarterAccess", false },
            { "EightRoomBannerAccess", false },
            { "EightRoomDwarfAccess", false },
            { "ForestDwarfAccess", false },
            { "SwampDwarfAccess", false },
            { "ZombieGrottoAccess", false },
            { "ScubaShopVicinityAccess", false },
            { "ScubaShopAccess", false },
            { "MountainCaveAccessLower", false },
            { "MountainCaveAccess", false },
            { "MountainLowerAccess", false },
            { "MountainAccess", false },
            { "MountainDwarfAccess", false },
            { "WesternWaterfallCaveAccess", false },
            { "SmithAccess", false },
            { "ZigguratAccess", false },
            { "LowerZigguratAccess", false },
            { "EarlyWoodsAccess", false },
            { "DeepWoodsAccess", false },
            { "TPToWoodsAccess", false },
            { "SnowyPeaksAccess", false },
            { "ColosseumAccess", false },
            { "BannerHallAccess", false },
            { "FactoryAccess", false },
            { "StartWarpAccess", false },
            { "SprawlingCaveLakeAccess", false },
            { "SprawlingCaveAccess", false },
            { "SprawlingCaveZombieAccess", false },
            { "SwampSecretCaveAccess", false },
            { "SwordDreamAccess", false },
            { "NightClubAccess", false },
            { "PotionShopAccess", false },
            { "ForestMushroomAccess", false },
            { "SnowMushroomAccess", false },
            { "SwampIslandGrottoAccess", false },
            { "RuinCityBasicAccess", false },
            { "RuinCityBasicWaterAccess", false },
            { "RuinCityFullWaterAccess", false },
            { "RuinCityMidAccess", false },
            { "RuinCityFullAccess", false },
            { "RuinCityGymLowerAccess", false },
            { "RuinCityGymUpperAccess", false },
            { "RuinCityEastRooflessAccess", false },
            { "RuinCitySouthRooflessAccess", false },
            { "RuinCityMarketAccess", false },
            { "RuinCityLibraryAccess", false },
            { "RuinCityBoulderHouse1Access", false },
            { "RuinCityBoulderHouse2Access", false },
            { "RuinCityHouseWithAccessToWestIslands", false },
            { "RuinCityHouseWithLensChestAccess", false },
            { "ForgeAccess", false },
            { "FinalDungeonAccess", false }
        };
    }
    public static int GetTier(string id)
    {
        AccessChecker.Access.TryGetValue(id, out int value);
        //LogInfo(id + ", " + value.ToString());
        return value;

    }
    public class ItemRandomizer
    {
        //fake items for simulating before generating.
        public static List<string> PrecollectedItems = new List<string>();
		private static readonly string saveSlot = GameObject.FindGameObjectWithTag("Player").GetComponent<FoxMove>().saveslot;

		public static void PopulatePrecollected()
		{
			LogInfo("Generating Items...");
            int junkTotal;
			PrecollectedItems.Clear();
            for (int i = 0; i < 6; i++)
            {
                PrecollectedItems.Add("Weapon Upgrade");
            }
            for (int i = 0; i < 2; i++)
            {
                PrecollectedItems.Add("Lightning Treasure");
                PrecollectedItems.Add("Ice Treasure");
                PrecollectedItems.Add("House Treasure");
                PrecollectedItems.Add("Water Treasure");
                PrecollectedItems.Add("Lens");
                PrecollectedItems.Add("Gloves");
                PrecollectedItems.Add("Boomerang");
                PrecollectedItems.Add("Grappling Hook");
                PrecollectedItems.Add("Balloon");
                PrecollectedItems.Add("Armor");
                PrecollectedItems.Add("Lantern");
                PrecollectedItems.Add("Vitamins");
                PrecollectedItems.Add("Boots");
                PrecollectedItems.Add("Ziggurat Key");
                PrecollectedItems.Add("Clover");
                PrecollectedItems.Add("Magnet");
				PrecollectedItems.Add("Snorkel");
			}
            for (int i = 0; i < 3; i++)
            {
                PrecollectedItems.Add("Water Dungeon Key");
                PrecollectedItems.Add("Snowy Peaks Key");
                PrecollectedItems.Add("Wallet Upgrade");
            }
            for (int i = 0; i < 9; i++)
            {
                PrecollectedItems.Add("Factory Pass");
                PrecollectedItems.Add("Light Bulb");
            }
            for (int i = 0; i < 4; i++)
            {
                PrecollectedItems.Add("Health Container");
            }
            for (int i = 0; i < 57; i++)
                PrecollectedItems.Add("Crystal");
            for (int i = 0; i < 34; i++)
                PrecollectedItems.Add("Half Container");
            for (int i = 0; i < 11; i++)
                PrecollectedItems.Add("Forge Pass");
            PrecollectedItems.Add("Gear Piece 1");
            PrecollectedItems.Add("Gear Piece 2");
            PrecollectedItems.Add("Gear Piece 3");
            PrecollectedItems.Add("Snow Potion");
            PrecollectedItems.Add("Ruins Warp");
            PrecollectedItems.Add("Start Warp");
            PrecollectedItems.Add("Woods Potion");
            PrecollectedItems.Add("Triple Crystal");
            PrecollectedItems.Add("Cheese");
            PrecollectedItems.Add("Overworld Map");
            PrecollectedItems.Add("Water Dungeon Map");
            PrecollectedItems.Add("Haunted House Map");
            PrecollectedItems.Add("Snowy Peaks Map");
            PrecollectedItems.Add("Ruins Map");
            PrecollectedItems.Add("Factory Map");
            PrecollectedItems.Add("Ziggurat Map");
            PrecollectedItems.Add("Forge Map");
            PrecollectedItems.Add("Final Dungeon Map");
            PrecollectedItems.Add("Moster Sword");
            junkTotal = (CheckLookup.Locations.Count - 6) - PrecollectedItems.Count;
            for (int i = 0; i < junkTotal; i++)
            {
                if (i / junkTotal < PlayerPrefs.GetInt(saveSlot + "TrapRate", 0)/100)
                {
                    int trapType = rand.Next(1, 4);
                    if (trapType == 1)
                        PrecollectedItems.Add("Cannon Trap");
                    else if (trapType == 2)
                        PrecollectedItems.Add("Damage Trap");
                    else if (trapType == 3)
                        PrecollectedItems.Add("Spike Trap");
                }
                else if (i % 13 == 0)
                    PrecollectedItems.Add("100 Diamond");
                else if (i % 13 <= 1)
                    PrecollectedItems.Add("50 Gold");
                else if (i % 13 <= 5)
                    PrecollectedItems.Add("20 Bucks");
                else if (i % 13 <= 8)
                    PrecollectedItems.Add("10 Coins");
                else if (i % 13 == 9)
                    PrecollectedItems.Add("5 Coins");
                else if (i % 13 == 10)
                    PrecollectedItems.Add("1 Coin");
                else if (i % 13 == 11)
                    PrecollectedItems.Add("Meat");
                else
                    PrecollectedItems.Add("Super Meat");
            }
        }
        public static void AssumeFill(List<CheckData> ShuffledChecks)
		{
			LogInfo("Beginning Fill...");
			List<CheckData> FilledLocations = new List<CheckData>();                // A list of all the checks we've filled with items
            List<CheckData> EmptyChecks = new();                                    // A list of all the checks that still need items
            List<CheckData> RecollectedChecks = new List<CheckData>();              // A list of checks that we've already collected items from
            Dictionary<string, int> NoAccess = AccessChecker.Access;                // A stat table to track what our current inventory grants us access to.
            Dictionary<string, int> RemainingItems = new Dictionary<string, int>(); // What items we still need to place before this seed is considered generated.
            Dictionary<string, int> JustTheKeys = new Dictionary<string, int>();    // A pool of just the key items that still need to be placed
            EmptyChecks.AddRange(ShuffledChecks);
            LogInfo("EmptyChecks Filled.");
            Dictionary<string, CheckData> HardcodedLocations = new Dictionary<string, CheckData>();
            HardcodedLocations.Clear();
            HardcodedLocations.AddRange(new Dictionary<string, CheckData>()
            {
                { "Coffee", CheckLookup.Locations["Cafe(30.25, -20.25, -1.00)"] },
                { "Apple", CheckLookup.Locations["Pomme(-599.50, 2.50, -1.00)"] },
                { "Cheese", CheckLookup.Locations["Fromage(-634.50, 0.50, -1.00)"] },
                { "Meat", CheckLookup.Locations["REZ(-603.50, 2.50, -1.00)"] },
                { "Super Meat", CheckLookup.Locations["REZ2(-632.50, 0.50, -1.00)"] },
                { "Dark Ore", CheckLookup.Locations["PierreFinal()"] }
            });
            if (PlayerPrefs.GetInt("WarpShuffle") == 1)
            {
                HardcodedLocations.Add("Start Warp", CheckLookup.Locations["CoffreLeyndell(555.50, -48.50, 0.00)"]);
				HardcodedLocations.Add("Ruins Warp", CheckLookup.Locations["CoffreRetour(-8.50, 242.50, 0.00)"]);
				HardcodedLocations.Add("Woods Potion", CheckLookup.Locations["potionForet(-523.50, -18.50, 0.00)"]);
				HardcodedLocations.Add("Snow Potion", CheckLookup.Locations["potionRoute(-521.50, -18.50, 0.00)"]);
                PrecollectedItems.Remove("Ruins Warp");
                PrecollectedItems.Remove("Start Warp");
				PrecollectedItems.Remove("Woods Potion");
				PrecollectedItems.Remove("Snow Potion");
                EmptyChecks.Remove(CheckLookup.Locations["CoffreLeyndell(555.50, -48.50, 0.00)"]);
				EmptyChecks.Remove(CheckLookup.Locations["CoffreRetour(-8.50, 242.50, 0.00)"]);
				EmptyChecks.Remove(CheckLookup.Locations["potionForet(-523.50, -18.50, 0.00)"]);
				EmptyChecks.Remove(CheckLookup.Locations["potionRoute(-521.50, -18.50, 0.00)"]);
			}
            if (PlayerPrefs.GetInt("DreamLogic") == 1)
            {
                HardcodedLocations.Add("Moster Sword", CheckLookup.Locations["DreamSword"]);
                PrecollectedItems.Remove("Moster Sword");
                EmptyChecks.Remove(CheckLookup.Locations["DreamSword"]);
            }

            LogInfo("Cataloging Key Items...");
            foreach (string item in PrecollectedItems.ToList().OrderBy(r => rand.Next()))
            {
                //add progression item to our pool of progression items
                if (ProgressionNames.Contains(item))
                {
                    if (JustTheKeys.ContainsKey(item))
                        JustTheKeys[item]++;
                    else
                        JustTheKeys.Add(item, 1);
                    AccessAdd(item, AccessChecker.Access);
                }
            }

			LogInfo("Placing Hardcoded items...");
			//first, we place the hardcoded vanilla items.
			foreach (KeyValuePair<string, CheckData> pair in HardcodedLocations)
            {
                EmptyChecks.Remove(pair.Value);
                pair.Value.CheckItem = ItemCheatSheet.GetData(pair.Key);
			}

			int iterationNumber = 0;

			LogInfo("Beginning randomization of Key items");
			//BEGIN RANDOMIZATION
			while (true) {
                //Place the progression items
                PrecollectedItems.ToList().OrderBy(r => rand.Next());
                
                foreach (string item in PrecollectedItems.ToList().OrderBy(r => rand.Next()))
                {
                    iterationNumber++;
					int emptyAccessesRemaining = 0;
                    int startCount = 0;
					//pick an item from our full set of items
					if (JustTheKeys.Keys.Contains(item))
                    {
                        //clear and make a new log of what items we still need to place
                        foreach (KeyValuePair<string, int> keyValuePair in NoAccess.ToList())
                            AccessChecker.Access[keyValuePair.Key] = 0;

						RemainingItems.Clear();
						foreach (KeyValuePair<string, int> unplacedItem in JustTheKeys.ToList())
                        {
                            RemainingItems.Add(unplacedItem.Key, unplacedItem.Value);
                            for (int i = 0; i < unplacedItem.Value; i++)
                                AccessAdd(unplacedItem.Key, AccessChecker.Access);
                            startCount += unplacedItem.Value;
						}
						//LogInfo(startCount.ToString() + " unplaced items.");

						//reset our checks for collection
						AccessSubtract(item, AccessChecker.Access);
						foreach (string location in AccessChecker.AccessCache.Keys.ToList())
                            AccessChecker.AccessCache[location] = false;

                        startCount = 0;
                        foreach (int count in RemainingItems.Values.ToList())
                            startCount += count;
                        emptyAccessesRemaining = EmptyChecks.Count;
						//find a location for the current item
						foreach (CheckData check in EmptyChecks.ToList().OrderBy(r => rand.Next()))
						{
							string checkField = "CheckAccess" + check.LocationID.ToString();
                            MethodInfo CheckAccessMethod = typeof(CheckIndex).GetMethod(checkField, BindingFlags.Public | BindingFlags.Static);
							bool canAccess = (bool)CheckAccessMethod.Invoke(null, null);
							emptyAccessesRemaining--;

                            //we've found an empty check that we can access with our current inventory. We can place the current item here.
                            if (canAccess && (!HardBlacklist.ContainsKey(item) || !HardBlacklist[item].Contains(check.LocationID)) )
                            {
                                //LogInfo("Placing item at check " + check.LocationID.ToString());
                                //Time to place the item!
                                check.CheckItem = ItemCheatSheet.GetData(item);
                                FilledLocations.Add(check);
                                EmptyChecks.Remove(check);
                                PrecollectedItems.Remove(item);
								JustTheKeys[item]--;
								if (JustTheKeys[item] == 0)
									JustTheKeys.Remove(item);
                                break;
                            }
                            //LogInfo("Can't place here!");
						}
						//LogInfo(emptyAccessesRemaining.ToString() + " empty accesses remaining, " + JustTheKeys.Count.ToString() + " key items remaining.");

						//Wuh oh! There's still key items to place, but we can't access any more empty checks!
						if (emptyAccessesRemaining == 0 && JustTheKeys.Count != 0)
                        {
							//First we have to put our picked up item back in our inventory
							AccessAdd(item, AccessChecker.Access);

                            //Now we have to pick up a key item that we still have access to and place it elsewhere!
                            foreach (CheckData check in FilledLocations.ToList())
                            {
                                string checkField = "CheckAccess" + check.LocationID.ToString();
                                MethodInfo CheckAccessMethod = typeof(CheckIndex).GetMethod(checkField, BindingFlags.Public | BindingFlags.Static);
                                bool canAccess = (bool)CheckAccessMethod.Invoke(null, null);

                                //we've found a filled check that we can access with our current inventory. We can take this item and start again.
                                if (canAccess)
                                {
                                    //LogInfo("Removing item from check " + check.LocationID.ToString());
                                    emptyAccessesRemaining++;
                                    //Time to take the item!
                                    FilledLocations.Remove(check);
                                    RecollectedChecks.Add(check);
                                    PrecollectedItems.Add(check.CheckItem.Name);
                                    if (JustTheKeys.ContainsKey(check.CheckItem.Name))
                                        JustTheKeys[check.CheckItem.Name]++;
                                    else
                                        JustTheKeys.Add(check.CheckItem.Name, 1);
                                };
                            }
                            foreach (CheckData check in RecollectedChecks.ToList())
                            {
								AccessAdd(check.CheckItem.Name, AccessChecker.Access);
								check.CheckItem = null;
							}

                            EmptyChecks.AddRange(RecollectedChecks);
                            RecollectedChecks.Clear();
						}
					}
                    //PrecollectedItems.ToList();
				}
                if (JustTheKeys.Count <= 0) //we have placed every item.
                    break;
			}
			LogInfo("All Major items have been placed. Placing remaining items...");

            //Now all that remains are minor items. No logic is necessary to shuffle these so we're going completely random and calling it a day.
			foreach (string item in PrecollectedItems.ToList().OrderBy(r => rand.Next()))
            {
                EmptyChecks.First().CheckItem = ItemCheatSheet.GetData(item);
                FilledLocations.Add(EmptyChecks.First());
                EmptyChecks.Remove(EmptyChecks.First());
            }
;

        }
        public static void AccessAdd(string item, Dictionary<string, int> CurrentAccess)
        { 
            ItemData itemData = ItemCheatSheet.GetData(item);
            CurrentAccess[item] += itemData.QuantityToGive;
            if (itemData.Name == "Half Container")
               CurrentAccess["Health"]++;
            if (itemData.Name == "Water Treasure" && CurrentAccess["Water Treasure"] == 1)
               CurrentAccess["Health"] += 2;
            if (itemData.Name == "House Treasure" && CurrentAccess["House Treasure"] == 1)
               CurrentAccess["Health"] += 2;
            if (itemData.Name == "Ice Treasure" && CurrentAccess["Ice Treasure"] == 1)
               CurrentAccess["Health"] += 2;
            if (itemData.Name == "Lightning Treasure" && CurrentAccess["Lightning Treasure"] == 1)
               CurrentAccess["Health"] += 2;
            if (itemData.Name == "Triple Crystal")
                CurrentAccess["Crystal"] += 3;
        }
        public static void AccessSubtract(string item, Dictionary<string, int> CurrentAccess)
        {
            ItemData itemData = ItemCheatSheet.GetData(item);
            CurrentAccess[item] -= itemData.QuantityToGive;
            if (itemData.Name == "Half Container")
                CurrentAccess["Health"]--;
            if (itemData.Name == "Water Treasure" && CurrentAccess["Water Treasure"] == 0)
                CurrentAccess["Health"] -= 2;
            if (itemData.Name == "House Treasure" && CurrentAccess["House Treasure"] == 0)
                CurrentAccess["Health"] -= 2;
            if (itemData.Name == "Ice Treasure" && CurrentAccess["Ice Treasure"] == 0)
                CurrentAccess["Health"] -= 2;
            if (itemData.Name == "Lightning Treasure" && CurrentAccess["Lightning Treasure"] == 0)
                CurrentAccess["Health"] -= 2;
			if (itemData.Name == "Triple Crystal")
				CurrentAccess["Crystal"] -= 3;
		}
    }
    public static Dictionary<string, int[]> HardBlacklist = new()
    {
        { "Snow Potion", new int[30] {106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 139, 181, 221, 222, 223, 224, 228, 230, 231, 236, 237, 273, 280, 281, 282, 283, 284, 285, 286 } },
        { "Woods Potion", new int[30]{106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 139, 181, 221, 222, 223, 224, 228, 230, 231, 236, 237, 273, 280, 281, 282, 283, 284, 285, 286 } },
        { "Ruins Warp", new int[30]  {106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 139, 181, 221, 222, 223, 224, 228, 230, 231, 236, 237, 273, 280, 281, 282, 283, 284, 285, 286 } },
        { "Start Warp", new int[30]  {106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 139, 181, 221, 222, 223, 224, 228, 230, 231, 236, 237, 273, 280, 281, 282, 283, 284, 285, 286 } }
    };
}
