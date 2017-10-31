using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SimpleTouchAreaButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	private bool touched;
	private int pointerId;
	private bool canFire;

	void Awake() {
		touched = false;
	}
		
	public void OnPointerDown(PointerEventData data) {
		if (!touched) {
			touched = true;
			pointerId = data.pointerId;
			canFire = true;
		}
	}

	public void OnPointerUp(PointerEventData data) {
		if (data.pointerId == pointerId) {
			touched = false;
			canFire = false;
		}
	}

	public bool CanFire() {
		return canFire;
	}
}

