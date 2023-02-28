// GENERATED AUTOMATICALLY FROM 'Assets/Configs/InputSystem/MainPlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

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
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""6413734a-bbdf-4bc5-8647-5daa38814aca"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Walk"",
                    ""type"": ""Value"",
                    ""id"": ""14176a71-9e3b-402a-bf98-ecca1ae15732"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""OnMoveRightStateEnter"",
                    ""type"": ""Button"",
                    ""id"": ""2ed76d89-44a1-40ee-a1f6-cf64c8a8ae10"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""OnMoveLeftStateEnter"",
                    ""type"": ""Button"",
                    ""id"": ""8a91f9dc-ef34-4b5a-9599-62be249b8299"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveBoost"",
                    ""type"": ""Button"",
                    ""id"": ""9cdd7dab-4eb7-4e5b-b512-23f9e19cb494"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SimpleAttackStartState"",
                    ""type"": ""Button"",
                    ""id"": ""e8ef5ab1-2652-41dc-9135-1a3f44d8120d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""40d786a4-76ca-4372-9fd0-700e9d4db322"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""AD"",
                    ""id"": ""153ce7b1-099b-4c26-9089-0e7c535e34f8"",
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
                    ""id"": ""7bb79807-4602-40c0-8ac0-b4f9cb023957"",
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
                    ""id"": ""0c904de2-9b18-4621-be60-8cf533aad404"",
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
                    ""id"": ""56b9b9c6-1306-4be4-9e0a-205d32fbb97e"",
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
                    ""id"": ""1eddbde1-13e7-4240-b9ef-4a17d7564007"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Walk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""1884574e-b836-40cb-8afd-7abf533d0cda"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""OnMoveRightStateEnter"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c8b45800-ec8a-450e-97b1-6ac0b5ac9546"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""OnMoveLeftStateEnter"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fdd7a69c-9705-4218-870f-123330a6a700"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MoveBoost"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ee482abd-84fb-40db-bd67-a2b796c4e843"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""SimpleAttackStartState"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
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
        },
        {
            ""name"": ""Mouse"",
            ""bindingGroup"": ""Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Actions
        m_Actions = asset.FindActionMap("Actions", throwIfNotFound: true);
        m_Actions_Jump = m_Actions.FindAction("Jump", throwIfNotFound: true);
        m_Actions_Walk = m_Actions.FindAction("Walk", throwIfNotFound: true);
        m_Actions_MoveRight = m_Actions.FindAction("OnMoveRightStateEnter", throwIfNotFound: true);
        m_Actions_MoveLeft = m_Actions.FindAction("OnMoveLeftStateEnter", throwIfNotFound: true);
        m_Actions_MoveBoost = m_Actions.FindAction("MoveBoost", throwIfNotFound: true);
        m_Actions_SimpleAttack = m_Actions.FindAction("SimpleAttackStartState", throwIfNotFound: true);
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
    private readonly InputAction m_Actions_Jump;
    private readonly InputAction m_Actions_Walk;
    private readonly InputAction m_Actions_MoveRight;
    private readonly InputAction m_Actions_MoveLeft;
    private readonly InputAction m_Actions_MoveBoost;
    private readonly InputAction m_Actions_SimpleAttack;
    public struct ActionsActions
    {
        private @MainPlayerInput m_Wrapper;
        public ActionsActions(@MainPlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_Actions_Jump;
        public InputAction @Walk => m_Wrapper.m_Actions_Walk;
        public InputAction @MoveRight => m_Wrapper.m_Actions_MoveRight;
        public InputAction @MoveLeft => m_Wrapper.m_Actions_MoveLeft;
        public InputAction @MoveBoost => m_Wrapper.m_Actions_MoveBoost;
        public InputAction @SimpleAttack => m_Wrapper.m_Actions_SimpleAttack;
        public InputActionMap Get() { return m_Wrapper.m_Actions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ActionsActions set) { return set.Get(); }
        public void SetCallbacks(IActionsActions instance)
        {
            if (m_Wrapper.m_ActionsActionsCallbackInterface != null)
            {
                @Jump.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnJump;
                @Walk.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnWalk;
                @Walk.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnWalk;
                @Walk.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnWalk;
                @MoveRight.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnMoveRight;
                @MoveRight.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnMoveRight;
                @MoveRight.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnMoveRight;
                @MoveLeft.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnMoveLeft;
                @MoveLeft.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnMoveLeft;
                @MoveLeft.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnMoveLeft;
                @MoveBoost.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnMoveBoost;
                @MoveBoost.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnMoveBoost;
                @MoveBoost.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnMoveBoost;
                @SimpleAttack.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSimpleAttack;
                @SimpleAttack.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSimpleAttack;
                @SimpleAttack.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSimpleAttack;
            }
            m_Wrapper.m_ActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Walk.started += instance.OnWalk;
                @Walk.performed += instance.OnWalk;
                @Walk.canceled += instance.OnWalk;
                @MoveRight.started += instance.OnMoveRight;
                @MoveRight.performed += instance.OnMoveRight;
                @MoveRight.canceled += instance.OnMoveRight;
                @MoveLeft.started += instance.OnMoveLeft;
                @MoveLeft.performed += instance.OnMoveLeft;
                @MoveLeft.canceled += instance.OnMoveLeft;
                @MoveBoost.started += instance.OnMoveBoost;
                @MoveBoost.performed += instance.OnMoveBoost;
                @MoveBoost.canceled += instance.OnMoveBoost;
                @SimpleAttack.started += instance.OnSimpleAttack;
                @SimpleAttack.performed += instance.OnSimpleAttack;
                @SimpleAttack.canceled += instance.OnSimpleAttack;
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
    private int m_MouseSchemeIndex = -1;
    public InputControlScheme MouseScheme
    {
        get
        {
            if (m_MouseSchemeIndex == -1) m_MouseSchemeIndex = asset.FindControlSchemeIndex("Mouse");
            return asset.controlSchemes[m_MouseSchemeIndex];
        }
    }
    public interface IActionsActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnWalk(InputAction.CallbackContext context);
        void OnMoveRight(InputAction.CallbackContext context);
        void OnMoveLeft(InputAction.CallbackContext context);
        void OnMoveBoost(InputAction.CallbackContext context);
        void OnSimpleAttack(InputAction.CallbackContext context);
    }
}
