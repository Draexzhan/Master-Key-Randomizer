using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;
using static MasterKeyRandomizer.MKLogger;

namespace FinalBossPatcher.patches;
class FinalBossPatch
{
	[HarmonyPrefix]
	[HarmonyPatch(typeof(FC_cinematic), nameof(FC_cinematic.OnTriggerEnter2D))]
	public static bool GoModeChecker(FC_cinematic __instance)
	{
		string saveSlot = GameObject.FindGameObjectWithTag("Player").GetComponent<FoxMove>().saveslot;
		if (PlayerPrefs.GetInt(saveSlot + "FinalBossAccess", 0) == 0) //no extra clear conditions for boss
		{
			LogDebug("No bosses required. Starting final boss.");
			return true;
		}
		else if (PlayerPrefs.GetInt(saveSlot + "FinalBossAccess", 0) == 1)
		{
			LogDebug("Four bosses required.");
			return (PlayerPrefs.GetInt(saveSlot + "BOSS1MOLETempleSO", 0) == 1
				&& PlayerPrefs.GetInt(saveSlot + "BOSS2GHOSTManoirHante", 0) == 1
				&& PlayerPrefs.GetInt(saveSlot + "BOSS3SHIPTempleNeige", 0) == 1
				&& PlayerPrefs.GetInt(saveSlot + "BOSS4HYDRACentrale", 0) == 1);
		}
		else if (PlayerPrefs.GetInt(saveSlot + "FinalBossAccess", 0) == 2)
		{
			LogDebug("All bosses required.");
			return (PlayerPrefs.GetInt(saveSlot + "BOSS1MOLETempleSO", 0) == 1
				&& PlayerPrefs.GetInt(saveSlot + "BOSS2GHOSTManoirHante", 0) == 1
				&& PlayerPrefs.GetInt(saveSlot + "BOSS3SHIPTempleNeige", 0) == 1
				&& PlayerPrefs.GetInt(saveSlot + "BOSS4HYDRACentrale", 0) == 1
				&& PlayerPrefs.GetInt(saveSlot + "BOSSDIVEForge", 0) == 1);
		}
		return true;
	}
}