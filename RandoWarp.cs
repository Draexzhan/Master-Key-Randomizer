using System.Collections;
using UnityEngine;
using BepInEx;

public class RandoWarp
{
	static FoxMove foxMove;
	public static void PotionWarp(UnityEngine.Vector3 destination)
	{
		foxMove = UnityEngine.Object.FindObjectOfType<FoxMove>();
		ThreadingHelper.Instance.StartCoroutine(PotionWarper(destination));
	}

	static IEnumerator PotionWarper( UnityEngine.Vector3 destination)
	{
		yield return new WaitForSeconds(3f);
		foxMove.gameObject.transform.position = destination;

	}
}
