using NonStandard;
using System;
using UnityEngine;

[Serializable] public struct Turret3d {
	public Vector2 pitchYaw;
	public float distance;
	public Turret3d(Vector2 pitchYaw, float distance) { this.pitchYaw = pitchYaw; this.distance = distance; }
	public override bool Equals(object obj) { return obj is Turret3d value && pitchYaw.Equals(value.pitchYaw) && distance == value.distance; }
	public override int GetHashCode() { return HashCode.Combine(pitchYaw, distance); }
	public static bool operator ==(Turret3d a, Turret3d b) { return a.pitchYaw == b.pitchYaw && a.distance == b.distance; }
	public static bool operator !=(Turret3d a, Turret3d b) { return !(a == b); }

	public void NormalizeAngles() {
		while (pitchYaw.x > 180) { pitchYaw.x -= 360; }
		while (pitchYaw.y > 180) { pitchYaw.y -= 360; }
		while (pitchYaw.x < -180) { pitchYaw.x += 360; }
		while (pitchYaw.y < -180) { pitchYaw.y += 360; }
	}
	public static Vector2 CalculatePitchYaw(Vector3 dir, Vector3 forwardVec, Vector3 rightVec) {
		Vector2 pitchYaw;
		Vector3 up = Vector3.Cross(forwardVec, rightVec);
		Vector3 right = Vector3.Cross(up, dir);
		if (right == Vector3.zero) { right = rightVec; }
		Vector3 straightForward = Vector3.Cross(right, up).normalized;
		pitchYaw.x = Vector3.SignedAngle(straightForward, dir, right);
		pitchYaw.y = Vector3.SignedAngle(forwardVec, straightForward, up);
		if (float.IsNaN(pitchYaw.x)) { pitchYaw.x = 0; }
		if (float.IsNaN(pitchYaw.y)) { pitchYaw.y = 0; }
		return pitchYaw;
	}
	public void Calculate(Vector3 position, Vector3 center, Vector3 forward, Vector3 right) {
		Vector3 delta = position - center;
		distance = delta.magnitude;
		Vector3 dir = delta / distance;
		pitchYaw = CalculatePitchYaw(dir, forward, right);
	}
	public Vector3 CalculateEndPoint(Vector3 center, Vector3 forward, Vector3 right) {
		if (distance > 0) {
			Vector3 delta = forward;
			if (pitchYaw.x != 0) {
				delta = Quaternion.AngleAxis(pitchYaw.x, right) * delta;
			}
			if (pitchYaw.y != 0) {
				Vector3 up = Vector3.Cross(forward, right);
				delta = Quaternion.AngleAxis(pitchYaw.y, up) * delta;
			}
			delta *= distance;
			if (!float.IsNaN(delta.x)) {
				return delta + center;
			}
		}
		return center;
	}
	public void Draw(Vector3 position, Vector3 center, Vector3 forward, Vector3 right, Transform parentLines) {
		const float lineSize = 1f / 64;
		Lines.Make("F").Arrow(center, center + forward, Color.blue, lineSize).transform.SetParent(parentLines);
		Lines.Make("R").Arrow(center, center + right, Color.red, lineSize).transform.SetParent(parentLines);
		Lines.Make("H").Arrow(center, position, Color.white, lineSize).transform.SetParent(parentLines);
		Vector3 up = Vector3.Cross(forward, right);
		Lines.Make("yaw").Arc(pitchYaw.y, up, forward / 2, center, Color.yellow, startSize: lineSize).transform.SetParent(parentLines);
		Vector3 delta = position - center;
		float distance = delta.magnitude;
		Vector3 dir = delta / distance;
		Vector3 r = Vector3.Cross(up, dir).normalized;
		//Lines.Make("RIGHT").Line(center, center + r, Color.red);
		//Lines.Make("UP").Line(center, center + up, Color.green);
		if (r == Vector3.zero) { r = right; }
		Vector3 straightForward = Vector3.Cross(r, up).normalized;
		float pitch = pitchYaw.x;
		if (Mathf.Abs(pitchYaw.x) >= 90) { straightForward = -straightForward; pitch *= -1; }
		//Lines.Make("FORW").Line(center, center + straightForward, Color.blue);
		Lines.Make("pitch").Arc(pitch, r, straightForward / 2, center, Color.magenta, startSize: lineSize).transform.SetParent(parentLines);
	}
}

