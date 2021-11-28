using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.Controls;
#if UNITY_EDITOR
using UnityEditor.Events;
#endif

namespace NonStandard.Inputs {
	[System.Serializable] public class AxBind : ControlBind<float> {
		public AxBind(AppInput.StandardAxis axis, string name = null, AxisChangeHandler onAxisEvent = null, Func<bool> additionalRequirement = null)
			: base(new Axis(axis), name, onAxisEvent, additionalRequirement) {
		}
		public AxBind(Axis axis,                  string name = null, AxisChangeHandler onAxisEvent = null, Func<bool> additionalRequirement = null)
			: base(axis, name, onAxisEvent, additionalRequirement) {
		}
		public AxBind(AppInput.StandardAxis axis, AxisChangeHandler onAxisEvent, string name = null) : this(axis, name, onAxisEvent) { }
		public void AddAxis(AppInput.StandardAxis axis, string nameToUse, AxisChangeHandler onAxis = null) {
			AddAxis(new Axis[] { new Axis(axis) }, nameToUse, onAxis);
		}
		public AxBind(Axis axis, string name, object target, string setMethodName, Func<bool> additionalRequirement = null) :
			base(new[] { axis }, name, null, additionalRequirement) {
			AddListener(target, setMethodName);
		}

	}
	[System.Serializable] public class Vector3Bind : ControlBind<Vector3> {
		public KCode vector3Code;
		public Vector3Bind(KCode axis, AxisChangeHandler onAxisEvent, string name = null) : base(axis, onAxisEvent, name) {
			Debug.Log("doin it for "+axis+" not "+vector3Code);
		}
        public override void Init() {
			Debug.Log("make it happen for "+vector3Code+", not ("+axis+")");
        }
    }
	/// <summary>
	/// the working class that handles axis input binding to an input handler. can bind multiple controllers to the same input binding (like for using both 'w' and 'UpArrow' for moving up)
	/// </summary>
	/// <typeparam name="TYPE"></typeparam>
	[System.Serializable] public class ControlBind<TYPE> : InputBind {
		public bool disable;
		private bool wasAllowedLastFrame;
		[System.Serializable] public class UnityEvent : UnityEngine.Events.UnityEvent<TYPE> { }

		/// <summary>
		/// name of the axis, e.g.: Horizontal, Vertical
		/// </summary>
		public ControlInterface<TYPE> [] axis = new ControlInterface<TYPE>[] { };

		/// <summary>
		/// </summary>
		/// <param name="value"></param>
		/// <returns>true if the event should be consumed by this handler</returns>
		public delegate bool AxisChangeHandler(TYPE value);

		/// <summary>
		/// additional requirements for the input
		/// </summary>
		public Func<bool> additionalRequirement;

		[ContextMenuItem("DoActivateTrigger", "DoActivateTrigger")]
		public EventSet axisEvent = new EventSet();

		/// <summary>
		/// stores bindings for an input handler. the handler can be a bool-returning function (return true to consume event), or a UnityEvent
		/// </summary>
		[System.Serializable] public class EventSet {
			/// <summary>
			/// for UI inspectable and set-able delegates
			/// </summary>
			[SerializeField, ContextMenuItem("DoAxisChange", "DoAxisChangeEmpty")] public UnityEvent onAxisChange;

			/// <summary>
			/// for pure code delegates
			/// </summary>
			public AxisChangeHandler actionAxisChange;

			public int CountAxisChangeEvents {
				get { return (onAxisChange != null ? onAxisChange.GetPersistentEventCount() : 0) + (actionAxisChange != null ? actionAxisChange.GetInvocationList().Length : 0); }
			}
			public void AddAxisChangeEvent(AxisChangeHandler a) {
				if (!Application.isPlaying) { Debug.LogWarning("cannot serialize callbacks, only use this method at runtime!"); }
				if (actionAxisChange != null) { actionAxisChange += a; } else { actionAxisChange = a; }
			}
			public bool DoAxisChange(TYPE value) {
				if (onAxisChange != null) onAxisChange.Invoke(value);
				return (actionAxisChange != null) ? actionAxisChange.Invoke(value) : false;
			}
			public bool DoAxisChangeEmpty() { return DoAxisChange(default(TYPE)); }
			public void RemoveAxisChange() { onAxisChange.RemoveAllListeners(); actionAxisChange = null; }

			public string GetDelegateText(UnityEvent ue, AxisChangeHandler a) {
				StringBuilder text = new StringBuilder();
				if (ue != null) {
					for (int i = 0; i < ue.GetPersistentEventCount(); ++i) {
						if (text.Length > 0) { text.Append("\n"); }
						UnityEngine.Object obj = ue.GetPersistentTarget(i);
						string t = obj != null ? obj.name : "<???>";
						text.Append(t).Append(".").Append(KBind.EventSet.FilterMethodName(ue.GetPersistentMethodName(i)));
					}
				}
				if (a != null) {
					Delegate[] delegates = a.GetInvocationList();
					for (int i = 0; i < delegates.Length; ++i) {
						if (text.Length > 0) { text.Append("\n"); }
						text.Append(delegates[i].Target).Append(".").Append(delegates[i].Method.Name);
					}
				}
				return text.ToString();
			}

			public string CalculateDescription() {
				StringBuilder text = new StringBuilder();
				string desc = GetDelegateText(onAxisChange, actionAxisChange);
				text.Append(desc);
				return text.ToString();
			}
		}

		public bool IsAllowed() { return !disable && (additionalRequirement == null || additionalRequirement.Invoke()); }

		public ControlBind(KCode axis, AxisChangeHandler onAxisEvent, string name = null) : this(axis, name, onAxisEvent) { }
		public ControlBind(KCode axis, string name = null, AxisChangeHandler onAxisEvent = null, Func<bool> additionalRequirement = null)
			: this(ControlInterface<TYPE>.CreateNew(axis), name, onAxisEvent, additionalRequirement) {
		}

		public ControlBind(ControlInterface<TYPE> axis, string name = null, AxisChangeHandler onAxisEvent = null, Func<bool> additionalRequirement = null)
			: this(new[] { axis }, name, onAxisEvent, additionalRequirement) {
		}

		public ControlBind(ControlInterface<TYPE> axis, string name, object target, string setMethodName, Func<bool> additionalRequirement = null)
			: this(new[] { axis }, name, null, additionalRequirement) {
			AddListener(target, setMethodName);
		}

		public void AddListener(object target, string setMethodName) {
			System.Reflection.MethodInfo targetinfo = UnityEvent.GetValidMethodInfo(target, setMethodName, new Type[] { typeof(TYPE) });
			if(targetinfo == null) {
				Debug.LogError("no method " + setMethodName + " in " + target.ToString());
			}
			UnityAction<TYPE> action = Delegate.CreateDelegate(typeof(UnityAction<TYPE>), target, targetinfo, false) as UnityAction<TYPE>;
			if (axisEvent.onAxisChange == null) {
				axisEvent.onAxisChange = new UnityEvent();
			}
#if UNITY_EDITOR
			UnityEventTools.AddPersistentListener(axisEvent.onAxisChange, action);
#else
			axisEvent.onAxisChange.AddListener(f => { targetinfo.Invoke(target, new object[] { f }); });
#endif
		}

		/// <summary>
		/// describes functions to execute when any of the specified key-combinations are pressed/held/released
		/// </summary>
		public ControlBind(ControlInterface<TYPE>[] axis, string name = null, AxisChangeHandler onAxisEvent = null, Func<bool> additionalRequirement = null) {
			this.axis = axis;
			Init();
			this.name = name;
			AddEvents(onAxisEvent);
			if (additionalRequirement != null) {
				this.additionalRequirement = additionalRequirement;
			}
		}

		public virtual void Init() {
			if (axis == null) {
				Debug.Log("we have a problem for "+name+" type "+typeof(TYPE));
            }
			Array.ForEach(axis, ax => {
			ax.Init();
#if UNITY_EDITOR
			if(ax.ValueEquals(ax.multiplier, default(TYPE))) {
				Debug.LogWarning("AxisBind->"+name+"->" + ax + " has a zero multiplier. Was this intentional?");
			}
#endif
		}); }

		public void AddEvents(AxisChangeHandler onAxisEvent = null) {
			if (onAxisEvent != null) { axisEvent.AddAxisChangeEvent(onAxisEvent); }
		}

		public void AddAxis(ControlInterface<TYPE>[] axisToUse) {
			if (axis.Length == 0) { axis = axisToUse; } else {
				List<ControlInterface<TYPE>> currentAxis = new List<ControlInterface<TYPE>>(axis);
				currentAxis.AddRange(axisToUse);
				// remove duplicates
				for (int a = 0; a < currentAxis.Count; ++a) {
					for (int b = currentAxis.Count - 1; b > a; --b) {
						if (currentAxis[a].CompareTo(currentAxis[b]) == 0) {
							currentAxis.RemoveAt(b);
						}
					}
				}
				axis = currentAxis.ToArray();
			}
			Init();
		}

		public void AddAxis(ControlInterface<TYPE>[] axisToAdd, string nameToUse, AxisChangeHandler onAxis = null) {
			if (axisToAdd != null) { AddAxis(axisToAdd); }
			if (string.IsNullOrEmpty(name)) { name = nameToUse; }
			AddEvents(onAxis);
		}

		public string ShortDescribe(string betweenKeyPresses = "\n") {
			if (axis == null || axis.Length == 0) return "";
			string text = "";
			for (int i = 0; i < axis.Length; ++i) {
				if (i > 0) text += betweenKeyPresses;
				text += axis[i].ToString();
			}
			return text;
		}

		public override string ToString() { return ShortDescribe(" || ")+" \""+name+"\""; }

		/// <returns>if the action succeeded (which may remove other actions from queue, due to priority)</returns>
		public bool DoAxis(TYPE value) { return axisEvent.DoAxisChange(value); }

		public ControlInterface<TYPE> GetActiveAxis() {
			bool allowedChecked = false;
			for (int i = 0; i < axis.Length; ++i) {
				if (axis[i].IsValueChanged()) {
					if (!allowedChecked) { if (!IsAllowed()) { return null; } allowedChecked = true; }
					axis[i].MarkValueAsKnown();
					return axis[i];
				}
			}
			return null;
		}
		public bool IsActive() { return GetActiveAxis() != null; }

		public void DoActivateTrigger() {
			if (axisEvent.CountAxisChangeEvents > 0) { DoAxis(default(TYPE)); }
		}

		public void Update() {
			bool allowed = IsAllowed();
			if (!allowed) {
				if (wasAllowedLastFrame) {
					wasAllowedLastFrame = false;
					ClearNonStickyInput();
				}
				return;
			}
			wasAllowedLastFrame = allowed;
			for (int i = 0; i < axis.Length; ++i) {
				ControlInterface<TYPE> ax = axis[i];
				if (ax.IsValueChanged()) {
					ax.MarkValueAsKnown();
					TYPE value = ax.useMultiplier ? ax.MultiplyValueByMultiplier(ax.cachedValue, ax.multiplier) : ax.cachedValue;
					DoAxis(value);
					break;
				}
			}
		}

		private void ClearNonStickyInput() {
			for (int i = 0; i < axis.Length; ++i) {
				ControlInterface<TYPE> ax = axis[i];
				if (!ax.stickyInput) { ax.cachedValue = default(TYPE); }
			}
		}
	}

//    [System.Serializable] public class AxiiVector3 : ControlInterface<Vector3> {
//#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
//		InputSystemInterfaceLogic.InputMapping_Vector3 inputMapping;
//#endif
//		public AxiiVector3(KCode kCode) : base(kCode) { Refresh(); }
//		public void Refresh() {
//#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
//			InputSystemInterfaceLogic.InputMapping_base mapping = null;
//			if (InputSystemInterfaceLogic.Instance.inputBinding.TryGetValue(kCode, out mapping)) {
//				inputMapping = mapping as InputSystemInterfaceLogic.InputMapping_Vector3;
//			}
//#endif
//		}
//		public override Vector3 GetValueRaw() {
//#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
//			return inputMapping.input;
//#else
//			return Vector3.zero;
//#endif
//		}
//        public override Vector3 MultiplyValueByMultiplier(Vector3 value, Vector3 multiplier) { return Vector3.Scale(value, multiplier); }
//        public override bool ValueEquals(Vector3 a, Vector3 b) { return a == b; }
//    }

    [System.Serializable] public class Axis : ControlInterface<float> {
		public AppInput.StandardAxis standardAxis = AppInput.StandardAxis.None;
#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
		AxisControl axisControl;
#endif
		public Axis(AppInput.StandardAxis axis) : base (KCode.None) { this.standardAxis = axis; useMultiplier = false; }
		public Axis(AppInput.StandardAxis axis, float multiplier) : this(axis) { this.multiplier = multiplier; useMultiplier = true; }
		public Axis(KCode kCode) : base(kCode) {
			if (kCode != KCode.None) {
#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
				axisControl = KCodeExtensionUnity.GetInputController(kCode);
#else
				Debug.LogError("needs the Unity Input System to gather axis controls from a KCode");
#endif
			}
		}
		public override float MultiplyValueByMultiplier(float value, float multiplier) { return value * multiplier; }
		public override float GetValueRaw() {
			float value;
#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
			if (standardAxis != AppInput.StandardAxis.None) {
				value = AppInput.GetStandardAxis(standardAxis);
			} else {
				value = axisControl.ReadValue();
			}
#else
			value = AppInput.GetStandardAxis(standardAxis);
#endif
			return value;
		}
        public override bool ValueEquals(float a, float b) { return a == b; }
		public override int CompareTo(ControlInterface<float> other) {
			AppInput.StandardAxis sa = AppInput.StandardAxis.None;
			if (other is Axis a) { sa = a.standardAxis; }
			int v = standardAxis.CompareTo(sa);
			if (v != 0) return v;
			return kCode.CompareTo(other.kCode);
		}

	}
	/// <summary>
	/// a wrapper around an individual type of control, like a stick axis (or virtual control, like a Vector3 velocity from a hand controller)
	/// </summary>
	/// <typeparam name="TYPE"></typeparam>
	public abstract class ControlInterface<TYPE> : IComparable<ControlInterface<TYPE>> {
		public KCode kCode = KCode.None;
		public TYPE multiplier;
		private TYPE knownValue;
		public TYPE cachedValue { get; set; }

		public abstract TYPE MultiplyValueByMultiplier(TYPE value, TYPE multiplier);

		public bool useMultiplier = false;
		/// <summary>
		/// if true, use software dampened value, instead of instantaneous raw value
		/// </summary>
		public bool filteredValue = false;
		/// <summary>
		/// if true, value will remain unchanged when axis is disabled
		/// </summary>
		public bool stickyInput = false;

		public KModifier[] modifiers;

		public ControlInterface(KCode kCode) { this.kCode = kCode; useMultiplier = false; }

		public bool IsValueChanged() {
			bool isAllowed = modifiers == null || modifiers.Length == 0 || KCombo.IsSatisfiedHeld(modifiers);
			if (!isAllowed) {
				if (!stickyInput) { cachedValue = default(TYPE); }
			} else {
				cachedValue = filteredValue ? GetValue() : GetValueRaw();
			}
			return !ValueEquals(cachedValue, knownValue);// (cachedValue != knownValue);
		}
		public abstract bool ValueEquals(TYPE a, TYPE b);
		public void MarkValueAsKnown() { knownValue = cachedValue; }
		public virtual TYPE GetValue() {
			// TODO make an easing function over time for pure software axis, like Horizontal and Vertical
			return GetValueRaw();
		}
		public abstract TYPE GetValueRaw();
		public virtual int CompareTo(ControlInterface<TYPE> other) { return kCode.CompareTo(other.kCode); }
		public void Init() { }
		public override string ToString() { return KCombo.ToString(modifiers)+kCode.ToString(); }

		public static ControlInterface<TYPE> CreateNew(KCode kCode) {
			ControlInterface<TYPE> a = null;
			if (typeof(TYPE) == typeof(float)) {
				a = new Axis(kCode) as ControlInterface<TYPE>;
            } else {
				Debug.LogError("need handler code to create a controller for " + typeof(TYPE));
            }
			return a;
        }
	}
}