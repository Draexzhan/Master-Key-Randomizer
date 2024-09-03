using HarmonyLib;
using UnityEngine;
using static MasterKeyRandomizer.MKLogger;

class QOLPatcher
{
	[HarmonyPrefix]
	[HarmonyPatch(typeof(regenPlayerHP), nameof(regenPlayerHP.Start))]
	public static void RegenStartPatch(regenPlayerHP __instance)
	{
		__instance.rate = (1f / 8f);
	}

	[HarmonyPrefix]
	[HarmonyPatch(typeof(blocPoussableScript), nameof(blocPoussableScript.Start))]
	public static void FasterBlockPatch(blocPoussableScript __instance)
	{
		__instance.transform.position = (Vector3)(Vector2)__instance.transform.position - 2f * Vector3.forward;
		__instance.truePos = __instance.transform.position;
		__instance.seuil = 12;
		if (!(__instance.truePos == new Vector3(13.5f, 580.5f, -2f) || __instance.truePos == new Vector3(19.5f, 578.5f, -2f) || __instance.truePos == new Vector3(48.5f, 592.5f, -2f) || __instance.truePos == new Vector3(26.5f, 607.5f, -2f) || __instance.truePos == new Vector3(30.5f, 607.5f, -2f) || __instance.truePos == new Vector3(34.5f, 607.5f, -2f) || __instance.truePos == new Vector3(566.5f, 42.5f, -2)))
		{
			__instance.transitionTime = .5f;
		}
	}
}
