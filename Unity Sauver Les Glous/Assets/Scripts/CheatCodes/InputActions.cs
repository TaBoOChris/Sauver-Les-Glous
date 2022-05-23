//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.2.0
//     from Assets/InputActions.inputactions
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

public partial class @InputActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActions"",
    ""maps"": [
        {
            ""name"": ""CheatCodes"",
            ""id"": ""d1ece551-9f55-4704-beea-b986e0968a6a"",
            ""actions"": [
                {
                    ""name"": ""AddTime"",
                    ""type"": ""Button"",
                    ""id"": ""908eb27d-133d-4f83-afc2-e8f2761c3380"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""EndTimer"",
                    ""type"": ""Button"",
                    ""id"": ""52105add-3070-43f0-889e-54868dce530c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SpawnGlou"",
                    ""type"": ""Button"",
                    ""id"": ""0146dff4-9995-4e73-9bf6-ae7ca044b6fc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""da673f88-f40b-4b5b-8df1-34f4a30f692b"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AddTime"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e7424777-acc6-4d58-ba6f-cc53afd69855"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""EndTimer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""175d26b4-b456-41fd-920e-20285d53a921"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpawnGlou"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // CheatCodes
        m_CheatCodes = asset.FindActionMap("CheatCodes", throwIfNotFound: true);
        m_CheatCodes_AddTime = m_CheatCodes.FindAction("AddTime", throwIfNotFound: true);
        m_CheatCodes_EndTimer = m_CheatCodes.FindAction("EndTimer", throwIfNotFound: true);
        m_CheatCodes_SpawnGlou = m_CheatCodes.FindAction("SpawnGlou", throwIfNotFound: true);
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

    // CheatCodes
    private readonly InputActionMap m_CheatCodes;
    private ICheatCodesActions m_CheatCodesActionsCallbackInterface;
    private readonly InputAction m_CheatCodes_AddTime;
    private readonly InputAction m_CheatCodes_EndTimer;
    private readonly InputAction m_CheatCodes_SpawnGlou;
    public struct CheatCodesActions
    {
        private @InputActions m_Wrapper;
        public CheatCodesActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @AddTime => m_Wrapper.m_CheatCodes_AddTime;
        public InputAction @EndTimer => m_Wrapper.m_CheatCodes_EndTimer;
        public InputAction @SpawnGlou => m_Wrapper.m_CheatCodes_SpawnGlou;
        public InputActionMap Get() { return m_Wrapper.m_CheatCodes; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CheatCodesActions set) { return set.Get(); }
        public void SetCallbacks(ICheatCodesActions instance)
        {
            if (m_Wrapper.m_CheatCodesActionsCallbackInterface != null)
            {
                @AddTime.started -= m_Wrapper.m_CheatCodesActionsCallbackInterface.OnAddTime;
                @AddTime.performed -= m_Wrapper.m_CheatCodesActionsCallbackInterface.OnAddTime;
                @AddTime.canceled -= m_Wrapper.m_CheatCodesActionsCallbackInterface.OnAddTime;
                @EndTimer.started -= m_Wrapper.m_CheatCodesActionsCallbackInterface.OnEndTimer;
                @EndTimer.performed -= m_Wrapper.m_CheatCodesActionsCallbackInterface.OnEndTimer;
                @EndTimer.canceled -= m_Wrapper.m_CheatCodesActionsCallbackInterface.OnEndTimer;
                @SpawnGlou.started -= m_Wrapper.m_CheatCodesActionsCallbackInterface.OnSpawnGlou;
                @SpawnGlou.performed -= m_Wrapper.m_CheatCodesActionsCallbackInterface.OnSpawnGlou;
                @SpawnGlou.canceled -= m_Wrapper.m_CheatCodesActionsCallbackInterface.OnSpawnGlou;
            }
            m_Wrapper.m_CheatCodesActionsCallbackInterface = instance;
            if (instance != null)
            {
                @AddTime.started += instance.OnAddTime;
                @AddTime.performed += instance.OnAddTime;
                @AddTime.canceled += instance.OnAddTime;
                @EndTimer.started += instance.OnEndTimer;
                @EndTimer.performed += instance.OnEndTimer;
                @EndTimer.canceled += instance.OnEndTimer;
                @SpawnGlou.started += instance.OnSpawnGlou;
                @SpawnGlou.performed += instance.OnSpawnGlou;
                @SpawnGlou.canceled += instance.OnSpawnGlou;
            }
        }
    }
    public CheatCodesActions @CheatCodes => new CheatCodesActions(this);
    public interface ICheatCodesActions
    {
        void OnAddTime(InputAction.CallbackContext context);
        void OnEndTimer(InputAction.CallbackContext context);
        void OnSpawnGlou(InputAction.CallbackContext context);
    }
}