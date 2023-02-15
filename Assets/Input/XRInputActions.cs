//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Input/XRInputActions.inputactions
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

public partial class @XRInputActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @XRInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""XRInputActions"",
    ""maps"": [
        {
            ""name"": ""XRUser"",
            ""id"": ""297096dd-adef-4e70-9a53-07ffbbf2f24f"",
            ""actions"": [
                {
                    ""name"": ""LControllerPosition"",
                    ""type"": ""Value"",
                    ""id"": ""561255c4-520c-4bb1-9a85-603acdbcd8b7"",
                    ""expectedControlType"": ""Vector3"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""LControllerRotation"",
                    ""type"": ""Value"",
                    ""id"": ""120b69ba-7450-4c2c-b613-e9a71a720741"",
                    ""expectedControlType"": ""Quaternion"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""RControllerPosition"",
                    ""type"": ""Value"",
                    ""id"": ""51b9b8d4-befa-4d6d-acc0-bb13102a0f65"",
                    ""expectedControlType"": ""Vector3"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""RControllerRotation"",
                    ""type"": ""Value"",
                    ""id"": ""892bc023-02a1-41b8-8f3d-88fb7eaf3a6b"",
                    ""expectedControlType"": ""Quaternion"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""LControllerIsTracking"",
                    ""type"": ""Value"",
                    ""id"": ""f92f0d2c-27ce-43b8-bec2-452bf802b0be"",
                    ""expectedControlType"": ""Integer"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""RControllerIsTracking"",
                    ""type"": ""Value"",
                    ""id"": ""a7373e01-bbb3-4798-90bb-51a2452d8224"",
                    ""expectedControlType"": ""Integer"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f287a2e3-5bb1-4924-8ab3-55018952f3fd"",
                    ""path"": ""<XRController>{LeftHand}/devicePosition"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XRPlayer"",
                    ""action"": ""LControllerPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""68c01e23-7f82-466f-9057-90b439304210"",
                    ""path"": ""<XRController>{LeftHand}/deviceRotation"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XRPlayer"",
                    ""action"": ""LControllerRotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eb24ad43-ab94-4c52-b683-a27077dd7595"",
                    ""path"": ""<XRController>{RightHand}/deviceRotation"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XRPlayer"",
                    ""action"": ""RControllerRotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b000c4f9-46d3-49d1-864e-9151ffefe4d5"",
                    ""path"": ""<XRController>{RightHand}/devicePosition"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XRPlayer"",
                    ""action"": ""RControllerPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b23cb3f7-ba5d-4820-8a05-f7aba0af1785"",
                    ""path"": ""<XRController>{LeftHand}/trackingState"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XRPlayer"",
                    ""action"": ""LControllerIsTracking"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dabb5624-b0c4-417f-973c-c4e23b0eac7e"",
                    ""path"": ""<XRController>{RightHand}/trackingState"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XRPlayer"",
                    ""action"": ""RControllerIsTracking"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""XRPlayer"",
            ""bindingGroup"": ""XRPlayer"",
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
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<EyeGaze>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // XRUser
        m_XRUser = asset.FindActionMap("XRUser", throwIfNotFound: true);
        m_XRUser_LControllerPosition = m_XRUser.FindAction("LControllerPosition", throwIfNotFound: true);
        m_XRUser_LControllerRotation = m_XRUser.FindAction("LControllerRotation", throwIfNotFound: true);
        m_XRUser_RControllerPosition = m_XRUser.FindAction("RControllerPosition", throwIfNotFound: true);
        m_XRUser_RControllerRotation = m_XRUser.FindAction("RControllerRotation", throwIfNotFound: true);
        m_XRUser_LControllerIsTracking = m_XRUser.FindAction("LControllerIsTracking", throwIfNotFound: true);
        m_XRUser_RControllerIsTracking = m_XRUser.FindAction("RControllerIsTracking", throwIfNotFound: true);
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

    // XRUser
    private readonly InputActionMap m_XRUser;
    private IXRUserActions m_XRUserActionsCallbackInterface;
    private readonly InputAction m_XRUser_LControllerPosition;
    private readonly InputAction m_XRUser_LControllerRotation;
    private readonly InputAction m_XRUser_RControllerPosition;
    private readonly InputAction m_XRUser_RControllerRotation;
    private readonly InputAction m_XRUser_LControllerIsTracking;
    private readonly InputAction m_XRUser_RControllerIsTracking;
    public struct XRUserActions
    {
        private @XRInputActions m_Wrapper;
        public XRUserActions(@XRInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @LControllerPosition => m_Wrapper.m_XRUser_LControllerPosition;
        public InputAction @LControllerRotation => m_Wrapper.m_XRUser_LControllerRotation;
        public InputAction @RControllerPosition => m_Wrapper.m_XRUser_RControllerPosition;
        public InputAction @RControllerRotation => m_Wrapper.m_XRUser_RControllerRotation;
        public InputAction @LControllerIsTracking => m_Wrapper.m_XRUser_LControllerIsTracking;
        public InputAction @RControllerIsTracking => m_Wrapper.m_XRUser_RControllerIsTracking;
        public InputActionMap Get() { return m_Wrapper.m_XRUser; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(XRUserActions set) { return set.Get(); }
        public void SetCallbacks(IXRUserActions instance)
        {
            if (m_Wrapper.m_XRUserActionsCallbackInterface != null)
            {
                @LControllerPosition.started -= m_Wrapper.m_XRUserActionsCallbackInterface.OnLControllerPosition;
                @LControllerPosition.performed -= m_Wrapper.m_XRUserActionsCallbackInterface.OnLControllerPosition;
                @LControllerPosition.canceled -= m_Wrapper.m_XRUserActionsCallbackInterface.OnLControllerPosition;
                @LControllerRotation.started -= m_Wrapper.m_XRUserActionsCallbackInterface.OnLControllerRotation;
                @LControllerRotation.performed -= m_Wrapper.m_XRUserActionsCallbackInterface.OnLControllerRotation;
                @LControllerRotation.canceled -= m_Wrapper.m_XRUserActionsCallbackInterface.OnLControllerRotation;
                @RControllerPosition.started -= m_Wrapper.m_XRUserActionsCallbackInterface.OnRControllerPosition;
                @RControllerPosition.performed -= m_Wrapper.m_XRUserActionsCallbackInterface.OnRControllerPosition;
                @RControllerPosition.canceled -= m_Wrapper.m_XRUserActionsCallbackInterface.OnRControllerPosition;
                @RControllerRotation.started -= m_Wrapper.m_XRUserActionsCallbackInterface.OnRControllerRotation;
                @RControllerRotation.performed -= m_Wrapper.m_XRUserActionsCallbackInterface.OnRControllerRotation;
                @RControllerRotation.canceled -= m_Wrapper.m_XRUserActionsCallbackInterface.OnRControllerRotation;
                @LControllerIsTracking.started -= m_Wrapper.m_XRUserActionsCallbackInterface.OnLControllerIsTracking;
                @LControllerIsTracking.performed -= m_Wrapper.m_XRUserActionsCallbackInterface.OnLControllerIsTracking;
                @LControllerIsTracking.canceled -= m_Wrapper.m_XRUserActionsCallbackInterface.OnLControllerIsTracking;
                @RControllerIsTracking.started -= m_Wrapper.m_XRUserActionsCallbackInterface.OnRControllerIsTracking;
                @RControllerIsTracking.performed -= m_Wrapper.m_XRUserActionsCallbackInterface.OnRControllerIsTracking;
                @RControllerIsTracking.canceled -= m_Wrapper.m_XRUserActionsCallbackInterface.OnRControllerIsTracking;
            }
            m_Wrapper.m_XRUserActionsCallbackInterface = instance;
            if (instance != null)
            {
                @LControllerPosition.started += instance.OnLControllerPosition;
                @LControllerPosition.performed += instance.OnLControllerPosition;
                @LControllerPosition.canceled += instance.OnLControllerPosition;
                @LControllerRotation.started += instance.OnLControllerRotation;
                @LControllerRotation.performed += instance.OnLControllerRotation;
                @LControllerRotation.canceled += instance.OnLControllerRotation;
                @RControllerPosition.started += instance.OnRControllerPosition;
                @RControllerPosition.performed += instance.OnRControllerPosition;
                @RControllerPosition.canceled += instance.OnRControllerPosition;
                @RControllerRotation.started += instance.OnRControllerRotation;
                @RControllerRotation.performed += instance.OnRControllerRotation;
                @RControllerRotation.canceled += instance.OnRControllerRotation;
                @LControllerIsTracking.started += instance.OnLControllerIsTracking;
                @LControllerIsTracking.performed += instance.OnLControllerIsTracking;
                @LControllerIsTracking.canceled += instance.OnLControllerIsTracking;
                @RControllerIsTracking.started += instance.OnRControllerIsTracking;
                @RControllerIsTracking.performed += instance.OnRControllerIsTracking;
                @RControllerIsTracking.canceled += instance.OnRControllerIsTracking;
            }
        }
    }
    public XRUserActions @XRUser => new XRUserActions(this);
    private int m_XRPlayerSchemeIndex = -1;
    public InputControlScheme XRPlayerScheme
    {
        get
        {
            if (m_XRPlayerSchemeIndex == -1) m_XRPlayerSchemeIndex = asset.FindControlSchemeIndex("XRPlayer");
            return asset.controlSchemes[m_XRPlayerSchemeIndex];
        }
    }
    public interface IXRUserActions
    {
        void OnLControllerPosition(InputAction.CallbackContext context);
        void OnLControllerRotation(InputAction.CallbackContext context);
        void OnRControllerPosition(InputAction.CallbackContext context);
        void OnRControllerRotation(InputAction.CallbackContext context);
        void OnLControllerIsTracking(InputAction.CallbackContext context);
        void OnRControllerIsTracking(InputAction.CallbackContext context);
    }
}
