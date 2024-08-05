using HarmonyLib;
using BepInEx.Logging;
using System;
using System.IO;
using System.Reflection;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static ItemCheatSheet;
using static CheckClass;
using BepInEx;
using MasterKeyRandomizer;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Threading;
using UnityEngine.UIElements;

public class UpdateInventory : MonoBehaviour
{
    private GameObject rareEffectObj;
    private readonly SynchronizationContext context = SynchronizationContext.Current;
    private static bool busy = false;
    public static void AddToInventory(ItemData itemData)
    {
        if (busy)
        {
            busyCheck();
        }
        busy = true;
        Debug.Log(itemData.ItemNameForSave);
        FoxMove foxMove = UnityEngine.Object.FindObjectOfType<FoxMove>();
        FieldInfo fieldInfo = foxMove.GetType().GetField(itemData.ItemNameForSave);
        int foxInt = (int)fieldInfo.GetValue(foxMove);
        if (itemData.ItemNameForSave != "")
        {
            if (itemData.ItemNameForSave != "argent" || foxMove.argent < foxMove.maxArgent)
            {
                if (fieldInfo.FieldType == typeof(int))
                {
                    fieldInfo.SetValue(foxMove, Math.Min(foxInt + itemData.QuantityToGive, itemData.TierCap));
                }
                else if (fieldInfo.FieldType == typeof(float))
                {
                    fieldInfo.SetValue(foxMove, (float)Math.Min(foxInt + itemData.QuantityToGive, itemData.TierCap));
                }
            }
            if (itemData.ItemNameForSave.Contains("Fragment") && foxInt == 1)
            {
                foxMove.PDVMax += 2;
                foxMove.PDVActuels = foxMove.PDVMax;
            }
            if (foxMove.demiReceptacle >= 2)
            {
                foxMove.PDVMax += 2;
                foxMove.PDVActuels = foxMove.PDVMax;
                foxMove.demiReceptacle = 0;
            }
            if (itemData.ItemNameForSave == "PDVMax")
            {
                foxMove.PDVActuels = foxMove.PDVMax;
            }
            if (itemData.ItemNameForSave == "lanterne")
            {
                foxMove.transform.parent.GetChild(3).GetChild(0).GetComponent<LanterneSwitcher>().updateLanterneSprites();
                foxMove.transform.parent.GetChild(3).GetComponent<LanterneSwitcher>().updateLanterneSprites();
            }
            if (itemData.ItemNameForSave == "loupe")
            {
                foxMove.transform.parent.GetChild(4).GetComponent<VisionSwitcher>().updateVisionSprite();
            }
        }
        if (itemData.Classification == "progression")
        {
            GameObject newItem = new();
            newItem.AddComponent<UpdateInventory>().RareEffect(itemData);
        }
        if (itemData.Classification == "useful")
        {
            GameObject newItem = new();
            newItem.AddComponent<UpdateInventory>().CommonEffect(itemData);
        }
        if (itemData.Classification == "junk")
        {
            GameObject newItem = new();
            newItem.AddComponent<UpdateInventory>().JunkEffect(itemData);
        }
        if (itemData.Classification == "trap")
        {
            GameObject newItem = new();
            newItem.AddComponent<UpdateInventory>().TrapEffect(itemData);
        }
    }

    //if the object is progression, this method will run.
    public void RareEffect(ItemData itemData)
    {
        AssetBundle Bundle = AssetBundle.LoadFromFile("BepInEx\\plugins\\MasterKeyRandomizer\\masterkeyrandoassets");
        Sprite item = Bundle.LoadAsset<Sprite>("Assets\\" + itemData.SpriteName);
        GameObject itemObject = new GameObject("Held Item");
        itemObject.AddComponent<SpriteRenderer>().sprite = item;
        itemObject.GetComponent<SpriteRenderer>().sortingOrder = 11;
        FoxMove foxMove = UnityEngine.Object.FindObjectOfType<FoxMove>();
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        itemObject.transform.parent = Player.transform;
        itemObject.transform.localPosition = Vector3.zero;
        itemObject.transform.position = itemObject.transform.position + Vector3.right * 0.25f + 0.75f * Vector3.up;
        float grabDuration = 6;
        AudioClip Fanfare = Bundle.LoadAsset<AudioClip>("Assets\\Progression_Jingle.wav");
        foxMove.objetTrouve(grabDuration);
        foxMove.removeVelo();
        rareEffectObj = new GameObject("objRareEffect");
        rareEffectObj.transform.parent = itemObject.transform;
        rareEffectObj.transform.localPosition = Vector3.zero;
        rareEffectObj.AddComponent<rotateTransform>().axis = Vector3.forward;
        rareEffectObj.GetComponent<rotateTransform>().rotationPerSecond = 0.125f;
        Sprite white = Bundle.LoadAsset<Sprite>("Assets\\rareEffectWhite.png");
        Sprite black = Bundle.LoadAsset<Sprite>("Assets\\rareEffectBlack.png");
        context.Send(delegate
        {
            ThreadingHelper.Instance.StartSyncInvoke(() => StartCoroutine(timeForKill(itemObject, grabDuration)));
        }, null);
        Fanfare.LoadAudioData();
        AudioSource.PlayClipAtPoint(Fanfare, itemObject.transform.position, GameObject.FindGameObjectWithTag("Starter").GetComponent<starterScript>().SFXVol);
        Bundle.Unload(false);
        for (int i = 0; i < 32; i++)
        {
            GameObject gameObject = new GameObject("pale " + i);
            gameObject.SetActive(true);
            gameObject.transform.parent = rareEffectObj.transform;
            gameObject.AddComponent<SpriteRenderer>().sprite = ((i % 2) == 0 ? white : black);
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 21;
            gameObject.transform.Rotate(0f, 0f, ((float)i / 16f * 180f));
            gameObject.transform.localPosition = Vector3.zero;
            Debug.LogWarning(gameObject.transform.rotation.eulerAngles);
            gameObject.transform.Translate(11f * Vector3.right);
            gameObject.AddComponent<TranslateTransform>().TotalMove = -gameObject.transform.localPosition;
            gameObject.transform.Translate(11f * Vector3.right);
            gameObject.GetComponent<TranslateTransform>().OverTime = 0.5f;
            gameObject.GetComponent<TranslateTransform>().goBackScheduled = grabDuration - 1f;
        }
        busy = false;
    }

    public void CommonEffect(ItemData itemData)
    {

        AssetBundle Bundle = AssetBundle.LoadFromFile("BepInEx\\plugins\\MasterKeyRandomizer\\masterkeyrandoassets");
        Sprite item = Bundle.LoadAsset<Sprite>("Assets\\" + itemData.SpriteName);
        GameObject itemObject = new GameObject("Held Item");
        itemObject.AddComponent<SpriteRenderer>().sprite = item;
        itemObject.GetComponent<SpriteRenderer>().sortingOrder = 11;
        FoxMove foxMove = UnityEngine.Object.FindObjectOfType<FoxMove>();
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        itemObject.transform.parent = Player.transform;
        itemObject.transform.localPosition = Vector3.zero;
        itemObject.transform.position = itemObject.transform.position + Vector3.right * 0.25f + 0.75f * Vector3.up;
        float grabDuration = 3;
        AudioClip Fanfare = Bundle.LoadAsset<AudioClip>("Assets\\Useful_Jingle.wav");
        foxMove.objetTrouve(grabDuration);
        foxMove.removeVelo();
        context.Send(delegate
        {
            ThreadingHelper.Instance.StartSyncInvoke(() => StartCoroutine(timeForKill(itemObject, grabDuration)));
        }, null);
        Fanfare.LoadAudioData();
        AudioSource.PlayClipAtPoint(Fanfare, itemObject.transform.position, GameObject.FindGameObjectWithTag("Starter").GetComponent<starterScript>().SFXVol);
        Bundle.Unload(false);
    }

    public void JunkEffect(ItemData itemData)
    {

        AssetBundle Bundle = AssetBundle.LoadFromFile("BepInEx\\plugins\\MasterKeyRandomizer\\masterkeyrandoassets");
        Sprite item = Bundle.LoadAsset<Sprite>("Assets\\" + itemData.SpriteName);
        GameObject itemObject = new GameObject("Held Item");
        itemObject.AddComponent<SpriteRenderer>().sprite = item;
        itemObject.GetComponent<SpriteRenderer>().sortingOrder = 11;
        FoxMove foxMove = UnityEngine.Object.FindObjectOfType<FoxMove>();
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        itemObject.transform.parent = Player.transform;
        itemObject.transform.localPosition = Vector3.zero;
        itemObject.transform.position = itemObject.transform.position + Vector3.right * 0.25f + 0.75f * Vector3.up;
        float grabDuration = 2.5f;
        AudioClip Fanfare = Bundle.LoadAsset<AudioClip>("Assets\\Junk_Jingle.wav");
        foxMove.objetTrouve(grabDuration);
        foxMove.removeVelo();
        context.Send(delegate
        {
            ThreadingHelper.Instance.StartSyncInvoke(() => StartCoroutine(timeForKill(itemObject, grabDuration)));
        }, null);
        Fanfare.LoadAudioData();
        AudioSource.PlayClipAtPoint(Fanfare, itemObject.transform.position, GameObject.FindGameObjectWithTag("Starter").GetComponent<starterScript>().SFXVol);
        Bundle.Unload(false);
        busy = false;
    }

    public void TrapEffect(ItemData itemData)
    {

        AssetBundle Bundle = AssetBundle.LoadFromFile("BepInEx\\plugins\\MasterKeyRandomizer\\masterkeyrandoassets");
        Sprite item = Bundle.LoadAsset<Sprite>("Assets\\" + itemData.SpriteName);
        GameObject itemObject = new GameObject("Held Item");
        itemObject.AddComponent<SpriteRenderer>().sprite = item;
        itemObject.GetComponent<SpriteRenderer>().sortingOrder = 11;
        FoxMove foxMove = UnityEngine.Object.FindObjectOfType<FoxMove>();
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        itemObject.transform.parent = Player.transform;
        itemObject.transform.localPosition = Vector3.zero;
        itemObject.transform.position = itemObject.transform.position + Vector3.right * 0.25f + 0.75f * Vector3.up;
        float grabDuration = 4;
        AudioClip Fanfare = Bundle.LoadAsset<AudioClip>("Assets\\Trap_Jingle.ogg");
        foxMove.objetTrouve(grabDuration);
        foxMove.removeVelo();
        context.Send(delegate
        {
            ThreadingHelper.Instance.StartSyncInvoke(() => StartCoroutine(timeForKill(itemObject, grabDuration)));
        }, null);
        Fanfare.LoadAudioData();
        AudioSource.PlayClipAtPoint(Fanfare, itemObject.transform.position, GameObject.FindGameObjectWithTag("Starter").GetComponent<starterScript>().SFXVol);
        Bundle.Unload(false);
        busy = false;
    }

    static IEnumerator timeForKill(GameObject itemObject, float grabDuration)
    {
        yield return new WaitForSeconds(grabDuration);
        UnityEngine.Object.Destroy(itemObject);
    }

    public static Sprite UpdateAppearance(string checkIdentifier)
    {
        Debug.Log(checkIdentifier);
        FoxMove foxMove = UnityEngine.Object.FindObjectOfType<FoxMove>();
        CheckData checkData = CheckClass.GetData(checkIdentifier);
        ItemData itemData = new ItemData();
        itemData = ItemCheatSheet.GetData(checkData.CheckItem.Name);
        FieldInfo fieldInfo = foxMove.GetType().GetField(itemData.ItemNameForSave);
        AssetBundle Bundle = AssetBundle.LoadFromFile("BepInEx\\plugins\\MasterKeyRandomizer\\masterkeyrandoassets");
        Sprite sprite = Bundle.LoadAsset<Sprite>(itemData.SpriteName);
        Bundle.Unload(false);
        return sprite;
    }

    static IEnumerator busyCheck()
    {
        yield return new WaitUntil(() => !busy);
    }
    public static object GetPropValue(object target, string propName)
    {
        return target.GetType().GetProperty(propName).GetValue(target, null);
    }

    ThreadingHelper Instance { get { return ThreadingHelper.Instance; } }
}
