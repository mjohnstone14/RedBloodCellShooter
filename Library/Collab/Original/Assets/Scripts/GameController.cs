using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public GameObject redBloodCell;
	public float timeToSpawn = 1.0f;
	public GameObject virusCell;
	public float virusSpawnTime = 0.0f;
	public float bloodSpawnTime = 0.0f;
	public float virusSpeed = 0.0f;
	public float bloodSpeed = 0.0f;


	// Use this for initialization
	void Start () {
		StartCoroutine (SpawnVirusCoroutine());
		StartCoroutine (SpawnRedCellsCoroutine());

	}
	
	// Update is called once per frame
	void Update () {
		//Updater ();
		//Debug.Log (Mathf.Log10(Time.time));
		virusSpawnTime = 6.0f - Mathf.Log10 (Time.time);
		bloodSpawnTime = 6.0f - Mathf.Log10 (Time.time);
		Debug.Log (virusSpawnTime);

		
	}

	IEnumerator SpawnVirusCoroutine () {
		while (true)
		{
			Instantiate(virusCell, new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), this.transform.position.z), Quaternion.identity);
			yield return new WaitForSeconds(virusSpawnTime);
		}
	}

	IEnumerator SpawnRedCellsCoroutine () {
		while (true)
		{
			Instantiate(redBloodCell, new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), this.transform.position.z), Quaternion.identity);
			yield return new WaitForSeconds(bloodSpawnTime);
		}
	}
		
}
