using static MasterKeyRandomizer.MasterKeyRandomizer;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MasterKeyRandoMenu;

public class RandomizerEditor : MonoBehaviour
{
    public static bool expanded = false;

    private static int saveNumber = PlayerPrefs.GetInt("RandoLastWorld", 1);
    private static string saveNumberString = saveNumber.ToString(); 
    private void OnGUI()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        if (((Scene)(activeScene)).name == "MainMenu")
        {
            Cursor.visible = true;
            if (expanded)
            {
                GUI.Window(101, new Rect(10f, 10f, 500f, 600f), new GUI.WindowFunction(RandomizerEditorWindow), "Randomizer Settings");
            }
            else if (GUI.Button(new Rect(20f, 20f, 100f, 30f), "Randomizer"))
            {
                expanded = true;
            }
        }
    }

    private static void RandomizerEditorWindow(int windowID)
    {
        bool EraseConfirm = false;
        GUI.Label(new Rect(200f, 40f, 160f, 30f), "Save Number:");
        saveNumberString = GUI.TextField(new Rect(360f, 40f, 100f, 20f), saveNumberString);
        if (int.TryParse(saveNumberString, out var result))
        {
            saveNumber = result;
        }
        GUI.Label(new Rect(10f, 20f, 160f, 40f), "Summary:" );
        GUI.Label(new Rect(10f, 40f, 160f, 40f), "Money: " + PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "argent", 0) + " / " + PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "maxArgent", 0));
        GUI.Label(new Rect(10f, 60f, 160f, 40f), "Crystals: " + PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "cristals", 0) + " / " + PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "cristalsGiven", 0));
        GUI.Label(new Rect(10f, 80f, 160f, 40f), "Weapon Level: " + PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "picLVL", 0));
        GUI.Label(new Rect(10f, 100f, 160f, 40f), "Health: " + PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "PDVActuels", 0) + " / " + PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "PDVMax", 0));
        GUI.Label(new Rect(10f, 120f, 100f, 40f), "Armor Level: " + PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "armor", 0));
        GUI.Label(new Rect(10f, 140f, 160f, 40f), "Balloon Level: " + PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "ballon", 0));
        GUI.Label(new Rect(10f, 160f, 160f, 40f), "Boomerang Level: " + PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "boomerang", 0));
        GUI.Label(new Rect(10f, 180f, 160f, 40f), "Boots Level: " + PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "bottes", 0));
        GUI.Label(new Rect(10f, 200f, 160f, 40f), "Clover Level: " + PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "clover", 0));
        GUI.Label(new Rect(10f, 220f, 160f, 40f), "Gloves Level: " + PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "creuse", 0));
        GUI.Label(new Rect(10f, 240f, 160f, 40f), "Grapple Level: " + PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "distance", 0));
        GUI.Label(new Rect(10f, 260f, 160f, 40f), "Lantern Level: " + PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "lanterne", 0));
        GUI.Label(new Rect(10f, 280f, 160f, 40f), "Lens Level: " + PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "vision", 0));
        GUI.Label(new Rect(10f, 300f, 160f, 40f), "Magnet Level: " + PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "magnet", 0));
        GUI.Label(new Rect(10f, 320f, 160f, 40f), "Muscle Level: " + PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "muscle", 0));
        GUI.Label(new Rect(10f, 340f, 160f, 40f), "Swim Level: " + PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "palmes", 0));
        GUI.Label(new Rect(10f, 360f, 160f, 40f), "Gear Pieces: " + (PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "Gear1", 0) + PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "Gear2", 0) + PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "Gear3", 0)));
        GUI.Label(new Rect(10f, 380f, 160f, 40f), "Seed: " + PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "randoSeed", 0));

        if (GUI.Button(new Rect(10f, 400f, 50f, 30f), "Close"))
        {
            expanded = false;
        }
        if (GUI.Button(new Rect(70f, 400f, 50f, 30f), "Erase"))
        {
            EraseConfirm = GUI.Button(new Rect(130f, 400f, 150f, 30f), "Are you sure?");
            LoadSceneOnClick SaveLoader = UnityEngine.Object.FindObjectOfType<LoadSceneOnClick>();
            SaveLoader.saveslot = "randomizerSlot" + saveNumber;
            PlayerPrefs.SetInt("randomizerSlot" + saveNumber + "randoSeed", default);
            SaveLoader.eraseSave();
        }
        if (GUI.Button(new Rect(360f, 120f, 100f, 50f), "Play") && saveNumber > 0)
        {
            RandoMode();
            LoadSceneOnClick SaveLoader = UnityEngine.Object.FindObjectOfType<LoadSceneOnClick>();
            SaveLoader.saveslot = "randomizerSlot" + saveNumber;
            PlayerPrefs.SetInt("RandoLastWorld", saveNumber);
            SaveLoader.LoadSceneAndPlay("OverWorld");
        }
        if (EraseConfirm)
        {
            LoadSceneOnClick SaveLoader = UnityEngine.Object.FindObjectOfType<LoadSceneOnClick>();
            SaveLoader.saveslot = "randomizerSlot" + saveNumber;
            SaveLoader.eraseSave();
            EraseConfirm = false;
        }
    }

    private static void UpdateLoadedSave()
    {
        LoadSceneOnClick SaveLoader = UnityEngine.Object.FindObjectOfType<LoadSceneOnClick>();
        SaveLoader.saveslot = "randomizerSlot" + saveNumber;

    }
}
