using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Outer bound script.
/// Destroy Viruses and Red Blood Cells that collide with the outer tube.
/// </summary>
public class OuterBoundScript : MonoBehaviour {

	void OnCollisionEnter(Collision other) {
		if (other.collider.CompareTag ("Virus") || other.collider.CompareTag ("RedBloodCell")) {
			Destroy (other.gameObject);
		}
	}
}
