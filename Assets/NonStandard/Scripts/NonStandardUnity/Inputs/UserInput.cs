using NonStandard.Utility;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace NonStandard.Inputs {
	public class InputBind {
		/// <summary>
		/// how to name this key binding in any user interface that pops up.
		/// </summary>
		public string name;
	}

	public class UserInput : MonoBehaviour {
		public List<KBind> KeyBinds = new List<KBind>();
		public List<AxBind> AxisBinds = new List<AxBind>();
		public List<Vector3Bind> Vector3Binds = new List<Vector3Bind>();

		private void Start() { KeyInput.Init(KeyBinds); AxisInput.Init(AxisBinds); Vector3Input.Init(Vector3Binds); }
		private void OnEnable() { KeyInput.Enable(KeyBinds); AxisInput.OnEnable(AxisBinds); Vector3Input.OnEnable(Vector3Binds); }
		private void OnDisable() { KeyInput.Disable(KeyBinds); AxisInput.OnDisable(AxisBinds); Vector3Input.OnDisable(Vector3Binds); }

		public void KeyBind(KCode kCode, KModifier modifier, string name, string methodName, object value = null, object target = null) {
			KeyInput.Bind(KeyBinds, kCode, modifier, name, methodName, value, target);
		}
		public bool RemoveKeyBind(string name) { return KeyInput.RemoveBind(KeyBinds, name); }
		public bool RemoveAxisBind(string name) { return AxisInput.RemoveBind(AxisBinds, name); }
		public bool RemoveVector3Bind(string name) { return Vector3Input.RemoveBind(Vector3Binds, name); }

		public bool SetEnableKeyBind(string name, bool enable) { return KeyInput.SetEnableBind(KeyBinds, name, enable); }
		public bool SetEnableAxisBind(string name, bool enable) { return AxisInput.SetEnableBind(AxisBinds, name, enable); }
		public bool SetEnableVector3Bind(string name, bool enable) { return Vector3Input.SetEnableBind(Vector3Binds, name, enable); }
	}
}
