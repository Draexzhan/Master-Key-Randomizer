using HarmonyLib;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using System.Collections;
using static MasterKeyRandomizer.MKLogger;

namespace IntroV2Override.patches;

class IntroScriptV2Patch1
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(introScriptV2), nameof(introScriptV2.Start))]
    public static bool DontDeleteUnlessCollected(introScriptV2 __instance)
	{
		MonoBehaviour.print("This is the part where the wall breaks!");
		CassableScript[] componentsInChildren = __instance.transform.GetComponentsInChildren<CassableScript>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			GameObject.Destroy(componentsInChildren[i].gameObject);
		}
		__instance.GetComponentInChildren<objetRareScript>().transform.SetParent(null);
		__instance.timer = 0f;
		__instance.etape = 0;
		__instance.timer2 = 0f;
        __instance.player = GameObject.FindGameObjectWithTag("Player");
        __instance.player.GetComponent<FoxMove>().Save();
		LogDebug(PlayerPrefs.GetString(__instance.player.GetComponent<FoxMove>().saveslot + "respawn", "nul"));
		if (PlayerPrefs.GetInt(__instance.player.GetComponent<FoxMove>().saveslot + "IntroFinie", 0) == 1)
		{
            Object.Destroy(__instance.fakePlayer);
            return false; 
        }
        if (PlayerPrefs.GetString(__instance.player.GetComponent<FoxMove>().saveslot + "respawn", "nul") != "error")
        {
            Object.Destroy(__instance.fakePlayer);
            return false;
		}
        __instance.starter = GameObject.FindGameObjectWithTag("Starter").GetComponent<starterScript>();
		MonoBehaviour.print(PlayerPrefs.GetInt(__instance.player.GetComponent<FoxMove>().saveslot + "IntroFinie", 0));
		for (int i = 0; i < __instance.braseros.Length; i++)
		{
			__instance.braseros[i].transform.GetChild(0).gameObject.SetActive(value: false);
			__instance.braseros[i].transform.GetChild(1).gameObject.SetActive(value: false);
		}
		return false;
    }
    [HarmonyPrefix]
    [HarmonyPatch(typeof(introScriptV2), nameof(introScriptV2.casseMurs))]
    public static bool StirredNotShaken(introScriptV2 __instance)
    {
        return false;
    }

		[HarmonyPrefix]
    [HarmonyPatch(typeof(introScriptV2), nameof(introScriptV2.criChute))]
    public static void CriChutePrefix(introScriptV2 __instance)
	{
        PlayerPrefs.SetString(GameObject.FindGameObjectWithTag("Player").GetComponent<FoxMove>().saveslot + "respawn", "keyCave");
        if (__instance.player.GetComponent<FoxMove>().MasterKey == 1 && !__instance.got)
    		__instance.got = true;
	}

    [HarmonyPrefix]
    [HarmonyPatch(typeof(introScriptV2), nameof(introScriptV2.FixedUpdate))]
    public static bool FixedUpdatePrefix(introScriptV2 __instance)
	{
		return __instance.starter != null;
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

    //check if we need to use a different spawn coroutine
    [HarmonyPrefix]
    [HarmonyPatch(typeof(LoadSceneOnClick), nameof(LoadSceneOnClick.LoadSceneAndPlay))]
    private static bool GameLaunchPatch(LoadSceneOnClick __instance, ref string sceneName)
	{
		if (Time.timeScale == 1f && __instance.oldMainCam.GetComponent<fonduCam>().fonduCD >= __instance.oldMainCam.GetComponent<fonduCam>().fonduDuration)
		{
			__instance.oldMainCam.GetComponent<fonduCam>().fondu();
			PlayerPrefs.SetString("lastSlotLaunched", __instance.saveslot);
			PlayerPrefs.SetInt("ID" + __instance.saveslot, PlayerPrefs.GetInt("ID" + __instance.saveslot, 0) + 1);
            if (PlayerPrefs.GetString(__instance.saveslot + "respawn", "nul") == "keyCave" || PlayerPrefs.GetString(__instance.saveslot + "respawn", "nul") == "error")
			{
                PlayerPrefs.SetString(__instance.saveslot + "respawn", "keyCave");
				__instance.StartCoroutine(KeyCaveSpawn(__instance));
			}
            else
                __instance.StartCoroutine(__instance.launchGameAfterFondu(sceneName));
		}
        return false;
    }

    private static IEnumerator KeyCaveSpawn(LoadSceneOnClick __instance)
	{
        LogDebug("Should be spawning in the Key Cave.");
		Object.Destroy(__instance.EventSystemToDestroy);
		yield return new WaitForSeconds(__instance.oldMainCam.GetComponent<fonduCam>().fonduDuration);
		Object.Destroy(__instance.AudioListenerToDestroy);
		__instance.instCam = Object.Instantiate(__instance.mainCamera, new Vector3(15.5f, -100f, -10f), Quaternion.identity);
		__instance.instCam.GetComponent<Camera>().enabled = false;
		__instance.instCam.GetComponent<SpriteRenderer>().enabled = true;
		Transform obj9 = Object.Instantiate(__instance.fox, new Vector3(15.5f, -100f, 10f), Quaternion.identity);
		obj9.GetComponent<SpriteRenderer>().enabled = false;
        obj9.GetComponent<FoxMove>().directionFacing = 3;
		obj9.GetComponent<FoxMove>().saveslot = __instance.saveslot;
		__instance.instUI = Object.Instantiate(__instance.ui, new Vector3(-1000f, -1000f, 0f), Quaternion.identity);
		__instance.instUI.GetComponentInChildren<Camera>().rect = Rect.zero;
		__instance.oldMainCam.transform.GetComponent<Camera>().rect = Rect.zero;
		__instance.Invoke("fixNullRef", 0.1f);
	}

}