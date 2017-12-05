using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {


	public int SelectedWeapon;
	public GameObject ProjectilePrefab;
	public LineRenderer LaserLineRenderer;
	public ParticleSystem particles;
	private bool CanMove;

	// Use this for initialization
	void Start () {
		SelectedWeapon = 0;
		LaserLineRenderer.enabled = false;
		CanMove = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (CanMove) {
			float x = Input.GetAxis ("Horizontal") * Time.deltaTime * 10.0f;
			float y = Input.GetAxis ("Vertical") * Time.deltaTime * 3.0f;
			transform.Translate (x, y, 0);
		}

		if (Input.GetKeyDown ("q")) {
			float yRotation = 90.0f;
			//Debug.Log ("rotated to the right");
			//transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + yRotation, transform.eulerAngles.z);
			UpdateWeapon (-1);
		}

		if (Input.GetKeyDown ("e")) {
			float yRotation = -90.0f;
			//Debug.Log ("rotated to the left");
			//transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + yRotation, transform.eulerAngles.z);
			UpdateWeapon (1);
		}
			
		if (Input.GetKeyDown (KeyCode.Space) && GameManager.gameState == GameManager.GameState.Running) {
			Fire ();
		}	
			
	}

	private void Fire() {
		GameObject.FindGameObjectWithTag ("ShootSound").GetComponent<AudioSource>().Play();
		switch (SelectedWeapon) {

		// fire the single projectile
		case 0:
			Vector3 Direction = new Vector3(0,0,1);
			ProjectilePrefab.transform.position = gameObject.transform.position;
			ProjectilePrefab.GetComponent<Projectile> ().Direction = Direction;
			ProjectilePrefab.GetComponent<Projectile> ().Distance = 60;
			Instantiate (ProjectilePrefab);
			break;
		// spray projectiles
		case 1:
			List<Vector3> Directions = new List<Vector3> ();
			Directions.Add(new Vector3 (.25f, 0, 1));
			Directions.Add(new Vector3 (-.25f, 0, 1));
			Directions.Add(new Vector3 (0, 0, 1));
			foreach(Vector3 D in Directions) {
				ProjectilePrefab.transform.position = gameObject.transform.position;
				ProjectilePrefab.GetComponent<Projectile> ().Direction = D;
				ProjectilePrefab.GetComponent<Projectile> ().Distance = 20;
				Instantiate (ProjectilePrefab);
			}
			break;
		// laser
		case 2:
			StartCoroutine (LaserCoroutine ());
			break;
		}
	}

	private IEnumerator LaserCoroutine() {
		
		CanMove = false;
		while(Input.GetKey(KeyCode.Space)) {
			Ray ray = new Ray (gameObject.transform.position, new Vector3 (0, 0, 1));
			RaycastHit hit;
			if (Physics.SphereCast(ray, 1.0f, out hit)) {
				if (hit.collider.gameObject.CompareTag ("Virus")) {
					Destroy (hit.collider.gameObject);
					ScoreScript.AddPoints ();
				}
				if (hit.collider.gameObject.CompareTag("RedBloodCell"))
				{
					GameObject stamSlider = GameObject.FindGameObjectWithTag("Stamina");
					GameObject background = GameObject.FindGameObjectWithTag("Background");
					Vector3 scale = stamSlider.transform.localScale;

					Vector3 bgscale = stamSlider.transform.localScale;
					bgscale = background.transform.localScale;
					float bgrelativeScale = (float)10 / (float)10;
					scale.x = bgrelativeScale;
					background.transform.localScale = scale;

					Projectile.currentDeath++;
					Destroy (hit.collider.gameObject);
//					Debug.Log (Projectile.currentDeath);
					if (Projectile.currentDeath == 10)
						SceneManager.LoadScene ("MainMenu"); // you die, game over
					else {
						scale = stamSlider.transform.localScale;
						float relativeScale = (float)Projectile.currentDeath / (float)10;
						scale.x = relativeScale;
						stamSlider.transform.localScale = scale;
					}
				}
			}

			LaserLineRenderer.SetPosition (0, gameObject.transform.position);
			LaserLineRenderer.SetPosition (1, gameObject.transform.position + new Vector3 (0, 0, 60));
			LaserLineRenderer.startWidth = 1f;
			LaserLineRenderer.endWidth = 1f;
			LaserLineRenderer.enabled = true;
		
			// make player not able to move
			yield return null;
		}
		LaserLineRenderer.enabled = false;
		CanMove = true;
	}

	private void UpdateWeapon(int i) {
		GameObject.FindObjectOfType<UIManager> ().UpdateWeapon (SelectedWeapon);
		SelectedWeapon += i;
		SelectedWeapon %= 3;
		if (SelectedWeapon < 0) {
			SelectedWeapon = 2;
		}
	}


	/// <summary>
	/// Selects type of projectile fire based on rotation
	/// </summary>
	void TypeOfFire() {
		
		
	}
}
