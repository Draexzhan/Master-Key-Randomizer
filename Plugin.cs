using BepInEx;
using HarmonyLib;
using IntroV2Override.patches;
using ItemDebugger.patches;
using ChestPatcher.patches;
using SaveData.patches;
using PortalPatcher.patches;
using BepInEx.Logging;
using RareItem.patches;
using System;
using UnityEngine;
using Ghost.patches;
using MenuMod.patches;
using MasterKeyRandoMenu;
using Music.patches;

namespace MasterKeyRandomizer;

[BepInPlugin("com.draexzhan.MasterKeyRandomizer", "Master Key Randomizer", "0.1.0.0")]
	[BepInProcess("Master Key.exe")]
public class MasterKeyRandomizer : BaseUnityPlugin
{
    private void Awake()
    {
        MKLogger.SetLogger(((MasterKeyRandomizer)this).Logger);
        ((MasterKeyRandomizer)this).Logger.LogInfo((object)"Master Key Randomizer (v0.1.0.0) is loaded!");
        var harmony = new Harmony("com.draexzhan.patch");
        harmony.PatchAll(typeof(TitleImagePatch1));
        harmony.PatchAll(typeof(RandomizerEditor));
		harmony.PatchAll(typeof(IntroScriptV2Patch1));
        harmony.PatchAll(typeof(PortalPatcherPatch1));
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
        harmony.PatchAll(typeof(BlockPatch1));
        harmony.PatchAll(typeof(MosterSwordPatcher));
        harmony.PatchAll(typeof(RandoWarp));
        harmony.PatchAll(typeof(MusicPatch1));
        Application.runInBackground = true;
        UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object)new GameObject("quick settings gui", new Type[1] { typeof(RandomizerEditor) })
        {
            hideFlags = (HideFlags)61
        });
    }
}

public class MKLogger
{
    private static ManualLogSource Logger;
    public static void LogInfo(string message)
    {
        Logger.LogInfo((object)message);
    }

    public static void LogWarning(string message)
    {
        Logger.LogWarning((object)message);
    }

    public static void LogError(string message)
    {
        Logger.LogError((object)message);
    }

    public static void LogDebug(string message)
    {
        Logger.LogDebug((object)message);
    }

    public static void SetLogger(ManualLogSource logger)
    {
        Logger = logger;
    }
}
