using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Projectile.
/// </summary>
public class Projectile : MonoBehaviour {
	public float Speed;
	public static int currentDeath = 0;
	public Vector3 Direction;
	public int Distance;

	// Use this for initialization
	void Start () {
		IgnorePlayer ();
	}

	// Update is called once per frame
	void Update () {
		gameObject.transform.position += Direction * Time.deltaTime * Speed;
		if (gameObject.transform.position.z >= Distance) {
			Destroy (gameObject);
		}
	}
	/// <summary>
	/// Raises the collision enter event.
	/// </summary>
	/// <param name="other">Other.</param>
	void OnCollisionEnter(Collision other) {
		int max = 10;

		if(other.gameObject.CompareTag("Hit")) {
			Debug.Log ("Already hit");
			return;
		}

		StartCoroutine(CountToDie (1, other));

		if (other.collider.CompareTag ("Virus")) {
			GameObject.FindGameObjectWithTag ("VirusDeath").GetComponent<AudioSource>().Play();
			ScoreScript.AddPoints ();
		}
		else if (other.collider.CompareTag("RedBloodCell"))
		{
			GameObject stamSlider = GameObject.FindGameObjectWithTag("Stamina");
			GameObject background = GameObject.FindGameObjectWithTag("Background");
			Vector3 scale = stamSlider.transform.localScale;

			Vector3 bgscale = stamSlider.transform.localScale;
			bgscale = background.transform.localScale;
			float bgrelativeScale = (float)10 / (float)max;
			scale.x = bgrelativeScale;
			background.transform.localScale = scale;

			currentDeath++;

			//			Debug.Log (currentDeath);

			if (currentDeath == 10  && ScoreScript.score <= 120)
				SceneManager.LoadScene ("BronzeMedal"); // you die, game over
			else {
				scale = stamSlider.transform.localScale;
				float relativeScale = (float)currentDeath / (float)max;
				scale.x = relativeScale;
				stamSlider.transform.localScale = scale;
			}
		}
		other.gameObject.tag = "Hit";
	}

	/// <summary>
	/// Ignores the player.
	/// </summary>
	void IgnorePlayer() {
		Player player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
		Physics.IgnoreCollision (this.GetComponent<SphereCollider> (), player.GetComponent<SphereCollider> ());

	}

	IEnumerator CountToDie(int seconds, Collision other) {
		Debug.Log ("got here");
		yield return new WaitForSeconds(seconds);
		if (other != null) {
			Destroy (other.gameObject);
			Destroy (gameObject);
		}
	}
}
