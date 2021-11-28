using NonStandard;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestVrController : MonoBehaviour {
	public Collider selector;
	private Vector3 calculatedSelectorPosition;
	public Turret3d value;
	public float rotateSpeed = 90;
	Vector2 pitchYawChange;
	float distanceChange;
	private Turret3d calculated;
	public bool drawCalculations;
	public bool useAngleSnap = true;
	bool turretControlsNeedUpdate = false;

	public TMP_Text textOutput;

	public float angleSnap = 15;
	public float angleSnapStickiness = 15;
	public float distanceSnap = 1f / 8;
	public float distanceSnapStickiness = 1f/4;
	Vector2 angleSnapProgress;
	float distanceSnapProgress;

	public void Calculate(Vector3 position, Vector3 center, Vector3 forward, Vector3 right) {
		value.Calculate(position, center, forward, right);
		if (drawCalculations) { value.Draw(position, center, forward, right, transform); }
	}

	public void UpdateText() {
		if (textOutput == null) return;
		textOutput.text = "pitch: " + value.pitchYaw.x.ToString("0.0") + "\nyaw: " + value.pitchYaw.y.ToString("0.0")+"\ndist:"+value.distance.ToString("0.000");
	}

	public void UpdateSelectorTurret() {
		selector.transform.position = value.CalculateEndPoint(transform.position, transform.forward, transform.right);
		UpdateText();
	}
	private void Start() {
		if (selector != null) {
			CalculateSelectorTurret();
		}
	}
	private void Update() {
		if (distanceChange != 0) {
			//value.distance += distanceChange * Time.deltaTime;
			float change = distanceChange * Time.deltaTime;
			IncrementWithSnap(ref value.distance, change, ref distanceSnapProgress, distanceSnap, distanceSnapStickiness);
			if (value.distance < 0) { value.distance = 0; }
		}
		if (pitchYawChange != Vector2.zero) {
			Vector2 change = pitchYawChange * rotateSpeed * Time.deltaTime;
			IncrementWithSnap(ref value.pitchYaw.y, change.y, ref angleSnapProgress.y, angleSnap, angleSnapStickiness);
			IncrementWithSnap(ref value.pitchYaw.x, change.x, ref angleSnapProgress.x, angleSnap, angleSnapStickiness);
			value.NormalizeAngles();
		}
		if (selector != null) {
			bool turretControlsChanged = value != calculated;
			if (turretControlsChanged) {
				value.NormalizeAngles();
				UpdateSelectorTurret();
				if (drawCalculations) {
					value.Draw(selector.transform.position, transform.position, transform.forward, transform.right, transform);
				}
				calculatedSelectorPosition = selector.transform.position;
			}
			bool turretPositionChanged = calculatedSelectorPosition != selector.transform.position;
			if (turretPositionChanged) { turretControlsNeedUpdate = true; }
		}
		if (turretControlsNeedUpdate) {
			CalculateSelectorTurret();
		}
	}

	public void CalculateSelectorTurret() {
		Calculate(selector.transform.position, transform.position, transform.forward, transform.right);
		calculated = value;
		calculatedSelectorPosition = selector.transform.position;
		turretControlsNeedUpdate = false;
		UpdateText();
	}

	public void SetRotation(InputAction.CallbackContext context) {
		//Debug.Log(context.ToString());
		transform.localRotation = context.ReadValue<Quaternion>();
	}
	public void SetPosition(InputAction.CallbackContext context) {
		//Debug.Log(context.ToString());
		transform.localPosition = context.ReadValue<Vector3>();
	}

	public static void IncrementWithSnap(ref float value, float change, ref float snapProgress, float snap, float angleSnapStickiness) {
		if (change == 0) return;
		float lowerBound, upperBound;
		if (value >= 0) {
			lowerBound = Math3d.RoundDownToNearest(value, snap);
			upperBound = (lowerBound == value) ? value : Math3d.RoundUpToNearest(value, snap);
		} else {
			upperBound = Math3d.RoundUpToNearest(value, snap);
			lowerBound = (upperBound == value) ? value : Math3d.RoundDownToNearest(value, snap);
		}
		IncrementWithSnap(ref value, lowerBound, upperBound, change, ref snapProgress, angleSnapStickiness);
	}
	public static void IncrementWithSnap(ref float value, float lowerBound, float upperBound, float change, ref float snapProgress, float angleSnapStickiness) {
		float excess;
		float newValue = value + change;
		if (change < 0) {
			if (newValue < lowerBound) {
				excess = newValue - lowerBound;
				snapProgress += excess;
				newValue = lowerBound;
			}
			if (snapProgress < -angleSnapStickiness) {
				excess = snapProgress + angleSnapStickiness;
				newValue += excess;
				snapProgress = 0;
			}
		} else {
			if (newValue > upperBound) {
				excess = newValue - upperBound;
				snapProgress += excess;
				newValue = upperBound;
			}
			if (snapProgress > +angleSnapStickiness) {
				excess = snapProgress - angleSnapStickiness;
				newValue += excess;
				snapProgress = 0;
			}
		}
		value = newValue;
	}
	public void RotateTurretWithJoystick(InputAction.CallbackContext context) {
		switch (context.phase) {
		case InputActionPhase.Canceled: pitchYawChange = Vector2.zero; angleSnapProgress = Vector2.zero; break;
		}
		pitchYawChange = context.ReadValue<Vector2>();
		float temp = pitchYawChange.x; pitchYawChange.x = pitchYawChange.y; pitchYawChange.y = temp;
	}
	public void ForwardTurretWithTrigger(InputAction.CallbackContext context) {
		switch (context.phase) {
		case InputActionPhase.Canceled: distanceChange = 0; distanceSnapProgress = 0; return;
		}
		distanceChange = context.ReadValue<float>();
	}
}