using BepInEx;
using HarmonyLib;
using IntroV2Override.patches;
using ItemDebugger.patches;
using ChestPatcher.patches;
using SaveData.patches;
using BepInEx.Logging;
using RareItem.patches;
using System;
using System.Collections;
using UnityEngine;
using System.Threading;
using Ghost.patches;
using static Seed;
using MenuMod.patches;

namespace MasterKeyRandomizer
{
    [BepInPlugin("com.draexzhan.MasterKeyRandomizer", "Master Key Randomizer", "0.1.0.0")]
	[BepInProcess("Master Key.exe")]
    public class Plugin : BaseUnityPlugin
    {
        internal static new ManualLogSource Logger;
        private void Awake()
        {
            //Logger.LogInfo("The Master Key Randomizer is loaded!");
			var harmony = new Harmony("com.draexzhan.patch");
            harmony.PatchAll(typeof(TitleImagePatch1));
			harmony.PatchAll(typeof(IntroScriptV2Patch1));
            harmony.PatchAll(typeof(ItemDebuggerPatch1));
            harmony.PatchAll(typeof(ChestPatcherPatch1));
            harmony.PatchAll(typeof(SaveDataPatch1));
            harmony.PatchAll(typeof(RareItemPatcher));
            harmony.PatchAll(typeof(GhostPatch1));
            harmony.PatchAll(typeof(DragonPatcher));
            harmony.PatchAll(typeof(PlayerPatch));
            harmony.PatchAll(typeof(SwordForgePatcher));
            harmony.PatchAll(typeof(TreadmillPatch1));
            harmony.PatchAll(typeof(EnemyPatch1));
        }
    }
}
