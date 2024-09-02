using HarmonyLib;

class QOLPatcher
{
	[HarmonyPrefix]
	[HarmonyPatch(typeof(regenPlayerHP), nameof(regenPlayerHP.Start))]
	public static void RegenStartPatch(regenPlayerHP __instance)
	{
		__instance.rate = (1f / 8f);
	}
}
