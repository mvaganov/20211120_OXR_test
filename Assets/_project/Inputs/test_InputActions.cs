//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.1.1
//     from Assets/_project/Inputs/test_InputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @Test_InputActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Test_InputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""test_InputActions"",
    ""maps"": [
        {
            ""name"": ""Agent"",
            ""id"": ""56011619-3931-4cfd-8c16-6b9a8b98e3ac"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Value"",
                    ""id"": ""f0e6553a-ce72-4490-880f-63fc0d750a5a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""b72956d1-c28d-4f0b-be99-74b29d184bd6"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""RunSpeed"",
                    ""type"": ""Value"",
                    ""id"": ""6307fd91-9b40-4318-99e2-5dc9f9f3d63e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3bb3e36b-5a88-4565-9103-5bd962c1660f"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5fe38693-d7bb-4ef1-9a31-6785b7b4166c"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""90a5912a-4485-40f4-b03f-06418413e090"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""09243427-b04b-4af6-a4b8-571d691deddc"",
                    ""path"": ""<XRController>{RightHand}/joystickClicked"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VR"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5578d0a3-d61d-4eb1-8b6a-a012701b9821"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RunSpeed"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a9519881-bcf7-42ff-b762-10a8f0c0c87f"",
                    ""path"": ""<XRController>{RightHand}/gripPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VR"",
                    ""action"": ""RunSpeed"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""a047c53c-5ddf-45ff-85ec-b74cc872f249"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""ed2366e9-f802-428a-b49a-504d5525438d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""5435b35a-aee4-40cd-adaf-0ac07301ba08"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""4039d28f-c131-4cc8-bec3-7ebe41f63247"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""fa8ab49d-a5d7-4677-85c1-636cde1e937c"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""0e949131-c2df-414d-b900-4114cdf26e53"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""01e31564-4a0d-40f6-81e1-008a717d33d7"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""1ba977c3-1a50-4ee1-aa58-1559bf34f5d4"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""4109db0a-3b3e-452e-8ccc-d075efb7aeb1"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""d9608fa3-d11f-4588-baf7-7bb2d56538bb"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e120c9b1-50b5-4e0f-a95f-795c14651560"",
                    ""path"": ""<XRController>{RightHand}/joystick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VR"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""VR"",
            ""id"": ""469af097-9fbe-4bd7-9ae9-08bbba17d6b9"",
            ""actions"": [
                {
                    ""name"": ""LeftRotation"",
                    ""type"": ""Value"",
                    ""id"": ""f4d98b16-a56a-4c04-bfb8-eb6af2d36dd7"",
                    ""expectedControlType"": ""Quaternion"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""RightRotation"",
                    ""type"": ""Value"",
                    ""id"": ""0d919947-79bf-4e7b-a986-482d018797f7"",
                    ""expectedControlType"": ""Quaternion"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""LeftPosition"",
                    ""type"": ""Value"",
                    ""id"": ""6554c901-f4e1-4f8a-a154-4562c2327fcf"",
                    ""expectedControlType"": ""Vector3"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""RightPosition"",
                    ""type"": ""Value"",
                    ""id"": ""ea17b1df-178b-4887-bba1-c00b8deabfc8"",
                    ""expectedControlType"": ""Vector3"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""37345ca1-95ce-47e9-b3f8-56a8978233c4"",
                    ""path"": ""<XRController>{LeftHand}/pointerRotation"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VR"",
                    ""action"": ""LeftRotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6726d80d-2fa7-453b-bd4c-7286b5fa3505"",
                    ""path"": ""<XRController>{LeftHand}/devicePosition"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VR"",
                    ""action"": ""LeftPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f37bba5e-8908-4d38-9628-75699e5068c1"",
                    ""path"": ""<XRController>{RightHand}/devicePosition"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VR"",
                    ""action"": ""RightPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4b79501a-a928-44dc-a770-3e02b0644efb"",
                    ""path"": ""<XRController>{RightHand}/pointerRotation"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VR"",
                    ""action"": ""RightRotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Grab"",
            ""id"": ""529762fa-290d-4daf-9680-78daf8a5f5d7"",
            ""actions"": [
                {
                    ""name"": ""Grab"",
                    ""type"": ""Button"",
                    ""id"": ""bfc97301-1c0a-4380-a379-17274a8d904b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": ""Invert"",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""985a1e65-632a-47a4-891d-588765792dd7"",
                    ""path"": ""<XRController>{RightHand}/gripPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VR"",
                    ""action"": ""Grab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""RightStickTurret"",
            ""id"": ""2e5c149f-5aad-40a3-bee6-5c0a59b88aea"",
            ""actions"": [
                {
                    ""name"": ""TurretTurn"",
                    ""type"": ""Value"",
                    ""id"": ""4d9451d8-52b8-46d6-8281-e8548c9db3fd"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Forward"",
                    ""type"": ""Value"",
                    ""id"": ""36c97367-047d-4618-b2c3-413b2948ebd9"",
                    ""expectedControlType"": ""Analog"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Backward"",
                    ""type"": ""Button"",
                    ""id"": ""c49859de-12ee-450d-8baa-b985f8f766d5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": ""Invert"",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6214ba6e-916c-424f-a2b1-d41f8b27c389"",
                    ""path"": ""<XRController>{RightHand}/joystick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VR"",
                    ""action"": ""TurretTurn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""747cd9b6-c76e-4d8b-b86f-db0205dd2a33"",
                    ""path"": ""<OculusTouchController>/trigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VR"",
                    ""action"": ""Forward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""89d9eaa2-a19b-43f9-9e55-bcce8ff14ee7"",
                    ""path"": ""<XRController>{RightHand}/thumbstickClicked"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""VR"",
                    ""action"": ""Backward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""RodDraw"",
            ""id"": ""818b4bc9-d5fa-444d-a82a-94f0721ac830"",
            ""actions"": [
                {
                    ""name"": ""Draw"",
                    ""type"": ""Value"",
                    ""id"": ""5080754d-9d0e-48f9-a17d-d20f53ded014"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""258ecc56-cb42-408c-81f7-73188fe145af"",
                    ""path"": ""<XRController>{RightHand}/primaryButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Draw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Next"",
            ""id"": ""ee78c8b3-dedc-46fb-aa20-a25da0d0dc5c"",
            ""actions"": [
                {
                    ""name"": ""NextMapButton"",
                    ""type"": ""Button"",
                    ""id"": ""e52a8786-df0d-4814-a497-d4b70a9212c0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""773e5030-86e4-436f-bbbb-117b0cfe59fe"",
                    ""path"": ""<XRController>{RightHand}/secondaryButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NextMapButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""VR"",
            ""bindingGroup"": ""VR"",
            ""devices"": [
                {
                    ""devicePath"": ""<XRController>{LeftHand}"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<XRController>{RightHand}"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<XRHMD>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Desktop"",
            ""bindingGroup"": ""Desktop"",
            ""devices"": []
        }
    ]
}");
        // Agent
        m_Agent = asset.FindActionMap("Agent", throwIfNotFound: true);
        m_Agent_Jump = m_Agent.FindAction("Jump", throwIfNotFound: true);
        m_Agent_Movement = m_Agent.FindAction("Movement", throwIfNotFound: true);
        m_Agent_RunSpeed = m_Agent.FindAction("RunSpeed", throwIfNotFound: true);
        // VR
        m_VR = asset.FindActionMap("VR", throwIfNotFound: true);
        m_VR_LeftRotation = m_VR.FindAction("LeftRotation", throwIfNotFound: true);
        m_VR_RightRotation = m_VR.FindAction("RightRotation", throwIfNotFound: true);
        m_VR_LeftPosition = m_VR.FindAction("LeftPosition", throwIfNotFound: true);
        m_VR_RightPosition = m_VR.FindAction("RightPosition", throwIfNotFound: true);
        // Grab
        m_Grab = asset.FindActionMap("Grab", throwIfNotFound: true);
        m_Grab_Grab = m_Grab.FindAction("Grab", throwIfNotFound: true);
        // RightStickTurret
        m_RightStickTurret = asset.FindActionMap("RightStickTurret", throwIfNotFound: true);
        m_RightStickTurret_TurretTurn = m_RightStickTurret.FindAction("TurretTurn", throwIfNotFound: true);
        m_RightStickTurret_Forward = m_RightStickTurret.FindAction("Forward", throwIfNotFound: true);
        m_RightStickTurret_Backward = m_RightStickTurret.FindAction("Backward", throwIfNotFound: true);
        // RodDraw
        m_RodDraw = asset.FindActionMap("RodDraw", throwIfNotFound: true);
        m_RodDraw_Draw = m_RodDraw.FindAction("Draw", throwIfNotFound: true);
        // Next
        m_Next = asset.FindActionMap("Next", throwIfNotFound: true);
        m_Next_NextMapButton = m_Next.FindAction("NextMapButton", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Agent
    private readonly InputActionMap m_Agent;
    private IAgentActions m_AgentActionsCallbackInterface;
    private readonly InputAction m_Agent_Jump;
    private readonly InputAction m_Agent_Movement;
    private readonly InputAction m_Agent_RunSpeed;
    public struct AgentActions
    {
        private @Test_InputActions m_Wrapper;
        public AgentActions(@Test_InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_Agent_Jump;
        public InputAction @Movement => m_Wrapper.m_Agent_Movement;
        public InputAction @RunSpeed => m_Wrapper.m_Agent_RunSpeed;
        public InputActionMap Get() { return m_Wrapper.m_Agent; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(AgentActions set) { return set.Get(); }
        public void SetCallbacks(IAgentActions instance)
        {
            if (m_Wrapper.m_AgentActionsCallbackInterface != null)
            {
                @Jump.started -= m_Wrapper.m_AgentActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_AgentActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_AgentActionsCallbackInterface.OnJump;
                @Movement.started -= m_Wrapper.m_AgentActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_AgentActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_AgentActionsCallbackInterface.OnMovement;
                @RunSpeed.started -= m_Wrapper.m_AgentActionsCallbackInterface.OnRunSpeed;
                @RunSpeed.performed -= m_Wrapper.m_AgentActionsCallbackInterface.OnRunSpeed;
                @RunSpeed.canceled -= m_Wrapper.m_AgentActionsCallbackInterface.OnRunSpeed;
            }
            m_Wrapper.m_AgentActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @RunSpeed.started += instance.OnRunSpeed;
                @RunSpeed.performed += instance.OnRunSpeed;
                @RunSpeed.canceled += instance.OnRunSpeed;
            }
        }
    }
    public AgentActions @Agent => new AgentActions(this);

    // VR
    private readonly InputActionMap m_VR;
    private IVRActions m_VRActionsCallbackInterface;
    private readonly InputAction m_VR_LeftRotation;
    private readonly InputAction m_VR_RightRotation;
    private readonly InputAction m_VR_LeftPosition;
    private readonly InputAction m_VR_RightPosition;
    public struct VRActions
    {
        private @Test_InputActions m_Wrapper;
        public VRActions(@Test_InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @LeftRotation => m_Wrapper.m_VR_LeftRotation;
        public InputAction @RightRotation => m_Wrapper.m_VR_RightRotation;
        public InputAction @LeftPosition => m_Wrapper.m_VR_LeftPosition;
        public InputAction @RightPosition => m_Wrapper.m_VR_RightPosition;
        public InputActionMap Get() { return m_Wrapper.m_VR; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(VRActions set) { return set.Get(); }
        public void SetCallbacks(IVRActions instance)
        {
            if (m_Wrapper.m_VRActionsCallbackInterface != null)
            {
                @LeftRotation.started -= m_Wrapper.m_VRActionsCallbackInterface.OnLeftRotation;
                @LeftRotation.performed -= m_Wrapper.m_VRActionsCallbackInterface.OnLeftRotation;
                @LeftRotation.canceled -= m_Wrapper.m_VRActionsCallbackInterface.OnLeftRotation;
                @RightRotation.started -= m_Wrapper.m_VRActionsCallbackInterface.OnRightRotation;
                @RightRotation.performed -= m_Wrapper.m_VRActionsCallbackInterface.OnRightRotation;
                @RightRotation.canceled -= m_Wrapper.m_VRActionsCallbackInterface.OnRightRotation;
                @LeftPosition.started -= m_Wrapper.m_VRActionsCallbackInterface.OnLeftPosition;
                @LeftPosition.performed -= m_Wrapper.m_VRActionsCallbackInterface.OnLeftPosition;
                @LeftPosition.canceled -= m_Wrapper.m_VRActionsCallbackInterface.OnLeftPosition;
                @RightPosition.started -= m_Wrapper.m_VRActionsCallbackInterface.OnRightPosition;
                @RightPosition.performed -= m_Wrapper.m_VRActionsCallbackInterface.OnRightPosition;
                @RightPosition.canceled -= m_Wrapper.m_VRActionsCallbackInterface.OnRightPosition;
            }
            m_Wrapper.m_VRActionsCallbackInterface = instance;
            if (instance != null)
            {
                @LeftRotation.started += instance.OnLeftRotation;
                @LeftRotation.performed += instance.OnLeftRotation;
                @LeftRotation.canceled += instance.OnLeftRotation;
                @RightRotation.started += instance.OnRightRotation;
                @RightRotation.performed += instance.OnRightRotation;
                @RightRotation.canceled += instance.OnRightRotation;
                @LeftPosition.started += instance.OnLeftPosition;
                @LeftPosition.performed += instance.OnLeftPosition;
                @LeftPosition.canceled += instance.OnLeftPosition;
                @RightPosition.started += instance.OnRightPosition;
                @RightPosition.performed += instance.OnRightPosition;
                @RightPosition.canceled += instance.OnRightPosition;
            }
        }
    }
    public VRActions @VR => new VRActions(this);

    // Grab
    private readonly InputActionMap m_Grab;
    private IGrabActions m_GrabActionsCallbackInterface;
    private readonly InputAction m_Grab_Grab;
    public struct GrabActions
    {
        private @Test_InputActions m_Wrapper;
        public GrabActions(@Test_InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Grab => m_Wrapper.m_Grab_Grab;
        public InputActionMap Get() { return m_Wrapper.m_Grab; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GrabActions set) { return set.Get(); }
        public void SetCallbacks(IGrabActions instance)
        {
            if (m_Wrapper.m_GrabActionsCallbackInterface != null)
            {
                @Grab.started -= m_Wrapper.m_GrabActionsCallbackInterface.OnGrab;
                @Grab.performed -= m_Wrapper.m_GrabActionsCallbackInterface.OnGrab;
                @Grab.canceled -= m_Wrapper.m_GrabActionsCallbackInterface.OnGrab;
            }
            m_Wrapper.m_GrabActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Grab.started += instance.OnGrab;
                @Grab.performed += instance.OnGrab;
                @Grab.canceled += instance.OnGrab;
            }
        }
    }
    public GrabActions @Grab => new GrabActions(this);

    // RightStickTurret
    private readonly InputActionMap m_RightStickTurret;
    private IRightStickTurretActions m_RightStickTurretActionsCallbackInterface;
    private readonly InputAction m_RightStickTurret_TurretTurn;
    private readonly InputAction m_RightStickTurret_Forward;
    private readonly InputAction m_RightStickTurret_Backward;
    public struct RightStickTurretActions
    {
        private @Test_InputActions m_Wrapper;
        public RightStickTurretActions(@Test_InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @TurretTurn => m_Wrapper.m_RightStickTurret_TurretTurn;
        public InputAction @Forward => m_Wrapper.m_RightStickTurret_Forward;
        public InputAction @Backward => m_Wrapper.m_RightStickTurret_Backward;
        public InputActionMap Get() { return m_Wrapper.m_RightStickTurret; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(RightStickTurretActions set) { return set.Get(); }
        public void SetCallbacks(IRightStickTurretActions instance)
        {
            if (m_Wrapper.m_RightStickTurretActionsCallbackInterface != null)
            {
                @TurretTurn.started -= m_Wrapper.m_RightStickTurretActionsCallbackInterface.OnTurretTurn;
                @TurretTurn.performed -= m_Wrapper.m_RightStickTurretActionsCallbackInterface.OnTurretTurn;
                @TurretTurn.canceled -= m_Wrapper.m_RightStickTurretActionsCallbackInterface.OnTurretTurn;
                @Forward.started -= m_Wrapper.m_RightStickTurretActionsCallbackInterface.OnForward;
                @Forward.performed -= m_Wrapper.m_RightStickTurretActionsCallbackInterface.OnForward;
                @Forward.canceled -= m_Wrapper.m_RightStickTurretActionsCallbackInterface.OnForward;
                @Backward.started -= m_Wrapper.m_RightStickTurretActionsCallbackInterface.OnBackward;
                @Backward.performed -= m_Wrapper.m_RightStickTurretActionsCallbackInterface.OnBackward;
                @Backward.canceled -= m_Wrapper.m_RightStickTurretActionsCallbackInterface.OnBackward;
            }
            m_Wrapper.m_RightStickTurretActionsCallbackInterface = instance;
            if (instance != null)
            {
                @TurretTurn.started += instance.OnTurretTurn;
                @TurretTurn.performed += instance.OnTurretTurn;
                @TurretTurn.canceled += instance.OnTurretTurn;
                @Forward.started += instance.OnForward;
                @Forward.performed += instance.OnForward;
                @Forward.canceled += instance.OnForward;
                @Backward.started += instance.OnBackward;
                @Backward.performed += instance.OnBackward;
                @Backward.canceled += instance.OnBackward;
            }
        }
    }
    public RightStickTurretActions @RightStickTurret => new RightStickTurretActions(this);

    // RodDraw
    private readonly InputActionMap m_RodDraw;
    private IRodDrawActions m_RodDrawActionsCallbackInterface;
    private readonly InputAction m_RodDraw_Draw;
    public struct RodDrawActions
    {
        private @Test_InputActions m_Wrapper;
        public RodDrawActions(@Test_InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Draw => m_Wrapper.m_RodDraw_Draw;
        public InputActionMap Get() { return m_Wrapper.m_RodDraw; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(RodDrawActions set) { return set.Get(); }
        public void SetCallbacks(IRodDrawActions instance)
        {
            if (m_Wrapper.m_RodDrawActionsCallbackInterface != null)
            {
                @Draw.started -= m_Wrapper.m_RodDrawActionsCallbackInterface.OnDraw;
                @Draw.performed -= m_Wrapper.m_RodDrawActionsCallbackInterface.OnDraw;
                @Draw.canceled -= m_Wrapper.m_RodDrawActionsCallbackInterface.OnDraw;
            }
            m_Wrapper.m_RodDrawActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Draw.started += instance.OnDraw;
                @Draw.performed += instance.OnDraw;
                @Draw.canceled += instance.OnDraw;
            }
        }
    }
    public RodDrawActions @RodDraw => new RodDrawActions(this);

    // Next
    private readonly InputActionMap m_Next;
    private INextActions m_NextActionsCallbackInterface;
    private readonly InputAction m_Next_NextMapButton;
    public struct NextActions
    {
        private @Test_InputActions m_Wrapper;
        public NextActions(@Test_InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @NextMapButton => m_Wrapper.m_Next_NextMapButton;
        public InputActionMap Get() { return m_Wrapper.m_Next; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(NextActions set) { return set.Get(); }
        public void SetCallbacks(INextActions instance)
        {
            if (m_Wrapper.m_NextActionsCallbackInterface != null)
            {
                @NextMapButton.started -= m_Wrapper.m_NextActionsCallbackInterface.OnNextMapButton;
                @NextMapButton.performed -= m_Wrapper.m_NextActionsCallbackInterface.OnNextMapButton;
                @NextMapButton.canceled -= m_Wrapper.m_NextActionsCallbackInterface.OnNextMapButton;
            }
            m_Wrapper.m_NextActionsCallbackInterface = instance;
            if (instance != null)
            {
                @NextMapButton.started += instance.OnNextMapButton;
                @NextMapButton.performed += instance.OnNextMapButton;
                @NextMapButton.canceled += instance.OnNextMapButton;
            }
        }
    }
    public NextActions @Next => new NextActions(this);
    private int m_VRSchemeIndex = -1;
    public InputControlScheme VRScheme
    {
        get
        {
            if (m_VRSchemeIndex == -1) m_VRSchemeIndex = asset.FindControlSchemeIndex("VR");
            return asset.controlSchemes[m_VRSchemeIndex];
        }
    }
    private int m_DesktopSchemeIndex = -1;
    public InputControlScheme DesktopScheme
    {
        get
        {
            if (m_DesktopSchemeIndex == -1) m_DesktopSchemeIndex = asset.FindControlSchemeIndex("Desktop");
            return asset.controlSchemes[m_DesktopSchemeIndex];
        }
    }
    public interface IAgentActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnRunSpeed(InputAction.CallbackContext context);
    }
    public interface IVRActions
    {
        void OnLeftRotation(InputAction.CallbackContext context);
        void OnRightRotation(InputAction.CallbackContext context);
        void OnLeftPosition(InputAction.CallbackContext context);
        void OnRightPosition(InputAction.CallbackContext context);
    }
    public interface IGrabActions
    {
        void OnGrab(InputAction.CallbackContext context);
    }
    public interface IRightStickTurretActions
    {
        void OnTurretTurn(InputAction.CallbackContext context);
        void OnForward(InputAction.CallbackContext context);
        void OnBackward(InputAction.CallbackContext context);
    }
    public interface IRodDrawActions
    {
        void OnDraw(InputAction.CallbackContext context);
    }
    public interface INextActions
    {
        void OnNextMapButton(InputAction.CallbackContext context);
    }
}
