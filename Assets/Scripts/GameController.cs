using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Game controller.
/// Controls gameplay and flow of game. Runs different methods according to a time interval.
/// Responsible for spawning virus and blood cells.
/// </summary>
public class GameController : MonoBehaviour {
	public GameObject redBloodCell;
	public GameObject virusCell;
	public float virusSpawnTime = 0.0f;
	public float bloodSpawnTime = 0.0f;
	public float virusSpeed = 4.0f;
	public float bloodSpeed = 4.0f;
	public bool bloodSpawn = true;
	public bool virusSpawn = true;
	public bool virusSpecialSpawn = true;
	public float startTime = 40;
	public float endTime = 100;
	public float virusSpecialSpeed = 8;
	public float virusSpecialSpawnTime = .7f;
	public float EpicMusicCounter = 2;


	// Use this for initialization
	void Start () {
		StartCoroutine (SpawnVirusCoroutine());
		StartCoroutine (SpawnRedCellsCoroutine());
		GameObject.FindGameObjectWithTag ("PassiveMusic1").GetComponent<AudioSource>().Play();
	
	}

	/// <summary>
	/// Spawns the virus coroutine.
	/// </summary>
	/// <returns>The virus coroutine.</returns>
	IEnumerator SpawnVirusCoroutine () {
		while (virusSpawn) {
			Instantiate(virusCell, new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
			yield return new WaitForSeconds(virusSpawnTime);
		}
	}

	/// <summary>
	/// Spawns the special virus coroutine.
	/// </summary>
	/// <returns>The special virus coroutine.</returns>
	IEnumerator SpawnSpecialVirusCoroutine () {
		while (virusSpecialSpawn) {
			Instantiate(virusCell, new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
			yield return new WaitForSeconds(virusSpecialSpawnTime);
		}
	}

	/// <summary>
	/// Spawns the red cells coroutine.
	/// </summary>
	/// <returns>The red cells coroutine.</returns>
	IEnumerator SpawnRedCellsCoroutine () {
		while (bloodSpawn) {
			Instantiate(redBloodCell, new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), this.transform.position.z), Quaternion.identity);
			yield return new WaitForSeconds(bloodSpawnTime);
		}
	}

	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update() {
		float time = Time.timeSinceLevelLoad;
		//virus speed run 
		if (time > startTime && time < endTime) {
			StopCoroutine (SpawnRedCellsCoroutine ());
			StopCoroutine (SpawnVirusCoroutine ());
			if (bloodSpawn == true) {
				PlayEpicMusic ();
				virusSpecialSpawn = true;
				StartCoroutine (SpawnSpecialVirusCoroutine());
				virusSpecialSpeed += 3;
				virusSpecialSpawnTime -= .1f;
				if (virusSpecialSpawnTime < .1) {
					virusSpecialSpawnTime = .1f;
				} 

				bloodSpawn = false;
				virusSpawn = false;

			}
			GameObject[] viruses;
			viruses = GameObject.FindGameObjectsWithTag ("Virus");
			foreach (GameObject virus in viruses) {
				VirusScript virus1 = virus.GetComponent<VirusScript> (); 
				virus1.speed = virusSpecialSpeed;
			}
		// normal game mode with red blood cells and viruses 
		} else {
			StopCoroutine (SpawnSpecialVirusCoroutine ());
			virusSpecialSpawn = false;
			if (bloodSpawn == false) {
				GameObject.FindGameObjectWithTag ("PassiveMusic1").GetComponent<AudioSource>().Play();
				bloodSpawn = true;
				virusSpawn = true;
				startTime += 100;
				endTime += 100;
				virusSpeed++;
				bloodSpeed++;
				StartCoroutine (SpawnRedCellsCoroutine());
				StartCoroutine (SpawnVirusCoroutine());

			}
			//changes virus and blood cell speed
			GameObject[] viruses;
			viruses = GameObject.FindGameObjectsWithTag ("Virus");
			foreach (GameObject virus in viruses) {
				VirusScript virus1 = virus.GetComponent<VirusScript> (); 
				virus1.speed = virusSpeed;
			}
			GameObject[] bloods;
			bloods = GameObject.FindGameObjectsWithTag ("RedBloodCell");
			foreach (GameObject blood in bloods) {
				RedBloodCell blood1 = blood.GetComponent<RedBloodCell> (); 
				blood1.speed = bloodSpeed;
			}
			virusSpawnTime = 4.5f - Mathf.Log10 (Time.time);
			bloodSpawnTime = 4.0f - Mathf.Log10 (Time.time);
		}
	}

	/// <summary>
	/// Decides what audio files to play 
	/// </summary>
	void PlayEpicMusic() {
		if (EpicMusicCounter % 2 == 1) {
			GameObject.FindGameObjectWithTag ("EpicMusic2").GetComponent<AudioSource> ().Play ();

		} else if (EpicMusicCounter % 4 == 0) {
			GameObject.FindGameObjectWithTag ("EpicMusic3").GetComponent<AudioSource> ().Play ();

		} else {
			GameObject.FindGameObjectWithTag ("EpicMusic1").GetComponent<AudioSource> ().Play ();

		}
		EpicMusicCounter++;	
	}
}
