using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	private Rigidbody rb;
	public float speed;
	public float tilt;

	public Boundary boundary;

	void Start () {
		rb = GetComponent <Rigidbody> ();
	}

	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * speed;

		var xBoundary = Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax);
		var zBoundary = Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax);

		rb.position = new Vector3 (xBoundary, 0.0f, zBoundary);
		rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
	}
}
