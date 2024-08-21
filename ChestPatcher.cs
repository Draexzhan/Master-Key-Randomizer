using HarmonyLib;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UpdateInventory;
using static ItemCheatSheet;
using static MasterKeyRandomizer.MKLogger;

namespace ChestPatcher.patches;

class ChestPatcherPatch1
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(CoffreScript), nameof(CoffreScript.Start))]
    public static void RepeatablePatch(CoffreScript __instance)
	{
        try
        {
            ItemData Treasure = CheckClass.GetData(__instance.gameObject.name + __instance.transform.position.ToString()).CheckItem;
            if (Treasure.Name == "Ruins Warp" || Treasure.Name == "Start Warp" || Treasure.Name == "Woods Potion" || Treasure.Name == "Snow Potion")
            {
                __instance.repeatable = true;
            }
            else __instance.repeatable = false;
        }
        catch (Exception e) { LogError(e.ToString()); }
	}

    [HarmonyPrefix]
    [HarmonyPatch(typeof(CoffreScript), nameof(CoffreScript.OnTriggerStay2D))]
    public static bool OnTriggerStay2DPatch1(CoffreScript __instance, ref Collider2D collision)
    {
        //MonoBehaviour.print(collision.gameObject.name);
        if (collision.gameObject.tag == "Player" && __instance.player.GetComponent<FoxMove>().PDVActuels > 0 && __instance.coffreVide == 0 && (!__instance.coffreCaché || __instance.player.GetComponent<FoxMove>().vision > 0) && __instance.soulo == (__instance.player.GetComponent<FoxMove>().isSoulo || (__instance.player.GetComponent<FoxMove>().isSouterre && __instance.player.GetComponent<FoxMove>().souterreTimer > 0.4f)) && !__instance.player.GetComponent<FoxMove>().isFloating && !__instance.player.GetComponent<FoxMove>().invulnerable && (!__instance.key1 || __instance.player.GetComponent<FoxMove>().Fragment1 == 1) && (!__instance.key2 || __instance.player.GetComponent<FoxMove>().Fragment2 == 1) && (!__instance.key3 || __instance.player.GetComponent<FoxMove>().Fragment3 == 1) && (!__instance.key4 || __instance.player.GetComponent<FoxMove>().Fragment4 == 1))
        {
            if (__instance.contenuPrefab.GetComponent<objetRareScript>() == null)
            {
                //_ = __instance.contenuPrefab.GetComponent<pieceScript>() != null;
                //__instance.player.GetComponent<FoxMove>().objetTrouve(2.5f);
            }
            else
            {
                __instance.contenuPrefab.GetComponent<objetRareScript>().EnCoffre = true;
                //__instance.player.GetComponent<FoxMove>().invulnerable = true;
                //__instance.player.GetComponent<FoxMove>().invulnerableCD = 0.5f;
            }
            __instance.player.GetComponent<Rigidbody2D>().velocity = UnityEngine.Vector2.zero;
            if (UnityEngine.Object.Instantiate(__instance.contenuPrefab, __instance.player.transform.position + UnityEngine.Vector3.right * 0.25f, UnityEngine.Quaternion.identity).TryGetComponent<pieceScript>(out var component))
            {
                UnityEngine.Object.Destroy(component.gameObject);
            }
            UnityEngine.Object.Destroy(__instance.contentBubble);
            __instance.coffreVide = 1;
            ItemData Treasure = CheckClass.GetData(__instance.gameObject.name + __instance.transform.position.ToString()).CheckItem;
            AddToInventory(Treasure);
            Debug.Log("Benis");
            string text = __instance.player.GetComponent<FoxMove>().saveslot + __instance.gameObject.name + SceneManager.GetActiveScene().name;
            PlayerPrefs.SetInt(text, __instance.coffreVide);
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
        }
        if (__instance.coffreVide == 1)
        {
            __instance.rendy.sprite = __instance.coffreOuvert;
            if (__instance.soulo)
            {
                __instance.gameObject.GetComponent<instGOrepeat>().enabled = false;
            }
        }
        return false;
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(CoffreScript), nameof(CoffreScript.OnCollisionStay2D))]
    public static bool OnCollisionStay2DPatch1(CoffreScript __instance, ref Collision2D collision)
    {
        //MonoBehaviour.print(collision.gameObject.name);
        if (collision.gameObject.tag == "Player" && __instance.player.GetComponent<FoxMove>().PDVActuels > 0 && __instance.coffreVide == 0 && (!__instance.coffreCaché || __instance.player.GetComponent<FoxMove>().vision > 0) && __instance.soulo == (__instance.player.GetComponent<FoxMove>().isSoulo || (__instance.player.GetComponent<FoxMove>().isSouterre && __instance.player.GetComponent<FoxMove>().souterreTimer > 0.4f)) && !__instance.player.GetComponent<FoxMove>().isFloating && !__instance.player.GetComponent<FoxMove>().invulnerable && (!__instance.key1 || __instance.player.GetComponent<FoxMove>().Fragment1 >= 1) && (!__instance.key2 || __instance.player.GetComponent<FoxMove>().Fragment2 >= 1) && (!__instance.key3 || __instance.player.GetComponent<FoxMove>().Fragment3 >= 1) && (!__instance.key4 || __instance.player.GetComponent<FoxMove>().Fragment4 >= 1))
        {
            if (__instance.contenuPrefab.GetComponent<objetRareScript>() == null)
            {
                //_ = __instance.contenuPrefab.GetComponent<pieceScript>() != null;
                //__instance.player.GetComponent<FoxMove>().objetTrouve(2.5f);
            }
            else
            {
                __instance.contenuPrefab.GetComponent<objetRareScript>().EnCoffre = true;
                //__instance.player.GetComponent<FoxMove>().invulnerable = true;
                //__instance.player.GetComponent<FoxMove>().invulnerableCD = 0.5f;
            }
            __instance.player.GetComponent<Rigidbody2D>().velocity = UnityEngine.Vector2.zero;
            /*if (UnityEngine.Object.Instantiate(__instance.contenuPrefab, __instance.player.transform.position + UnityEngine.Vector3.right * 0.25f, UnityEngine.Quaternion.identity).TryGetComponent<pieceScript>(out var component))
            {
                //component.EnCoffre = true;
            }*/
            UnityEngine.Object.Destroy(__instance.contentBubble);
            __instance.coffreVide = 1;
            ItemData Treasure = CheckClass.GetData(__instance.gameObject.name + __instance.transform.position.ToString()).CheckItem;
			AddToInventory(Treasure);
            Debug.Log("Crenis");
            string text = __instance.player.GetComponent<FoxMove>().saveslot + __instance.gameObject.name + SceneManager.GetActiveScene().name;
            PlayerPrefs.SetInt(text, __instance.coffreVide);
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
        }
        if (__instance.coffreVide == 1)
        {
            __instance.rendy.sprite = __instance.coffreOuvert;
            if (__instance.soulo)
            {
                __instance.gameObject.GetComponent<instGOrepeat>().enabled = false;
            }
        }
        return false;
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(CoffreScript), nameof(CoffreScript.Update))]
    public static bool UpdatePatchChest(CoffreScript __instance)
    {
        try
        {
            if (__instance.rendy.isVisible && !__instance.contentbubbleShown && __instance.player.GetComponent<FoxMove>().vision >= 2 && __instance.coffreVide == 0 && !__instance.key1 && !__instance.key2 && !__instance.key3 && !__instance.key4 && (bool)__instance.contenuPrefab.GetComponent<SpriteRenderer>())
            {
                __instance.contentbubbleShown = true;
                __instance.contentBubble = new GameObject();
                __instance.contentBubble.transform.parent = __instance.transform;
                __instance.contentBubble.AddComponent<SpriteRenderer>().sprite = __instance.cadreSprite;
                __instance.contentBubble.GetComponent<SpriteRenderer>().sortingOrder = 7;
                __instance.contentBubble.GetComponent<SpriteRenderer>().flipY = true;
                __instance.contentBubble.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                __instance.contentBubble.transform.position = __instance.transform.position + UnityEngine.Vector3.up;
                GameObject obj = new GameObject();
                obj.transform.parent = __instance.contentBubble.transform;
                obj.transform.localPosition = UnityEngine.Vector3.back;
                obj.AddComponent<SpriteRenderer>().sprite = UpdateAppearance(__instance.gameObject.name + __instance.transform.position.ToString());
                obj.GetComponent<SpriteRenderer>().sortingOrder = 7;
                obj.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            }
            if (__instance.contentBubble != null)
            {
                if (!__instance.contentBubble.activeSelf && __instance.rendy.isVisible)
                {
                    __instance.contentBubble.SetActive(value: true);
                }
                if (__instance.contentBubble.activeSelf && !__instance.rendy.isVisible)
                {
                    __instance.contentBubble.SetActive(value: false);
                }
            }
        }
        catch (Exception e) { LogError("Error in chest update: " + e.ToString());  }
        return false;
        }
}