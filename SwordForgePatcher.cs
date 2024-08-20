using HarmonyLib;
using System.Collections;
using UnityEngine;
using static ItemCheatSheet;
using static UpdateInventory;

public class SwordForgePatcher
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(SwordCutscene), nameof(SwordCutscene.OnTriggerEnter2D))]
    public static bool SwordPatcherCollide(SwordCutscene __instance, ref Collider2D collision)
    {

        if (collision.gameObject.tag == "Player" && __instance.cutscenePhase == 0 && __instance.enabledNain == 6 && __instance.player.GetComponent<FoxMove>().picLVL >= 5)
        {
            for (int i = 0; i < 6; i++)
            {
                __instance.nains[i].gameObject.SetActive(value: true);
            }
            __instance.StartCoroutine(ForgeCutscene(__instance));
        }
        return false;
    }
    private static IEnumerator ForgeCutscene(SwordCutscene __instance)
    {
        __instance.music.volume = 0.2f;
        __instance.cutscenePhase = 1;
        GameObject.FindGameObjectWithTag("Controls").GetComponent<FoxControllerScript>().disableInput = true;
        while (__instance.cutscenePhase != 2)
        {
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.5f);
        __instance.striking = false;
        for (int l = 0; l < 6; l++)
        {
            __instance.nains[l].GetComponent<SpriteRenderer>().sprite = __instance.nainStanding;
        }
        __instance.BrandisSword.SetActive(value: true);
        yield return new WaitForSeconds(0.5f);
        for (int k = 0; k < __instance.speech.Length; k++)
        {
            __instance.speech[k].speak();
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(2.5f);
        __instance.BrandisSword.SetActive(value: false);
        __instance.SwordForging.SetActive(value: true);
        __instance.PlaceSword.SetActive(value: true);
        yield return new WaitForSeconds(1f);
        __instance.PlaceSword.SetActive(value: false);
        yield return new WaitForSeconds(1f);
        for (int m = 0; m < 6; m++)
        {
            __instance.nains[m].GetComponent<SpriteRenderer>().sprite = __instance.nainCelebrating;
        }
        for (int k = 0; k < 20; k++)
        {
            yield return new WaitForSeconds(0.2f);
            int num = UnityEngine.Random.Range(0, 6);
            if (__instance.nains[num].GetComponent<AutoVelocity>().targetVelo == 0f)
            {
                __instance.StartCoroutine(__instance.nainSaute(num));
            }
        }
        yield return new WaitForSeconds(1f);
        __instance.striking = true;
        __instance.cadenceModifier = 1.5f;
        yield return new WaitForSeconds(2f);
        __instance.cadenceModifier = 2f;
        yield return new WaitForSeconds(2f);
        __instance.cadenceModifier = 3f;
        yield return new WaitForSeconds(2f);
        __instance.cadenceModifier = 5f;
        yield return new WaitForSeconds(4f);
        __instance.striking = false;
        for (int n = 0; n < 6; n++)
        {
            __instance.nains[n].GetComponent<SpriteRenderer>().sprite = __instance.nainHolding;
        }
        yield return new WaitForSeconds(1f);
        for (int num2 = 0; num2 < 6; num2++)
        {
            __instance.nains[num2].GetComponent<SpriteRenderer>().sprite = __instance.nainSmashing;
            AudioSource.PlayClipAtPoint((UnityEngine.Random.Range(0f, 1f) < 0.5f) ? __instance.smashSFX1 : __instance.smashSFX2, __instance.gameObject.transform.position, GameObject.FindGameObjectWithTag("Starter").GetComponent<starterScript>().SFXVol);
        }
        __instance.SwordForging.SetActive(value: false);
        __instance.SwordReady.SetActive(value: true);
        yield return new WaitForSeconds(1f);
        for (int num3 = 0; num3 < 6; num3++)
        {
            __instance.nains[num3].GetComponent<SpriteRenderer>().sprite = __instance.nainStanding;
        }
        yield return new WaitForSeconds(1f);
        for (int num4 = 0; num4 < 6; num4++)
        {
            __instance.nains[num4].GetComponent<SpriteRenderer>().sprite = __instance.nainCelebrating;
        }
        __instance.SwordReady.SetActive(value: false);
        ItemData MasterfulForge = CheckClass.GetData("UltimaSword").CheckItem;
        AddToInventory(MasterfulForge);
        GameObject.FindGameObjectWithTag("Controls").GetComponent<FoxControllerScript>().disableInput = false;
        __instance.player.GetComponent<FoxMove>().enableMove(b: true);
        yield return new WaitForSeconds(6f);
        __instance.music.volume = 1f;
        for (int k = 0; k < 50; k++)
        {
            yield return new WaitForSeconds(0.2f);
            int num5 = UnityEngine.Random.Range(0, 6);
            if (__instance.nains[num5].GetComponent<AutoVelocity>().targetVelo == 0f)
            {
                __instance.StartCoroutine(__instance.nainSaute(num5));
            }
        }
        yield return new WaitForSeconds(1f);
        __instance.striking = true;
        __instance.cadenceModifier = 1f;
    }
}
