using UnityEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using static pieceScript;

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

        public float? QuantityToGiveFloat
        {
            get;
            set;
        }

        public bool appendTier
        {
            get;
            set;
		}

		public int TierCap
		{
			get; set;
		}

		public ItemData()
		{
		}

		public ItemData(string name, string classification, string itemNameForSave, string spriteName, bool appendTier, int quantityToGive, int tierCap)
		{
			Name = name;
			Classification = classification;
			ItemNameForSave = itemNameForSave;
			SpriteName = spriteName + ((appendTier) ? " " + Math.Min((int)quantityToGive, tierCap) : "") + ".png";
			QuantityToGive = quantityToGive;
			TierCap = tierCap;
        }
        public ItemData(string name, string classification, string itemNameForSave, string spriteName, bool appendTier, float quantityToGivef, int tierCap)
        {
            Name = name;
            Classification = classification;
            ItemNameForSave = itemNameForSave;
            SpriteName = spriteName + ((appendTier) ? " " + Math.Min((int)quantityToGivef, tierCap) : "") + ".png";
            QuantityToGiveFloat = quantityToGivef;
            TierCap = tierCap;
        }
    }
	public class ItemLookup
	{
		public static Dictionary<String, ItemData> TranslatedItemNames = new()
		{
			{ "1 Coin", new ItemData("1 Coin", "junk", "argent", "Small Coin", false, 1, 9999 ) },
			{ "5 Coins", new ItemData("5 Coins", "junk", "argent", "Medium Coin", false, 5, 9999 ) },
			{ "10 Coins", new ItemData("10 Coins", "junk", "argent", "Big Coin", false, 10, 9999 ) },
			{ "20 Bucks", new ItemData("20 Bucks", "junk", "argent", "Bill", false, 20, 9999 ) },
			{ "50 Gold", new ItemData("50 Gold", "useful", "argent", "Gold", false, 50, 9999 ) },
			{ "100 Diamond", new ItemData("100 Diamond", "useful", "argent", "Diamond", false, 100, 9999 ) },
			{ "Crystal", new ItemData("Crystal", "progression", "cristals", "Crystal", false, 1, 60) },
			{ "Triple Crystal", new ItemData("Triple Crystal", "progression", "cristals", "Triple Crystal", false, 3, 60) },
			{ "Health Container", new ItemData("Health Container", "useful", "PDVMax", "Container", false, 2, 60) },
			{ "Half Container", new ItemData("Half Container", "useful", "demiReceptacle", "HalfContainer", false, 1, 60) },
			{ "Boots", new ItemData("Boots", "progression", "bottes", "Boots", true, 1, 2) },
			{ "Vitamins", new ItemData("Vitamins", "progression", "muscle", "Vitamins", false, 1, 2) },
			{ "Grappling Hook", new ItemData("Grappling Hook", "progression", "distance", "Grappling Hook", true, 1, 2) },
			{ "Apple", new ItemData("Apple", "useful", "PDVActuels", "Apple", false, 10, 60) },
			{ "Cheese", new ItemData("Cheese", "useful", "PDVActuels", "Cheese", false, 50, 60) },
			{ "Meat", new ItemData("Meat", "useful", "rez", "Meat", true, 1, 60) },
			{ "Super Meat", new ItemData("Super Meat", "useful", "rez", "Meat", true, 2, 60) },
			{ "Wallet Upgrade", new ItemData("Wallet Upgrade", "progression", "Bourse", "Wallet", true, 1, 3) },
			{ "Melee Upgrade", new ItemData("Melee Upgrade", "progression", "picLVL", "Sword", true, 1, 6) },
			{ "Snorkel", new ItemData("Snorkel", "progression", "palmes", "Snorkel", true, 1, 2) },
			{ "Lantern", new ItemData("Lantern", "progression", "lanterne", "Lantern", true, 1, 2) },
			{ "Lens", new ItemData("Lens", "progression", "vision", "Lens", true, 1, 2) },
			{ "Boomerang", new ItemData("Boomerang", "progression", "boomerang", "Boomerang", true, 1, 2) },
			{ "Gloves", new ItemData("Gloves", "progression", "creuse", "Gloves", true, 1, 2) },
			{ "Balloon", new ItemData("Balloon", "progression", "ballon", "Balloon", true, 1, 2) },
			{ "Luck", new ItemData("Luck", "useful", "clover", "Clover", true, 1, 2) },
			{ "Magnet", new ItemData("Magnet", "useful", "magnet", "Magnet", true, 1, 2) },
			{ "Armor", new ItemData("Armor", "useful", "armor", "Armor", true, 1, 2) },
			{ "Overworld Map", new ItemData("Overworld Map", "useful", "aCarteOW", "Map", false, 1, 1) },
			{ "Ruins Map", new ItemData("Ruins Map", "useful", "aCarteEW", "Map", false, 1, 1) },
			{ "Water Dungeon Map", new ItemData("Water Dungeon Map", "useful", "aCarteDonjonSO", "Map", false, 1, 1) },
			{ "Haunted House Map", new ItemData("Haunted House Map", "useful", "aCarteDonjonNE", "Map", false, 1, 1) },
			{ "Snowy Peaks Map", new ItemData("Snowy Peaks Map", "useful", "aCarteDonjonNO", "Map", false, 1, 1) },
			{ "Factory Map", new ItemData("Factory Map", "useful", "aCarteDonjonSE", "Map", false, 1, 1) },
			{ "Ziggurat Map", new ItemData("Ziggurat Map", "useful", "aCarteDonjonPyra", "Map", false, 1, 1) },
			{ "Forge Map", new ItemData("Forge Map", "useful", "aCarteDonjonBonus", "Map", false, 1, 1) },
			{ "Final Dungeon Map", new ItemData("Final Dungeon Map", "useful", "aCarteDonjonFinal", "Map", false, 1, 1) },
			{ "Water Treasure", new ItemData("Water Treasure", "progression", "Fragment1", "Water Key", true, 1, 2) },
			{ "House Treasure", new ItemData("House Treasure", "progression", "Fragment2", "House Key", true, 1, 2) },
			{ "Ice Treasure", new ItemData("Ice Treasure", "progression", "Fragment3", "Ice Key", true, 1, 2) },
			{ "Lightning Treasure", new ItemData("Lightning Treasure", "progression", "Fragment4", "Lightning Key", true, 1, 2) },
			{ "Gear Piece 1", new ItemData("Gear Piece 1", "progression", "Gear1", "Gear Piece 1", false, 1, 1) },
			{ "Gear Piece 2", new ItemData("Gear Piece 2", "progression", "Gear2", "Gear Piece 2", false, 1, 1) },
			{ "Gear Piece 3", new ItemData("Gear Piece 3", "progression", "Gear3", "Gear Piece 3", false, 1, 1) },
			{ "Disc Number ", new ItemData("Disc Number ", "junk", "discs", "disc", false, 1, 40) },
			{ "Water Dungeon Key", new ItemData("Water Dungeon Key", "progression", "keys", "Key", false, 1, 3) },
			{ "Light Bulb", new ItemData("Light Bulb", "progression", "bulbs", "Bulb", false, 1, 9) },
			{ "Snowy Peaks Key", new ItemData("Snowy Peaks Key", "progression", "keyIce", "Key", false, 1, 3) },
			{ "Factory Pass", new ItemData("Factory Pass", "progression", "pass", "pass", false, 1, 9) },
			{ "Ziggurat Key", new ItemData("Ziggurat Key", "progression", "keySecret", "Key", false, 1, 2) },
			{ "Forge Pass", new ItemData("Forge Pass", "progression", "passForge", "pass", false, 1, 11) },
			{ "Woods Potion", new ItemData("Woods Potion", "useful", "", "Potion", false, 0, 1) },
			{ "Snow Potion", new ItemData("Snow Potion", "progression", "", "Potion", false, 0, 1) },
			{ "Ruins Warp", new ItemData("Ruins Warp", "progression", "", "Warp Cloud", false, 0, 1) },
			{ "Start Warp", new ItemData("Start Warp", "useful", "", "Warp Cloud", false, 0, 1) },
			{ "Coffee", new ItemData("Coffee", "useful", "cafeCountdown", "Coffee", false, 60f, 60) },
			{ "Moster Sword", new ItemData("Moster Sword", "trap", "", "MosterSword", false, 0, 1) }
        };
	}
	public static ItemData GetData(string id)
	{
		UnityEngine.Debug.Log(id);
		ItemLookup.TranslatedItemNames.TryGetValue(id, out ItemData value);
		return value;
	}
}
