using Pathfinding.Util;
using System;
using System.Collections.Generic;
using static ItemCheatSheet;
using static CheckClass;
using System.Reflection;
using UnityEngine;
using System.Linq;
using UnityEngine.XR;
using static UnityEngine.ParticleSystem.PlaybackState;

public class Seed
{
    public const long BASE_ID = 12011993;
    public static CheckData[] Checks;
    public static System.Random rand = new System.Random(0);
    public static void GenerateSeed(int seed)
	{
        int i = 0;
        foreach (var value in CheckLookup.Locations.Values)
        {
            Checks = new CheckData[CheckLookup.Locations.Count];
            Checks[i] = value;
            value.CheckItem = GetRandomItem(seed);
            Debug.Log(value.LocationName + " has " + value.CheckItem.Name + ".");
            i++;
        }
	}

    private static ItemData GetRandomItem(int seed)
    {
        return ItemLookup.TranslatedItemNames.ElementAt( rand.Next(0, ItemLookup.TranslatedItemNames.Count)).Value;
    }
}
