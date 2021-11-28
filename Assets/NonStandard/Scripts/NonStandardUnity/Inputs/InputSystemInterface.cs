using NonStandard.Extension;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
#endif

namespace NonStandard.Inputs {
	public class InputSystemInterface : MonoBehaviour {
#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
		public InputSystemInterfaceLogic inputSystemInterface;
		public bool debugEvents = false;
		public Dictionary<KCode, InputSystemInterfaceLogic.SpecificControlHandler> OnPressed => inputSystemInterface.OnPressed;
		public Dictionary<KCode, InputSystemInterfaceLogic.SpecificControlHandler> OnRelease => inputSystemInterface.OnRelease;
		public Dictionary<KCode, InputSystemInterfaceLogic.SpecificControlHandler> OnPressing => inputSystemInterface.OnPressing;
		public InputSystemInterfaceLogic.SpecificControlHandler OnPressedAny => inputSystemInterface.OnPressedAny;
		public InputSystemInterfaceLogic.SpecificControlHandler OnReleaseAny => inputSystemInterface.OnReleaseAny;
		public InputSystemInterfaceLogic.SpecificControlHandler OnPressingAny => inputSystemInterface.OnPressingAny;

		public List<InputSystemInterfaceLogic.Vector3Mapping> vector3Mapping = new List<InputSystemInterfaceLogic.Vector3Mapping>();
		void Awake() {
			inputSystemInterface = InputSystemInterfaceLogic.Instance;
			if (debugEvents) {
				InputSystem.onEvent += DebugEventHandler;
			}
			inputSystemInterface.vector3Mapping = vector3Mapping;
			inputSystemInterface.RefreshVector3Mapping();
		}
		void OnDestroy() {
			InputSystem.onEvent -= DebugEventHandler;
			inputSystemInterface.Release();
		}
        private void Start() {
			inputSystemInterface.Start();
		}
        void Update() {
			inputSystemInterface.Update();
		}
		private void DebugEventHandler(InputEventPtr eventPtr, InputDevice device) {
			// only spend time on input events
			if (!eventPtr.IsA<StateEvent>() && !eventPtr.IsA<DeltaStateEvent>()) { return; }
			// go through all of the inputs
			foreach (InputControl control in eventPtr.EnumerateChangedControls(device)) {
				Debug.Log(control.ToString());
			}
			
		}
#endif
	}
#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
	[Serializable] public class InputSystemInterfaceLogic {
		private static InputSystemInterfaceLogic _instance;
		public static InputSystemInterfaceLogic Instance => _instance != null ? _instance : _instance = new InputSystemInterfaceLogic();
		/// <summary>
		/// Delegate for controller input handling
		/// </summary>
		/// <param name="control"></param>
		/// <returns>true if the input was handled and consumed, false if not (which means the event should be passed to other handlers)</returns>
		public delegate void DeviceControlHandler(object control);
		public delegate void SpecificControlHandler(KCode kCode, object control);

		/// <summary>
		/// list of input handlers, where the integer device ID is the index in the list
		/// </summary>
		private List<Dictionary<object, DeviceControlHandler>> deviceInputHandler = new List<Dictionary<object, DeviceControlHandler>>();
		public Dictionary<KCode, SpecificControlHandler> OnPressed = new Dictionary<KCode, SpecificControlHandler>();
		public Dictionary<KCode, SpecificControlHandler> OnRelease = new Dictionary<KCode, SpecificControlHandler>();
		public Dictionary<KCode, SpecificControlHandler> OnPressing = new Dictionary<KCode, SpecificControlHandler>();

		public SpecificControlHandler OnPressedAny;
		public SpecificControlHandler OnReleaseAny;
		public SpecificControlHandler OnPressingAny;

		/// <summary>
		/// each device (first Dictionary dimension) has inputs of TYPE
		/// </summary>
		public Dictionary<int,Dictionary<string, InputMapping_Vector3>> Vector3Inputs = new Dictionary<int, Dictionary<string, InputMapping_Vector3>>();
		public List<InputMapping_Vector3> Vector3Inputs_listing = new List<InputMapping_Vector3>();
		public Dictionary<int, Dictionary<string, InputMapping_Quaternion>> QuaternionInputs = new Dictionary<int, Dictionary<string, InputMapping_Quaternion>>();
		public List<InputMapping_Quaternion> QuaternionInputs_listing = new List<InputMapping_Quaternion>();
		public Dictionary<int, Dictionary<string, InputMapping_Float>> ButtonInputs = new Dictionary<int, Dictionary<string, InputMapping_Float>>();
		public List<InputMapping_Float> ButtonInputs_listing = new List<InputMapping_Float>();
		public Dictionary<int, Dictionary<string, InputMapping_Int>> IntegerInputs = new Dictionary<int, Dictionary<string, InputMapping_Int>>();
		public List<InputMapping_Int> IntegerInputs_listing = new List<InputMapping_Int>();
		public Dictionary<int, Dictionary<KCode, InputMapping_base>> inputBinding = new Dictionary<int, Dictionary<KCode, InputMapping_base>>();

		/// <summary>
		/// which controls are active right now, by their keycode
		/// </summary>
		public Dictionary<KCode, object> activeControl = new Dictionary<KCode, object>();

		List<string> errors	= new List<string>();

		public static Dictionary<string, KCode> hmd_ExpectedVector3 = new Dictionary<string, KCode>() {
			["centereyevelocity"] = KCode.XrHmdCenterEyeVelocity,
			["centereyeangularvelocity"] = KCode.XrHmdCenterEyeAngularVelocity,
			["devicevelocity"] = KCode.XrHmdDeviceVelocity,
			["deviceangularvelocity"] = KCode.XrHmdDeviceAngularVelocity,
			["lefteyevelocity"] = KCode.XrHmdLeftEyeVelocity,
			["lefteyeangularvelocity"] = KCode.XrHmdLeftEyeAngularVelocity,
			["righteyevelocity"] = KCode.XrHmdRightEyeVelocity,
			["righteyeangularvelocity"] = KCode.XrHmdRightEyeAngularVelocity,
		};
		public static Dictionary<string, KCode> hmd_ExpectedButtons = new Dictionary<string, KCode>() {
			["userpresence"] = KCode.XrHmdUserPresence
		};
		public static Dictionary<string, KCode> hmd_ExpectedIntegers = new Dictionary<string, KCode>();
		public static Dictionary<string, KCode> hmd_ExpectedQuaternion = new Dictionary<string, KCode>();
		public static Dictionary<string, KCode> hc_ExpectedVector3 = new Dictionary<string, KCode>() {
			["pointerPosition"] = KCode.XrControlPosition,
			["thumbstick"] = KCode.XrControlThumbStickStickChange
		};
		public static Dictionary<string, KCode> hc_ExpectedButtons = new Dictionary<string, KCode>() {
			["triggertouched"] = KCode.XrControlTriggerTouch,
			["trigger"] = KCode.XrControlTrigger,
			["grippressed"] = KCode.XrControlGripPressed,
			["grip"] = KCode.XrControlGrip,
			["primarytouched"] = KCode.XrControlPrimaryTouch,
			["primarybutton"] = KCode.XrControlPrimary,
			["secondarytouched"] = KCode.XrControlSecondaryTouch,
			["secondarybutton"] = KCode.XrControlSecondary,
			["thumbsticktouched"] = KCode.XrControlThumbStickStickTouched,
			["thumbstickclicked"] = KCode.XrControlThumbStickStickClicked,
			["triggerpressed"] = KCode.XrControlTriggerPressed,
			["devicepose/isTracked"] = KCode.XrControlDeviceTracked,
			["pointer/isTracked"] = KCode.XrControlPointerTracked,
		};
		public static Dictionary<string, KCode> hc_ExpectedIntegers = new Dictionary<string, KCode>() {
			["devicepose/trackingState"] = KCode.XrControlDeviceTrackedState,
			["pointer/trackingState"] = KCode.XrControlPointerTrackedState,
		};
		public static Dictionary<string, KCode> hc_ExpectedQuaternion = new Dictionary<string, KCode>() {
			["pointerRotation"] = KCode.XrControlRotation,
		};

		public class Mapping<V> {
			public KCode code;
			public UnityEvent<V> value;
        }

		[Serializable] public class Vector3Mapping : Mapping<Vector3> { }

		public List<Vector3Mapping> vector3Mapping = new List<Vector3Mapping>();

		public Vector3Mapping GetMapping(KCode kCode) { return vector3Mapping.Find(m=>m.code== kCode); }

		public void RefreshVector3Mapping() {
			for (int i = 0; i < Vector3Inputs_listing.Count; ++i) {
				InputMapping_Vector3 iv3 = Vector3Inputs_listing[i];
				iv3.notify = null;
			}
			for (int i = 0; i < vector3Mapping.Count; ++i) {
				Vector3Mapping m = vector3Mapping[i];
				InputMapping_Vector3 iv3 = Vector3Inputs_listing.Find(i3 => i3.kCode == m.code);
				if (iv3 != null) {
					iv3.notify += m.value.Invoke;
				}
			}
        }

		public InputSystemInterfaceLogic() { }

		private void RootEventHandler(InputEventPtr eventPtr, InputDevice device) {
			// only spend time on input events
			if (!eventPtr.IsA<StateEvent>() && !eventPtr.IsA<DeltaStateEvent>()) { return; }
			// fail if the input device is unknown to the input handler
			if (eventPtr.deviceId >= deviceInputHandler.Count || deviceInputHandler[eventPtr.deviceId] == null) {
                switch (device) {
				case UnityEngine.InputSystem.XR.XRControllerWithRumble xrController:
					CreateDeviceHanlderFor(device);
					// TODO insert code to listen to hand controller events
					break;
				case UnityEngine.InputSystem.XR.XRHMD xrHeadMountedDisplay:
					CreateDeviceHanlderFor(device);
					// TODO insert code to listen to HMD events
					break;
				default:
					string error = "unknown device id " + eventPtr.deviceId + ", what is " + device + " (" + device.GetType() + ")";
					errors.Add(error);
					return;
				}
			}
			Dictionary<object, DeviceControlHandler> deviceHandler = deviceInputHandler[eventPtr.deviceId];
			// go through all of the inputs
			foreach (InputControl control in eventPtr.EnumerateChangedControls(device)) {
				// execute the input handler for the device
				bool found = deviceHandler.TryGetValue(control, out DeviceControlHandler controlHandler); // find the handler for the control in this device's handlers
				if (!found) { found = deviceHandler.TryGetValue(control.GetType(), out controlHandler); } // specific control not found, how about it's type?
				if (!found) {
					switch (device) {
					case UnityEngine.InputSystem.XR.XRHMD xrHeadMountedDisplay:
						controlHandler = XrDeviceControlMapping(control, hmd_ExpectedVector3, hmd_ExpectedQuaternion, hmd_ExpectedButtons, hmd_ExpectedIntegers);
						if (controlHandler != null) { deviceHandler[control] = controlHandler; }
						break;
					case UnityEngine.InputSystem.XR.XRControllerWithRumble xrController:
						controlHandler = XrDeviceControlMapping(control, hc_ExpectedVector3, hc_ExpectedQuaternion, hc_ExpectedButtons, hc_ExpectedIntegers);
						if (controlHandler != null) { deviceHandler[control] = controlHandler; }
						break;
					}
					found = controlHandler != null;
				}
				if (!found) {
					string error = "not expecting {" + control + "} for " + device.deviceId + " {" + device + "} <" + device.GetType() + "> [" + control.GetType() + "]";
					errors.Add(error);
				}
				if (controlHandler == null) { controlHandler = HandleUnexpectedControl; }
				controlHandler.Invoke(control);
			}
		}
		public DeviceControlHandler GetButtonHandler(InputControl control, Dictionary<string, KCode> expectedButton) {
			return ButtonInputMapping(control, expectedButton.Keys, (control, controlName) => {
				// add the device to the listing if the device is unknown
				AxisControl ac = control as AxisControl;
				int id = ac.device.deviceId;
				if(!ButtonInputs.TryGetValue(id, out Dictionary<string, InputMapping_Float> buttonInputs)) {
					ButtonInputs[id] = buttonInputs = new Dictionary<string, InputMapping_Float>();
					inputBinding[id] = new Dictionary<KCode, InputMapping_base>();

					var allc = ac.device.allControls;
					foreach (var c in allc) {
						object v = c.ReadValueAsObject();
						Debug.LogWarning(c + " " + c.aliases.JoinToString() + " " + v.GetType() + " " + v);
					}
				}
				// add the control to the device's control listing
				if (!buttonInputs.TryGetValue(controlName, out InputMapping_Float inputBind)) {
					buttonInputs[controlName] = inputBind = new InputMapping_Float(expectedButton[controlName], controlName, id);
					ButtonInputs_listing.Add(inputBind);
					inputBinding[id][inputBind.kCode] = inputBind;
				}
				inputBind.refresh += () => inputBind.Set(control);
				return inputBind.Set;
			});
		}
		public DeviceControlHandler GetVector3InputHandler(InputControl control, Dictionary<string, KCode> expectedVector3) {
			return VectorInputMapping(control, expectedVector3.Keys, (control, controlName, lastLetter) => {
				AxisControl ac = control as AxisControl;
				int id = ac.device.deviceId;
				if (!Vector3Inputs.TryGetValue(id, out Dictionary<string, InputMapping_Vector3> vector3Inputs)) {
					Vector3Inputs[id] = vector3Inputs = new Dictionary<string, InputMapping_Vector3>();
					inputBinding[id] = new Dictionary<KCode, InputMapping_base>();
				}
				if (!vector3Inputs.TryGetValue(controlName, out InputMapping_Vector3 inputBind)) {
					vector3Inputs[controlName] = inputBind = new InputMapping_Vector3(expectedVector3[controlName], controlName, id);
					Vector3Inputs_listing.Add(inputBind);
					inputBinding[id][inputBind.kCode] = inputBind;
					Vector3Mapping v3m = GetMapping(inputBind.kCode);
					Debug.Log(inputBind.kCode+" ["+ vector3Mapping.JoinToString(",", vm=>vm.code.ToString())+"]");
					if (v3m != null) {
						Debug.Log("CONNECTING "+ inputBind.kCode);
						inputBind.notify += v3m.value.Invoke;
                    }
				}
				DeviceControlHandler h = null;
				switch (lastLetter) {
				case 'x': h = inputBind.SetX; break;
				case 'y': h = inputBind.SetY; break;
				case 'z': h = inputBind.SetZ; break;
				}
				if (h != null) { inputBind.refresh += () => h.Invoke(control); } else { Show.Warning("unknown input axis " + controlName + "/" + lastLetter); }
				return h;
			});
		}
		public DeviceControlHandler GetQuaternionInputHandler(InputControl control, Dictionary<string, KCode> expectedQuaternion) {
			return VectorInputMapping(control, expectedQuaternion.Keys, (control, controlName, lastLetter) => {
				AxisControl ac = control as AxisControl;
				int id = ac.device.deviceId;
				if (!QuaternionInputs.TryGetValue(id, out Dictionary<string, InputMapping_Quaternion> quaternionInputs)) {
					QuaternionInputs[id] = quaternionInputs = new Dictionary<string, InputMapping_Quaternion>();
					inputBinding[id] = new Dictionary<KCode, InputMapping_base>();
				}
				if (!quaternionInputs.TryGetValue(controlName, out InputMapping_Quaternion inputBind)) {
					quaternionInputs[controlName] = inputBind = new InputMapping_Quaternion(expectedQuaternion[controlName], controlName, id);
					QuaternionInputs_listing.Add(inputBind);
					inputBinding[id][inputBind.kCode] = inputBind;
				}
				DeviceControlHandler h = null;
				switch (lastLetter) {
				case 'w': h = inputBind.SetW; break;
				case 'x': h = inputBind.SetX; break;
				case 'y': h = inputBind.SetY; break;
				case 'z': h = inputBind.SetZ; break;
				}
				if (h != null) { inputBind.refresh += () => h.Invoke(control); } else { Show.Warning("unknown input axis " + controlName + "/" + lastLetter); }
				return h;
			});
		}
		public DeviceControlHandler GetIntegerStateHandler(InputControl control, Dictionary<string, KCode> expectedIntegers) {
			return ButtonInputMapping(control, expectedIntegers.Keys, (control, controlName) => {
				AxisControl ac = control as AxisControl;
				int id = ac.device.deviceId;
				if (!IntegerInputs.TryGetValue(id, out Dictionary<string, InputMapping_Int> integerInputs)) {
					IntegerInputs[id] = integerInputs = new Dictionary<string, InputMapping_Int>();
					inputBinding[id] = new Dictionary<KCode, InputMapping_base>();
				}
				if (!integerInputs.TryGetValue(controlName, out InputMapping_Int inputBind)) {
					integerInputs[controlName] = inputBind = new InputMapping_Int(expectedIntegers[controlName], controlName, id);
					IntegerInputs_listing.Add(inputBind);
					inputBinding[id][inputBind.kCode] = inputBind;
				}
				inputBind.refresh += () => inputBind.Set(control);
				return inputBind.Set;
			});
		}
		public DeviceControlHandler XrDeviceControlMapping(InputControl control, Dictionary<string, KCode> expectedVector3, Dictionary<string, KCode> expectedQuaternion, Dictionary<string,KCode> expectedButton, Dictionary<string, KCode> expectedIntegers) {
			DeviceControlHandler handler = GetButtonHandler(control, expectedButton);
			if (handler != null) return handler;
			handler = GetVector3InputHandler(control, expectedVector3);
			if (handler != null) return handler;
			handler = GetQuaternionInputHandler(control, expectedQuaternion);
			if (handler != null) return handler;
			handler = GetIntegerStateHandler(control, expectedIntegers);
			return handler;
		}
		public DeviceControlHandler ButtonInputMapping(InputControl control, IEnumerable<string> names, Func<object, string, DeviceControlHandler> inputMapByName) {
			if (names == null) return null;
			string bigName = control.ToString();
			string whichOne = StringExtension.EndsWith(bigName, names);
			//StringExtension.IndexOfFirstCheckingBackward(bigName, names, out int whichOne, 0, bigName.Length);
			if (whichOne == null) { return null; }
			//Debug.Log("I've been expecting a button like you, " + bigName);
			return inputMapByName(control, whichOne);
		}
		public DeviceControlHandler VectorInputMapping(InputControl control, IEnumerable<string> names, Func<object, string, char, DeviceControlHandler> inputMapByChar) {
			if (names == null) return null;
			string bigName = control.ToString();
			StringExtension.IndexOfFirstCheckingBackward(bigName, names, out string whichOne, 0, bigName.Length - 2);
			if (whichOne == null) { return null; }
			//Debug.Log("I've been expecting an axis like you, " + bigName);
			char lastLetter = bigName[bigName.Length - 1];
			return inputMapByChar(control, whichOne, lastLetter);
		}
		public enum InputType { None, Float, Vector3, Quaternion }
		public abstract class InputMapping_base {
			public static HashSet<InputMapping_base> needsUpdate = new HashSet<InputMapping_base>();
			public static HashSet<InputMapping_base> needsUpdateBuffer = new HashSet<InputMapping_base>();
			public static bool updating = true;
			public static long updatesCalculated;

			public string name;
			public KCode kCode;
			public Action refresh;
			public long lastUpdate;
			public int deviceId;
			public abstract void Notify();
			public InputMapping_base(KCode kCode, string name, int deviceId) { this.kCode = kCode; this.name = name; this.deviceId = deviceId; }
			public void MarkUpdated() { needsUpdate.Add(this); lastUpdate = updatesCalculated; }
			public static void RefreshAll() {
				updating = false;
				const int updatesToWaitBeforeDoingAnotherStateCalculation = 1;
				long lastDataCheckMoment = updatesCalculated - updatesToWaitBeforeDoingAnotherStateCalculation;
				needsUpdateBuffer.Clear();
				foreach (InputMapping_base im in needsUpdate) {
					if (im.lastUpdate == updatesCalculated) { im.Notify(); }
					if (lastDataCheckMoment > im.lastUpdate) {
						im.refresh?.Invoke();
						im.Notify();
					} else {
						needsUpdateBuffer.Add(im);
					}
				}
				needsUpdate.Clear();
				needsUpdate.UnionWith(needsUpdateBuffer);
				updating = true;
				++updatesCalculated;
			}
		}
		public abstract class InputMapping<T> : InputMapping_base {
			public InputMapping(KCode kCode, string name, int deviceId) : base(kCode, name, deviceId) { }
			public T value;
			public Action<T> notify;
			public override void Notify() {
				//if (notify != null) { Debug.Log("notifying "+notify.Method.Name+" "+input); }
				notify?.Invoke(value);
			}
			protected TYPE ReadValue<TYPE>(object c) where TYPE : struct {
				if (updating) MarkUpdated();
				if (c is InputControl<TYPE> ac && ac.device.added) { return ac.ReadValue(); }
				return default(TYPE);
			}
		}
		[Serializable] public class InputMapping_Vector3 : InputMapping<Vector3> {
            public InputMapping_Vector3(KCode kCode, string name, int deviceId) : base(kCode, name, deviceId) { }
            public void SetX(object c) { value.x = ReadValue<float>(c); }
			public void SetY(object c) { value.y = ReadValue<float>(c); }
			public void SetZ(object c) { value.z = ReadValue<float>(c); }
		}
		[Serializable] public class InputMapping_Quaternion : InputMapping<Quaternion> {
            public InputMapping_Quaternion(KCode kCode, string name, int deviceId) : base(kCode, name, deviceId) { }
            public void SetX(object c) { value.x = ReadValue<float>(c); }
			public void SetY(object c) { value.y = ReadValue<float>(c); }
			public void SetZ(object c) { value.z = ReadValue<float>(c); }
			public void SetW(object c) { value.w = ReadValue<float>(c); }
		}
		[Serializable] public class InputMapping_Float : InputMapping<float> {
            public InputMapping_Float(KCode kCode, string name, int deviceId) : base(kCode, name, deviceId) { }
            public void Set(object c) { value = ReadValue<float>(c); }
		}
		[Serializable] public class InputMapping_Int : InputMapping<int> {
            public InputMapping_Int(KCode kCode, string name, int deviceId) : base(kCode, name, deviceId) { }
            public void Set(object c) { value = ReadValue<int>(c); }
		}

		public void CreateDeviceHanlderFor(InputDevice device) {
			Show.Log("creating device handler for " + device.deviceId + " " + device + " ["+ device.GetType()+ "]");
			EnsureInputDeviceHandlerCount(device.deviceId+1);
			deviceInputHandler[device.deviceId] = new Dictionary<object, DeviceControlHandler>();
		}
		public void Update() {
			foreach (KeyValuePair<KCode, object> kvp in activeControl) {
				if (OnPressing.TryGetValue(kvp.Key, out SpecificControlHandler handler)) {
					handler.Invoke(kvp.Key, kvp.Value);
				}
				OnPressingAny?.Invoke(kvp.Key, kvp.Value);
			}
			if (errors.Count > 0) {
				Show.Warning(errors.JoinToString("\n"));
				errors.Clear();
            }
			InputMapping_base.RefreshAll();
		}

		/// <summary>
		/// an abstraction layer to catch inconsistencies with presses and releases. ignores bad presses and bad releases
		/// </summary>
		/// <param name="control"></param>
		/// <param name="kCode"></param>
		/// <returns>true if there are no errors</returns>
		public bool PressAndReleaseUpdate(ButtonControl control, KCode kCode) {
			if (AppInput.IsQuitting) return true;
			bool isRelease = control.IsPressed();
			int k = (int)kCode;
			if (isRelease) {
				if (!activeControl.ContainsKey(kCode)) { Debug.Log("double release " + control + "? " + kCode); } else {
					//Debug.Log("good release");
					if (OnRelease.TryGetValue(kCode, out SpecificControlHandler handler)) { handler.Invoke(kCode, control); }
					activeControl.Remove(kCode);
					OnReleaseAny?.Invoke(kCode, control);
				}
			} else {
				if (activeControl.ContainsKey(kCode)) { Debug.Log("double press " + control + "? " + kCode); } else {
					//Debug.Log("good press");
					if (OnPressed.TryGetValue(kCode, out SpecificControlHandler handler)) { handler.Invoke(kCode, control); }
					activeControl[kCode] = control;
					OnPressedAny?.Invoke(kCode, control);
				}
			}
			return true;
		}
		public bool AxisUpdate(AxisControl control, KCode kCode) {
			if (AppInput.IsQuitting) return true;
			bool isRelease = control.IsPressed();
			int k = (int)kCode;
			if (isRelease) {
				if (OnRelease.TryGetValue(kCode, out SpecificControlHandler handler)) { handler.Invoke(kCode, control); }
				activeControl.Remove(kCode);
			} else {
				if (OnPressed.TryGetValue(kCode, out SpecificControlHandler handler)) { handler.Invoke(kCode, control); }
				activeControl[kCode] = control;
			}
			return true;
		}

		private void HandleKeyControl(object keyControl) {
			KeyControl kc = (KeyControl)keyControl;
			//Debug.Log(kc.keyCode);
			PressAndReleaseUpdate(kc, KCodeExtensionUnity.GetInputCode(kc));
		}
		private void HandleMouseButtonControl(object mouseButtonControl) {
			ButtonControl bc = (ButtonControl)mouseButtonControl;
			//Debug.Log(bc.shortDisplayName);
			PressAndReleaseUpdate(bc, KCodeExtensionUnity.GetInputCode(bc));
		}
		private void HandleMouseAxisControl(object mouseAxisControl) {
			AxisControl ac = (AxisControl)mouseAxisControl;
			//Debug.Log(ac.shortDisplayName);
			AxisUpdate(ac, KCodeExtensionUnity.GetInputCode(ac));
		}
		private void HandleUnexpectedControl(object control) {
			//Debug.Log(control+" ("+control.GetType()+")");
		}

		private List<int> keyboardDeviceId = new List<int>(), mouseDeviceId = new List<int>(), touchScreenDeviceId = new List<int>();
		private bool _inputSystemInitialized = false;
		public void Start() {
			if (_inputSystemInitialized) return;
			_inputSystemInitialized = true;
			int max = 0;
			for (int i = 0; i < InputSystem.devices.Count; ++i) {
				Debug.Log(InputSystem.devices[i].deviceId + " " + InputSystem.devices[i].GetType() +" " +InputSystem.devices[i].shortDisplayName+" "+InputSystem.devices[i].Stringify());
                switch (InputSystem.devices[i]) {
				case Keyboard k: keyboardDeviceId.Add(k.deviceId); break;
				case Mouse m: mouseDeviceId.Add(m.deviceId); break;
				case Touchscreen ts: touchScreenDeviceId.Add(ts.deviceId); break;
				default:
					errors.Add($"misunderstood device {InputSystem.devices[i].deviceId}: " + InputSystem.devices[i]);
					break;
				}
				max = Math.Max(max, InputSystem.devices[i].deviceId);
			}
			EnsureInputDeviceHandlerCount(max + 1);
			keyboardDeviceId.ForEach(keyboardId => {
				deviceInputHandler[keyboardId] = new Dictionary<object, DeviceControlHandler>() {
					[typeof(KeyControl)] = HandleKeyControl
				};
			});
			// TODO map specific mouse controls (there's not that many of them) // Mouse mouse = Mouse.current;
			mouseDeviceId.ForEach(mouseDeviceId => {
				deviceInputHandler[mouseDeviceId] = new Dictionary<object, DeviceControlHandler>() {
					[typeof(ButtonControl)] = HandleMouseButtonControl,
					[typeof(AxisControl)] = HandleMouseAxisControl,
				};
			});
			InputSystem.onEvent += RootEventHandler;
		}
		public void EnsureInputDeviceHandlerCount(int count) {
			if (deviceInputHandler.Count >= count) return;
			deviceInputHandler.Capacity = count;
			while (deviceInputHandler.Count < count) { deviceInputHandler.Add(null); }
		}
		public void Release() {
			_inputSystemInitialized = false;
			deviceInputHandler.Clear();
			InputSystem.onEvent -= RootEventHandler;
		}
	}
#endif
}