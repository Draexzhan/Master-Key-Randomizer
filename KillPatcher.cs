using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;
using static ItemCheatSheet;
using static HealthScriptMono;
using static UpdateInventory;

class EnemyPatch1
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(HealthScriptMono), nameof(HealthScriptMono.testHP))]
    public static bool EnemyKillPatch(HealthScriptMono __instance)
    {
        if (__instance.hp > 0)
        {
            return false;
        }
        switch (__instance.nameForStatsOnDeath)
        {
            case statName.slimeKilled:
                __instance.player.GetComponent<FoxMove>().slimeKilled++;
                break;
            case statName.lapinKilled:
                __instance.player.GetComponent<FoxMove>().lapinKilled++;
                break;
            case statName.axoKilled:
                __instance.player.GetComponent<FoxMove>().axoKilled++;
                break;
            case statName.corbKilled:
                __instance.player.GetComponent<FoxMove>().corbKilled++;
                break;
            case statName.crocKilled:
                __instance.player.GetComponent<FoxMove>().crocKilled++;
                break;
            case statName.batKilled:
                __instance.player.GetComponent<FoxMove>().batKilled++;
                break;
            case statName.shellKilled:
                __instance.player.GetComponent<FoxMove>().shellKilled++;
                break;
            case statName.moaiKilled:
                __instance.player.GetComponent<FoxMove>().moaiKilled++;
                break;
            case statName.souchotKilled:
                __instance.player.GetComponent<FoxMove>().souchotKilled++;
                break;
            case statName.skelKilled:
                __instance.player.GetComponent<FoxMove>().skelKilled++;
                break;
            case statName.mageKilled:
                __instance.player.GetComponent<FoxMove>().mageKilled++;
                break;
            case statName.hopKilled:
                __instance.player.GetComponent<FoxMove>().hopKilled++;
                break;
            case statName.snowKilled:
                __instance.player.GetComponent<FoxMove>().snowKilled++;
                break;
            case statName.pinguKilled:
                __instance.player.GetComponent<FoxMove>().pinguKilled++;
                break;
            case statName.lanceKilled:
                __instance.player.GetComponent<FoxMove>().lanceKilled++;
                break;
            case statName.jellyKilled:
                __instance.player.GetComponent<FoxMove>().jellyKilled++;
                break;
            case statName.ghoulKilled:
                __instance.player.GetComponent<FoxMove>().ghoulKilled++;
                break;
            case statName.bjellyKilled:
                __instance.player.GetComponent<FoxMove>().bjellyKilled++;
                break;
            case statName.bslimeKilled:
                __instance.player.GetComponent<FoxMove>().bslimeKilled++;
                break;
            case statName.harpyKilled:
                __instance.player.GetComponent<FoxMove>().harpyKilled++;
                break;
            case statName.oilKilled:
                __instance.player.GetComponent<FoxMove>().oilKilled++;
                break;
            case statName.lfrogKilled:
                __instance.player.GetComponent<FoxMove>().lfrogKilled++;
                break;
            case statName.firesnKilled:
                __instance.player.GetComponent<FoxMove>().firesnKilled++;
                break;
            case statName.cycloKilled:
                __instance.player.GetComponent<FoxMove>().cycloKilled++;
                break;
            case statName.crossbKilled:
                __instance.player.GetComponent<FoxMove>().crossbKilled++;
                break;
            case statName.robotKilled:
                __instance.player.GetComponent<FoxMove>().robotKilled++;
                break;
        }
        __instance.hasBeenHit = false;
        UnityEngine.Vector3 vector;
        if (!__instance.isLooting && UnityEngine.Vector3.Distance(__instance.transform.parent.position, new UnityEngine.Vector3(1000f, 1000f, 1000f)) > 10f)
        {
            __instance.loote();
            if (__instance.exploMort != null)
            {
                UnityEngine.Object.Instantiate(__instance.exploMort, __instance.transform.position, UnityEngine.Quaternion.identity);
            }
            if (__instance.sureThingLooted != null)
            {
                Debug.Log("Killed " + __instance.anim.name);
                ItemData Bounty = CheckClass.GetData(__instance.anim.name).CheckItem;
                Debug.Log(Bounty.ItemNameForSave);
                AddToInventory(Bounty);
            }
			if (__instance.animMortBoss != null)
			{
                UnityEngine.Object.Instantiate(__instance.animMortBoss, __instance.transform.position, UnityEngine.Quaternion.identity);
            __instance.animMortBossPos = __instance.transform.position;
            __instance.Invoke("insantiateBigExploMort", 2.1f);
            __instance.Invoke("CancelInvoke", 2.2f);
			}
		}
		Time.timeScale = 1f;
		if (__instance.respawnable)
		{
			__instance.transform.parent.position = new UnityEngine.Vector3(1000f, 1000f, 1000f);
			return false;
		}
		string saveslot = __instance.player.GetComponent<FoxMove>().saveslot;
		string text2 = __instance.gameObject.name;
		string text3 = SceneManager.GetActiveScene().name;
		vector = __instance.startPos;
		string text4 = saveslot + text2 + text3 + vector.ToString();
		PlayerPrefs.SetInt(text4, 1);
		string key2 = __instance.player.GetComponent<FoxMove>().saveslot + "infoWorld";
		string[] stringArray3 = PlayerPrefsX.GetStringArray(key2);
		string[] array3 = new string[stringArray3.Length + 1];
		stringArray3.CopyTo(array3, 0);
		new string[1] { text4 }.CopyTo(array3, stringArray3.Length);
		PlayerPrefsX.SetStringArray(key2, array3);
		string[] stringArray4 = PlayerPrefsX.GetStringArray("binaryResetOnQuit");
		string[] array4 = new string[stringArray4.Length + 1];
		stringArray4.CopyTo(array4, 0);
		new string[1] { text4 }.CopyTo(array4, stringArray4.Length);
		PlayerPrefsX.SetStringArray("binaryResetOnQuit", array4);
		if (__instance.transform.parent.gameObject.GetComponent<CassableScript>() != null)
		{
            __instance.transform.parent.gameObject.GetComponent<CassableScript>().lootePas = true;
            __instance.transform.parent.gameObject.GetComponent<CassableScript>().casse();
		}
		else
		{
			UnityEngine.Object.Destroy(__instance.transform.parent.gameObject, 10f);
		}
        __instance.transform.parent.position = new UnityEngine.Vector3(1000f, 1000f, 1000f);
        __instance.gameObject.GetComponent<Collider2D>().enabled = false;
        return false;
    }
}