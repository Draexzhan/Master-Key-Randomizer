using BepInEx;
using HarmonyLib;
using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.TextCore.Text;
using UnityEngine.Tilemaps;
using System.Numerics;
using System.Diagnostics;
using static UpdateInventory;

namespace Ghost.patches;

class GhostPatch1
{

    [HarmonyPrefix]
    [HarmonyPatch(typeof(fantomeScript), nameof(fantomeScript.Update))]
    public static void GhostPrefix(fantomeScript __instance)
    {
        if (!__instance.dead && __instance.healthScript.hp <= 0)
        {
            __instance.dead = true;
            __instance.activateOnDeath.SetActive(value: true);
            if (!__instance.reset && UnityEngine.Object.Instantiate(__instance.lootOnDeath, __instance.player.transform.position, UnityEngine.Quaternion.identity).TryGetComponent<pieceScript>(out var component))
            {
                AddToInventory(CheckClass.GetData(__instance.mainCamera.transform.position.ToString()).CheckItem);
            }
        }
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(HealthScriptMono), nameof(HealthScriptMono.Start))]
    public static void EnemyPrefix(HealthScriptMono __instance)
    {
        if (__instance.sureThingLooted)
        {
            UnityEngine.Debug.Log(__instance.sureThingLooted.ToString());
        }
    }
}
