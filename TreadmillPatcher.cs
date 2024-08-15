﻿using BepInEx;
using HarmonyLib;
using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Bindings;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using UnityEngine.SceneManagement;
using Steamworks;
using UnityEngine.UIElements.UIR;
using static Seed;
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
				treadmillName = "TreadmillGym";
			}
			else if (__instance.transform.position.y > 500f)
			{
				treadmillName = "TreadmillHaunt";
			}
			else
			{
				treadmillName = "TreadmillRuin";
			}
			if (__instance.vitesse == 1f && PlayerPrefs.GetInt(__instance.player.GetComponent<FoxMove>().saveslot + treadmillName, 0) != 1)
            {
				PlayerPrefs.SetInt(__instance.player.GetComponent<FoxMove>().saveslot + treadmillName, 1);
				string key = GameObject.FindGameObjectWithTag("Player").GetComponent<FoxMove>().saveslot + "infoWorld";
				string[] stringArray = PlayerPrefsX.GetStringArray(key);
				string[] array = new string[stringArray.Length + 1];
				stringArray.CopyTo(array, 0);
				new string[1] { treadmillName }.CopyTo(array, stringArray.Length);
				PlayerPrefsX.SetStringArray(key, array);
				string[] stringArray2 = PlayerPrefsX.GetStringArray("binaryResetOnQuit");
				string[] array2 = new string[stringArray2.Length + 1];
				stringArray2.CopyTo(array2, 0);
				new string[1] { treadmillName }.CopyTo(array2, stringArray2.Length);
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