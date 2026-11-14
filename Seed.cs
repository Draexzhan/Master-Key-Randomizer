using System;
using System.Collections.Generic;
using static ItemCheatSheet;
using static CheckClass;
using System.Reflection;
using static MasterKeyRandomizer.MKPluginInfo;
using static MasterKeyRandomizer.MKLogger;
using UnityEngine;
using System.Linq;
using System.IO;
using MonoMod.Utils;
using UnityEngine.UIElements.Collections;
using static UnityEngine.GridBrushBase;
using MasterKeyRandomizer;


public class Seed
{
	public const long BASE_ID = 12011993;
	public static List<CheckData> Checks = new();
	public static List<CheckData> RecollectedChecks = new(); // A list of checks that we've already collected items from
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
			ItemRandomizer.AssumeFill(Checks, PlayerPrefs.GetInt("KeysanityLogic") == 0, PlayerPrefs.GetInt(saveSlot + "TrapRate", 0));
			string path = Application.persistentDataPath + "\\SpoilerLogs\\SpoilerLogForSeed" + seed + ".txt";
			StreamWriter SpoilerLog = new StreamWriter(path, true);
			SpoilerLog.WriteLine("Master Key Randomizer version " + MKPluginInfo.VERSION.ToString() + "\n==========================================");
			SpoilerLog.WriteLine("Spoiler Log for Seed " + seed.ToString() + "\n------------------------------------------\n");
			SpoilerLog.WriteLine("Seed Settings: \n\nStarting Location - " + (PlayerPrefs.GetInt("StartLogic") == 0 ? "Starting Cave" : "Town"));
			SpoilerLog.WriteLine("Keysanity - " + (PlayerPrefs.GetInt("KeysanityLogic") == 0 ? "On" : "Off"));
			SpoilerLog.WriteLine("Lantern Logic - " + (PlayerPrefs.GetInt("LanternLogic") == 0 ? "Need in the Dark" : "Minimum"));
			if (PlayerPrefs.GetInt("LensLogic") == 0) 
			{
				SpoilerLog.WriteLine("Lens Logic - Always Require");
			}
			else SpoilerLog.WriteLine("Lens Logic - " + (PlayerPrefs.GetInt("LensLogic") == 1 ? "Unmarked Only" : "Minimum"));
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
			if (PlayerPrefs.GetInt("MapSettings") == 0)
			{
				SpoilerLog.WriteLine("Maps - Start with");
			}
			else SpoilerLog.WriteLine("Maps - " + (PlayerPrefs.GetInt("MapSettings") == 1 ? "Local" : "Anywhere"));

			if (PlayerPrefs.GetInt("FinalBossAccess") == 0)
			{
				SpoilerLog.WriteLine("Final Boss Access - Vanilla");
			}
			else SpoilerLog.WriteLine("Final Boss Access - " + (PlayerPrefs.GetInt("FinalBossAccess") == 1 ? "Standard Bosses" : "All Bosses"));
			PlayerPrefs.SetInt(saveSlot + "FinalBossAccess", PlayerPrefs.GetInt("FinalBossAccess", 0));

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
			//Checks.Clear();
			//Checks.AddRange(CheckLookup.Locations.Values);
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
					SpoilerLog.WriteLine("\n----------------------------------------------------------------------------------------------------------------------------\nEnd of Logic. Remaining checks (which should only be out of logic junk, or excluded locations) are:\n");
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
					}
				}
				SphereStash.Clear();
			}
			SpoilerLog.Flush();
			PlayerPrefs.SetInt(saveSlot + "randoSeed", seed);
			LogInfo("A new randomized world has been made with seed " + seed + ".");
			foreach (string s in AccessChecker.Access.Keys)
				LogDebug(s + " x" + AccessChecker.Access[s].ToString());

			if (PlayerPrefs.GetInt("MapSettings") == 0)
			{
				LogDebug("Giving player maps");
				FoxMove foxMove = UnityEngine.Object.FindObjectOfType<FoxMove>();
				foxMove.aCarteOW = 1;
				foxMove.aCarteEW = 1;
				foxMove.aCarteDonjonNE = 1;
				foxMove.aCarteDonjonSE = 1;
				foxMove.aCarteDonjonNO = 1;
				foxMove.aCarteDonjonSO = 1;
				foxMove.aCarteDonjonPyra = 1;
				foxMove.aCarteDonjonBonus = 1;
				foxMove.aCarteDonjonFinal = 1;
				PlayerPrefs.SetInt(saveSlot + "carteOW", 1);
				PlayerPrefs.SetInt(saveSlot + "carteEW", 1);
				PlayerPrefs.SetInt(saveSlot + "carteSO", 1);
				PlayerPrefs.SetInt(saveSlot + "carteNE", 1);
				PlayerPrefs.SetInt(saveSlot + "carteNO", 1);
				PlayerPrefs.SetInt(saveSlot + "carteSE", 1);
				PlayerPrefs.SetInt(saveSlot + "cartePyra", 1);
				PlayerPrefs.SetInt(saveSlot + "carteBonus", 1);
				PlayerPrefs.SetInt(saveSlot + "carteFinal", 1);
			}

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
			
			//LogDebug(PlayerPrefs.GetInt(saveSlot + "BOSS1MOLETempleSO", 0).ToString() + " Mole.");
			//LogDebug(PlayerPrefs.GetInt(saveSlot + "BOSS2GHOSTManoirHante", 0) + " Ghost.");
			//LogDebug(PlayerPrefs.GetInt(saveSlot + "BOSS3SHIPTempleNeige", 0) + " Baldo.");
			//LogDebug(PlayerPrefs.GetInt(saveSlot + "BOSS4HYDRACentrale", 0) + " Hydra.");
			//LogDebug(PlayerPrefs.GetInt(saveSlot + "BOSSDIVEForge", 0) + " Golem.");
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
			if (PlayerPrefs.GetInt("MapSettings") == 2)
			{
				PrecollectedItems.Add("Overworld Map");
				PrecollectedItems.Add("Water Dungeon Map");
				PrecollectedItems.Add("Haunted House Map");
				PrecollectedItems.Add("Snowy Peaks Map");
				PrecollectedItems.Add("Ruins Map");
				PrecollectedItems.Add("Factory Map");
				PrecollectedItems.Add("Ziggurat Map");
				PrecollectedItems.Add("Forge Map");
				PrecollectedItems.Add("Final Dungeon Map");
			}
			PrecollectedItems.Add("Moster Sword");
		}
		public static void AssumeFill(List<CheckData> ShuffledChecks, bool keysanity, int trapRate)
		{
			LogInfo("Beginning Fill...");
			List<CheckData> FilledLocations = new List<CheckData>();                // A list of all the checks we've filled with items
			List<CheckData> EmptyChecks = new();                                    // A list of all the checks that still need items
			Dictionary<string, int> NoAccess = AccessChecker.Access;                // A stat table to track what our current inventory grants us access to.
			Dictionary<string, int> RemainingItems = new Dictionary<string, int>(); // What items we still need to place before this seed is considered generated.
			Dictionary<string, int> JustTheKeyItems = new Dictionary<string, int>();    // A pool of just the key items that still need to be placed
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
			EmptyChecks.Remove(CheckLookup.Locations["Cafe(30.25, -20.25, -1.00)"]);
			EmptyChecks.Remove(CheckLookup.Locations["Pomme(-599.50, 2.50, -1.00)"]);
			EmptyChecks.Remove(CheckLookup.Locations["Fromage(-634.50, 0.50, -1.00)"]);
			EmptyChecks.Remove(CheckLookup.Locations["REZ(-603.50, 2.50, -1.00)"]);
			EmptyChecks.Remove(CheckLookup.Locations["REZ2(-632.50, 0.50, -1.00)"]);
			EmptyChecks.Remove(CheckLookup.Locations["PierreFinal()"]);
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
					if (JustTheKeyItems.ContainsKey(item))
						JustTheKeyItems[item]++;
					else
						JustTheKeyItems.Add(item, 1);
					AccessAdd(item, AccessChecker.Access);
				}
			}

			LogInfo("Placing Hardcoded items...");
			//first, we place the hardcoded vanilla items.
			foreach (KeyValuePair<string, CheckData> pair in HardcodedLocations)
			{
				LogInfo("Placing " + pair.Key + " at check " + pair.Value.LocationName);
				EmptyChecks.Remove(pair.Value);
				pair.Value.CheckItem = ItemCheatSheet.GetData(pair.Key);
			}

			int iterationNumber = 0;

			LogInfo("Beginning randomization of Key items");
			//BEGIN RANDOMIZATION
			List<string> ShuffleList = new();
			List<string> KeyList = new();
			if (!keysanity)
			{
				foreach (string item in PrecollectedItems.ToList())
				{
					if (IsDungeonKey(item))
					{ 
						KeyList.Add(item);
					}
				}
				foreach (string item in PrecollectedItems.ToList().OrderBy(r => rand.Next()))
				{
					if (!IsDungeonKey(item))
						ShuffleList.Add(item);
				}
			}
			else
			{
				foreach (string item in PrecollectedItems.ToList().OrderBy(r => rand.Next()))
				{
					ShuffleList.Add(item);
				}
			}
			bool placedMaps = false;
			while (true)
			{
				int emptyAccessesRemaining = 0;
				if (!keysanity && KeyList.Count > 0) //We have keys to place!
				{
					emptyAccessesRemaining = EmptyChecks.Count;
					foreach (string item in KeyList.OrderBy(r => rand.Next()))
					{
						iterationNumber++;
						List<CheckData> currentPool = PreparePool(DungeonChecks.Get(item), ShuffledChecks);
						CheckData ChosenCheck = PlaceItem(currentPool, item);
						if (ChosenCheck == null) //There were no places available to place this item, put it back and empty a check
						{
							List<CheckData> UnchosenChecks = TakeItem(currentPool, KeyList, false, keysanity);
							foreach (CheckData u in UnchosenChecks)
							{
								FilledLocations.Remove(u);
								EmptyChecks.Add(u);
								emptyAccessesRemaining++;
								PrecollectedItems.Add(u.CheckItem.Name);
								if (JustTheKeyItems.TryGetValue(u.CheckItem.Name, out int _))
									JustTheKeyItems[u.CheckItem.Name]++;
								else
									JustTheKeyItems.Add(u.CheckItem.Name, 1);
								AccessAdd(u.CheckItem.Name, AccessChecker.Access);
								LogDebug("Retrieving " + u.CheckItem.Name + " from " + u.LocationName + "!");
								KeyList.Add(u.CheckItem.Name);
								u.CheckItem = null;
							}
							break;
						}
						else
						{
							FilledLocations.Add(ChosenCheck);
							EmptyChecks.Remove(ChosenCheck);
							KeyList.Remove(item);
							emptyAccessesRemaining--;
							PrecollectedItems.Remove(item);
							JustTheKeyItems[item]--;
							RecollectedChecks.Clear();
							if (JustTheKeyItems[item] == 0)
								JustTheKeyItems.Remove(item);
							break;
						}
					}
				}
				if (KeyList.Count == 0)
				{//Place the progression items
					//LogInfo("Placing all remaining progression items.");
					if (!keysanity && AccessChecker.Access["Light Bulb"] < 1)
					{
						AccessChecker.Access["Water Dungeon Key"] += 3;
						AccessChecker.Access["Light Bulb"] += 9;
						AccessChecker.Access["Snowy Peaks Key"] += 3;
						AccessChecker.Access["Factory Pass"] += 9;
						AccessChecker.Access["Ziggurat Key"] += 2;
						AccessChecker.Access["Forge Pass"] += 11;
					}
					if (PlayerPrefs.GetInt("MapSettings") == 1 && placedMaps == false)
					{
						LogDebug("Adding Maps to pool");
						EmptyChecks.Remove(PlaceItem(PreparePool(DungeonChecks.Get("Water Dungeon Key"), ShuffledChecks), "Water Dungeon Map"));
						EmptyChecks.Remove(PlaceItem(PreparePool(DungeonChecks.Get("Light Bulb"), ShuffledChecks), "Haunted House Map"));
						EmptyChecks.Remove(PlaceItem(PreparePool(DungeonChecks.Get("Snowy Peaks Key"), ShuffledChecks), "Snowy Peaks Map"));
						EmptyChecks.Remove(PlaceItem(PreparePool(DungeonChecks.Get("Factory Pass"), ShuffledChecks), "Factory Map"));
						EmptyChecks.Remove(PlaceItem(PreparePool(DungeonChecks.Get("Ziggurat Key"), ShuffledChecks), "Ziggurat Map"));
						EmptyChecks.Remove(PlaceItem(PreparePool(DungeonChecks.Get("Forge Pass"), ShuffledChecks), "Forge Map"));
						EmptyChecks.Remove(PlaceItem(PreparePool(DungeonChecks.Get("Final Dungeon"), ShuffledChecks), "Final Dungeon Map"));
						EmptyChecks.Remove(PlaceItem(PreparePool(DungeonChecks.Get("Ruins"), ShuffledChecks), "Ruins Map"));
						EmptyChecks.Remove(PlaceItem(EmptyChecks, "Overworld Map"));
						placedMaps = true;
					}
					string item = ShuffleList.OrderBy(r => rand.Next()).First();
					if (!JustTheKeyItems.TryGetValue(item, out int _))
					{
						ShuffleList.Remove(item);
					}
					else
					{
						CheckData ChosenCheck = PlaceItem(EmptyChecks, item);
						if (ChosenCheck == null) //There were no places available to place this item, put it back and empty a check
						{
							List<CheckData> UnchosenChecks = TakeItem(ShuffledChecks, ShuffleList, true, keysanity);
							foreach (CheckData u in UnchosenChecks)
							{
								FilledLocations.Remove(u);
								EmptyChecks.Add(u);
								emptyAccessesRemaining++;
								PrecollectedItems.Add(u.CheckItem.Name);
								if (JustTheKeyItems.TryGetValue(u.CheckItem.Name, out int _))
									JustTheKeyItems[u.CheckItem.Name]++;
								else
									JustTheKeyItems.Add(u.CheckItem.Name, 1);
								AccessAdd(u.CheckItem.Name, AccessChecker.Access);
								LogInfo("Retrieving " + u.CheckItem.Name + " from " + u.LocationName + "!");
								ShuffleList.Add(u.CheckItem.Name);
								u.CheckItem = null;
							}
						}
						else
						{
							FilledLocations.Add(ChosenCheck);
							EmptyChecks.Remove(ChosenCheck);
							ShuffleList.Remove(item);
							emptyAccessesRemaining--;
							PrecollectedItems.Remove(item);
							JustTheKeyItems[item]--;
							RecollectedChecks.Clear();
							if (JustTheKeyItems[item] == 0)
								JustTheKeyItems.Remove(item);
						}
					}
				}
				if (JustTheKeyItems.Count <= 0) //we have placed every item.
					break;
			}
			LogInfo("All Major items have been placed. Placing remaining items...");
			LogInfo(trapRate.ToString() + "% of junk converted to traps.");

			//Now all that remains are minor items. No logic is necessary to shuffle these so we're going completely random and calling it a day.
			//First the unique junk gets placed

			foreach (string i in PrecollectedItems)
			{
				if (!ProgressionNames.Contains(i))
				{
					CheckData c = EmptyChecks.ToList().OrderBy(r => rand.Next()).FirstOrDefault();
					c.CheckItem = ItemCheatSheet.GetData(i);
					LogInfo("Placing " + i + " at check " + c.LocationName.ToString());
					EmptyChecks.Remove(c);
				}
			}
			//then the lazy fill for whatever might be left.
			int junk = 0;
			foreach (CheckData check in EmptyChecks.ToList().OrderBy(r => rand.Next()))
			{
				if ((float)(junk) / (float)EmptyChecks.Count() < (float)trapRate / 100)
				{
					int trapType = rand.Next(1, 4);
					if (trapType == 1)
						check.CheckItem = ItemCheatSheet.GetData("Cannon Trap");
					else if (trapType == 2)
						check.CheckItem = ItemCheatSheet.GetData("Damage Trap");
					else if (trapType == 3)
						check.CheckItem = ItemCheatSheet.GetData("Spike Trap");
				}
				else if (junk % 13 == 0)
					check.CheckItem = ItemCheatSheet.GetData("100 Diamond");
				else if (junk % 13 <= 1)
					check.CheckItem = ItemCheatSheet.GetData("50 Gold");
				else if (junk % 13 <= 5)
					check.CheckItem = ItemCheatSheet.GetData("20 Bucks");
				else if (junk % 13 <= 8)
					check.CheckItem = ItemCheatSheet.GetData("10 Coins");
				else if (junk % 13 == 9)
					check.CheckItem = ItemCheatSheet.GetData("5 Coins");
				else if (junk % 13 == 10)
					check.CheckItem = ItemCheatSheet.GetData("1 Coin");
				else if (junk % 13 == 11)
					check.CheckItem = ItemCheatSheet.GetData("Meat");
				else
					check.CheckItem = ItemCheatSheet.GetData("Super Meat");
				junk++;
				//LogInfo("Placing " + check.CheckItem.Name + " at check " + check.LocationName);
			}

		}
		public static void AccessAdd(string item, Dictionary<string, int> CurrentAccess)
		{
			if (!CurrentAccess.ContainsKey(item))
				return;
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

			LogInfo(item + " added to access");
		}
		public static void AccessSubtract(string item, Dictionary<string, int> CurrentAccess)
		{
			if (!CurrentAccess.ContainsKey(item))
				return;
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

			//LogInfo(item + " removed from access");
		}

		public static bool IsDungeonKey(string item)
		{
			return (item == "Water Dungeon Key" || item == "Light Bulb" || item == "Snowy Peaks Key" || item == "Factory Pass" || item == "Ziggurat Key" || item == "Forge Pass");
		}

		public static List<CheckData> PreparePool( int[] list, List<CheckData> ShuffledChecks)
		{
			List<CheckData> Pool = new List<CheckData>();
			foreach (CheckData c in ShuffledChecks)
			{
				if (list.Contains(c.LocationID))
				{
					Pool.Add(c);
				}
			}
			return Pool;
		}

		public static CheckData PlaceItem(List<CheckData> Pool, string item)
		{
			if (AccessChecker.Access.ContainsKey(item))
				AccessSubtract(item, AccessChecker.Access);
			foreach (CheckData check in Pool.ToList().OrderBy(r => rand.Next()))
			{
				string checkField = "CheckAccess" + check.LocationID.ToString();
				MethodInfo CheckAccessMethod = typeof(CheckIndex).GetMethod(checkField, BindingFlags.Public | BindingFlags.Static);
				bool canAccess = (bool)CheckAccessMethod.Invoke(null, null);
				//LogInfo(check.LocationName + " is accessible: " + canAccess.ToString());

				//we've found an empty check that we can access with our current inventory. We can place the current item here.
				if (canAccess && (!HardBlacklist.ContainsKey(item) || !HardBlacklist[item].Contains(check.LocationID)) && !RecollectedChecks.Contains(check) && check.CheckItem == null)
				{
					LogInfo("Placing " + item + " at check " + check.LocationName);
					//Time to place the item!
					check.CheckItem = ItemCheatSheet.GetData(item);
					//reset our checks for collection
					foreach (string location in AccessChecker.AccessCache.Keys.ToList())
						AccessChecker.AccessCache[location] = false;

					return check;
				}
				//LogInfo("Can't place here!");
			}
			if (AccessChecker.Access.ContainsKey(item))
				AccessAdd(item, AccessChecker.Access);
			LogInfo("No places to place item.");
			return null;
		}

		public static List<CheckData> TakeItem(List<CheckData> pool, List<string> inventory, bool isGlobalPool, bool keysanity)
		{ //Wuh oh! There's still key items to place, but we can't access any more empty checks!
			string takenItem = string.Empty;
			List<CheckData> clearedCheck = new();
			//First we have to put a placed item back in our inventory
			foreach (CheckData check in pool.ToList().OrderBy(r => rand.Next()))
			{
				if (check.CheckItem != null)
				{
					//LogDebug(check.LocationName + " found with " + check.CheckItem.Name);
					string checkField = "CheckAccess" + check.LocationID.ToString();
					MethodInfo CheckAccessMethod = typeof(CheckIndex).GetMethod(checkField, BindingFlags.Public | BindingFlags.Static);
					bool canAccess = (bool)CheckAccessMethod.Invoke(null, null);

					//we've found a filled check that we can access with our current inventory. We can take this item and start again.
					if (canAccess)
					{
						//Dungeon keys should not be moved from their current locations unless we are placing Dungeon keys.
						if (((isGlobalPool != IsDungeonKey(check.CheckItem.Name)) || keysanity) && (!check.CheckItem.Name.Contains("Map") || PlayerPrefs.GetInt("MapSettings") != 1))
						{
							clearedCheck.Add(check);
						}
					}
				}
			}

			if (clearedCheck.Count() == 0)
			{
				LogError("No accessible checks found!");
			}
			return clearedCheck;
		}
	}
	public static Dictionary<string, int[]> HardBlacklist = new()
	{
		{ "Overworld Map", new int[1] { 1 } },
		{ "Snow Potion", new int[30] {106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 139, 181, 221, 222, 223, 224, 228, 230, 231, 236, 237, 273, 280, 281, 282, 283, 284, 285, 286 } },
		{ "Woods Potion", new int[30]{106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 139, 181, 221, 222, 223, 224, 228, 230, 231, 236, 237, 273, 280, 281, 282, 283, 284, 285, 286 } },
		{ "Ruins Warp", new int[30]  {106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 139, 181, 221, 222, 223, 224, 228, 230, 231, 236, 237, 273, 280, 281, 282, 283, 284, 285, 286 } },
		{ "Start Warp", new int[30]  {106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 139, 181, 221, 222, 223, 224, 228, 230, 231, 236, 237, 273, 280, 281, 282, 283, 284, 285, 286 } }
	};
	public static Dictionary<string, int[]> DungeonChecks = new()
	{
		{ "Water Dungeon Key", new int[22] {202, 203, 204, 205, 206, 207, 208, 209, 210, 211, 212, 213, 214, 215, 216, 217, 218, 219, 220, 364, 365, 382} },
		{ "Light Bulb", new int[20] {221, 222, 223, 224, 225, 226, 227, 228, 229, 230, 231, 232, 233, 234, 235, 236, 237, 238, 239, 240} },
		{ "Snowy Peaks Key", new int[16] {241, 242, 243, 244, 245, 246, 247, 248, 249, 250, 251, 252, 253, 254, 255, 256} },
		{ "Factory Pass", new int[23] {257, 258, 259, 260, 261, 262, 263, 264, 265, 266, 267, 268, 269, 270, 271, 272, 273, 274, 275, 276, 277, 278, 279} },
		{ "Ziggurat Key", new int[16] {287, 288, 289, 290, 291, 292, 293, 294, 295, 296, 297, 298, 299, 300, 301, 302} },
		{ "Forge Pass", new int[27] {326, 327, 328, 329, 330, 331, 332, 333, 334, 335, 336, 337, 338, 339, 340, 341, 342, 343, 344, 345, 346, 347, 348, 349, 350, 351, 374} },
		{ "Final Dungeon", new int[10] {352, 353, 354, 355, 356, 357, 358, 359, 360, 361 } },
		{ "Ruins" , new int[23] {303, 304, 305, 306, 307, 308, 309, 310, 311, 312, 313, 314, 315, 316, 317, 318, 319, 320, 321, 322, 323, 324, 325 } }
	};
}
