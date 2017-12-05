using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Red blood cell.
/// Control the movement of the Red Blood Cells.
/// </summary>
public class RedBloodCell : MonoBehaviour {
	private Transform thisTransf;
	public float speed = 1;
	// Use this for initialization
	void Start () {
		thisTransf = transform;
	}

	// Update is called once per frame
	void Update () {
		thisTransf.Translate (0, 0, -speed * Time.deltaTime);
	}


}
