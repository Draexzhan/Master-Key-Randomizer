using HarmonyLib;
using UnityEngine;
using static UpdateInventory;
using static MasterKeyRandomizer.MKLogger;

public class PlayerPatch 
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(PlayerHealthAndItemScript), nameof(PlayerHealthAndItemScript.OnTriggerStay2D))]
    public static bool PatchPlayerTriggerStay2D(PlayerHealthAndItemScript __instance, Collider2D collider)
    {
        if (collider != null)
        {
            if (collider.CompareTag("attaqueEnemie") || (collider.gameObject.GetComponent<ATKScript>() != null && collider.gameObject.GetComponent<ATKScript>().toEveryOne))
            {
                if (collider.gameObject.GetComponent<ATKScript>().moneySteal != 0)
                {
                    if (GameObject.FindGameObjectWithTag("Starter").GetComponent<starterScript>().diffLevel > 2)
                    {
                        __instance.volePiece(collider.gameObject.GetComponent<ATKScript>().moneySteal * 2, collider.gameObject.transform.parent.GetComponentInChildren<HealthScriptMono>());
                    }
                    else
                    {
                        __instance.volePiece(collider.gameObject.GetComponent<ATKScript>().moneySteal, collider.gameObject.transform.parent.GetComponentInChildren<HealthScriptMono>());
                    }
                }
                Vector2 vector = collider.gameObject.transform.position;
                if (collider.gameObject.GetComponent<ATKScript>().toEveryOne)
                {
                    vector = collider.ClosestPoint(__instance.transform.position);
                }
                __instance.subisDegats(collider.gameObject.GetComponent<ATKScript>().degats, vector, collider.gameObject.GetComponent<ATKScript>().Repousse, collider.gameObject.GetComponent<ATKScript>().fromFinalBoss);
			}
			if (collider.CompareTag("ObjetRare") && (__instance.character.argent >= collider.GetComponent<objetRareScript>().valeur))
			{
				LogDebug("item collected and affordable. Adding to inventory and destroying original.");
				__instance.character.argent -= collider.GetComponent<objetRareScript>().valeur;
				AddToInventory(CheckClass.GetData(collider.GetComponent<objetRareScript>().gameObject.name + collider.GetComponent<objetRareScript>().OrigPos).CheckItem);
				Object.Destroy(collider.gameObject);
			}
		}
        if (__instance.character.PDVActuels > __instance.character.PDVMax)
        {
            __instance.character.PDVActuels = __instance.character.PDVMax;
        }
        return false;
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(PlayerHealthAndItemScript), nameof(PlayerHealthAndItemScript.OnTriggerEnter2D))]
    public static bool PatchPlayerTriggerEnter2D(PlayerHealthAndItemScript __instance, Collider2D collider)
    {
        if (collider != null)
        {
            if (collider.gameObject.GetComponent<pieceScript>() != null)
            {
                pieceScript component = collider.gameObject.GetComponent<pieceScript>();
                if (component.type == pieceScript.ItemType.money)
                {
                    if (__instance.argentSc.argentaffiche < __instance.character.argent || __instance.character.argent < __instance.character.maxArgent)
                    {
                        __instance.character.argent += ((!collider.gameObject.GetComponent<pieceScript>().used) ? collider.gameObject.GetComponent<pieceScript>().valeur : 0);
                    }
                    __instance.character.PDVActuels += ((!collider.gameObject.GetComponent<pieceScript>().used) ? (((__instance.starter.diffLevel >= 2) ? 1 : 2) * ((__instance.   starter.diffLevel != 4) ? 1 : 0)) : 0);
                }
                collider.gameObject.GetComponent<pieceScript>().grabIt();
            }
            if (collider.CompareTag("attaqueEnemie") || (collider.gameObject.GetComponent<ATKScript>() != null && collider.gameObject.GetComponent<ATKScript>().toEveryOne))
            {
                if (collider.gameObject.GetComponent<ATKScript>().moneySteal != 0)
                {
                __instance.volePiece(collider.gameObject.GetComponent<ATKScript>().moneySteal, collider.gameObject.transform.parent.GetComponentInChildren<HealthScriptMono>());
                }
                Vector2 vector = collider.gameObject.transform.position;
                if (collider.gameObject.GetComponent<ATKScript>().toEveryOne)
                {
                    vector = collider.ClosestPoint(__instance.transform.position);
                }
            __instance.subisDegats(collider.gameObject.GetComponent<ATKScript>().degats, vector, collider.gameObject.GetComponent<ATKScript>().Repousse, collider.gameObject.GetComponent<ATKScript>().fromFinalBoss);
            }
        }
        if (__instance.character.PDVActuels > __instance.character.PDVMax)
        {
        __instance.character.PDVActuels = __instance.character.PDVMax;
        }
        if (collider.CompareTag("ObjetRare") && (__instance.character.argent >= collider.GetComponent<objetRareScript>().valeur))
        {
            __instance.character.argent -= collider.GetComponent<objetRareScript>().valeur;
            AddToInventory(CheckClass.GetData(collider.gameObject.name + collider.transform.position.ToString()).CheckItem);
            Object.Destroy(collider.gameObject);
        }
        return false;
    }
}
