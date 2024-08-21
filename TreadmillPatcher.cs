using HarmonyLib;
using UnityEngine;
using static ItemCheatSheet;
using static UpdateInventory;

class TreadmillPatch1
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(treadmillScript), nameof(treadmillScript.Update))]
    public static bool TreadmillPatch(treadmillScript __instance)
    {
        if (__instance.vitesse >= 1f)
        {
            __instance.counter += Time.deltaTime;
        }
        else
        {
            __instance.counter = 0f;
        }
        if (__instance.counter > 5f)
		{
			string treadmillName;
			if (__instance.transform.position.x < -500f)
			{
				treadmillName = "Gym - Treadmill";
			}
			else if (__instance.transform.position.y > 500f)
			{
				treadmillName = "Haunted House - Treadmill";
			}
			else
			{
				treadmillName = "Ruined City - Treadmill";
			}
			if (__instance.vitesse >= 1f && PlayerPrefs.GetInt(__instance.player.GetComponent<FoxMove>().saveslot + treadmillName, 0) != 1)
            {
				string text = Object.FindObjectOfType<FoxMove>().saveslot + treadmillName;
				PlayerPrefs.SetInt(text, 1);
				string key = Object.FindObjectOfType<FoxMove>().saveslot + "infoWorld";
				string[] stringArray = PlayerPrefsX.GetStringArray(key);
				string[] array = new string[stringArray.Length + 1];
				stringArray.CopyTo(array, 0);
				new string[1] { text }.CopyTo(array, stringArray.Length);
				PlayerPrefsX.SetStringArray(key, array);
				string[] stringArray2 = PlayerPrefsX.GetStringArray("binaryResetOnQuit");
				string[] array2 = new string[stringArray2.Length + 1];
				stringArray2.CopyTo(array2, 0);
				new string[1] { text }.CopyTo(array2, stringArray2.Length);
				PlayerPrefsX.SetStringArray("binaryResetOnQuit", array2);
				Debug.Log(treadmillName);
                ItemData TreadmillReward = CheckClass.GetData(treadmillName).CheckItem;
                AddToInventory(TreadmillReward);
                __instance.gave = true;
            }
        }
        return false;
    }
}