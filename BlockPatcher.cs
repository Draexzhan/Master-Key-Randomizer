using HarmonyLib;
using UnityEngine;

class BlockPatch1
{
    [HarmonyPostfix]
    [HarmonyPatch(typeof(blocPoussableScript), nameof(blocPoussableScript.Start))]
    public static void BlockPatch(blocPoussableScript __instance)
    {
        if (__instance.truePos == new UnityEngine.Vector3(-18.5f, 603.5f, -2f) || (__instance.truePos == new UnityEngine.Vector3(-18.5f, 602.5f, -2f)))
        {
            UnityEngine.Object.DestroyImmediate(__instance.GetComponent<GameObject>());
            __instance.transform.position = __instance.truePos + new UnityEngine.Vector3(-10f, 10f, 0);
        }
    }
}