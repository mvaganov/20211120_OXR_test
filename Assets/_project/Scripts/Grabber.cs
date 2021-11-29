using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour {
	/// <summary>
	/// what _could be_ grabbed
	/// </summary>
	public List<Grabbable> grabbables = new List<Grabbable>();
	/// <summary>
	/// what _is_ grabbed
	/// </summary>
	public List<Grabbable> grabbed = new List<Grabbable>();
	class WasKinematic : MonoBehaviour { }
	private void OnTriggerEnter(Collider other) {
		Grabbable g = other.GetComponent<Grabbable>();
		if (g) { MarkGrabbable(g); }
	}
	private void OnTriggerExit(Collider other) {
		Grabbable g = other.GetComponent<Grabbable>();
		if (g) { if (grabbables.Contains(g)) { grabbables.Remove(g); } }
	}
	public void MarkGrabbable(Grabbable g) {
		if (!grabbed.Contains(g)) { MarkGrabbableEvenIfGrabbed(g); }
	}
	public void MarkGrabbableEvenIfGrabbed(Grabbable g) {
		if (!grabbables.Contains(g)) { grabbables.Add(g); }
	}
	public void Grab(Grabbable g) {
		g.transform.SetParent(transform);
		if (!grabbed.Contains(g)) {
			grabbed.Add(g);
			Rigidbody rb = g.GetComponent<Rigidbody>();
			if (rb == false || rb.isKinematic) {
				g.gameObject.AddComponent<WasKinematic>();
			} else {
				rb.isKinematic = true;
            }
		}
	}
	public void ReleaseGrabbed(Grabbable g) {
		if (g.transform.parent == transform) {
			g.transform.SetParent(null);
			WasKinematic wk = g.GetComponent<WasKinematic>();
			if (wk == null) {
				Rigidbody rb = g.GetComponent<Rigidbody>();
				if (rb != null) { rb.isKinematic = false; }
			}
		}
	}

	public void ReleaseGrabbed() {
		grabbed.ForEach(ReleaseGrabbed);
		grabbed.ForEach(MarkGrabbableEvenIfGrabbed);
		grabbed.Clear();
	}
	public void Grab() {
		grabbables.ForEach(Grab);
		grabbables.Clear();
	}
}

