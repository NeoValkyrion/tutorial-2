using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SimpleTouchPad : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler {

	public float smoothing;

	private Vector2 origin;
	private Vector2 direction;
	private Vector2 smoothDirection;
	private bool touched;
	private int pointerId;

	void Awake() {
		direction = Vector2.zero;
		touched = false;
	}

	//Set our start point
	public void OnPointerDown(PointerEventData data) {
		if (!touched) {
			touched = true;
			pointerId = data.pointerId;
			origin = data.position;
		}
	}

	//Compare the difference between our start point and current pointer position
	public void OnDrag(PointerEventData data) {
		if (data.pointerId == pointerId) {
			Vector2 currentPosition = data.position;
			Vector2 directionRaw = currentPosition - origin;
			direction = directionRaw.normalized;
		}
	}

	//Reset all the things
	public void OnPointerUp(PointerEventData data) {
		if (data.pointerId == pointerId) {
			direction = Vector2.zero;
			touched = false;
		}
	}

	public Vector2 GetDirection() {
		smoothDirection = Vector2.MoveTowards(smoothDirection, direction, smoothing);
		return smoothDirection;
	}
}
