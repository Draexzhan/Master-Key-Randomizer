using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;
using static MasterKeyRandomizer.MKLogger;

namespace CannonPatch.patches;
class CannonQueue
{
	[HarmonyPrefix]
	[HarmonyPatch(typeof(tombeRocheScript), nameof(tombeRocheScript.Start))]
	public static bool CannonStart(tombeRocheScript __instance)
	{
		if (SceneManager.GetActiveScene().name != "MainMenu")
			return true;
		__instance.timer = -1000f;
		__instance.gameObject.name = "CannonBallTrap";
		GameObject.DontDestroyOnLoad(__instance.gameObject);
		__instance.gameObject.SetActive(false);
		return false;
	}

	[HarmonyPrefix]
	[HarmonyPatch(typeof(tombeRocheScript), nameof(tombeRocheScript.Update))]
	public static bool CannonUpdate(tombeRocheScript __instance)
	{
		if (__instance.timer > -500f && __instance.timer < 0f)
		{
			__instance.timer = Time.deltaTime;
			Object.Destroy(__instance.gameObject, __instance.timeBFRATK + 5f);
		}
		if (__instance.timer >= 0f)
		{
			__instance.timer += Time.deltaTime;
			if (!(__instance.timer > __instance.timeBFRATK))
			{
				return false;
			}
			__instance.rock.transform.position += Time.deltaTime * 10f * Vector3.down;
			if (__instance.rock.transform.localPosition.y <= 0f && __instance.rock.activeSelf)
			{
				__instance.rock.SetActive(value: false);
				Object.Instantiate(__instance.rockSplosion, __instance.transform.position, Quaternion.identity, __instance.transform);
				__instance.damage.SetActive(value: true);
				__instance.activationTime = __instance.timer;
				if (Random.Range(0f, 1f) <= 0.02f)
				{
					Object.Instantiate(__instance.petitePiece, __instance.transform.position, Quaternion.identity);
				}
			}
			if (__instance.timer > __instance.activationTime + 0.1f)
			{
				__instance.damage.SetActive(value: false);
			}
		}
		return false;
	}

}