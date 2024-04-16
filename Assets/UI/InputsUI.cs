//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/UI/InputsUI.inputactions
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

public partial class @InputsUI: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputsUI()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputsUI"",
    ""maps"": [
        {
            ""name"": ""Buttons"",
            ""id"": ""35a762fd-8255-4d6c-b750-f24151a83f0f"",
            ""actions"": [
                {
                    ""name"": ""Rotation"",
                    ""type"": ""PassThrough"",
                    ""id"": ""406de423-792a-43c2-b698-326d6a45a3a1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LeftClick"",
                    ""type"": ""Button"",
                    ""id"": ""362acf20-c1f7-4123-8954-9b98d31fef2b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""f971a314-c3ec-456f-bd33-66ac9bfd1a19"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""a5613419-2b85-45c4-8f40-e874bce5a99f"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""a73a7f3c-cddf-4af2-95a6-edd8a316d06e"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""5bda4d8a-4060-41f5-9325-82ae7bfecfbc"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""60c52411-742d-4e8e-879a-edaac9ee8608"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""24408dcb-a298-4841-a6c3-083873b5df50"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Buttons
        m_Buttons = asset.FindActionMap("Buttons", throwIfNotFound: true);
        m_Buttons_Rotation = m_Buttons.FindAction("Rotation", throwIfNotFound: true);
        m_Buttons_LeftClick = m_Buttons.FindAction("LeftClick", throwIfNotFound: true);
        m_Buttons_Movement = m_Buttons.FindAction("Movement", throwIfNotFound: true);
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

    // Buttons
    private readonly InputActionMap m_Buttons;
    private List<IButtonsActions> m_ButtonsActionsCallbackInterfaces = new List<IButtonsActions>();
    private readonly InputAction m_Buttons_Rotation;
    private readonly InputAction m_Buttons_LeftClick;
    private readonly InputAction m_Buttons_Movement;
    public struct ButtonsActions
    {
        private @InputsUI m_Wrapper;
        public ButtonsActions(@InputsUI wrapper) { m_Wrapper = wrapper; }
        public InputAction @Rotation => m_Wrapper.m_Buttons_Rotation;
        public InputAction @LeftClick => m_Wrapper.m_Buttons_LeftClick;
        public InputAction @Movement => m_Wrapper.m_Buttons_Movement;
        public InputActionMap Get() { return m_Wrapper.m_Buttons; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ButtonsActions set) { return set.Get(); }
        public void AddCallbacks(IButtonsActions instance)
        {
            if (instance == null || m_Wrapper.m_ButtonsActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_ButtonsActionsCallbackInterfaces.Add(instance);
            @Rotation.started += instance.OnRotation;
            @Rotation.performed += instance.OnRotation;
            @Rotation.canceled += instance.OnRotation;
            @LeftClick.started += instance.OnLeftClick;
            @LeftClick.performed += instance.OnLeftClick;
            @LeftClick.canceled += instance.OnLeftClick;
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
        }

        private void UnregisterCallbacks(IButtonsActions instance)
        {
            @Rotation.started -= instance.OnRotation;
            @Rotation.performed -= instance.OnRotation;
            @Rotation.canceled -= instance.OnRotation;
            @LeftClick.started -= instance.OnLeftClick;
            @LeftClick.performed -= instance.OnLeftClick;
            @LeftClick.canceled -= instance.OnLeftClick;
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
        }

        public void RemoveCallbacks(IButtonsActions instance)
        {
            if (m_Wrapper.m_ButtonsActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IButtonsActions instance)
        {
            foreach (var item in m_Wrapper.m_ButtonsActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_ButtonsActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public ButtonsActions @Buttons => new ButtonsActions(this);
    public interface IButtonsActions
    {
        void OnRotation(InputAction.CallbackContext context);
        void OnLeftClick(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
    }
}
