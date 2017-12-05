using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Player.
/// Control the player's movement, weapon and shooting ability.
/// </summary>
public class Player : MonoBehaviour {

	public int selectedWeapon;
	public GameObject projectilePrefab;
	public LineRenderer laserLineRenderer;
	public ParticleSystem particles;
	public static int currentDeath = 0;
	private bool canMove;
	public string currentTime;
	// Use this for initialization
	void Start () {
		selectedWeapon = 0;
		laserLineRenderer.enabled = false;
		canMove = true;
		currentDeath = 0;
	}

	
	// Update is called once per frame
	void Update () {
		if (canMove) {
			float x = Input.GetAxis ("Horizontal") * Time.deltaTime * 10.0f;
			float y = Input.GetAxis ("Vertical") * Time.deltaTime * 8.0f;
			transform.Translate (x, y, 0);
		}

		// Change weapons if Q or E keys are pressed
		if (Input.GetKeyDown ("q")) {
			UpdateWeapon (-1);
		}
		if (Input.GetKeyDown ("e")) {
			UpdateWeapon (1);
		}
			
		if (Input.GetKeyDown (KeyCode.Space) && GameManager.gameState == GameManager.GameState.Running) {
			Fire ();
		}
	}

	/// <summary>
	/// Fire using the single projectile, spray projectiles, or laser mechanics based on the selected weapon.
	/// </summary>
	private void Fire() {
		GameObject.FindGameObjectWithTag ("ShootSound").GetComponent<AudioSource>().Play();
		switch (selectedWeapon) {

		// fire the single projectile
		case 0:
			Vector3 direction = new Vector3(0,0,1);
			projectilePrefab.transform.position = gameObject.transform.position;
			projectilePrefab.GetComponent<Projectile> ().direction = direction;
			projectilePrefab.GetComponent<Projectile> ().distance = 60;
			Instantiate (projectilePrefab);
			break;
		// spray projectiles
		case 1:
			List<Vector3> directions = new List<Vector3> ();
			directions.Add(new Vector3 (.25f, 0, 1));
			directions.Add(new Vector3 (-.25f, 0, 1));
			directions.Add(new Vector3 (0, 0, 1));
			foreach(Vector3 D in directions) {
				projectilePrefab.transform.position = gameObject.transform.position;
				projectilePrefab.GetComponent<Projectile> ().direction = D;
				projectilePrefab.GetComponent<Projectile> ().distance = 20;
				Instantiate (projectilePrefab);
			}
			break;
		// laser
		case 2:
			StartCoroutine (LaserCoroutine ());
			break;
		}
	}

	/// <summary>
	/// Create a line renderer and raycast for the laser shooting mechanic, during which the player cannot move.
	/// </summary>
	/// <returns>The coroutine.</returns>
	private IEnumerator LaserCoroutine() {
		canMove = false;
		while(Input.GetKey(KeyCode.Space)) {
			Ray ray = new Ray (gameObject.transform.position, new Vector3 (0, 0, 1));
			RaycastHit hit;
			if (Physics.SphereCast(ray, 1.0f, out hit)) {
				if (hit.collider.gameObject.CompareTag ("Virus")) {
					Destroy (hit.collider.gameObject);
					ScoreScript.AddPoints ();
					GameObject.FindGameObjectWithTag ("VirusDeath").GetComponent<AudioSource>().Play();
				}
				if (hit.collider.gameObject.CompareTag("RedBloodCell"))
				{
					LoseLife ();
					Destroy (hit.collider.gameObject);
					GameObject.FindGameObjectWithTag ("RedCellDeath").GetComponent<AudioSource>().Play();
				}
			}
			laserLineRenderer.SetPosition (0, gameObject.transform.position);
			laserLineRenderer.SetPosition (1, gameObject.transform.position + new Vector3 (0, 0, 60));
			laserLineRenderer.startWidth = 1f;
			laserLineRenderer.endWidth = 1f;
			laserLineRenderer.enabled = true;
			yield return null;
		}
		laserLineRenderer.enabled = false;
		canMove = true;
	}

	/// <summary>
	/// Change the variable representing the selected weapon. Can be 0, 1 or 2, representing each shoot mechanic.
	/// </summary>
	/// <param name="i">The index.</param>
	private void UpdateWeapon(int i) {
		GameObject.FindObjectOfType<UIManager> ().UpdateWeapon (selectedWeapon);
		selectedWeapon += i;
		selectedWeapon %= 3;
		if (selectedWeapon < 0) {
			selectedWeapon = 2;
		}
	}

	/// <summary>
	/// Cause the player to lose health, reflected in the UI health bar element.
	/// </summary>
	public void LoseLife() {
		currentDeath++;
		GameObject.FindObjectOfType<UIManager> ().UpdateSlider (.1f);
		int score = ScoreScript.score;

		// If the player has died 10 times, end the game 
		if (currentDeath >= 10 && score <= 120) {
			SceneManager.LoadScene ("GameOver"); // you die, game over
		} else if (currentDeath >= 10 && score <= 620) {
			SceneManager.LoadScene ("BronzeMedal");
		} else if (currentDeath >= 10 && score <= 1210) {
			SceneManager.LoadScene ("SilverMedal");
		} else if (currentDeath >= 10 && score > 1210) {
			SceneManager.LoadScene ("GoldMedal");
		}
	}

	/// <summary>
	/// Raises the collision enter event.
	/// </summary>
	/// <param name="other">Other.</param>
	void OnCollisionEnter(Collision other) {
		if (other.collider.CompareTag ("Virus")) {
			LoseLife ();
			Destroy (other.gameObject);
			GameObject.FindGameObjectWithTag ("RedCellDeath").GetComponent<AudioSource>().Play();
		}	
	}
}
