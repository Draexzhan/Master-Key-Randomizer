using BepInEx;
using HarmonyLib;
using MasterKeyRandomizer;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static ItemCheatSheet;

public class RandoObject : MonoBehaviour
{
    private GameObject Player;

    public int value;

    public float grabDuration;

    public Transform moderatelyRareEffect;

    public Sprite rareEffectWhite;

    public Sprite rareEffectBlack;

    public Transform pancarteVendu;
    private Vector3 OrigPos { get; set; }

    private GameObject copyOfNextItem;

    public GameObject nextItem;
    public GameObject rareEffectObject;

    public ItemData Item { get; set; }

    public Sprite ItemSprite;

    public string rarity;
    public AudioClip tadadadaaaSFX;
    public bool inChest;
    public bool WeaponCatchable;

    public RandoObject(Vector3 OriginalPos, ItemData itemData, Transform transform)
    {
        OrigPos = OriginalPos;
        Item = itemData;
    }

    private void Start()
    {
        base.transform.position = OrigPos;
        Player = GameObject.FindGameObjectWithTag("Player");
        Item = ItemCheatSheet.GetData("Boomerang");
        rarity = Item.Classification;
        ItemSprite = GetTextureFromFile("MasterKeyRandomizer\\sprites\\Boomerang 1.png");
    }
    public static Sprite GetTextureFromFile(string path)
    {
        try
        {
            string fullPath = BepInEx.Utility.CombinePaths(Paths.PluginPath, path);
            byte[] data = File.Exists(fullPath) ? File.ReadAllBytes(fullPath) : null;

            if (data == null)
            {
                Debug.LogWarning($"Error in GetTextureFromFile(): No file was found at path '{fullPath}'");
                return null;
            }

            Texture2D texture = new(512, 512, TextureFormat.RGBA32, false);
            texture.LoadImage(data);
            var rect = new Rect(0, 0, texture.width, texture.height);
            var pivot = new Vector2(0.5f, 0.5f);

            return Sprite.Create(texture, rect, pivot);
        }
        catch (Exception ex)
        {
            Debug.LogWarning("Error in GetTextureFromFile(): " + ex.Message);
            return null;
        }
    }

    public void grabIt()
    {
        if (((Item.Name.Equals("Apple") || (Item.Name.Equals("Cheese")) && Player.GetComponent<FoxMove>().PDVActuels == Player.GetComponent<FoxMove>().PDVMax)))
        {
            return;
        }
        if (tadadadaaaSFX != null)
        {
            AudioSource.PlayClipAtPoint(tadadadaaaSFX, base.gameObject.transform.position, GameObject.FindGameObjectWithTag("Starter").GetComponent<starterScript>().SFXVol);
        }

        UnityEngine.Object.Destroy(base.gameObject, grabDuration);
        base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 11;
        if (base.transform.childCount != 0 && !Item.Name.Equals("Woods Potion") && !Item.Name.Equals("Cheese"))
        {
            UnityEngine.Object.Destroy(base.transform.GetChild(0).gameObject);
        }
        if (Player.GetComponent<FoxMove>().objetTrouvage)
        {
            base.transform.position = base.transform.position + 0.75f * Vector3.up;
            if (rarity.Equals("progression"))
            {
                invokeProgressionEffect();
            }
            invokeUsefulEffect();
        }
        else
        {
            Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            if (!Item.Name.Equals("Apple") && !Item.Name.Equals("Cheese") && !Item.Name.Equals("Woods Potion") && !Item.Name.Equals("Cheese"))
            {
                base.transform.position = Player.transform.position + Vector3.right * 0.25f + Vector3.up * 0.75f;
                Player.GetComponent<FoxMove>().objetTrouve(grabDuration);
                if (rarity.Equals("progression"))
                {
                    invokeProgressionEffect();
                }
                invokeUsefulEffect();
            }
            else
            {
                base.transform.position = Player.transform.position + 0.25f * Vector3.down;
                MonoBehaviour.print("mmmh");
            }
        }
        if (!inChest)
        {
            string text = Player.GetComponent<FoxMove>().saveslot + base.gameObject.name + SceneManager.GetActiveScene().name;
            PlayerPrefs.SetInt(text, 1);
            string key = Player.GetComponent<FoxMove>().saveslot + "infoWorld";
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
    }

    public void invokeProgressionEffect()
    {
        rareEffectObject = new GameObject("objRareEffect");
        rareEffectObject.transform.parent = base.gameObject.transform;
        rareEffectObject.transform.localPosition = Vector3.zero;
        rareEffectObject.AddComponent<rotateTransform>().axis = Vector3.forward;
        rareEffectObject.GetComponent<rotateTransform>().rotationPerSecond = 0.125f;
        for (int i = 0; i < 32; i++)
        {
            GameObject gameObject = new GameObject("pale " + i);
            gameObject.transform.parent = rareEffectObject.transform;
            gameObject.AddComponent<SpriteRenderer>().sprite = ((i % 2 == 0) ? rareEffectBlack : rareEffectWhite);
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 21;
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.Rotate(0f, 0f, (float)(i- 3) / 16f * 180f);
            gameObject.transform.Translate(11f * Vector3.right);
            gameObject.AddComponent<TranslateTransform>().TotalMove = -gameObject.transform.localPosition;
            gameObject.transform.Translate(11f * Vector3.right);
            gameObject.GetComponent<TranslateTransform>().OverTime = 0.5f;
            gameObject.GetComponent<TranslateTransform>().goBackScheduled = grabDuration - 1f;
        }
    }

    public void invokeUsefulEffect()
    {
        for (int i = 0; i < 6; i++)
        {
            Invoke("createPetiteEtoile", (float)i * grabDuration / 6f);
        }
    }

    private void createPetiteEtoile()
    {
        UnityEngine.Object.Instantiate(moderatelyRareEffect, base.transform.position + 0.5f * (Vector3)UnityEngine.Random.insideUnitCircle.normalized, Quaternion.identity);
    }

    private void OnDestroy()
    {
        if ((value > 0 || Item.Name.Equals("Woods Potion") || Item.Name.Equals("Cheese")) && Player != null)
        {
            MonoBehaviour.print(base.gameObject.transform.parent);
            MonoBehaviour.print(base.gameObject.name);
            MonoBehaviour.print(OrigPos);
            Transform transform = UnityEngine.Object.Instantiate(pancarteVendu, OrigPos, Quaternion.identity, base.gameObject.transform.parent);
            if (nextItem != null)
            {
                transform.GetComponent<spawnSmthOnPlayerFar>().ToSpawn = copyOfNextItem;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("pic") && collider.GetComponent<boomerangScript>() == null && WeaponCatchable && value <= Player.GetComponent<FoxMove>().argent && (((!Item.Name.Equals("Apple") && (!Item.Name.Equals("Cheese")) || Player.GetComponent<FoxMove>().GetComponent<FoxMove>().PDVActuels != Player.GetComponent<FoxMove>().GetComponent<FoxMove>().PDVMax))))
        {
            Invoke("itemTPOnPlayer", Player.GetComponent<FoxMove>().isAttackingCD);
        }
    }

    private void itemTPOnPlayer()
    {
        Player.GetComponent<FoxMove>().cancelAtk();
        base.gameObject.transform.position = Player.transform.position;
    }

    private void OnBecameVisible()
    {
        if (Player != null && Player.GetComponent<FoxMove>().picLVL >= 6)
        {
            UnityEngine.Object.Destroy(base.gameObject);
        }
    }
}
