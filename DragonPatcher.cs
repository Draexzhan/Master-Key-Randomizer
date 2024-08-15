using BepInEx;
using HarmonyLib;
using MasterKeyRandomizer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;
using static MasterKeyRandomizer.MKLogger;
using UnityStandardAssets.Utility;
using static ItemCheatSheet;
using static UpdateInventory;

public class DragonPatcher : MonoBehaviour
{
	private static readonly SynchronizationContext context = SynchronizationContext.Current;
	[HarmonyPrefix]
    [HarmonyPatch(typeof(dragonCristal), nameof(dragonCristal.OnTriggerEnter2D))]
    public static bool DragonPatcherCollide(dragonCristal __instance, ref Collider2D collision)
    {
        if (collision.gameObject == __instance.player && __instance.playerS.cristalsGiven < __instance.playerS.cristals && __instance.toGive == 0 && !__instance.playerS.isSouterre && !__instance.playerS.isRolling && !__instance.playerS.isRunning && !__instance.playerS.isFloating)
        {
            __instance.nextPalier = __instance.findNextPalier();
            __instance.toGive = Mathf.Min(__instance.playerS.cristals - __instance.playerS.cristalsGiven, __instance.rewards[__instance.nextPalier].cost - __instance.playerS.cristalsGiven);
            __instance.avaleIter = 0;
            if (__instance.toGive != 0)
            {
                __instance.playerS.setDesactiveMoveCinematics(val: true);
			    __instance.StartCoroutine(RandoExchange(__instance));
			}
        }
        return false;
    }
    private static IEnumerator RandoExchange(dragonCristal __instance)
    {
        __instance.playerS.directionFacing = 2;
        yield return new WaitForSeconds(0.5f);
        __instance.dragon.sprite = __instance.miam;
        __instance.playerS.objetTrouve(0.2f * (float)__instance.toGive + 0.5f);
        yield return new WaitForSeconds(0.5f);
        __instance.cristinstances = new List<Transform>();
        for (int k = 0; k < __instance.toGive; k++)
        {
            Transform item = UnityEngine.Object.Instantiate(__instance.cristalProjectile, __instance.player.transform.position + UnityEngine.Vector3.up, UnityEngine.Quaternion.identity);
            __instance.cristinstances.Add(item);
        }
        for (int j = 0; j < __instance.toGive; j++)
        {
            __instance.cristinstances[j].GetComponent<AutoMoveAndRotate>().moveUnitsPerSecond.value = UnityEngine.Vector3.up * 5f;
            __instance.Invoke("avale", 0.55f);
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(0.5f);
        for (int j = 0; j < 6; j++)
        {
            yield return new WaitForSeconds(0.25f);
            AudioSource.PlayClipAtPoint(__instance.crunchSFX, __instance.gameObject.transform.position, GameObject.FindGameObjectWithTag("Starter").GetComponent<starterScript>().SFXVol);
            __instance.dragon.sprite = __instance.crunch;
            yield return new WaitForSeconds(0.25f);
            __instance.dragon.sprite = __instance.munch;
        }
        if (__instance.playerS.cristalsGiven + __instance.toGive == __instance.rewards[__instance.nextPalier].cost)
        {
            yield return new WaitForSeconds(0.5f);
            __instance.dragon.sprite = __instance.crac;
            AudioSource.PlayClipAtPoint(__instance.tingSFX, __instance.gameObject.transform.position, GameObject.FindGameObjectWithTag("Starter").GetComponent<starterScript>().SFXVol);
            __instance.anim.enabled = false;
            __instance.anim.gameObject.GetComponent<SpriteRenderer>().sprite = __instance.chokingTail;
            yield return new WaitForSeconds(0.5f);
            __instance.dragon.sprite = __instance.choking1;
            AudioSource.PlayClipAtPoint(__instance.blipSFX, __instance.gameObject.transform.position, GameObject.FindGameObjectWithTag("Starter").GetComponent<starterScript>().SFXVol);
            yield return new WaitForSeconds(0.25f);
            __instance.dragon.sprite = __instance.choking2;
            AudioSource.PlayClipAtPoint(__instance.blipSFX, __instance.gameObject.transform.position, GameObject.FindGameObjectWithTag("Starter").GetComponent<starterScript>().SFXVol);
            yield return new WaitForSeconds(0.25f);
            __instance.dragon.sprite = __instance.choking1;
            AudioSource.PlayClipAtPoint(__instance.blipSFX, __instance.gameObject.transform.position, GameObject.FindGameObjectWithTag("Starter").GetComponent<starterScript>().SFXVol);
            yield return new WaitForSeconds(0.25f);
            __instance.dragon.sprite = __instance.choking2;
            AudioSource.PlayClipAtPoint(__instance.blipSFX, __instance.gameObject.transform.position, GameObject.FindGameObjectWithTag("Starter").GetComponent<starterScript>().SFXVol);
            yield return new WaitForSeconds(0.25f);
            __instance.dragon.sprite = __instance.pouah;
            AudioSource.PlayClipAtPoint(__instance.pouahSFX, __instance.gameObject.transform.position, GameObject.FindGameObjectWithTag("Starter").GetComponent<starterScript>().SFXVol);
            Transform obj = UnityEngine.Object.Instantiate(__instance.projectilePrefab, __instance.dragon.gameObject.transform.position, UnityEngine.Quaternion.identity);
            obj.GetComponent<projectile>().shooter = __instance.transform;
            obj.GetComponent<projectile>().velocity = 10f;
            obj.transform.Rotate(90f * UnityEngine.Vector3.forward);
            obj.GetComponent<ATKScript>().degats = 0;
            yield return new WaitForSeconds(1.5f);
            //Transform transform = UnityEngine.Object.Instantiate(__instance.rewards[__instance.nextPalier].rew, __instance.player.transform.position, UnityEngine.Quaternion.identity);
            AddToInventory(CheckClass.GetData("Dragon" + (__instance.nextPalier + 1)).CheckItem);
            __instance.anim.enabled = true;
            LogInfo("Dagron");
        }
        foreach (Transform cristinstance in __instance.cristinstances)
        {
            UnityEngine.Object.Destroy(cristinstance.gameObject);
        }
        yield return new WaitForSeconds(0.5f);
		LogInfo("Dargon");
		__instance.dragon.sprite = __instance.neutre;
        __instance.playerS.cristalsGiven += __instance.toGive;
        __instance.toGive = 0;
        __instance.playerS.setDesactiveMoveCinematics(val: false);
    }
}
