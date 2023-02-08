// GENERATED AUTOMATICALLY FROM 'Assets/Configs/InputSystem/MainPlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Assets.Configs.InputSystem
{
    public class @MainPlayerInput : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @MainPlayerInput()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""MainPlayerInput"",
    ""maps"": [
        {
            ""name"": ""Actions"",
            ""id"": ""e460b6ca-186d-43ad-9a9f-e7179bba779c"",
            ""actions"": [
                {
                    ""name"": ""Walk"",
                    ""type"": ""Value"",
                    ""id"": ""e8a17692-3f41-4e9b-a30a-ab78719d7edc"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""AD"",
                    ""id"": ""bc6a0d96-b64e-479c-9091-2b2b1ce22756"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Walk"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""021400b7-89f9-4b05-9fba-3543daf170a5"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Walk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""6e34fff6-7bda-4884-9561-59a1a1d05264"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Walk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e611e20a-7c93-4768-81e3-9572f77e1692"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Walk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""c1803131-94c9-406f-bdf6-61c0a18122f9"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Walk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // Actions
            m_Actions = asset.FindActionMap("Actions", throwIfNotFound: true);
            m_Actions_Walk = m_Actions.FindAction("Walk", throwIfNotFound: true);
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

        // Actions
        private readonly InputActionMap m_Actions;
        private IActionsActions m_ActionsActionsCallbackInterface;
        private readonly InputAction m_Actions_Walk;
        public struct ActionsActions
        {
            private @MainPlayerInput m_Wrapper;
            public ActionsActions(@MainPlayerInput wrapper) { m_Wrapper = wrapper; }
            public InputAction @Walk => m_Wrapper.m_Actions_Walk;
            public InputActionMap Get() { return m_Wrapper.m_Actions; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(ActionsActions set) { return set.Get(); }
            public void SetCallbacks(IActionsActions instance)
            {
                if (m_Wrapper.m_ActionsActionsCallbackInterface != null)
                {
                    @Walk.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnWalk;
                    @Walk.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnWalk;
                    @Walk.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnWalk;
                }
                m_Wrapper.m_ActionsActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Walk.started += instance.OnWalk;
                    @Walk.performed += instance.OnWalk;
                    @Walk.canceled += instance.OnWalk;
                }
            }
        }
        public ActionsActions @Actions => new ActionsActions(this);
        private int m_KeyboardSchemeIndex = -1;
        public InputControlScheme KeyboardScheme
        {
            get
            {
                if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
                return asset.controlSchemes[m_KeyboardSchemeIndex];
            }
        }
        public interface IActionsActions
        {
            void OnWalk(InputAction.CallbackContext context);
        }
    }
}

