using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NonStandard.Inputs {
	public class Vector3Input : MonoBehaviour {
		public List<Vector3Bind> Vector3Binds = new List<Vector3Bind>();

		public static void Init(IList<Vector3Bind> Vector3Binds) {
			if (Vector3Binds.Count > 0) {
				for (int i = 0; i < Vector3Binds.Count; ++i) { Vector3Binds[i].Init(); }
			}
		}

		public static void OnEnable(IList<Vector3Bind> Vector3Binds) {
			if (Vector3Binds.Count > 0 && !AppInput.HasVector3Bind(Vector3Binds[0])) {
				for (int i = 0; i < Vector3Binds.Count; ++i) { AppInput.AddListener(Vector3Binds[i]); }
			}
		}
		public static void OnDisable(IList<Vector3Bind> Vector3Binds) {
			if (AppInput.IsQuitting) return;
			if (Vector3Binds.Count > 0 && AppInput.HasVector3Bind(Vector3Binds[0])) {
				for (int i = 0; i < Vector3Binds.Count; ++i) { AppInput.RemoveListener(Vector3Binds[i]); }
			}
		}
		public static bool RemoveBind(List<Vector3Bind> AxisBinds, string name) {
			int index = AxisBinds.FindIndex(kb => kb.name == name);
			if (index >= 0) { AxisBinds.RemoveAt(index); return true; }
			return false;
		}
		public static bool SetEnableBind(List<Vector3Bind> Vector3Binds, string name, bool enable) {
			Vector3Bind kBind = Vector3Binds.Find(kb => kb.name == name);
			if (kBind != null) { kBind.disable = !enable; return true; }
			return false;
		}
		private void Start() { Init(Vector3Binds); }
		private void OnEnable() { OnEnable(Vector3Binds); }
		private void OnDisable() { OnDisable(Vector3Binds); }
		public bool RemoveBind(string name) { return RemoveBind(Vector3Binds, name); }
		public bool SetEnableBind(string name, bool enable) { return SetEnableBind(Vector3Binds, name, enable); }
	}
}