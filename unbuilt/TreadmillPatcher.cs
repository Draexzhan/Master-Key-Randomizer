using BepInEx;
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
            if (__instance.vitesse == 1f && PlayerPrefs.GetInt(__instance.player.GetComponent<FoxMove>().saveslot + __instance.transform.position.ToString(), 0) == 0)
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
                Debug.Log(treadmillName);
                ItemData TreadmillReward = CheckClass.GetData(treadmillName).CheckItem;
                AddToInventory(TreadmillReward);
                __instance.gave = true;
            }
        }
        return false;
    }
}