using static MasterKeyRandomizer.MasterKeyRandomizer;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MasterKeyRandoMenu;

public class RandomizerEditor : MonoBehaviour
{
    public static bool expanded = true;

    public static int saveNumber = PlayerPrefs.GetInt("RandoLastWorld", 1);
    public static string saveNumberString = saveNumber.ToString();
    public static int seedPreset = PlayerPrefs.GetInt("SeedPreset", 1);
	public static string seedPresetString = seedPreset.ToString();
    public static int StartLogic = PlayerPrefs.GetInt("StartLogic", 0);
    public static int LanternLogic = PlayerPrefs.GetInt("LanternLogic", 0);
    public static int LensLogic = PlayerPrefs.GetInt("LensLogic", 0);
    public static int BootsLogic = PlayerPrefs.GetInt("BootsLogic", 0);
    public static int KeysanityLogic = PlayerPrefs.GetInt("KeysanityLogic", 0);
    public static int DarkOreHuntLogic = PlayerPrefs.GetInt("DarkOreHuntLogic", 0);
    public static int BossKillLogic = PlayerPrefs.GetInt("BossKillLogic", 0);
    public static int DoSeedPreset = PlayerPrefs.GetInt("DoSeedPreset", 0);
    public static int WeaponLogic = PlayerPrefs.GetInt("WeaponLogic", 0);
    public static int VanillaBossKeys = PlayerPrefs.GetInt("VanillaBossKeys", 0);
    public static int WarpShuffle = PlayerPrefs.GetInt("WarpShuffle", 1);
    public static int DreamLogic = PlayerPrefs.GetInt("DreamLogic", 1);
    public static int SecretLogic = PlayerPrefs.GetInt("SecretLogic", 0);
    public static int EntranceShuffle = PlayerPrefs.GetInt("EntranceShuffle", 0);
    public static string ImportSeedFromFile = string.Empty;

    private void OnGUI()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        if ((activeScene).name == "MainMenu")
        {
            Cursor.visible = true;
            if (expanded)
            {
                if (PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "%maxArgent") == 0)
                    GUI.Window(101, new Rect(10f, 10f, 500f, 600f), new GUI.WindowFunction(RandomizerSettingsWindow), "Randomizer Settings");
				else
					GUI.Window(101, new Rect(10f, 10f, 500f, 600f), new GUI.WindowFunction(RandomizerStatsWindow), "Randomizer Settings");
			}
            else if (GUI.Button(new Rect(20f, 20f, 100f, 30f), "Randomizer"))
            {
                expanded = true;
            }
        }
    }

    private static void RandomizerSettingsWindow(int windowID)
    {
        //bool EraseConfirm = false;
        GUI.Label(new Rect(360f, 20f, 160f, 30f), "Save Number:");
        saveNumberString = GUI.TextField(new Rect(360f, 40f, 100f, 20f), saveNumberString);
        if (int.TryParse(saveNumberString, out var result))
        {
            saveNumber = result;
        }
        GUI.Label(new Rect(10f, 20f, 160f, 40f), "Settings:" );
        GUI.Label(new Rect(10f, 40f, 160f, 40f), "Start game at:"); bool flag00 = GUI.Toggle(new Rect(140f, 40f, 140f, 20f), PlayerPrefs.GetInt("StartLogic") == 0, "Cave"); bool flag01 = GUI.Toggle(new Rect(280f, 40f, 100f, 20f), PlayerPrefs.GetInt("StartLogic") == 1, "Town");
		GUI.Label(new Rect(10f, 60f, 160f, 40f), "Lantern Logic:"); bool flag10 = GUI.Toggle(new Rect(140f, 60f, 140f, 20f), PlayerPrefs.GetInt("LanternLogic") == 0, "Need in the Dark"); bool flag11 = GUI.Toggle(new Rect(280f, 60f, 100f, 20f), PlayerPrefs.GetInt("LanternLogic") == 1, "Minimum");
		GUI.Label(new Rect(10f, 80f, 160f, 40f), "Lens Logic:"); bool flag20 = GUI.Toggle(new Rect(140f, 80f, 140f, 20f), PlayerPrefs.GetInt("LensLogic") == 0, "Visibility Required"); bool flag21 = GUI.Toggle(new Rect(280f, 80f, 100f, 20f), PlayerPrefs.GetInt("LensLogic") == 1, "Minimum");
		GUI.Label(new Rect(10f, 100f, 160f, 40f), "Boots Logic:"); bool flag30 = GUI.Toggle(new Rect(140f, 100f, 140f, 20f), PlayerPrefs.GetInt("BootsLogic") == 0, "Ice Traction"); bool flag31 = GUI.Toggle(new Rect(280f, 100f, 100f, 20f), PlayerPrefs.GetInt("BootsLogic") == 1, "Minimum");
		GUI.Label(new Rect(10f, 120f, 160f, 40f), "Keysanity (TODO):"); bool flag40 = GUI.Toggle(new Rect(140f, 120f, 140f, 20f), PlayerPrefs.GetInt("KeysanityLogic") == 0, "On"); bool flag41 = GUI.Toggle(new Rect(280f, 120f, 100f, 20f), PlayerPrefs.GetInt("KeysanityLogic") == 1, "Off");
		GUI.Label(new Rect(10f, 140f, 160f, 40f), "Warp Shuffle:"); bool flag50 = GUI.Toggle(new Rect(140f, 140f, 140f, 20f), PlayerPrefs.GetInt("WarpShuffle") == 0, "On"); bool flag51 = GUI.Toggle(new Rect(280f, 140f, 100f, 20f), PlayerPrefs.GetInt("WarpShuffle") == 1, "Off");
		GUI.Label(new Rect(10f, 160f, 160f, 40f), "Dream Pedestal:"); bool flag60 = GUI.Toggle(new Rect(140f, 160f, 140f, 20f), PlayerPrefs.GetInt("DreamLogic") == 0, "In Logic"); bool flag61 = GUI.Toggle(new Rect(280f, 160f, 100f, 20f), PlayerPrefs.GetInt("DreamLogic") == 1, "Vanilla");
		GUI.Label(new Rect(10f, 180f, 160f, 40f), "Secret Logic:"); bool flag70 = GUI.Toggle(new Rect(140f, 180f, 140f, 20f), PlayerPrefs.GetInt("SecretLogic") == 0, "All in Logic"); bool flag71 = GUI.Toggle(new Rect(280f, 180f, 140f, 20f), PlayerPrefs.GetInt("SecretLogic") == 1, "Exclude Sneakiest");
		
        
        
        
        GUI.Label(new Rect(10f, 360f, 160f, 40f), "Seed Preset:"); bool flagy0 = GUI.Toggle(new Rect(140f, 360f, 100f, 20f), PlayerPrefs.GetInt("DoSeedPreset") == 0, "On"); bool flagy1 = GUI.Toggle(new Rect(240f, 360f, 100f, 20f), PlayerPrefs.GetInt("DoSeedPreset") == 1, "Off");

		if (flag00 && PlayerPrefs.GetInt("StartLogic") == 1)
		{
			PlayerPrefs.SetInt("StartLogic", 0);
		}
		else if (flag01 && PlayerPrefs.GetInt("StartLogic") == 0)
		{
			PlayerPrefs.SetInt("StartLogic", 1);
		}

		if (flag10 && PlayerPrefs.GetInt("LanternLogic") == 1)
		{
			PlayerPrefs.SetInt("LanternLogic", 0);
		}
		else if (flag11 && PlayerPrefs.GetInt("LanternLogic") == 0)
		{
			PlayerPrefs.SetInt("LanternLogic", 1);
		}

		if (flag20 && PlayerPrefs.GetInt("LensLogic") == 1)
		{
			PlayerPrefs.SetInt("LensLogic", 0);
		}
		else if (flag21 && PlayerPrefs.GetInt("LensLogic") == 0)
		{
			PlayerPrefs.SetInt("LensLogic", 1);
		}

		if (flag30 && PlayerPrefs.GetInt("BootsLogic") == 1)
		{
			PlayerPrefs.SetInt("BootsLogic", 0);
		}
		else if (flag31 && PlayerPrefs.GetInt("BootsLogic") == 0)
		{
			PlayerPrefs.SetInt("BootsLogic", 1);
		}


		if (flag50 && PlayerPrefs.GetInt("WarpShuffle") == 1)
		{
			PlayerPrefs.SetInt("WarpShuffle", 0);
		}
		else if (flag51 && PlayerPrefs.GetInt("WarpShuffle") == 0)
		{
			PlayerPrefs.SetInt("WarpShuffle", 1);
		}

		if (flag60 && PlayerPrefs.GetInt("DreamLogic") == 1)
		{
			PlayerPrefs.SetInt("DreamLogic", 0);
		}
		else if (flag61 && PlayerPrefs.GetInt("DreamLogic") == 0)
		{
			PlayerPrefs.SetInt("DreamLogic", 1);
		}

		if (flag70 && PlayerPrefs.GetInt("SecretLogic") == 1)
		{
			PlayerPrefs.SetInt("SecretLogic", 0);
		}
		else if (flag71 && PlayerPrefs.GetInt("SecretLogic") == 0)
		{
			PlayerPrefs.SetInt("SecretLogic", 1);
		}


		if (flagy0 && PlayerPrefs.GetInt("DoSeedPreset") == 1)
		{
			PlayerPrefs.SetInt("DoSeedPreset", 0);
		}
		else if (flagy1 && PlayerPrefs.GetInt("DoSeedPreset") == 0)
		{
			PlayerPrefs.SetInt("DoSeedPreset", 1);
		}
		if (flagy0)
		{
			GUI.Label(new Rect(10f, 380f, 160f, 40f), "Seed:"); seedPresetString = GUI.TextField(new Rect(80f, 380f, 100f, 20f), seedPresetString);
		}


		if (GUI.Button(new Rect(10f, 400f, 50f, 30f), "Close"))
        {
            expanded = false;
        }
        if (GUI.Button(new Rect(70f, 400f, 50f, 30f), "Erase"))
		{
			//EraseConfirm = GUI.Button(new Rect(130f, 400f, 150f, 30f), "Are you sure?");
			LoadSceneOnClick SaveLoader = FindObjectOfType<LoadSceneOnClick>();
            SaveLoader.saveslot = "randomizerSlot" + saveNumber + "%";
            PlayerPrefs.SetInt("randomizerSlot" + saveNumber + "%randoSeed", default);
			foreach (string key in CheckClass.CheckLookup.Locations.Keys)
				PlayerPrefs.DeleteKey(SaveLoader.saveslot + key);
            SaveLoader.eraseSave();
        }
        if (GUI.Button(new Rect(360f, 120f, 100f, 50f), "Play") && saveNumber > 0)
        {
            RandoMode();
            LoadSceneOnClick SaveLoader = FindObjectOfType<LoadSceneOnClick>();
            SaveLoader.saveslot = "randomizerSlot" + saveNumber + "%";
			if (int.TryParse(seedPresetString, out var result2))
			{
				seedPreset = result2;
			}
			PlayerPrefs.SetInt("SeedPreset", seedPreset);
			PlayerPrefs.SetInt("RandoLastWorld", saveNumber);
            SaveLoader.LoadSceneAndPlay("OverWorld");
        }
    }
	private static void RandomizerStatsWindow(int windowID)
	{
		//bool EraseConfirm = false;
		GUI.Label(new Rect(360f, 20f, 160f, 30f), "Save Number:");
		saveNumberString = GUI.TextField(new Rect(360f, 40f, 100f, 20f), saveNumberString);
		if (int.TryParse(saveNumberString, out var result))
		{
			saveNumber = result;
		}
		GUI.Label(new Rect(10f, 20f, 160f, 40f), "Summary:");
		GUI.Label(new Rect(10f, 40f, 160f, 40f), "Money: " + PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "%argent", 0) + " / " + PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "%maxArgent", 0));
		GUI.Label(new Rect(10f, 60f, 160f, 40f), "Crystals: " + PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "%cristals", 0) + " / " + PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "%cristalsGiven", 0));
		GUI.Label(new Rect(10f, 80f, 160f, 40f), "Weapon Level: " + PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "%picLVL", 0));
		GUI.Label(new Rect(10f, 100f, 160f, 40f), "Health: " + PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "%PDVActuels", 0) + " / " + PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "%PDVMax", 0));
		GUI.Label(new Rect(10f, 120f, 100f, 40f), "Armor Level: " + PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "%armor", 0));
		GUI.Label(new Rect(10f, 140f, 160f, 40f), "Balloon Level: " + PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "%ballon", 0));
		GUI.Label(new Rect(10f, 160f, 160f, 40f), "Boomerang Level: " + PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "%boomerang", 0));
		GUI.Label(new Rect(10f, 180f, 160f, 40f), "Boots Level: " + PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "%bottes", 0));
		GUI.Label(new Rect(10f, 200f, 160f, 40f), "Clover Level: " + PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "%clover", 0));
		GUI.Label(new Rect(10f, 220f, 160f, 40f), "Gloves Level: " + PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "%creuse", 0));
		GUI.Label(new Rect(10f, 240f, 160f, 40f), "Grapple Level: " + PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "%distance", 0));
		GUI.Label(new Rect(10f, 260f, 160f, 40f), "Lantern Level: " + PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "%lanterne", 0));
		GUI.Label(new Rect(10f, 280f, 160f, 40f), "Lens Level: " + PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "%vision", 0));
		GUI.Label(new Rect(10f, 300f, 160f, 40f), "Magnet Level: " + PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "%magnet", 0));
		GUI.Label(new Rect(10f, 320f, 160f, 40f), "Muscle Level: " + PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "%muscle", 0));
		GUI.Label(new Rect(10f, 340f, 160f, 40f), "Swim Level: " + PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "%palmes", 0));
		GUI.Label(new Rect(10f, 360f, 160f, 40f), "Gear Pieces: " + (PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "%Gear1", 0) + PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "%Gear2", 0) + PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "%Gear3", 0)));
		GUI.Label(new Rect(10f, 380f, 160f, 40f), "Seed: " + PlayerPrefs.GetInt("randomizerSlot" + saveNumber + "%randoSeed", 0));

		if (GUI.Button(new Rect(10f, 400f, 50f, 30f), "Close"))
		{
			expanded = false;
		}
		if (GUI.Button(new Rect(70f, 400f, 50f, 30f), "Erase"))
		{
			//EraseConfirm = GUI.Button(new Rect(130f, 400f, 150f, 30f), "Are you sure?");
			LoadSceneOnClick SaveLoader = FindObjectOfType<LoadSceneOnClick>();
			SaveLoader.saveslot = "randomizerSlot" + saveNumber + "%";
			PlayerPrefs.SetInt("randomizerSlot" + saveNumber + "%randoSeed", default);
			foreach (string key in CheckClass.CheckLookup.Locations.Keys)
				PlayerPrefs.DeleteKey(SaveLoader.saveslot + key);
			SaveLoader.eraseSave();
		}
		if (GUI.Button(new Rect(360f, 120f, 100f, 50f), "Play") && saveNumber > 0)
		{
			RandoMode();
			LoadSceneOnClick SaveLoader = FindObjectOfType<LoadSceneOnClick>();
			SaveLoader.saveslot = "randomizerSlot" + saveNumber + "%";
			PlayerPrefs.SetInt("RandoLastWorld", saveNumber);
			SaveLoader.LoadSceneAndPlay("OverWorld");
		}
	}

	private static void UpdateLoadedSave()
    {
        LoadSceneOnClick SaveLoader = FindObjectOfType<LoadSceneOnClick>();
        SaveLoader.saveslot = "randomizerSlot" + saveNumber + "%";
		PlayerPrefs.SetInt("SeedPreset", seedPreset);
    }
}
