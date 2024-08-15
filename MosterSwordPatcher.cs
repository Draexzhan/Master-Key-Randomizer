using HarmonyLib;
using BepInEx.Logging;
using System.IO;
using System.Reflection;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UpdateInventory;
using static ItemCheatSheet;
using static CheckClass;
using static MasterKeyRandomizer.MKLogger;
using BepInEx;
using MasterKeyRandomizer;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Threading;
using UnityEngine.UIElements;
using UnityEngine.TextCore.Text;
using UnityEngine.SocialPlatforms.Impl;
using System.Numerics;

public class MosterSwordPatcher
{
	[HarmonyPrefix]
	[HarmonyPatch(typeof(oopsMasterSwordCutscene), nameof(oopsMasterSwordCutscene.OnTriggerEnter2D))]
	public static bool Enter2DPatcher(oopsMasterSwordCutscene __instance)
	{
		__instance.player = GameObject.FindGameObjectWithTag("Player").GetComponent<FoxMove>();
		if (__instance.player.PDVActuels > __instance.PDVentrant && !__instance.done && !__instance.player.isSouterre && !__instance.player.isRolling && !__instance.player.isFloating)
		{
			__instance.PDVentrant = __instance.player.PDVActuels;
			GameObject.FindGameObjectWithTag("Controls").GetComponent<FoxControllerScript>().disableMenu = true;
			__instance.StartCoroutine(MosterSwordRedux(__instance));
		}
		return false;
	}

	static IEnumerator MosterSwordRedux(oopsMasterSwordCutscene __instance)
	{
		__instance.player = GameObject.FindGameObjectWithTag("Player").GetComponent<FoxMove>();
		__instance.playerTargetPos = __instance.transform.position + 0.75f * UnityEngine.Vector3.up;
		yield return new WaitForSeconds(1f);
		__instance.playerTargetPos = UnityEngine.Vector2.zero;
		__instance.player.GetComponent<FoxMove>().setDesactiveMoveCinematics(val: true);
		GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMonoMove>().changeVolumeMultiplierOverTime(0f, 2f);
		__instance.fakePlayerInst = Object.Instantiate(__instance.fakePlayer, __instance.transform.position, UnityEngine.Quaternion.identity);
		__instance.player.gameObject.GetComponent<SpriteRenderer>().enabled = false;
		__instance.Sword.GetComponent<SpriteRenderer>().sortingOrder = 0;
		__instance.player.setDesactiveMoveCinematics(val: true);
		for (int i = 0; i < 50; i++)
		{
			if (i == 1)
			{
				__instance.fakePlayerInst.GetComponent<SpriteRenderer>().sprite = __instance.fakePlayerSprites[0];
			}
			if (i == 2)
			{
				__instance.fakePlayerInst.GetComponent<SpriteRenderer>().sprite = __instance.fakePlayerSprites[1];
			}
			if (i == 3)
			{
				__instance.fakePlayerInst.GetComponent<SpriteRenderer>().sprite = __instance.fakePlayerSprites[0];
			}
			if (i == 5)
			{
				__instance.fakePlayerInst.GetComponent<SpriteRenderer>().sprite = __instance.fakePlayerSprites[1];
			}
			if (i == 6)
			{
				__instance.fakePlayerInst.GetComponent<SpriteRenderer>().sprite = __instance.fakePlayerSprites[0];
				__instance.instScin = Object.Instantiate(__instance.prefabScintille, __instance.transform.position + UnityEngine.Vector3.up * 0.5f, UnityEngine.Quaternion.identity);
				ParticleSystem.MainModule main = __instance.instScin.GetComponent<ParticleSystem>().main;
				main.simulationSpeed = 0.8f;
				__instance.chestSFXsansCroc.volume = GameObject.FindGameObjectWithTag("Starter").GetComponent<starterScript>().SFXVol;
				__instance.chestSFXsansCroc.Play();
			}
			if (i == 10)
			{
				__instance.fakePlayerInst.GetComponent<SpriteRenderer>().sprite = __instance.fakePlayerSprites[1];
			}
			if (i == 14)
			{
				__instance.fakePlayerInst.GetComponent<SpriteRenderer>().flipX = true;
				__instance.fakePlayerInst.GetComponent<SpriteRenderer>().sprite = __instance.fakePlayerSprites[0];
			}
			if (i == 17)
			{
				__instance.fakePlayerInst.GetComponent<SpriteRenderer>().sprite = __instance.fakePlayerSprites[1];
			}
			if (i == 20)
			{
				__instance.fakePlayerInst.GetComponent<SpriteRenderer>().sprite = __instance.fakePlayerSprites[0];
			}
			if (i == 23)
			{
				__instance.fakePlayerInst.GetComponent<SpriteRenderer>().sprite = __instance.fakePlayerSprites[1];
			}
			if (i == 26)
			{
				__instance.fakePlayerInst.GetComponent<SpriteRenderer>().sprite = __instance.fakePlayerSprites[0];
			}
			if (i == 29)
			{
				__instance.fakePlayerInst.GetComponent<SpriteRenderer>().sprite = __instance.fakePlayerSprites[1];
			}
			if (i == 32)
			{
				__instance.fakePlayerInst.GetComponent<SpriteRenderer>().flipX = false;
				__instance.fakePlayerInst.GetComponent<SpriteRenderer>().sprite = __instance.fakePlayerSprites[2];
			}
			if (i == 35)
			{
				__instance.fakePlayerInst.GetComponent<SpriteRenderer>().sprite = __instance.fakePlayerSprites[3];
			}
			if (i == 38)
			{
				__instance.fakePlayerInst.GetComponent<SpriteRenderer>().sprite = __instance.fakePlayerSprites[2];
			}
			if (i == 41)
			{
				__instance.fakePlayerInst.GetComponent<SpriteRenderer>().sprite = __instance.fakePlayerSprites[3];
			}
			if (i == 44)
			{
				__instance.fakePlayerInst.GetComponent<SpriteRenderer>().sprite = __instance.fakePlayerSprites[2];
			}
			if (i == 47)
			{
				__instance.fakePlayerInst.GetComponent<SpriteRenderer>().sprite = __instance.fakePlayerSprites[3];
			}
			if (i < 3)
			{
				yield return new WaitForSeconds(1f);
			}
			if (i >= 3 && i < 6)
			{
				yield return new WaitForSeconds(0.5f);
			}
			if (i >= 6)
			{
				yield return new WaitForSeconds(0.2f);
			}
			if (__instance.player.PDVActuels > 1)
			{
				__instance.player.PDVActuels--;
				AudioSource.PlayClipAtPoint(__instance.perdPDVSFX, __instance.gameObject.transform.position, GameObject.FindGameObjectWithTag("Starter").GetComponent<starterScript>().SFXVol * 0.25f);
				continue;
			}
			__instance.chestSFXsansCroc.Stop();
			__instance.player.setDesactiveMoveCinematics(val: false);
			GameObject.FindGameObjectWithTag("Controls").GetComponent<FoxControllerScript>().disableMenu = false;
			__instance.player.PDVActuels = __instance.PDVentrant;
			__instance.player.gameObject.GetComponent<SpriteRenderer>().enabled = true;
			Object.Destroy(__instance.fakePlayerInst.gameObject);
			Object.Destroy(__instance.instScin.gameObject);
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMonoMove>().changeVolumeMultiplierOverTime(1f, 2f);
			LogInfo("nah");
			yield break;
		}
		AudioSource.PlayClipAtPoint(__instance.perdPDVSFX, __instance.gameObject.transform.position, GameObject.FindGameObjectWithTag("Starter").GetComponent<starterScript>().SFXVol * 0.25f);
		__instance.player.PDVActuels = __instance.PDVentrant;
		AddToInventory(CheckClass.GetData("DreamSword").CheckItem);
		UnityEngine.Object.Destroy(__instance.Sword.gameObject);
		UnityEngine.Object.Destroy(__instance.fakePlayerInst.gameObject);
		LogInfo("seven years later...");
		__instance.player.setDesactiveMoveCinematics(val: false);
		GameObject.FindGameObjectWithTag("Controls").GetComponent<FoxControllerScript>().disableMenu = false;
		GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMonoMove>().changeVolumeMultiplierOverTime(1f, 2f);
		__instance.done = true;
	}
}
