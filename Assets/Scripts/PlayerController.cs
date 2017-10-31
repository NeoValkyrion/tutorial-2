using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Rigidbody rb;
	private AudioSource audioSource;
	private float nextFire;

	public float speed;
	public float tilt;
	public float fireRate;

	public GameObject shot;
	public Transform shotSpawn;
	public Boundary boundary;

	//private Quaternion calibrationQuaternion;
	public SimpleTouchPad touchPad;
	public SimpleTouchAreaButton touchAreaButton;

	void Start () {
		rb = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	
		//Calibrate for mobile accelerometer
		//CalibrateAccelerometer();
	}

	void Update () {

		//Original shooting guard
		//if (Input.GetButton("Fire1") && Time.time > nextFire)
		if (touchAreaButton.CanFire() && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			audioSource.Play();
		}
	}

	void FixedUpdate () {
		//Original movement
		//float moveHorizontal = Input.GetAxis ("Horizontal");
		//float moveVertical = Input.GetAxis ("Vertical");
		//Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		//Mobile accelerometer movement
		//Vector3 accelerationRaw = Input.acceleration;
		//Vector3 acceleration = FixAcceleration(accelerationRaw);
		//Vector3 movement = new Vector3 (acceleration.x, 0.0f, acceleration.y);

		//Mobile touchpad movement
		Vector2 direction = touchPad.GetDirection();
		Vector3 movement = new Vector3 (direction.x, 0.0f, direction.y);

		rb.velocity = movement * speed;

		var xBoundary = Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax);
		var zBoundary = Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax);

		rb.position = new Vector3 (xBoundary, 0.0f, zBoundary);
		rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
	}

	//Mobile accelerometer calibration

	//void CalibrateAccelerometer() {
		//Vector3 accelerationSnapshot = Input.acceleration;
		//Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3 (0.0f, 0.0f, -1.0f), accelerationSnapshot);
		//calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
	//}

	//Vector3 FixAcceleration(Vector3 acceleration) {
		//Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
		//return fixedAcceleration;
	//}
}
