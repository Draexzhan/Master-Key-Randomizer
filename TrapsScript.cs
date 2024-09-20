using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static System.Math;
using static MasterKeyRandomizer.MKLogger;

public class Traps : MonoBehaviour
{
	#region cannon trap
	public static List<Vector2> alreadyTargetedPos = new();
	public readonly HelicoPicScript Spikey = Resources.FindObjectsOfTypeAll<HelicoPicScript>().Where(obj => obj.name == "helicoPic").FirstOrDefault() as HelicoPicScript;
	public tombeRocheScript CannonBall;

	public void CannonBallsTrap(int quantity, float rateOfFire)
	{
		StartCoroutine(FireAway(quantity, rateOfFire));
	}

	IEnumerator FireAway(int quantity, float rateOfFire)
	{
		AssetBundle Cannonballs = AssetBundle.LoadFromFile("BepInEx\\plugins\\MasterKeyRandomizer\\masterkeyrandoassets");
		CannonBall = Cannonballs.LoadAsset<tombeRocheScript>("tombeRocheScript");
		for (int i = 0; i < quantity; i++) 
		{
			LogDebug("Firing a new volley");
			FireRandom();
			yield return new WaitForSeconds(rateOfFire);
			FireRandom();
			yield return new WaitForSeconds(rateOfFire);
			FirePlayer();
			yield return new WaitForSeconds(rateOfFire);
		}
		alreadyTargetedPos.Clear();
		Cannonballs.Unload(false);
	}
	private void FireRandom()
	{
		Vector3 CameraLocation = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
		Vector2 vector = new(CameraLocation.x + Random.Range(-7, 8), CameraLocation.y + Random.Range(-5, 6));
		bool flag = true;
		LogDebug("Ready");
		while (flag)
		{
			flag = false;
			foreach (Vector2 alreadyTargeted in alreadyTargetedPos)
			{
				if (alreadyTargeted == vector)
				{
					vector = new(CameraLocation.x + Random.Range(-7, 8), CameraLocation.y + Random.Range(-5, 6));
					flag = true;
				}
			}
		}
		LogDebug("Aim");
		alreadyTargetedPos.Add(vector);
		tombeRocheScript Cannonball = Instantiate(CannonBall);
		Cannonball.transform.position = vector;
		LogDebug("Fire!");
	}
	private void FirePlayer()
	{
		Vector3 CameraLocation = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
		Vector3 PlayerLocation = GameObject.FindGameObjectWithTag("Player").transform.position;
		Vector2 vector = new((int)(PlayerLocation.x + 0.5f) - 0.5f, (int)(PlayerLocation.y + 0.5f) - 0.5f);
		bool flag = true;
		while (flag)
		{
			flag = false;
			foreach (Vector2 alreadyTargeted in alreadyTargetedPos)
			{
				if (alreadyTargeted == vector)
				{
					vector = new(CameraLocation.x + Random.Range(-7, 8), CameraLocation.y + Random.Range(-5, 6));
					flag = true;
				}
			}
		}
		alreadyTargetedPos.Add(vector);
		tombeRocheScript Cannonball = Instantiate(CannonBall);
		Cannonball.transform.position = vector;
	}
	#endregion cannon trap

	#region damage trap
	public void DamageTrap(int damage)
	{
		StartCoroutine(DamagePlayer(damage));
	}
	private IEnumerator DamagePlayer(int damage)
	{
		yield return new WaitForSeconds(4f);
		FindObjectOfType<PlayerHealthAndItemScript>().subisDegats(Min(damage, FindObjectOfType<FoxMove>().PDVActuels - 1), Vector3.zero, 0, false);
	}
	#endregion damage trap

	#region spike trap
	public void SpikeTrap(int quantity)
	{
		StartCoroutine(MakeSpikes(quantity));
	}
	private IEnumerator MakeSpikes(int quantity)
	{
		Vector3 CameraLocation = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
		yield return new WaitForSeconds(4);
		for (int i = 0; i < quantity; i++)
		{
			HelicoPicScript newSpike = Instantiate(Spikey, GameObject.FindGameObjectWithTag("MainCamera").transform.position, Quaternion.identity);
			newSpike.gameObject.AddComponent<TrapSpike>();
			Vector3 PlayerLocation = GameObject.FindGameObjectWithTag("Player").transform.position;
			Vector3 vector = new(CameraLocation.x + Random.Range(-6, 7), CameraLocation.y + Random.Range(-4, 5) + 0.5f, 10f); 
			bool flag = true;
			LogDebug("trapspike");
			while (flag)
			{
				flag = false;
				if (Mathf.Abs(vector.x - PlayerLocation.x) < 3 && Mathf.Abs(vector.y - PlayerLocation.y) < 3)
				{
					vector = new(CameraLocation.x + Random.Range(-6, 7), CameraLocation.y + Random.Range(-4, 5) + 0.5f, 10f);
					flag = true;
				}
			}
			newSpike.gameObject.transform.position = vector;
			newSpike.positionDeDepart = vector;
			newSpike.speed = 1000f;
			LogDebug(vector.ToString());
		}
	}
	#endregion spike trap
}

public class TrapSpike : MonoBehaviour
{
	private float DeathTimer = 10f;
	public void Update()
	{
		DeathTimer -= Time.deltaTime;
		if (DeathTimer < 0)
			Destroy(gameObject);
	}
}