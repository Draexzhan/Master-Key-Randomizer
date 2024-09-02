using HarmonyLib;

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
		__instance.seuil = 12;
		__instance.transitionTime = .5f;
	}
}
