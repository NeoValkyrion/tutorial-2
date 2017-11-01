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

	public SimpleTouchPad touchPad;
	public SimpleTouchAreaButton touchAreaButton;

	//private Quaternion calibrationQuaternion;

	void Start () {
		rb = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	
		//CalibrateAccelerometer();
	}

	void Update () {

		bool readyToShoot;

		#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
			readyToShoot = Input.GetButton("Fire1") && Time.time > nextFire;
		#endif

		#if UNITY_ANDROID || UNITY_IOS && !UNITY_EDITOR
			readyToShoot = touchAreaButton.CanFire() && Time.time > nextFire;
		#endif

		if (readyToShoot) {
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			audioSource.Play();
		}
	}

	void FixedUpdate () {

		Vector3 movement;

		#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
			movement = GetKeyboardMovement();
		#endif

		#if UNITY_ANDROID || UNITY_IOS && !UNITY_EDITOR
			movement = GetTouchpadMovement();
		#endif

		rb.velocity = movement * speed;

		var xBoundary = Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax);
		var zBoundary = Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax);

		rb.position = new Vector3 (xBoundary, 0.0f, zBoundary);
		rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
	}

	Vector3 GetKeyboardMovement() {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		return new Vector3(moveHorizontal, 0.0f, moveVertical);
	}

	Vector3 GetTouchpadMovement() {
		Vector2 direction = touchPad.GetDirection();
		return new Vector3(direction.x, 0.0f, direction.y);
	}
		
	//Vector3 GetAccelerometerMovement() {
		//Vector3 accelerationRaw = Input.acceleration;
		//Vector3 acceleration = FixAcceleration(accelerationRaw);
		//return new Vector3 (acceleration.x, 0.0f, acceleration.y);
	//}

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
