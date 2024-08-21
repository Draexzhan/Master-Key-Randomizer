using HarmonyLib;
using UnityEngine;

namespace SaveData.patches;

class SaveDataPatch1
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(FoxMove), nameof(FoxMove.Save))]
    public static bool SavePrePatch(FoxMove __instance)
    {
        PlayerPrefs.SetInt(__instance.saveslot + "argent", __instance.argent);
        PlayerPrefs.SetInt(__instance.saveslot + "maxArgent", __instance.maxArgent);
        PlayerPrefs.SetInt(__instance.saveslot + "PDVActuels", __instance.PDVActuels);
        PlayerPrefs.SetInt(__instance.saveslot + "PDVMax", __instance.PDVMax);
        PlayerPrefs.SetInt(__instance.saveslot + "demiReceptacle", __instance.demiReceptacle);
        PlayerPrefs.SetInt(__instance.saveslot + "bottes", __instance.bottes);
        PlayerPrefs.SetInt(__instance.saveslot + "muscle", __instance.muscle);
        PlayerPrefs.SetInt(__instance.saveslot + "distance", __instance.distance);
        PlayerPrefs.SetInt(__instance.saveslot + "rez", __instance.rez);
        PlayerPrefs.SetInt(__instance.saveslot + "bourse", __instance.Bourse);
        PlayerPrefs.SetInt(__instance.saveslot + "picLVL", __instance.picLVL); 
        PlayerPrefs.SetInt(__instance.saveslot + "palmes", __instance.palmes);
        PlayerPrefs.SetInt(__instance.saveslot + "lanterne", __instance.lanterne);
        PlayerPrefs.SetInt(__instance.saveslot + "vision", __instance.vision);
        PlayerPrefs.SetInt(__instance.saveslot + "boomerang", __instance.boomerang);
        PlayerPrefs.SetInt(__instance.saveslot + "creuse", __instance.creuse);
        PlayerPrefs.SetInt(__instance.saveslot + "ballon", __instance.ballon);
        PlayerPrefs.SetInt(__instance.saveslot + "clover", __instance.clover);
        PlayerPrefs.SetInt(__instance.saveslot + "magnet", __instance.magnet);
        PlayerPrefs.SetInt(__instance.saveslot + "armor", __instance.armor);
        PlayerPrefs.SetInt(__instance.saveslot + "carteOW", __instance.aCarteOW);
        PlayerPrefs.SetInt(__instance.saveslot + "carteUW", __instance.aCarteUW);
        PlayerPrefs.SetInt(__instance.saveslot + "carteEW", __instance.aCarteEW);
        PlayerPrefs.SetInt(__instance.saveslot + "carteSO", __instance.aCarteDonjonSO);
        PlayerPrefs.SetInt(__instance.saveslot + "carteNE", __instance.aCarteDonjonNE);
        PlayerPrefs.SetInt(__instance.saveslot + "carteNO", __instance.aCarteDonjonNO);
        PlayerPrefs.SetInt(__instance.saveslot + "carteSE", __instance.aCarteDonjonSE);
        PlayerPrefs.SetInt(__instance.saveslot + "cartePyra", __instance.aCarteDonjonPyra);
        PlayerPrefs.SetInt(__instance.saveslot + "carteBonus", __instance.aCarteDonjonBonus);
        PlayerPrefs.SetInt(__instance.saveslot + "carteFinal", __instance.aCarteDonjonFinal);
        PlayerPrefs.SetInt(__instance.saveslot + "screensUncovered", __instance.screensUncovered);
        PlayerPrefs.SetInt(__instance.saveslot + "MasterKey", __instance.MasterKey);
        PlayerPrefs.SetInt(__instance.saveslot + "Fragment1", __instance.Fragment1);
        PlayerPrefs.SetInt(__instance.saveslot + "Fragment2", __instance.Fragment2);
        PlayerPrefs.SetInt(__instance.saveslot + "Fragment3", __instance.Fragment3);
        PlayerPrefs.SetInt(__instance.saveslot + "Fragment4", __instance.Fragment4);
        PlayerPrefs.SetInt(__instance.saveslot + "Gear1", __instance.Gear1);
        PlayerPrefs.SetInt(__instance.saveslot + "Gear2", __instance.Gear2);
        PlayerPrefs.SetInt(__instance.saveslot + "Gear3", __instance.Gear3);
        PlayerPrefs.SetInt(__instance.saveslot + "cristals", __instance.cristals);
        PlayerPrefs.SetInt(__instance.saveslot + "cristalsGiven", __instance.cristalsGiven);
        PlayerPrefs.SetInt(__instance.saveslot + "cafe", __instance.cafe);
        PlayerPrefs.SetInt(__instance.saveslot + "discs", __instance.discs);
        PlayerPrefs.SetInt(__instance.saveslot + "keys", __instance.keys);
        PlayerPrefs.SetInt(__instance.saveslot + "bulbs", __instance.bulbs);
        PlayerPrefs.SetInt(__instance.saveslot + "keyIce", __instance.keyIce);
        PlayerPrefs.SetInt(__instance.saveslot + "keySecret", __instance.keySecret);
        PlayerPrefs.SetInt(__instance.saveslot + "pass", __instance.pass);
        PlayerPrefs.SetInt(__instance.saveslot + "passForge", __instance.passForge);
        PlayerPrefs.SetString(__instance.saveslot + "respawn", __instance.respawnPoint);
        PlayerPrefs.SetInt(__instance.saveslot + "nouvellePhoto", __instance.nouvellePhoto);
        PlayerPrefs.SetInt(__instance.saveslot + "deathCount", __instance.deathCount);
        PlayerPrefs.SetInt(__instance.saveslot + "slimeKilled", __instance.slimeKilled);
        PlayerPrefs.SetInt(__instance.saveslot + "lapinKilled", __instance.lapinKilled);
        PlayerPrefs.SetInt(__instance.saveslot + "axoKilled", __instance.axoKilled);
        PlayerPrefs.SetInt(__instance.saveslot + "corbKilled", __instance.corbKilled);
        PlayerPrefs.SetInt(__instance.saveslot + "crocKilled", __instance.crocKilled);
        PlayerPrefs.SetInt(__instance.saveslot + "batKilled", __instance.batKilled);
        PlayerPrefs.SetInt(__instance.saveslot + "shellKilled", __instance.shellKilled);
        PlayerPrefs.SetInt(__instance.saveslot + "moaiKilled", __instance.moaiKilled);
        PlayerPrefs.SetInt(__instance.saveslot + "souchotKilled", __instance.souchotKilled);
        PlayerPrefs.SetInt(__instance.saveslot + "skelKilled", __instance.skelKilled);
        PlayerPrefs.SetInt(__instance.saveslot + "mageKilled", __instance.mageKilled);
        PlayerPrefs.SetInt(__instance.saveslot + "hopKilled", __instance.hopKilled);
        PlayerPrefs.SetInt(__instance.saveslot + "snowKilled", __instance.snowKilled);
        PlayerPrefs.SetInt(__instance.saveslot + "pinguKilled", __instance.pinguKilled);
        PlayerPrefs.SetInt(__instance.saveslot + "lanceKilled", __instance.lanceKilled);
        PlayerPrefs.SetInt(__instance.saveslot + "jellyKilled", __instance.jellyKilled);
        PlayerPrefs.SetInt(__instance.saveslot + "ghoulKilled", __instance.ghoulKilled);
        PlayerPrefs.SetInt(__instance.saveslot + "bjellyKilled", __instance.bjellyKilled);
        PlayerPrefs.SetInt(__instance.saveslot + "bslimeKilled", __instance.bslimeKilled);
        PlayerPrefs.SetInt(__instance.saveslot + "harpyKilled", __instance.harpyKilled);
        PlayerPrefs.SetInt(__instance.saveslot + "oilKilled", __instance.oilKilled);
        PlayerPrefs.SetInt(__instance.saveslot + "lfrogKilled", __instance.lfrogKilled);
        PlayerPrefs.SetInt(__instance.saveslot + "firesnKilled", __instance.firesnKilled);
        PlayerPrefs.SetInt(__instance.saveslot + "cycloKilled", __instance.cycloKilled);
        PlayerPrefs.SetInt(__instance.saveslot + "crossbKilled", __instance.crossbKilled);
        PlayerPrefs.SetInt(__instance.saveslot + "robotKilled", __instance.robotKilled);
        PlayerPrefs.SetInt(__instance.saveslot + "No Data", 0);
		string[] array = {
                "argent", "maxArgent", "PDVActuels", "PDVMax", "demiReceptacle", "bottes", "palmes", "muscle", "distance", "lanterne",
                "vision", "boomerang", "creuse", "ballon", "rez", "clover", "magnet", "armor", "bourse", "picLVL",
                "carteOW", "carteUW", "carteEW", "carteSO", "carteNE", "carteNO", "carteSE", "cartePyra", "carteBonus", "carteFinal",
                "screensUncovered", "MasterKey", "Fragment1", "Fragment2", "Fragment3", "Fragment4", "Gear1", "Gear2", "Gear3", "cristals",
                "cristalsGiven", "cafe", "keys", "bulbs", "keyIce", "pass", "passForge", "keySecret", "discs", "respawn",
                "nouvellePhoto", "deathCount", "slimeKilled", "lapinKilled", "axoKilled", "corbKilled", "crocKilled", "batKilled", "shellKilled", "moaiKilled",
                "souchotKilled", "skelKilled", "mageKilled", "hopKilled", "snowKilled", "pinguKilled", "lanceKilled", "jellyKilled", "ghoulKilled", "bjellyKilled",
                "bslimeKilled", "harpyKilled", "oilKilled", "lfrogKilled", "firesnKilled", "cycloKilled", "crossbKilled", "robotKilled", "deathCount", "No Data"
        };
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = __instance.saveslot + array[i];
        }
        PlayerPrefsX.SetStringArray("infoChara" + __instance.saveslot, array);
        PlayerPrefs.SetFloat(__instance.saveslot + "time", __instance.time);
        PlayerPrefs.SetString(__instance.saveslot + "version", "demo_piece");
        PlayerPrefsX.SetStringArray("binaryResetOnQuit", new string[0]);
        PlayerPrefs.Save();
        if (Time.time - __instance.lastSaveTime > 10f)
        {
            __instance.lastSaveTime = Time.time;
            __instance.starter.saveNX();
        }
        MonoBehaviour.print("saved");
        return false;
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(WorldHolderScript), nameof(WorldHolderScript.Start))]
    public static void RANDOMIZE(WorldHolderScript __instance)
    {
        Seed.GenerateSeed();
	}
}