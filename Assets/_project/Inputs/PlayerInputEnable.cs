using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputEnable : MonoBehaviour {
	[System.Serializable] public class ActionMapToggle {
		public string name;
		[SerializeField] private bool enabled = true;
		public bool Enabled {
			get { return enabled; }
			set {
				bool changed = enabled != value;
				enabled = value;
				if (!changed || !Application.isPlaying) { return; }
				PlayerInput[] playerInputs = FindObjectsOfType<PlayerInput>();
				for (int i = 0; i < playerInputs.Length; ++i) {
					ApplyActionMapToggle(this, playerInputs[i]);
				}
			}
		}
	}

	public List<ActionMapToggle> actionMaps = new List<ActionMapToggle> { };

	public static void ApplyActionMapToggle(List<ActionMapToggle> actionMaps, PlayerInput pi) {
		for (int i = 0; i < actionMaps.Count; i++) {
			ApplyActionMapToggle(actionMaps[i], pi);
		}
	}
	public static void ApplyActionMapToggle(ActionMapToggle amt, PlayerInput pi) {
		string name = amt.name;
		InputActionMap map = pi.actions.FindActionMap(name);
		if (map == null) {
			return;
		}
		if (amt.Enabled) {
			map.Enable();
			//Debug.Log("enabled " + name + " " + map);
		} else {
			map.Disable();
			//Debug.Log("DISabled " + name + " " + map);
		}
	}

	public void Start() { Refresh(); }

	public void Refresh() {
		Array.ForEach(FindObjectsOfType<PlayerInput>(), pi => ApplyActionMapToggle(actionMaps, pi));
	}

#if UNITY_EDITOR
	private void OnValidate() {
		if (!Application.isPlaying) { return; }
		Refresh();
	}
#endif
}
