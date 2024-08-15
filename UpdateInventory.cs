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
using static MasterKeyRandomizer.MKLogger;
using BepInEx;
using MasterKeyRandomizer;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Threading;
using UnityEngine.UIElements;
using UnityEngine.TextCore.Text;
using System.Numerics;
using UnityEngine.SocialPlatforms.Impl;
using System.Runtime.Serialization.Formatters;
using System.Drawing;
using System.Linq;

public class UpdateInventory : MonoBehaviour
{
    public static UnityEngine.Vector3 TPDestination;
    private GameObject rareEffectObj;
    //private static GameObject Warp;
    private readonly SynchronizationContext context = SynchronizationContext.Current;
    private static bool busy = false;
    public static void AddToInventory(ItemData itemData)
    {
		ThreadingHelper.Instance.StartCoroutine(BusyCheck(itemData));
    }
    public static void ActuallyAddToInventory(ItemData itemData) 
    {
        busy = true;
        Debug.Log(itemData.ItemNameForSave);
        FoxMove foxMove = UnityEngine.Object.FindObjectOfType<FoxMove>();
        FieldInfo fieldInfo = foxMove.GetType().GetField(itemData.ItemNameForSave);
        int foxInt;
        try
        { foxInt = (int)fieldInfo.GetValue(foxMove); }
        catch (Exception) { foxInt = 0; }
        LogInfo("Determining item type");
        if (itemData.ItemNameForSave != "No Data")
        {
            if (itemData.ItemNameForSave != "argent" || foxMove.argent < foxMove.maxArgent)
            {
                if (fieldInfo.FieldType == typeof(int))
                {
                    fieldInfo.SetValue(foxMove, Math.Min(foxInt + itemData.QuantityToGive, itemData.TierCap));
                }
            }
            if (itemData.ItemNameForSave.Contains("Fragment") && foxInt == 0)
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
                UnityEngine.Object.FindObjectOfType<PlayerHealthAndItemScript>().transform.parent.GetChild(3).GetChild(0).GetComponent<LanterneSwitcher>().updateLanterneSprites();
                UnityEngine.Object.FindObjectOfType<PlayerHealthAndItemScript>().transform.parent.GetChild(3).GetComponent<LanterneSwitcher>().updateLanterneSprites();
            }
            if (itemData.ItemNameForSave == "loupe")
            {
                UnityEngine.Object.FindObjectOfType<PlayerHealthAndItemScript>().transform.parent.GetChild(4).GetComponent<VisionSwitcher>().updateVisionSprite();
            }
            if (foxMove.picLVL > 0)
            {
                foxMove.MasterKey = 1;
            }
            if (itemData.ItemNameForSave == "PDVActuels")
            {
                if (foxMove.PDVActuels == foxMove.PDVMax)
                {

                }
                else
                {
                    LogInfo("OMNOMNOMNOMNOM");
                    PlayerHealthAndItemScript phais = UnityEngine.Object.FindObjectOfType<PlayerHealthAndItemScript>();
					phais.character.enableMove(b: false);
					GameObject.FindGameObjectWithTag("Controls").GetComponent<FoxControllerScript>().disableInput = true;
					phais.character.gameObject.GetComponent<SpriteRenderer>().enabled = false;
					if (itemData.Name == "Apple")
                    {
                        phais.mangePomme();
                        phais.deathPosition = phais.transform.position;
                        phais.Invoke("finiDeMangerLaPomme", 3f);
                        ThreadingHelper.Instance.StartCoroutine(WaitForFiveSeconds());
                        busy = false;
                        return;
                    }
                    else if (itemData.Name == "Cheese")
                    {
                        phais.mangeFromage();
                        phais.deathPosition = phais.transform.position;
                        phais.Invoke("finiDeMangerLeFromage", 3f);
						ThreadingHelper.Instance.StartCoroutine(WaitForFiveSeconds());
						busy = false;
						return;
                    }
                }
            }
        }
        else
		{
			if (itemData.Name == "Woods Potion") //-38134
			{
				LogDebug("Teleport to the Woods!\nThe Woods!\nIt's Woods Time for you!");
				foxMove.enableMove(b: false);
				GameObject.FindGameObjectWithTag("Controls").GetComponent<FoxControllerScript>().disableInput = true;
				foxMove.gameObject.GetComponent<SpriteRenderer>().enabled = false;
				UnityEngine.Object.FindObjectOfType<PlayerHealthAndItemScript>().boisPotion();
				UnityEngine.Object.FindObjectOfType<PlayerHealthAndItemScript>().Invoke("finiDeBoirePotion", 3f);
				var TP = GameObject.Instantiate(Resources.FindObjectsOfTypeAll<bulleCascadeSpawner>().Where(obj => obj.name == "potionForet").First());
				TP.transform.GetChild(0).GetComponent<SceneLoader>().Start();
				TP.transform.GetChild(0).GetComponent<SceneLoader>().Invoke("sceneLoadExtern", 3f);
				foxMove.removeVelo();
				LogDebug("All commands successful");
				ThreadingHelper.Instance.StartCoroutine(WaitForFiveSeconds());
				busy = false;
				return;
			}
			if (itemData.Name == "Snow Potion")
			{
				LogDebug("Teleport to the Snow!"); //-38112
				foxMove.enableMove(b: false);
				GameObject.FindGameObjectWithTag("Controls").GetComponent<FoxControllerScript>().disableInput = true;
				foxMove.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                UnityEngine.Object.FindObjectOfType<PlayerHealthAndItemScript>().boisPotion();
				UnityEngine.Object.FindObjectOfType<PlayerHealthAndItemScript>().Invoke("finiDeBoirePotion", 3f);
				var TP = GameObject.Instantiate(Resources.FindObjectsOfTypeAll<bulleCascadeSpawner>().Where(obj => obj.name == "potionRoute").First());
				TP.transform.GetChild(0).GetComponent<SceneLoader>().Start();
				TP.transform.GetChild(0).GetComponent<SceneLoader>().Invoke("sceneLoadExtern", 3f);
				foxMove.removeVelo();
				LogDebug("All commands successful");
				ThreadingHelper.Instance.StartCoroutine(WaitForFiveSeconds());
				busy = false;
				return;
			}
			if (itemData.Name == "Ruins Warp")
			{
				LogDebug("Teleport to the Ruins!");
				var TP = GameObject.Instantiate(Resources.FindObjectsOfTypeAll<bulleCascadeSpawner>().Where(obj => obj.name == "PoufTPRetour").First());
                TP.transform.GetChild(0).GetComponent<SceneLoader>().Start();
				TP.transform.GetChild(0).GetComponent<SceneLoader>().Invoke("sceneLoadExtern", 0f);
				foxMove.removeVelo();
				ThreadingHelper.Instance.StartCoroutine(WaitForFiveSeconds());
				busy = false;
				return;
			}
			if (itemData.Name == "Start Warp")
			{
				LogDebug("Teleport to the Start!");
				var TP = GameObject.Instantiate(Resources.FindObjectsOfTypeAll<bulleCascadeSpawner>().Where(obj => obj.name == "PoufTPLeyndell").First());
				TP.transform.GetChild(0).GetComponent<SceneLoader>().Start();
				TP.transform.GetChild(0).GetComponent<SceneLoader>().Invoke("sceneLoadExtern", 0f);
				foxMove.removeVelo();
				ThreadingHelper.Instance.StartCoroutine(WaitForFiveSeconds());
				busy = false;
				return;
			}
			if (itemData.Name == "Coffee")
			{
				LogInfo("Waiting for Godot");
				foxMove.cafeCountdown = 60f;
			}
		}
		if (itemData.CelebrationType == 3) //Progressive items and gear pieces
        {
            GameObject newItem = new();
            newItem.AddComponent<UpdateInventory>().RareEffect(itemData);
        }
        if (itemData.CelebrationType == 2) //most collectibles, regardless of how useful they are, so that the tier 3 celebration is not overdone
        {
            GameObject newItem = new();
            newItem.AddComponent<UpdateInventory>().CommonEffect(itemData);
        }
        if (itemData.CelebrationType == 1) //junk, usually
        {
            GameObject newItem = new();
            newItem.AddComponent<UpdateInventory>().JunkEffect(itemData);
        }
        if (itemData.CelebrationType == 0) //traps, really crappy items, and items where the fanfare shouldn't be playing at all (i.e. food, potions, warps)
        {
            GameObject newItem = new();
            newItem.AddComponent<UpdateInventory>().TrapEffect(itemData);
        }
    }

    //if the object is progression, this method will run.
    public void RareEffect(ItemData itemData)
    {
        AssetBundle Bundle = AssetBundle.LoadFromFile("BepInEx\\plugins\\MasterKeyRandomizer\\masterkeyrandoassets");
        Sprite item = Bundle.LoadAsset<Sprite>(itemData.SpriteName);
        GameObject itemObject = new GameObject("Held Item");
        itemObject.AddComponent<SpriteRenderer>().sprite = item;
        itemObject.GetComponent<SpriteRenderer>().sortingOrder = 11;
        FoxMove foxMove = UnityEngine.Object.FindObjectOfType<FoxMove>();
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        itemObject.transform.parent = Player.transform;
        itemObject.transform.localPosition = UnityEngine.Vector3.zero;
        itemObject.transform.position = itemObject.transform.position + UnityEngine.Vector3.right * 0.25f + 0.75f * UnityEngine.Vector3.up;
        float grabDuration = 6;
        AudioClip Fanfare = Bundle.LoadAsset<AudioClip>("Progression_Jingle.wav");
        foxMove.objetTrouve(grabDuration);
        foxMove.removeVelo();
        rareEffectObj = new GameObject("objRareEffect");
        rareEffectObj.transform.parent = itemObject.transform;
        rareEffectObj.transform.localPosition = UnityEngine.Vector3.zero;
        rareEffectObj.AddComponent<rotateTransform>().axis = UnityEngine.Vector3.forward;
        rareEffectObj.GetComponent<rotateTransform>().rotationPerSecond = 0.125f;
        Sprite white = Bundle.LoadAsset<Sprite>("rareEffectWhite.png");
        Sprite black = Bundle.LoadAsset<Sprite>("rareEffectBlack.png");
        context.Send(delegate
        {
            ThreadingHelper.Instance.StartSyncInvoke(() => StartCoroutine(timeForKill(itemObject, grabDuration, itemData, this.GetComponentInParent<GameObject>())));
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
            gameObject.transform.localPosition = UnityEngine.Vector3.zero;
            gameObject.transform.Translate(11f * UnityEngine.Vector3.right);
            gameObject.AddComponent<TranslateTransform>().TotalMove = -gameObject.transform.localPosition;
            gameObject.transform.Translate(11f * UnityEngine.Vector3.right);
            gameObject.GetComponent<TranslateTransform>().OverTime = 0.5f;
            gameObject.GetComponent<TranslateTransform>().goBackScheduled = grabDuration - 1f;
        }
        busy = false;
    }

    //if the item is useful, this method will run
    public void CommonEffect(ItemData itemData)
    {
        AssetBundle Bundle = AssetBundle.LoadFromFile("BepInEx\\plugins\\MasterKeyRandomizer\\masterkeyrandoassets");
        Sprite item = Bundle.LoadAsset<Sprite>(itemData.SpriteName);
        GameObject itemObject = new GameObject("Held Item");
        itemObject.AddComponent<SpriteRenderer>().sprite = item;
        itemObject.GetComponent<SpriteRenderer>().sortingOrder = 11;
        FoxMove foxMove = UnityEngine.Object.FindObjectOfType<FoxMove>();
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        itemObject.transform.parent = Player.transform;
        itemObject.transform.localPosition = UnityEngine.Vector3.zero;
        itemObject.transform.position = itemObject.transform.position + UnityEngine.Vector3.right * 0.25f + 0.75f * UnityEngine.Vector3.up;
        float grabDuration = 3;
        AudioClip Fanfare = Bundle.LoadAsset<AudioClip>("Useful_Jingle.wav");
        foxMove.objetTrouve(grabDuration);
        foxMove.removeVelo();
        context.Send(delegate
        {
            ThreadingHelper.Instance.StartSyncInvoke(() => StartCoroutine(timeForKill(itemObject, grabDuration, itemData, this.GetComponentInParent<GameObject>())));
        }, null);
        Fanfare.LoadAudioData();
        AudioSource.PlayClipAtPoint(Fanfare, itemObject.transform.position, GameObject.FindGameObjectWithTag("Starter").GetComponent<starterScript>().SFXVol);
        Bundle.Unload(false);
		busy = false;
	}

    //if the item is junk, this method will run
    public void JunkEffect(ItemData itemData)
    {
        AssetBundle Bundle = AssetBundle.LoadFromFile("BepInEx\\plugins\\MasterKeyRandomizer\\masterkeyrandoassets");
        Sprite item = Bundle.LoadAsset<Sprite>(itemData.SpriteName);
        GameObject itemObject = new GameObject("Held Item");
        itemObject.AddComponent<SpriteRenderer>().sprite = item;
        itemObject.GetComponent<SpriteRenderer>().sortingOrder = 11;
        FoxMove foxMove = UnityEngine.Object.FindObjectOfType<FoxMove>();
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        itemObject.transform.parent = Player.transform;
        itemObject.transform.localPosition = UnityEngine.Vector3.zero;
        itemObject.transform.position = itemObject.transform.position + UnityEngine.Vector3.right * 0.25f + 0.75f * UnityEngine.Vector3.up;
        float grabDuration = 2.5f;
        AudioClip Fanfare = Bundle.LoadAsset<AudioClip>("Junk_Jingle.wav");
        foxMove.objetTrouve(grabDuration);
        foxMove.removeVelo();
        context.Send(delegate
        {
            ThreadingHelper.Instance.StartSyncInvoke(() => StartCoroutine(timeForKill(itemObject, grabDuration, itemData, this.GetComponentInParent<GameObject>())));
        }, null);
        Fanfare.LoadAudioData();
        AudioSource.PlayClipAtPoint(Fanfare, itemObject.transform.position, GameObject.FindGameObjectWithTag("Starter").GetComponent<starterScript>().SFXVol);
        Bundle.Unload(false);
        busy = false;
    }

    //if the item is a trap, this method will run
    public void TrapEffect(ItemData itemData)
    {
        AssetBundle Bundle = AssetBundle.LoadFromFile("BepInEx\\plugins\\MasterKeyRandomizer\\masterkeyrandoassets");
        Sprite item = Bundle.LoadAsset<Sprite>(itemData.SpriteName);
        GameObject itemObject = new GameObject("Held Item");
        itemObject.AddComponent<SpriteRenderer>().sprite = item;
        itemObject.GetComponent<SpriteRenderer>().sortingOrder = 11;
        FoxMove foxMove = UnityEngine.Object.FindObjectOfType<FoxMove>();
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        itemObject.transform.parent = Player.transform;
        itemObject.transform.localPosition = UnityEngine.Vector3.zero;
        itemObject.transform.position = itemObject.transform.position + UnityEngine.Vector3.right * 0.25f + 0.75f * UnityEngine.Vector3.up;
        float grabDuration = 4;
        AudioClip Fanfare = Bundle.LoadAsset<AudioClip>("Trap_Jingle.ogg");
        if (itemData.Name == "Moster Sword")
		{
			itemObject.GetComponent<SpriteRenderer>().sprite = Bundle.LoadAsset<Sprite>("MosterSwordPulled.png");
            itemObject.GetComponent<SpriteRenderer>().flipY = true;
            itemObject.transform.position += UnityEngine.Vector3.right * .1f;
			foxMove.objetTrouve(8.5f);
			foxMove.removeVelo();
			context.Send(delegate
			{
				ThreadingHelper.Instance.StartSyncInvoke(() => StartCoroutine(ThrowAway(item, Bundle, itemData, itemObject)));
			}, null);
		}
        else
        {
            foxMove.objetTrouve(grabDuration);
            foxMove.removeVelo();
            context.Send(delegate
            {
                ThreadingHelper.Instance.StartSyncInvoke(() => StartCoroutine(timeForKill(itemObject, grabDuration, itemData, this.GetComponentInParent<GameObject>())));
            }, null);
			Bundle.Unload(false);
			busy = false;
		}
        Fanfare.LoadAudioData();
        AudioSource.PlayClipAtPoint(Fanfare, itemObject.transform.position, GameObject.FindGameObjectWithTag("Starter").GetComponent<starterScript>().SFXVol);
    }

    //If the item is an apple or cheese, and the player is not at max health, this method will run.
    public void FoodEffect()
    {
        context.Send(delegate
        {
            ThreadingHelper.Instance.StartSyncInvoke(() => StartCoroutine(WaitInvuln(3f)));
        }, null);
        busy = false;
    }

    public IEnumerator ThrowAway(Sprite sprite, AssetBundle bundle, ItemData itemData, GameObject itemObject)
	{
        FoxMove player = GameObject.FindGameObjectWithTag("Player").GetComponent<FoxMove>();
        player.gameObject.GetComponent<SpriteRenderer>().enabled = false;
		player.setDesactiveMoveCinematics(val: true);
        Sprite fakePlayerSprite = bundle.LoadAsset<Sprite>("MosterSwordFox0.png");
		GameObject fakePlayer = new GameObject("Fake Player");
		fakePlayer.AddComponent<SpriteRenderer>().sprite = fakePlayerSprite;
        fakePlayer.GetComponent<SpriteRenderer>().sortingOrder = 0;
        fakePlayer.transform.parent = player.transform;
		fakePlayer.transform.localPosition = UnityEngine.Vector3.zero;
		LogDebug("cutscene started");
		GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMonoMove>().changeVolumeMultiplierOverTime(0f, 2f);
        LogDebug(":D");
        yield return new WaitForSeconds(3f);
        fakePlayer.GetComponent<SpriteRenderer>().sprite = bundle.LoadAsset<Sprite>("MosterSwordFox1.png");
		LogDebug(":O");
		yield return new WaitForSeconds(1f);
		fakePlayer.GetComponent<SpriteRenderer>().sprite = bundle.LoadAsset<Sprite>("MosterSwordFox2.png");
		LogDebug(":(");
		yield return new WaitForSeconds(1f);
        itemObject.GetComponent<SpriteRenderer>().enabled = false;
		fakePlayer.GetComponent<SpriteRenderer>().sprite = bundle.LoadAsset<Sprite>("MosterSwordFox3.png");
		LogDebug("-_-");
		yield return new WaitForSeconds(3f);
		player.directionFacing = 1;
        player.boomerangTime = Time.time;
        player.GetComponent<Animator>().SetBool("boomerang", value: true);
        AudioSource.PlayClipAtPoint(player.coupEpeeSFX, player.transform.position, GameObject.FindGameObjectWithTag("Starter").GetComponent<starterScript>().SFXVol);
		itemObject.GetComponent<SpriteRenderer>().enabled = true;
        itemObject.AddComponent<rotateTransform>().axis = UnityEngine.Vector3.forward;
		itemObject.GetComponent<rotateTransform>().rotationPerSecond = -2f;
        fakePlayer.GetComponent<SpriteRenderer>().enabled = false;
        itemObject.transform.parent = fakePlayer.transform;
        itemObject.transform.localPosition = UnityEngine.Vector3.zero;
		player.gameObject.GetComponent<SpriteRenderer>().enabled = true;
		StartCoroutine(Throwing(itemObject));
		yield return new WaitForSeconds(0.5f);
		player.GetComponent<Animator>().SetBool("boomerang", value: false);
        player.setDesactiveMoveCinematics(val: false);
        GameObject.FindGameObjectWithTag("Controls").GetComponent<FoxControllerScript>().disableMenu = false;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMonoMove>().changeVolumeMultiplierOverTime(1f, 2f);
        bundle.Unload(false);
		busy = false;
	}

    static IEnumerator Throwing(GameObject itemObject)
    {
        float distanceThrown = 0f;
        UnityEngine.Vector3 ThrowPoint = itemObject.transform.position;
        while(distanceThrown < 40f)
        {
            itemObject.transform.position += UnityEngine.Vector3.right * 15 * Time.deltaTime;
            distanceThrown += 15 * Time.deltaTime;
            LogInfo(distanceThrown.ToString());
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Destroy(itemObject.GetComponentInParent<GameObject>());
        yield return null;
    }

    static IEnumerator WaitInvuln(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }

    static IEnumerator timeForKill(GameObject itemObject, float grabDuration, ItemData itemData, GameObject parent)
    {
        yield return new WaitForSeconds(grabDuration);
        if (itemData.AppendTier)
        {
            objetRareScript[] allItems = UnityEngine.Object.FindObjectsOfType<objetRareScript>();
            for (int i = 0; i < allItems.Length; i++)
            {
                if (UpdateAppearance(allItems[i].gameObject.name + allItems[i].transform.position.ToString(), itemData.Name))
                    allItems[i].gameObject.GetComponent<SpriteRenderer>().sprite = (UpdateAppearance(allItems[i].gameObject.name + allItems[i].transform.position.ToString(), itemData.Name));
            }
        }
        UnityEngine.Object.Destroy(itemObject);
        UnityEngine.GameObject.Destroy(parent);
    }

    public static Sprite UpdateAppearance(string checkIdentifier)
    {
        FoxMove foxMove = UnityEngine.Object.FindObjectOfType<FoxMove>();
        CheckData checkData = CheckClass.GetData(checkIdentifier);
        ItemData itemData;
        FieldInfo fieldInfo;
        itemData = checkData.CheckItem;
        try
		{
			try
			{
				fieldInfo = foxMove.GetType().GetField(itemData.ItemNameForSave);
				//LogInfo(fieldInfo.GetValue(foxMove).ToString());
				itemData.UpdateSpriteName((int)fieldInfo.GetValue(foxMove));
			}
			catch (Exception) { LogInfo("FieldData skipped. string given was " + checkIdentifier); }
		}
        catch (Exception e)
        {
            LogError("Sprite error of type " + e + " occurred.");
        }
        AssetBundle Bundle = AssetBundle.LoadFromFile("BepInEx\\plugins\\MasterKeyRandomizer\\masterkeyrandoassets");
		Sprite sprite = Bundle.LoadAsset<Sprite>("Error");
		try
        {
            sprite = Bundle.LoadAsset<Sprite>(itemData.SpriteName);
            if (itemData.Name.Equals("Ruins Warp") || itemData.Name.Equals("Start Warp"))
            {
                checkData.Reusable = true;
		    }
		}
		catch (NullReferenceException)
		{
			LogError("Sprite " + itemData.SpriteName + " not found!");
		}
		Bundle.Unload(false);
		return sprite;
	}
    public static Sprite UpdateAppearance(string checkIdentifier, string compareName)
    {
        Debug.Log(checkIdentifier);
        FoxMove foxMove = UnityEngine.Object.FindObjectOfType<FoxMove>();
        CheckData checkData = CheckClass.GetData(checkIdentifier);
        ItemData itemData;
        FieldInfo fieldInfo;
        itemData = ItemCheatSheet.GetData(checkData.CheckItem.Name);
        try
        {
            if (compareName.Equals(itemData.Name))
			{
				try
				{
					fieldInfo = foxMove.GetType().GetField(itemData.ItemNameForSave);
					//LogInfo(fieldInfo.GetValue(foxMove).ToString());
					itemData.UpdateSpriteName((int)fieldInfo.GetValue(foxMove));
				}
				catch (Exception) { LogInfo("FieldData skipped. string given was " + checkIdentifier); }
				foxMove.GetType().GetField(itemData.ItemNameForSave);
                AssetBundle Bundle = AssetBundle.LoadFromFile("BepInEx\\plugins\\MasterKeyRandomizer\\masterkeyrandoassets");
                Sprite sprite = Bundle.LoadAsset<Sprite>(itemData.SpriteName);
                Bundle.Unload(false);
                return sprite;
            }
        }
        catch (Exception)
        {
            try
			{
				fieldInfo = foxMove.GetType().GetField(itemData.ItemNameForSave);
				//LogInfo(fieldInfo.GetValue(foxMove).ToString());
				itemData.UpdateSpriteName((int)fieldInfo.GetValue(foxMove));
			}
			catch (Exception e)
			{
				LogError("Sprite error of type " + e + " occurred.");
			}
			AssetBundle Bundle = AssetBundle.LoadFromFile("BepInEx\\plugins\\MasterKeyRandomizer\\masterkeyrandoassets");
			Sprite sprite = Bundle.LoadAsset<Sprite>("Error");
			try
			{
				sprite = Bundle.LoadAsset<Sprite>(itemData.SpriteName);
				if (itemData.Name.Equals("Ruins Warp") || itemData.Name.Equals("Start Warp"))
				{
					checkData.Reusable = true;
				}
			}
			catch (NullReferenceException)
			{
				LogError("Sprite " + itemData.SpriteName + " not found!");
			}
			Bundle.Unload(false);
			return sprite;
		}
        return null;
    }

    static IEnumerator BusyCheck(ItemData itemData)
    {
        LogInfo("Entering queue...");
        yield return new WaitUntil(() => !busy);
        try
        {
            ActuallyAddToInventory(itemData);
        }
        catch (Exception e)
        {
            LogError("There was a problem with adding this item to the inventory: " + e);
            AssetBundle.UnloadAllAssetBundles(false);
            busy = false;
        }
    }

    static IEnumerator WaitForFiveSeconds()
    {
        yield return new WaitForSeconds(5);
        busy = false;
    }
    public static object GetPropValue(object target, string propName)
    {
        return target.GetType().GetProperty(propName).GetValue(target, null);
    }

    ThreadingHelper Instance { get { return ThreadingHelper.Instance; } }
}
