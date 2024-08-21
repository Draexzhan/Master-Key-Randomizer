using HarmonyLib;
using UnityEngine;
using static MasterKeyRandomizer.MasterKeyRandomizer;

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
}
