using HarmonyLib;
using BepInEx.Logging;
using System;
using System.IO;
using System.Reflection;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static MasterKeyRandomizer.MKLogger;
using BepInEx;
using MasterKeyRandomizer;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Threading;
using UnityEngine.UIElements;
using UnityEngine.TextCore.Text;
using System.Numerics;
using UnityEngine.SocialPlatforms.Impl;

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
