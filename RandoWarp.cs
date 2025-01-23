using System.Collections;
using UnityEngine;
using BepInEx;

public class RandoWarp
{
	static FoxMove foxMove;
	public static void PotionWarp(Vector3 destination)
	{
		foxMove = Object.FindObjectOfType<FoxMove>();
		ThreadingHelper.Instance.StartCoroutine(PotionWarper(destination));
	}

	static IEnumerator PotionWarper(Vector3 destination)
	{
		yield return new WaitForSeconds(3f);
		foxMove.gameObject.transform.position = destination;

	}
}
