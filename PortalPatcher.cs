using BepInEx;
using HarmonyLib;
using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Bindings;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using UnityEngine.SceneManagement;
using System.Numerics;
using static UpdateInventory;
using static UnityEngine.ParticleSystem.PlaybackState;
using static ItemCheatSheet;
using System.Diagnostics;

namespace PortalPatcher.patches;

class PortalPatcherPatch1
{

    [HarmonyPrefix]
    [HarmonyPatch(typeof(SceneLoader), nameof(SceneLoader.sceneLoad))]
    public static void PortalSceneLoadPatch1(SceneLoader __instance)
    {
        UnityEngine.Debug.Log("This door is " + __instance.name + " at " + __instance.transform.position.ToString() + " with an exit at " + __instance.futurePosition.ToString() + " facing " + __instance.futureDirectionFacing.ToString());
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(SceneLoader), nameof(SceneLoader.sceneLoad))]
    public static void PortalSceneLoadPatch2(SceneLoader __instance)
    {
        UnityEngine.Debug.Log("This exit is at " + __instance.joueur.transform.position.ToString());
    }
}