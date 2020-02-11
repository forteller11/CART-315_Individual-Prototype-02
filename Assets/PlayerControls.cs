// GENERATED AUTOMATICALLY FROM 'Assets/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""PlayerMovement"",
            ""id"": ""4ae4e7fc-0373-493c-a2ab-60f933875e54"",
            ""actions"": [
                {
                    ""name"": ""Translate"",
                    ""type"": ""Value"",
                    ""id"": ""ec530c1b-adeb-4aa8-ae55-92225678595e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""a7756874-6bbb-4bdc-bb54-f20ca9afebac"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Build"",
                    ""type"": ""Value"",
                    ""id"": ""dad7e0f3-e2d9-4033-8350-76725815f714"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dig"",
                    ""type"": ""Value"",
                    ""id"": ""3b89d1e5-35ee-4b61-b4ac-2a29e62fe331"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Value"",
                    ""id"": ""4fc798a3-6c66-4d9e-bbe1-3ef032c11de3"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2d1c832b-5022-40a1-9122-d48dd9852ffe"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Translate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""993159c9-e440-4756-b84c-a580ce1a53ed"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Translate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""617c96ad-0af0-447e-91d6-c2069b251651"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Translate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""901b4f98-f1e0-4e6a-bf91-012471350df5"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Translate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""67991681-d381-419a-aa74-90f18ee39572"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Translate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""17359657-2af9-4373-860d-b07d9a6a2d60"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Translate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""a9f6c735-29ed-4509-8946-8fd4bff85d3b"",
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
                    ""id"": ""75c24a5f-408a-402a-b04e-e509595ccd21"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f85c7f92-ef7f-437a-93c7-7841a3261e75"",
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
                    ""id"": ""5ef4a738-1c98-41ff-91dc-a70a2057fe1a"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Build"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""23c7b647-0486-4334-95b4-70bafd85b40f"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Build"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b95a38b9-422b-4bc7-9fd9-a7ff8e511408"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dig"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""70295404-cd59-446c-be67-d14d6906a42f"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dig"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c5422168-db2d-4f2e-9acb-06301af98b0c"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""8483cb6c-c8b1-4d76-bc74-e1ed9cd34b4e"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""df9016aa-6517-4f04-b2f6-3faf9985afa9"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""e7aeb8de-4f5a-41eb-94c2-ccbe92cfffa8"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ba60d31c-c22b-4ba4-86df-38e695c9c502"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""e176eb9e-0956-4b5b-9e7b-92eeea46854f"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""57df8fe2-7268-44e1-acad-4ca76bf10ca2"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2(x=0.02,y=0.02)"",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerMovement
        m_PlayerMovement = asset.FindActionMap("PlayerMovement", throwIfNotFound: true);
        m_PlayerMovement_Translate = m_PlayerMovement.FindAction("Translate", throwIfNotFound: true);
        m_PlayerMovement_Jump = m_PlayerMovement.FindAction("Jump", throwIfNotFound: true);
        m_PlayerMovement_Build = m_PlayerMovement.FindAction("Build", throwIfNotFound: true);
        m_PlayerMovement_Dig = m_PlayerMovement.FindAction("Dig", throwIfNotFound: true);
        m_PlayerMovement_Rotate = m_PlayerMovement.FindAction("Rotate", throwIfNotFound: true);
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

    // PlayerMovement
    private readonly InputActionMap m_PlayerMovement;
    private IPlayerMovementActions m_PlayerMovementActionsCallbackInterface;
    private readonly InputAction m_PlayerMovement_Translate;
    private readonly InputAction m_PlayerMovement_Jump;
    private readonly InputAction m_PlayerMovement_Build;
    private readonly InputAction m_PlayerMovement_Dig;
    private readonly InputAction m_PlayerMovement_Rotate;
    public struct PlayerMovementActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerMovementActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Translate => m_Wrapper.m_PlayerMovement_Translate;
        public InputAction @Jump => m_Wrapper.m_PlayerMovement_Jump;
        public InputAction @Build => m_Wrapper.m_PlayerMovement_Build;
        public InputAction @Dig => m_Wrapper.m_PlayerMovement_Dig;
        public InputAction @Rotate => m_Wrapper.m_PlayerMovement_Rotate;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMovementActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerMovementActions instance)
        {
            if (m_Wrapper.m_PlayerMovementActionsCallbackInterface != null)
            {
                @Translate.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnTranslate;
                @Translate.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnTranslate;
                @Translate.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnTranslate;
                @Jump.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnJump;
                @Build.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnBuild;
                @Build.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnBuild;
                @Build.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnBuild;
                @Dig.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnDig;
                @Dig.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnDig;
                @Dig.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnDig;
                @Rotate.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnRotate;
                @Rotate.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnRotate;
                @Rotate.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnRotate;
            }
            m_Wrapper.m_PlayerMovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Translate.started += instance.OnTranslate;
                @Translate.performed += instance.OnTranslate;
                @Translate.canceled += instance.OnTranslate;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Build.started += instance.OnBuild;
                @Build.performed += instance.OnBuild;
                @Build.canceled += instance.OnBuild;
                @Dig.started += instance.OnDig;
                @Dig.performed += instance.OnDig;
                @Dig.canceled += instance.OnDig;
                @Rotate.started += instance.OnRotate;
                @Rotate.performed += instance.OnRotate;
                @Rotate.canceled += instance.OnRotate;
            }
        }
    }
    public PlayerMovementActions @PlayerMovement => new PlayerMovementActions(this);
    public interface IPlayerMovementActions
    {
        void OnTranslate(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnBuild(InputAction.CallbackContext context);
        void OnDig(InputAction.CallbackContext context);
        void OnRotate(InputAction.CallbackContext context);
    }
}
