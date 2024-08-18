using BepInEx;
using HarmonyLib;
using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Bindings;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;
using static Mono.Security.X509.X520;
using static ItemCheatSheet;
using static UpdateInventory;
using static MasterKeyRandomizer.MKLogger;
using System.IO;
using UnityEngine.SocialPlatforms;
using static UnityEngine.UIElements.StylePropertyAnimationSystem;

namespace RareItem.patches;

class RareItemPatcher
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(objetRareScript), nameof(objetRareScript.Start))]
    public static bool StartPatch1(objetRareScript __instance)
    {
        __instance.joueur = GameObject.FindGameObjectWithTag("Player");
        __instance.OrigPos = __instance.transform.position;
        string itemname;
        string LocationID = __instance.gameObject.name + __instance.OrigPos.ToString();
		LogInfo(LocationID);
        try
        {
            __instance.gameObject.GetComponent<SpriteRenderer>().sprite = UpdateAppearance(LocationID);
            itemname = CheckClass.GetData(LocationID).CheckItem.Name;
        }
        catch (Exception) { LogInfo("itemname errored."); itemname = GetData("Error").Name; }

        //I think there's an issue with potions and warps not having a nextitem.
		if (__instance.nextItem != null)
		{
			__instance.copyOfNextItem = UnityEngine.Object.Instantiate(__instance.nextItem, __instance.transform.parent);
			__instance.copyOfNextItem.SetActive(value: false);
			__instance.copyOfNextItem.name = __instance.nextItem.name;
		}
		__instance.isPotion = false;
		if ((((__instance.isPomme || __instance.isFromage || __instance.isRez || __instance.isRez2 || __instance.isCafe || __instance.pierreFinale) && !__instance.EnCoffre && CheckClass.GetData(__instance.gameObject.name + __instance.OrigPos.ToString()).LocationName != "Fromage(-118.00, 101.94, -1.00)") || itemname == "Ruins Warp" || itemname == "Start Warp" || itemname == "Woods Potion" || itemname == "Snow Potion") && itemname != "Error")
        {
            __instance.isUWMap = true;
            LogInfo(itemname + " should respawn.");
		}
		if (__instance.isRez && __instance.joueur.GetComponent<FoxMove>().rez > 0 && !__instance.EnCoffre)
		{
			UnityEngine.Object.Destroy(__instance.gameObject);
		}
		if (__instance.isRez2 && __instance.joueur.GetComponent<FoxMove>().rez > 1 && !__instance.EnCoffre)
		{
			UnityEngine.Object.Destroy(__instance.gameObject);
		}
		return false;
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(objetRareScript), nameof(objetRareScript.grabIt))]
    public static bool GrabRarePatch1(objetRareScript __instance)
	{
		__instance.joueur = GameObject.FindGameObjectWithTag("Player");
        string CheckName = CheckClass.GetData(__instance.gameObject.name + __instance.OrigPos.ToString()).LocationName;
        PlayerPrefs.SetInt(UnityEngine.Object.FindObjectOfType<FoxMove>().saveslot + CheckName + "Collected", 1);
		if ((__instance.gameObject.name + __instance.OrigPos.ToString()).Equals("Force2(165.50, 229.50, -1.00)"))
        {
            CassableScript MyRock = FindMyRock(new Vector3(165, 232, 0));
            if (MyRock)
                MyRock.casse(true);
        }
        if (!__instance.used && __instance.joueur.GetComponent<FoxMove>().argent >= __instance.valeur && !__instance.EnCoffre)
		{
			if (!__instance.gameObject.name.Contains("(Clone)"))
			{
			    __instance.gameObject.GetComponent<SpriteRenderer>().sprite = UpdateAppearance(__instance.gameObject.name + __instance.OrigPos.ToString());
			}
			__instance.used = true;
			LogInfo("ItemData grabbed.");
			if (!__instance.isUWMap)
			{
				LogInfo("The item formerly known as " + __instance.gameObject.name + " has been destroyed.");
				UnityEngine.Object.Destroy(__instance.gameObject);
                return false;
            }
			return false;
        }
        else if (__instance.EnCoffre)
        {
            UnityEngine.Object.Destroy(__instance.gameObject);
        }
        return false;
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(objetRareScript), nameof(objetRareScript.OnDestroy))]
    public static bool DestroyPatch(objetRareScript __instance)
	{
		try
		{
			string itemname = CheckClass.GetData(__instance.gameObject.name + __instance.OrigPos.ToString()).CheckItem.Name;
			string CheckName = CheckClass.GetData(__instance.gameObject.name + __instance.OrigPos.ToString()).LocationName;
			PlayerPrefs.SetInt(UnityEngine.Object.FindObjectOfType<FoxMove>().saveslot + CheckName + "Collected", 1);
		}
		catch (Exception) { LogError("There was an error saving the collected status of this check."); }
		if ((__instance.valeur > 0 || __instance.isUWMap) && __instance.joueur != null)
		{
			MonoBehaviour.print(__instance.gameObject.transform.parent);
			MonoBehaviour.print(__instance.gameObject.name);
			MonoBehaviour.print(__instance.OrigPos);
			Transform transform = UnityEngine.Object.Instantiate(__instance.pancarteVendu, __instance.OrigPos, Quaternion.identity, __instance.gameObject.transform.parent);
			if (__instance.nextItem != null)
			{
				transform.GetComponent<spawnSmthOnPlayerFar>().ToSpawn = __instance.copyOfNextItem;
			}
		}
        return false;
	}

    public static CassableScript FindMyRock(Vector3 pos)
    {
        CassableScript[] Boulders = UnityEngine.Object.FindObjectsOfType<CassableScript>();
        for (int i = 0; i < Boulders.Length; i++)
        {
            if (Boulders[i].transform.position == pos) return Boulders[i];
        }
        return null;
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(objetRareScript), nameof(objetRareScript.OnBecameVisible))]
    public static bool DontDestroyThatWeaponWeHaveProgressivesOnThisLiterallyJustGetsInTheWayAndItsNotLikeWeAreUsingTheOriginalItemDataInThisRandomizerAnyway( objetRareScript __instance)
    {
        try
        {
			string CheckName = CheckClass.GetData(__instance.gameObject.name + __instance.OrigPos.ToString()).LocationName;
			string itemName = CheckClass.GetData(__instance.gameObject.name + __instance.OrigPos.ToString()).CheckItem.Name;
			if (PlayerPrefs.GetInt(UnityEngine.Object.FindObjectOfType<FoxMove>().saveslot + CheckName + "Collected") > 0 && !__instance.isUWMap)
            {
                UnityEngine.Object.Destroy(__instance.gameObject);
			}
			else if ((itemName == "Woods Potion" || itemName == "Snow Potion" || itemName == "Ruins Warp" || itemName == "Start Warp") && PlayerPrefs.GetInt(UnityEngine.Object.FindObjectOfType<FoxMove>().saveslot + CheckName) > 0)
			{
				__instance.valeur = 0;
                int i = __instance.GetComponent<Transform>().childCount - 1;
                foreach (GameObject child in __instance.transform.GetChild(i))
                {
                    GameObject.Destroy(child.gameObject);
                    i--;
                    LogInfo("Destroying price tag");
                }
			}
		}
        catch (Exception) { }
        return false; 
    }
}
