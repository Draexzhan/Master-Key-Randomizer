using HarmonyLib;
using UnityEngine;

namespace MenuMod.patches;

class TitleImagePatch1
{
    bool newTitle = false;
    [HarmonyPrefix]
    [HarmonyPatch(typeof(titleRotateOnButtonIsSelected), nameof(titleRotateOnButtonIsSelected.OnSelect))]
    public static void NewCredits(titleRotateOnButtonIsSelected __instance)
    {
        AssetBundle Bundle = AssetBundle.LoadFromFile("BepInEx\\plugins\\MasterKeyRandomizer\\masterkeyrandoassets");
        __instance.signatureSprite = Bundle.LoadAsset<Sprite>("signatures");
        Bundle.Unload(false);
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(MainMenuScript), nameof(MainMenuScript.Update))]
    public static void NewTitle(MainMenuScript __instance)
    {
            AssetBundle Bundle = AssetBundle.LoadFromFile("BepInEx\\plugins\\MasterKeyRandomizer\\masterkeyrandoassets");
            __instance.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = Bundle.LoadAsset<Sprite>("titleForTitleScreen11");
            Bundle.Unload(false);
    }
}
