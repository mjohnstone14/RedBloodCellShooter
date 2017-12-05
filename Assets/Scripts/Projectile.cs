using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Projectile.
/// Controls the projectiles shot by the player.
/// </summary>
public class Projectile : MonoBehaviour {

	public float speed;
	public static int currentDeath = 0;
	public Vector3 direction;
	public int distance;

	// Use this for initialization
	void Start () {
		IgnorePlayer ();
	}

	// Update is called once per frame
	void Update () {
		// Move the projectile the distance of the speed at each frame in a given direction
		gameObject.transform.position += direction * Time.deltaTime * speed;
		if (gameObject.transform.position.z >= distance) {
			Destroy (gameObject);
		}
	}
	/// <summary>
	/// Raises the collision enter event.
	/// </summary>
	/// <param name="other">Other.</param>
	void OnCollisionEnter(Collision other) {
		if (other.collider.CompareTag ("Virus")) {
			GameObject.FindGameObjectWithTag ("VirusDeath").GetComponent<AudioSource>().Play();
			ScoreScript.AddPoints ();
		}
		else if (other.collider.CompareTag("RedBloodCell"))
		{
			GameObject.FindGameObjectWithTag ("RedCellDeath").GetComponent<AudioSource>().Play();
			GameObject.FindObjectOfType<Player> ().GetComponent<Player> ().LoseLife ();

		}
		Destroy (other.gameObject);
		Destroy (gameObject);
	}

	/// <summary>
	/// Ignores the player.
	/// </summary>
	void IgnorePlayer() {
		Player player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
		Physics.IgnoreCollision (this.GetComponent<SphereCollider> (), player.GetComponent<SphereCollider> ());
	}
}
