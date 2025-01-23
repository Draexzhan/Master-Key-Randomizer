using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Linq;
using static MasterKeyRandomizer.MasterKeyRandomizer;
using MasterKeyRandomizer;
using BepInEx;

namespace MenuMod.patches;

class TitleImagePatch1
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(titleRotateOnButtonIsSelected), nameof(titleRotateOnButtonIsSelected.OnSelect))]
    public static void NewCredits(titleRotateOnButtonIsSelected __instance)
    {
        AssetBundle Bundle = AssetBundle.LoadFromFile("BepInEx\\plugins\\MasterKeyRandomizer\\masterkeyrandoassets");
        __instance.signatureSprite = Bundle.LoadAsset<Sprite>("signatures");
        Bundle.Unload(false);
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(mainMenuSaveSelectDecal), nameof(mainMenuSaveSelectDecal.Start))]
    public static void NewTitle(mainMenuSaveSelectDecal __instance)
	{
		AssetBundle.UnloadAllAssetBundles(false);
		VanillaMode();
		AssetBundle Bundle = AssetBundle.LoadFromFile("BepInEx\\plugins\\MasterKeyRandomizer\\masterkeyrandoassets");
        GameObject Title = GameObject.Find("title");
        Title.GetComponent<SpriteRenderer>().sprite = Bundle.LoadAsset<Sprite>("titleForTitleScreen11");
        Title.transform.position = new Vector3(-1000f, 2.9f, 0);
        Bundle.Unload(false);
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(starterScript), nameof(starterScript.Start))]
    public static void PreloadAssets(starterScript __instance)
    {
        ThreadingHelper.Instance.StartCoroutine(InitializeTraps());
	}
	static IEnumerator InitializeTraps()
	{
        MKLogger.LogInfo("About to get the cannonball");
        AsyncOperation NeigeScene = SceneManager.LoadSceneAsync("TempleNeige", LoadSceneMode.Additive);
		NeigeScene.allowSceneActivation = false;
		yield return new WaitUntil(() => NeigeScene.progress >= 0.9f);
        tombeRocheScript CannonBall = Resources.FindObjectsOfTypeAll<tombeRocheScript>().First();
        GameObject.Instantiate(CannonBall);
		MKLogger.LogInfo("CannonBallTrap should exist now");
	}
	ThreadingHelper Instance { get { return ThreadingHelper.Instance; } }
}
