using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// User interface manager.
/// Control the UI of the game, including health and weapon displays.
/// </summary>
public class UIManager : MonoBehaviour {

	public GameObject[] WeaponIcons;
	public GameObject Player;
	public GameObject virusSlider;

	private int SelectedWeapon;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateWeapon(int SelectedWeapon) {
		if(Input.GetKeyDown(KeyCode.Q)) {
			WeaponIcons [SelectedWeapon].GetComponent<Image> ().color = Color.white;
			SelectedWeapon -= 1;
		}
		if(Input.GetKeyDown(KeyCode.E)) {
			WeaponIcons [SelectedWeapon].GetComponent<Image> ().color = Color.white;
			SelectedWeapon += 1;
			SelectedWeapon %= 3;
		}
		if (SelectedWeapon < 0) {
			SelectedWeapon = 2;
		}
		WeaponIcons [SelectedWeapon].GetComponent<Image> ().color = Color.red;
	}

	public void UpdateSlider(float amount) {
		virusSlider.transform.localScale += new Vector3(amount, 0, 0);
	}
}
