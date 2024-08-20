using HarmonyLib;

namespace Music.patches;

class MusicPatch1
{

	[HarmonyPostfix]
	[HarmonyPatch(typeof(jukeBoxSongHandler), nameof(jukeBoxSongHandler.checkDisc))]
	public static void EnableAllSongs(jukeBoxSongHandler __instance, int id, ref bool __result)
	{
		if (id >= -1 && id <= 40 && id != 32) //33 is disabled until people find it legitimately.
		{
			__result = true;
		}
		else __result = false;
	}
}
