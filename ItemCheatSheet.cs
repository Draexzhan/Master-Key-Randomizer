using System;
using UnityEngine;
using System.Collections.Generic;
using static MasterKeyRandomizer.MKLogger;

public class ItemCheatSheet
{
	public class ItemData
	{

		public string Name
		{
			get;
			set;
		}

		public string Classification
		{
			get;
			set;
		}

		public string ItemNameForSave
		{
			get;
			set;
		}
		public string SpriteName
		{
			get;
			set;
		}

		public int QuantityToGive
		{
			get;
			set;
		}

		public bool AppendTier
		{
			get;
			set;
		}

		public int TierCap
		{
			get; set;
		}

		public string ItemTypeName
		{
			get; set;
		}

		public int CelebrationType
		{
			get; set;
		}

		public ItemData()
		{
		}

		public ItemData(string name, string classification, string itemNameForSave, string spriteName, bool appendTier, int quantityToGive, int tierCap, int celebrationType)
		{
			Name = name;
			Classification = classification;
			ItemNameForSave = itemNameForSave;
            AppendTier = appendTier;
            ItemTypeName = spriteName;
			SpriteName = ItemTypeName + (appendTier ? " " + Math.Min(quantityToGive, tierCap) : "") + ".png";
			QuantityToGive = quantityToGive;
			TierCap = tierCap;
			CelebrationType = celebrationType;
		}
		public void UpdateSpriteName(int currentTier, bool GiveQuantity = true)
		{
			LogDebug(currentTier.ToString() + " " + AppendTier.ToString());
			if (AppendTier)
				SpriteName = ItemTypeName + " " + Math.Min((GiveQuantity ? QuantityToGive : 0) + currentTier, TierCap) + ".png";

			else
				SpriteName = ItemTypeName + ".png";
		}
	}
	public class ItemLookup
	{
		public static Dictionary<String, ItemData> TranslatedItemNames = new()
		{
			{ "1 Coin", new ItemData("1 Coin", "junk", "argent", "Small Coin", false, 1, 9999, 1 ) },
			{ "5 Coins", new ItemData("5 Coins", "junk", "argent", "Medium Coin", false, 5, 9999, 1 ) },
			{ "10 Coins", new ItemData("10 Coins", "junk", "argent", "Big Coin", false, 10, 9999, 1 ) },
			{ "20 Bucks", new ItemData("20 Bucks", "junk", "argent", "Bill", false, 20, 9999, 1 ) },
			{ "50 Gold", new ItemData("50 Gold", "useful", "argent", "Gold", false, 50, 9999, 2 ) },
			{ "100 Diamond", new ItemData("100 Diamond", "useful", "argent", "Diamond", false, 100, 9999, 2 ) },
			{ "Crystal", new ItemData("Crystal", "progression", "cristals", "Crystal", false, 1, 60, 2 ) },
			{ "Triple Crystal", new ItemData("Triple Crystal", "progression", "cristals", "Triple Crystal", false, 3, 60, 2) },
			{ "Health Container", new ItemData("Health Container", "progression", "PDVMax", "Container", false, 2, 60, 2 ) },
			{ "Half Container", new ItemData("Half Container", "progression", "demiReceptacle", "HalfContainer", false, 1, 60, 2 ) },
			{ "Boots", new ItemData("Boots", "progression", "bottes", "Boots", true, 1, 2, 3 ) },
			{ "Vitamins", new ItemData("Vitamins", "progression", "muscle", "Vitamins", false, 1, 2, 3 ) },
			{ "Grappling Hook", new ItemData("Grappling Hook", "progression", "distance", "Grappling Hook", true, 1, 2, 3 ) },
			{ "Apple", new ItemData("Apple", "useful", "PDVActuels", "Apple", false, 0, 60, 0) }, //these are listed as healing 0 because the healing is done elsewhere.
			{ "Cheese", new ItemData("Cheese", "useful", "PDVActuels", "Cheese", false, 0, 60, 0) }, //these are listed as healing 0 because the healing is done elsewhere.
			{ "Meat", new ItemData("Meat", "useful", "rez", "Meat 1", false, 1, 60, 2) },
			{ "Super Meat", new ItemData("Super Meat", "useful", "rez", "Meat 2", false, 2, 60, 2) },
			{ "Wallet Upgrade", new ItemData("Wallet Upgrade", "progression", "Bourse", "Wallet", true, 1, 3, 3) },
			{ "Weapon Upgrade", new ItemData("Weapon Upgrade", "progression", "picLVL", "Sword", true, 1, 6, 3) },
			{ "Snorkel", new ItemData("Snorkel", "progression", "palmes", "Snorkel", true, 1, 2, 3) },
			{ "Lantern", new ItemData("Lantern", "progression", "lanterne", "Lantern", true, 1, 2, 3) },
			{ "Lens", new ItemData("Lens", "progression", "vision", "Lens", true, 1, 2, 3) },
			{ "Boomerang", new ItemData("Boomerang", "progression", "boomerang", "Boomerang", true, 1, 2, 3) },
			{ "Gloves", new ItemData("Gloves", "progression", "creuse", "Gloves", true, 1, 2, 3) },
			{ "Balloon", new ItemData("Balloon", "progression", "ballon", "Balloon", true, 1, 2, 3) },
			{ "Clover", new ItemData("Clover", "useful", "clover", "Clover", true, 1, 2, 3) },
			{ "Magnet", new ItemData("Magnet", "useful", "magnet", "Magnet", true, 1, 2, 3) },
			{ "Armor", new ItemData("Armor", "useful", "armor", "Armor", true, 1, 2, 3) },
			{ "Overworld Map", new ItemData("Overworld Map", "useful", "aCarteOW", "Map", false, 1, 1, 2) },
			{ "Ruins Map", new ItemData("Ruins Map", "useful", "aCarteEW", "MapRuins", false, 1, 1, 2) },
			{ "Water Dungeon Map", new ItemData("Water Dungeon Map", "useful", "aCarteDonjonSO", "MapWater", false, 1, 1, 2) },
			{ "Haunted House Map", new ItemData("Haunted House Map", "useful", "aCarteDonjonNE", "MapHouse", false, 1, 1, 2) },
			{ "Snowy Peaks Map", new ItemData("Snowy Peaks Map", "useful", "aCarteDonjonNO", "MapIce", false, 1, 1, 2 ) },
			{ "Factory Map", new ItemData("Factory Map", "useful", "aCarteDonjonSE", "MapLightning", false, 1, 1, 2 ) },
			{ "Ziggurat Map", new ItemData("Ziggurat Map", "useful", "aCarteDonjonPyra", "MapEye", false, 1, 1, 2 ) },
			{ "Forge Map", new ItemData("Forge Map", "useful", "aCarteDonjonBonus", "MapGear", false, 1, 1, 2 ) },
			{ "Final Dungeon Map", new ItemData("Final Dungeon Map", "useful", "aCarteDonjonFinal", "MapFinal", false, 1, 1, 2 ) },
			{ "Water Treasure", new ItemData("Water Treasure", "progression", "Fragment1", "Water Key", true, 1, 2, 3) },
			{ "House Treasure", new ItemData("House Treasure", "progression", "Fragment2", "House Key", true, 1, 2, 3) },
			{ "Ice Treasure", new ItemData("Ice Treasure", "progression", "Fragment3", "Ice Key", true, 1, 2, 3) },
			{ "Lightning Treasure", new ItemData("Lightning Treasure", "progression", "Fragment4", "Lightning Key", true, 1, 2, 3) },
			{ "Gear Piece 1", new ItemData("Gear Piece 1", "progression", "Gear1", "Gear Piece 1", false, 1, 1, 3) },
			{ "Gear Piece 2", new ItemData("Gear Piece 2", "progression", "Gear2", "Gear Piece 2", false, 1, 1, 3) },
			{ "Gear Piece 3", new ItemData("Gear Piece 3", "progression", "Gear3", "Gear Piece 3", false, 1, 1, 3) },
			{ "Disc Number ", new ItemData("Disc Number ", "junk", "discs", "disc", false, 1, 40, 2) },
			{ "Water Dungeon Key", new ItemData("Water Dungeon Key", "progression", "keys", "KeyWater", false, 1, 3, 2 ) },
			{ "Light Bulb", new ItemData("Light Bulb", "progression", "bulbs", "Bulb", false, 1, 9, 2 ) },
			{ "Snowy Peaks Key", new ItemData("Snowy Peaks Key", "progression", "keyIce", "KeySnow", false, 1, 3, 2 ) },
			{ "Factory Pass", new ItemData("Factory Pass", "progression", "pass", "passFactory", false, 1, 9, 2 ) },
			{ "Ziggurat Key", new ItemData("Ziggurat Key", "progression", "keySecret", "KeyZigg", false, 1, 2, 2 ) },
			{ "Forge Pass", new ItemData("Forge Pass", "progression", "passForge", "passForge", false, 1, 11, 2 ) },
			{ "Woods Potion", new ItemData("Woods Potion", "progression", "No Data", "Potion", false, 1, 1, 0) },
			{ "Snow Potion", new ItemData("Snow Potion", "progression", "No Data", "Potion", false, 1, 1, 0) },
			{ "Start Warp", new ItemData("Start Warp", "progression", "No Data", "Warp Cloud", false, 1, 1, 0) },
			{ "Ruins Warp", new ItemData("Ruins Warp", "useful", "No Data", "Warp Cloud", false, 1, 1, 0) },
			{ "Coffee", new ItemData("Coffee", "useful", "No Data", "Coffee", false, 1, 60, 2) },
			{ "Moster Sword", new ItemData("Moster Sword", "trap", "No Data", "MosterSword", false, 1, 1, 0) },
			{ "Archipelago Trap", new ItemData("Archipelago Trap", "trap", "No Data", "PellyMinus", false, 1, 1, 0) },
			{ "Archipelago Junk", new ItemData("Archipelago Junk", "junk", "No Data", "Pelly", false, 1, 1, 1) },
			{ "Archipelago Item", new ItemData("Archipelago Item", "useful", "No Data", "PellyPlus", false, 1, 1, 2) },
			{ "Archipelago Progress", new ItemData("Archipelago Progress", "progression", "No Data", "PellyProgress", false, 1, 1, 3) },
			{ "Damage Trap", new ItemData("Damage Trap", "trap", "No Data", "Trap", true, UnityEngine.Random.Range(1, 14), 13, 0) },
			{ "Cannon Trap", new ItemData("Cannon Trap", "trap", "No Data", "Trap", true, UnityEngine.Random.Range(1, 14), 13, 0) },
			{ "Spike Trap", new ItemData("Spike Trap", "trap", "No Data", "Trap", true, UnityEngine.Random.Range(1, 14), 13, 0) },
			{ "Error", new ItemData("Error", "trap", "No Data", "Error", false, 0, 1, 0) }
		};
	}
	public static ItemData GetData(string id)
	{
		//UnityEngine.Debug.Log(id);
		ItemLookup.TranslatedItemNames.TryGetValue(id, out ItemData value);
		return value;
	}
}
