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
	static readonly Harmony harmonyCore = new Harmony("com.draexzhan.patch");
	static readonly Harmony harmonyRando = new Harmony("com.draexzhanRando.patch");
	private void Awake()
    {
        MKLogger.SetLogger(((MasterKeyRandomizer)this).Logger);
        ((MasterKeyRandomizer)this).Logger.LogInfo((object)"Master Key Randomizer (v0.1.0.0) is loaded!");
        harmonyCore.PatchAll(typeof(TitleImagePatch1));
        harmonyCore.PatchAll(typeof(RandomizerEditor));
        Application.runInBackground = true;
        UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object)new GameObject("quick settings gui", new Type[1] { typeof(RandomizerEditor) })
        {
            hideFlags = (HideFlags)61
        });
    }

    static public void RandoMode()
    {
		harmonyRando.PatchAll(typeof(IntroScriptV2Patch1));
		harmonyRando.PatchAll(typeof(PortalPatcherPatch1));
		harmonyRando.PatchAll(typeof(ItemDebuggerPatch1));
		harmonyRando.PatchAll(typeof(ChestPatcherPatch1));
		harmonyRando.PatchAll(typeof(SaveDataPatch1));
		harmonyRando.PatchAll(typeof(RareItemPatcher));
		harmonyRando.PatchAll(typeof(GhostPatch1));
		harmonyRando.PatchAll(typeof(DragonPatcher));
		harmonyRando.PatchAll(typeof(PlayerPatch));
		harmonyRando.PatchAll(typeof(SwordForgePatcher));
		harmonyRando.PatchAll(typeof(TreadmillPatch1));
		harmonyRando.PatchAll(typeof(EnemyPatch1));
		harmonyRando.PatchAll(typeof(BlockPatch1));
		harmonyRando.PatchAll(typeof(MosterSwordPatcher));
		harmonyRando.PatchAll(typeof(RandoWarp));
		harmonyRando.PatchAll(typeof(MusicPatch1));
	}

    static public void VanillaMode()
	{
		harmonyRando.UnpatchSelf();
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
