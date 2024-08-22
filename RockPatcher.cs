using HarmonyLib;
using UnityEngine;

namespace Rock.patches;

class RockPatch1
{

	[HarmonyPostfix]
	[HarmonyPatch(typeof(CassableScript), nameof(CassableScript.Start))]
	public static void RockPostfix(CassableScript __instance)
	{
		if (__instance.transform.parent.name == "bloqueurs" && __instance.tag =="BlockGrappin")
			__instance.transform.position = new Vector3(__instance.origPos.x, 1.2f + __instance.origPos.y, 0f);
	}
}
