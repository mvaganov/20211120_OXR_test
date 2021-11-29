using NonStandard.Extension;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputEnable : MonoBehaviour {
	public enum ActionMapState { Disabled, Enabled, Ignored }
	public int actionGroupIndex;
	public List<ActionMapGroup> actionGroups = new List<ActionMapGroup>();

	[System.Serializable] public class ActionMapToggle {
		public string name;
		public ActionMapState state;
		public bool Enabled {
			get { return state == ActionMapState.Enabled; }
			set {
				bool same = (value && state == ActionMapState.Enabled) || (!value && state == ActionMapState.Disabled);
				state = value ? ActionMapState.Enabled : ActionMapState.Disabled;
				if (same || !Application.isPlaying || name == null) { return; }
				PlayerInput[] playerInputs = FindObjectsOfType<PlayerInput>();
				for (int i = 0; i < playerInputs.Length; ++i) {
					ApplyActionMapToggle(playerInputs[i]);
				}
			}
		}
		public void ApplyActionMapToggle(PlayerInput pi) {
			InputActionMap map = pi.actions.FindActionMap(name);
			if (map == null) { return; }
			switch (state) {
				case ActionMapState.Enabled: map.Enable(); break;
				case ActionMapState.Disabled: map.Disable(); break;
			}
		}
	}

	[System.Serializable] public class ActionMapGroup {
		public string name;
		public List<ActionMapToggle> actionMaps = new List<ActionMapToggle> { };
		public void Refresh() {
			Array.ForEach(FindObjectsOfType<PlayerInput>(), pi => ApplyActionMapToggle(actionMaps, pi));
		}
	}

	public void NextActionGroup() {
		++actionGroupIndex;
		while (actionGroupIndex < 0) { actionGroupIndex += actionGroups.Count; }
		while (actionGroupIndex >= actionGroups.Count) { actionGroupIndex -= actionGroups.Count; }
		Refresh();
	}
	public static void ApplyActionMapToggle(List<ActionMapToggle> actionMaps, PlayerInput pi) {
		for (int i = 0; i < actionMaps.Count; i++) {
			actionMaps[i].ApplyActionMapToggle(pi);
		}
	}
	public void Start() {
		Refresh();
	}
	public void Refresh() {
		if (actionGroups.Count > actionGroupIndex) {
			actionGroups[actionGroupIndex].Refresh();
		}
	}
	public void SetActionGroup(string groupName) {
		int index = actionGroups.FindIndex(g=>g.name== groupName);
		if (index < 0) {
			throw new Exception($"unknown group name \"{groupName}\". valid names: "+actionGroups.JoinToString(", ", g=>g.name));
		}
		SetActionGroup(index);
    }
	public void SetActionGroup(int index) {
		actionGroupIndex = index;
		Refresh();
	}
	public static void EnableInputActionMap(string inputMapName, bool enable) {
		ActionMapToggle amt = new ActionMapToggle { name = inputMapName };
		amt.Enabled = enable;
	}
	public static void DisableInputActionMap(string inputMapName) { EnableInputActionMap(inputMapName, false); }

	public void NextActionGroup(InputAction.CallbackContext context) {
		switch (context.phase) { case InputActionPhase.Canceled: NextActionGroup(); break; }
    }

#if UNITY_EDITOR
	private void OnValidate() {
		if (!Application.isPlaying) { return; }
		Refresh();
	}
#endif
}
