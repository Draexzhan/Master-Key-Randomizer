using BepInEx;
using HarmonyLib;
using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.TextCore.Text;
using UnityEngine.Tilemaps;
using static UnityEngine.UIElements.UIR.BestFitAllocator;
using UnityEngine.SceneManagement;

namespace IntroV2Override.patches;

class IntroScriptV2Patch1
{

    [HarmonyPrefix]
    [HarmonyPatch(typeof(introScriptV2), nameof(introScriptV2.criChute))]
    public static void CriChutePrefix(introScriptV2 __instance)
	{
        MonoBehaviour.print("This is the part where the wall breaks!");
        CassableScript[] componentsInChildren = __instance.transform.GetComponentsInChildren<CassableScript>();
        for (int i = 0; i < componentsInChildren.Length; i++)
        {
            componentsInChildren[i].transform.Translate(16, 999, 999);
            componentsInChildren[i].casse();
        }
        //PlayerPrefs.SetString(GameObject.FindGameObjectWithTag("Player").GetComponent<FoxMove>().saveslot + "respawn", "keyCave");
        __instance.got = true;
		
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(PlayerHealthAndItemScript), nameof(PlayerHealthAndItemScript.GoodToGo))]
    public static void GoodToGo(PlayerHealthAndItemScript __instance)
    {

        if ((__instance.character.rez == 0) && (__instance.character.respawnPoint == "keyCave"))
        {
            __instance.DL.GetComponent<SceneLoader>().futurePosition = new Vector3(15.5f, -100f, 10f);
            __instance.DL.GetComponent<SceneLoader>().futureDirectionFacing = 2;
        }
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(WorldHolderScript), nameof(WorldHolderScript.Start))]
    public static void ExtraSpawnPatch1(WorldHolderScript __instance)
    {
        if (PlayerPrefs.GetString(GameObject.FindGameObjectWithTag("Player").GetComponent<FoxMove>().saveslot + "respawn") == "keyCave")
        {
            __instance.OWmaster.SetActive(value: true);
            Tilemap[] componentsInChildren = __instance.UWmaster.GetComponentsInChildren<Tilemap>();
            for (int i = 0; i < componentsInChildren.Length; i++)
            {
                componentsInChildren[i].RefreshAllTiles();
            }
        }
    }
    [HarmonyPrefix]
    [HarmonyPatch(typeof(salleDeGymAfterShaker), nameof(salleDeGymAfterShaker.OnTriggerEnter2D))]
    private static void OnTriggerEnter2DPatch(salleDeGymAfterShaker __instance, ref Collider2D collision)
    {
        if (!collision.CompareTag("Player") || __instance.fini != 0 || __instance.entered)
        {
            return;
        }
        MonoBehaviour.print("ok");
        if (collision.GetComponent<FoxMove>().muscle > 0 && collision.GetComponent<FoxMove>().picLVL > 0)
        {
            __instance.entered = true;
            __instance.fini = 1;
            string text = GameObject.FindGameObjectWithTag("Player").GetComponent<FoxMove>().saveslot + __instance.gameObject.name + SceneManager.GetActiveScene().name;
            PlayerPrefs.SetInt(text, 1);
            string key = GameObject.FindGameObjectWithTag("Player").GetComponent<FoxMove>().saveslot + "infoWorld";
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
            for (int i = 0; i < __instance.blocs.Length; i++)
            {
                __instance.blocs[i].SetActive(value: true);
                __instance.info.SetActive(value: true);
            }
        }
    }
}