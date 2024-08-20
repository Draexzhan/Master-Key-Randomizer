using HarmonyLib;
using UnityEngine;
using static ItemCheatSheet;
using static CheckClass;

namespace ItemDebugger.patches;

class ItemDebuggerPatch1
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(objetRareScript), nameof(objetRareScript.itemTPOnPlayer))]
    public static void GrabMessagePatch1(objetRareScript __instance)
    {
        ItemDebuggerPatch1.CollectMessage(__instance.gameObject.name + " 1located at " + __instance.transform.name + __instance.transform.position.ToString());
    }
    [HarmonyPrefix]
    [HarmonyPatch(typeof(pieceScript), nameof(pieceScript.grabIt))]
    public static bool GrabMessagePatch2(pieceScript __instance)
    {
        if (__instance.EnCoffre)
        {
            UnityEngine.Object.Destroy(__instance.gameObject);
            ItemDebuggerPatch1.CollectMessage(__instance.gameObject.name + " 2located at " + __instance.transform.name + __instance.transform.position.ToString());
            return false;
        }
        return true;
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(PlayerHealthAndItemScript), nameof(PlayerHealthAndItemScript.OnTriggerEnter2D))]
    public static bool ObtainItemPatch1(PlayerHealthAndItemScript __instance, ref Collider2D collider)
    {
        if (collider.CompareTag("ObjetRare"))
        {
            collider.GetComponent<objetRareScript>().grabIt();
            return false;
        }
        return true;
    }

    public static void CollectMessage(string debugText)
    {
        Debug.Log("You just picked up " + debugText + "! (freestanding item)");
    }

    public static ItemData GetLocationItem(string locationString)
    {
        CheckData randoCheck = CheckClass.GetData(locationString);
        ItemData randoItem = randoCheck.CheckItem;
        return randoItem;
    }
}