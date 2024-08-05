//using BepInEx;
//using HarmonyLib;
//using System;
//using System.Reflection;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Bindings;
//using static UnityEngine.UIElements.UxmlAttributeDescription;
//using UnityEngine.UIElements;
//using UnityEngine.SceneManagement;
//using static System.Net.Mime.MediaTypeNames;
//using static Mono.Security.X509.X520;
//using static ItemCheatSheet;
//using static UpdateInventory;
//using MasterKeyRandomizer;
//using System.IO;

//namespace CommonItem.patches;

//class CommonItemPatcher
//{
//    public static class RandoItem
//    {
//        public static ItemData itemData { get; private set; }
//    }
//    [HarmonyPrefix]
//    [HarmonyPatch(typeof(pieceScript), nameof(pieceScript.Start))]
//    public static bool StartPatch1(pieceScript __instance)
//    {
//        if (__instance.EnCoffre)
//        {
//            ItemData itemData;
//            itemData = GetItemData("Boomerang");
//            if (itemData != null)
//            {
//                FoxMove foxMove = UnityEngine.Object.FindObjectOfType<FoxMove>();
//                FieldInfo fieldInfo = foxMove.GetType().GetField(itemData.ItemNameForSave);
//                AssetBundle Bundle = AssetBundle.LoadFromFile("BepInEx\\plugins\\MasterKeyRandomizer\\masterkeyrandoassets");
//                __instance.gameObject.GetComponent<SpriteRenderer>().sprite = Bundle.LoadAsset<Sprite>(itemData.Name + " " + ((int)fieldInfo.GetValue(foxMove) + 1) + ".png");
//                Bundle.Unload(false);
//            }
//            return false;
//        }
//        return true;
//    }

//    [HarmonyPrefix]
//    [HarmonyPatch(typeof(pieceScript), nameof(pieceScript.grabIt))]
//    public static bool GrabCommonPatch1(pieceScript __instance)
//    {
//        if (GetItemData("boomerang") != null)
//        {
//            if (__instance.used)
//            {
//                return false;
//            }
//            __instance.used = true;
//            UnityEngine.Object.Destroy(__instance.gameObject, __instance.EnCoffre ? 2.5f : 1f);
//            __instance.body.velocity = Vector2.zero;
//            if (__instance.EnCoffre)
//            {
//                __instance.transform.position = __instance.transform.position + 0.75f * Vector3.up;
//                if (__instance.inChestSFX != null)
//                {
//                    ItemData randoItem = ItemCheatSheet.GetData("Boomerang");
//                    UpdateInventory.AddToInventory(randoItem);
//                    UnityEngine.Object.Destroy(__instance.gameObject);
//                    return false;
//                }
//                __instance.body.isKinematic = true;
//            }
//            return true;
//        }
//        return true;
//    }

//    public static ItemData GetItemData(string item)
//    {
//        ItemData newItemData = ((ItemCheatSheet.GetData("Boomerang") != null) ? ItemCheatSheet.GetData("Boomerang") : null);
//        return newItemData;
//    }
//}
