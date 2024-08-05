using BepInEx;
using HarmonyLib;
using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Bindings;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;
using static Mono.Security.X509.X520;
using static ItemCheatSheet;
using static UpdateInventory;
using MasterKeyRandomizer;
using System.IO;

namespace RareItem.patches;

class RareItemPatcher
{
    public static class RandoItem
    {
        public static ItemData itemData { get; private set; }
    }
    [HarmonyPrefix]
    [HarmonyPatch(typeof(objetRareScript), nameof(objetRareScript.Start))]
    public static bool StartPatch1(objetRareScript __instance)
    {
        __instance.OrigPos = __instance.transform.position;
        __instance.gameObject.GetComponent<SpriteRenderer>().sprite = UpdateAppearance(__instance.gameObject.name + __instance.OrigPos.ToString());
        return false;
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(objetRareScript), nameof(objetRareScript.grabIt))]
    public static bool GrabRarePatch1(objetRareScript __instance)
    {
        if (!__instance.used && __instance.joueur.GetComponent<FoxMove>().argent >= __instance.valeur)
        {
            __instance.gameObject.GetComponent<SpriteRenderer>().sprite = UpdateAppearance(__instance.gameObject.name + __instance.OrigPos.ToString());
            __instance.used = true;
            ItemData randoItem = ItemCheatSheet.GetData(__instance.gameObject.name + __instance.OrigPos.ToString());
            UpdateInventory.AddToInventory(randoItem);
            UnityEngine.Object.Destroy(__instance.gameObject);
            __instance.joueur.GetComponent<FoxMove>().argent -= __instance.valeur;
            return false;
        }
        if (!__instance.EnCoffre && __instance.joueur.GetComponent<FoxMove>().argent >= __instance.valeur)
        {
            UnityEngine.Object.Destroy(__instance.gameObject);
            return false;

        }
        return false;
    }
}
