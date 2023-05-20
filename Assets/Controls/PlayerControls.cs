// GENERATED AUTOMATICALLY FROM 'Assets/Controls/PlayerControls.inputactions'

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
            ""name"": ""Player"",
            ""id"": ""6a66dbb1-8e44-48c7-b51f-a95ddfead32c"",
            ""actions"": [
                {
                    ""name"": ""Mouse Look"",
                    ""type"": ""PassThrough"",
                    ""id"": ""a58b2ecc-965a-4aa0-bf87-1b39da89f2ca"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""8053fb64-df9d-4d3f-8b00-10baed42ff13"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""f83a877c-8b29-436e-8dc4-0c0167d370b5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Button"",
                    ""id"": ""9cc8eb24-dbb8-4e9b-9fee-d1160aeb9953"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire Single Bullet"",
                    ""type"": ""Button"",
                    ""id"": ""4300d758-6c2a-47cb-8786-909d03f1ea20"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""FireMultipleBullets"",
                    ""type"": ""Button"",
                    ""id"": ""3c50c2e2-32af-4af0-ab73-0aa396c8598a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""Open Inventory"",
                    ""type"": ""Button"",
                    ""id"": ""aa966db1-29cd-4c24-9adc-b68f572235fb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact/Pickup Item"",
                    ""type"": ""Button"",
                    ""id"": ""2e2cf554-9a8f-4916-a968-6994f7b9cfc2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select Item"",
                    ""type"": ""Button"",
                    ""id"": ""0c36ef51-c918-4be6-83a6-7d7e678f4490"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Center Camera"",
                    ""type"": ""Button"",
                    ""id"": ""f6993321-fa4c-4606-940d-3be6a1e54738"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""6c1b11ff-3266-4b2a-9794-044702f87d9a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5c80a635-985e-4958-b440-41815188fb60"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouse Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""479dfea8-cf10-49c5-984b-0fd0e07dcfc8"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""8e0dd840-82d3-4bb0-a31d-2d401c3ad418"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""90eabdf9-57f9-4c66-858d-5c958a7fb906"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""eced1fda-a6d7-4ad1-ae37-f82a441bb58e"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""2979dd7a-f5d7-4cb7-9805-02254de0f66a"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b2b8c614-32fc-4825-9dad-74f171ef405b"",
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
                    ""id"": ""8534986a-eb4e-44c6-811e-efe8d4381c6d"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""14d0b9b1-4c50-4d2e-8c3e-5df44d03ea2d"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire Single Bullet"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9cce0122-eded-46d4-b9ff-ac07ec0507cd"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Open Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0d31b3b9-ccfd-4548-809c-34ad3aaeeb11"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact/Pickup Item"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""62cbb75a-045b-41f6-bec9-8e60db47bebc"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select Item"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f3cf78d1-dcd7-40d2-a5fa-ebd5eeeb92f3"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Center Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""99206cd0-9281-4b1f-b97d-c246931e45cd"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""40b6ca7e-ff5c-46ab-bb0c-f7710aeda766"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FireMultipleBullets"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Inventory"",
            ""id"": ""0026b077-2698-426c-af14-617854d1ca12"",
            ""actions"": [
                {
                    ""name"": ""Select Item"",
                    ""type"": ""Button"",
                    ""id"": ""dfac266d-72d2-48fb-82b9-b00edf7444ee"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotate Item"",
                    ""type"": ""Button"",
                    ""id"": ""57dfe9fc-66af-4b5e-a416-fa5f1a7b77eb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Use Item"",
                    ""type"": ""Button"",
                    ""id"": ""ce05bea8-8e01-46c0-85a6-a47074426320"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Drop Item "",
                    ""type"": ""Button"",
                    ""id"": ""26ba5fcd-a1eb-4c3a-b8c1-5ad10782da8a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""72784512-2548-468b-9aad-bf1bc51fb85e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select Item"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""77271e27-5e43-446b-98ba-f7122784071b"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate Item"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d38f89a1-3406-42d5-873d-d255bb90ff62"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Use Item"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c33d27b3-ba47-4ff1-a8e3-f6236d094d5a"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Drop Item "",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Weapon"",
            ""id"": ""15124809-8fdb-438e-bdfd-e94fb27b1edb"",
            ""actions"": [
                {
                    ""name"": ""Weapon 1"",
                    ""type"": ""Button"",
                    ""id"": ""477f9212-f21f-46ff-b006-5d9265d3282e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Weapon 2"",
                    ""type"": ""Button"",
                    ""id"": ""cb438ae0-a22a-4838-adf1-57a6226a030d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Weapon 3"",
                    ""type"": ""Button"",
                    ""id"": ""d21a62c4-cb27-400f-b3e4-e657f6000f68"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Weapon 4"",
                    ""type"": ""Button"",
                    ""id"": ""56140e0e-9923-45a2-9d57-6d5a801e1e19"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""94a2228c-97d4-4e33-8b44-233310b68db7"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Weapon 1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8aabf183-456b-467a-8cd4-6556cacc9d9f"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Weapon 2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9938458c-a324-436a-91cb-c021866ae668"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Weapon 3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4b8c2bf2-ac31-4ef3-92c1-e0b295e62289"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Weapon 4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_MouseLook = m_Player.FindAction("Mouse Look", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Aim = m_Player.FindAction("Aim", throwIfNotFound: true);
        m_Player_FireSingleBullet = m_Player.FindAction("Fire Single Bullet", throwIfNotFound: true);
        m_Player_FireMultipleBullets = m_Player.FindAction("FireMultipleBullets", throwIfNotFound: true);
        m_Player_OpenInventory = m_Player.FindAction("Open Inventory", throwIfNotFound: true);
        m_Player_InteractPickupItem = m_Player.FindAction("Interact/Pickup Item", throwIfNotFound: true);
        m_Player_SelectItem = m_Player.FindAction("Select Item", throwIfNotFound: true);
        m_Player_CenterCamera = m_Player.FindAction("Center Camera", throwIfNotFound: true);
        m_Player_Reload = m_Player.FindAction("Reload", throwIfNotFound: true);
        // Inventory
        m_Inventory = asset.FindActionMap("Inventory", throwIfNotFound: true);
        m_Inventory_SelectItem = m_Inventory.FindAction("Select Item", throwIfNotFound: true);
        m_Inventory_RotateItem = m_Inventory.FindAction("Rotate Item", throwIfNotFound: true);
        m_Inventory_UseItem = m_Inventory.FindAction("Use Item", throwIfNotFound: true);
        m_Inventory_DropItem = m_Inventory.FindAction("Drop Item ", throwIfNotFound: true);
        // Weapon
        m_Weapon = asset.FindActionMap("Weapon", throwIfNotFound: true);
        m_Weapon_Weapon1 = m_Weapon.FindAction("Weapon 1", throwIfNotFound: true);
        m_Weapon_Weapon2 = m_Weapon.FindAction("Weapon 2", throwIfNotFound: true);
        m_Weapon_Weapon3 = m_Weapon.FindAction("Weapon 3", throwIfNotFound: true);
        m_Weapon_Weapon4 = m_Weapon.FindAction("Weapon 4", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_MouseLook;
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Aim;
    private readonly InputAction m_Player_FireSingleBullet;
    private readonly InputAction m_Player_FireMultipleBullets;
    private readonly InputAction m_Player_OpenInventory;
    private readonly InputAction m_Player_InteractPickupItem;
    private readonly InputAction m_Player_SelectItem;
    private readonly InputAction m_Player_CenterCamera;
    private readonly InputAction m_Player_Reload;
    public struct PlayerActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MouseLook => m_Wrapper.m_Player_MouseLook;
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Aim => m_Wrapper.m_Player_Aim;
        public InputAction @FireSingleBullet => m_Wrapper.m_Player_FireSingleBullet;
        public InputAction @FireMultipleBullets => m_Wrapper.m_Player_FireMultipleBullets;
        public InputAction @OpenInventory => m_Wrapper.m_Player_OpenInventory;
        public InputAction @InteractPickupItem => m_Wrapper.m_Player_InteractPickupItem;
        public InputAction @SelectItem => m_Wrapper.m_Player_SelectItem;
        public InputAction @CenterCamera => m_Wrapper.m_Player_CenterCamera;
        public InputAction @Reload => m_Wrapper.m_Player_Reload;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @MouseLook.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseLook;
                @MouseLook.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseLook;
                @MouseLook.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseLook;
                @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Aim.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAim;
                @FireSingleBullet.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFireSingleBullet;
                @FireSingleBullet.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFireSingleBullet;
                @FireSingleBullet.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFireSingleBullet;
                @FireMultipleBullets.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFireMultipleBullets;
                @FireMultipleBullets.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFireMultipleBullets;
                @FireMultipleBullets.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFireMultipleBullets;
                @OpenInventory.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenInventory;
                @OpenInventory.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenInventory;
                @OpenInventory.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenInventory;
                @InteractPickupItem.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteractPickupItem;
                @InteractPickupItem.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteractPickupItem;
                @InteractPickupItem.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteractPickupItem;
                @SelectItem.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectItem;
                @SelectItem.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectItem;
                @SelectItem.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectItem;
                @CenterCamera.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCenterCamera;
                @CenterCamera.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCenterCamera;
                @CenterCamera.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCenterCamera;
                @Reload.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
                @Reload.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
                @Reload.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MouseLook.started += instance.OnMouseLook;
                @MouseLook.performed += instance.OnMouseLook;
                @MouseLook.canceled += instance.OnMouseLook;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
                @FireSingleBullet.started += instance.OnFireSingleBullet;
                @FireSingleBullet.performed += instance.OnFireSingleBullet;
                @FireSingleBullet.canceled += instance.OnFireSingleBullet;
                @FireMultipleBullets.started += instance.OnFireMultipleBullets;
                @FireMultipleBullets.performed += instance.OnFireMultipleBullets;
                @FireMultipleBullets.canceled += instance.OnFireMultipleBullets;
                @OpenInventory.started += instance.OnOpenInventory;
                @OpenInventory.performed += instance.OnOpenInventory;
                @OpenInventory.canceled += instance.OnOpenInventory;
                @InteractPickupItem.started += instance.OnInteractPickupItem;
                @InteractPickupItem.performed += instance.OnInteractPickupItem;
                @InteractPickupItem.canceled += instance.OnInteractPickupItem;
                @SelectItem.started += instance.OnSelectItem;
                @SelectItem.performed += instance.OnSelectItem;
                @SelectItem.canceled += instance.OnSelectItem;
                @CenterCamera.started += instance.OnCenterCamera;
                @CenterCamera.performed += instance.OnCenterCamera;
                @CenterCamera.canceled += instance.OnCenterCamera;
                @Reload.started += instance.OnReload;
                @Reload.performed += instance.OnReload;
                @Reload.canceled += instance.OnReload;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Inventory
    private readonly InputActionMap m_Inventory;
    private IInventoryActions m_InventoryActionsCallbackInterface;
    private readonly InputAction m_Inventory_SelectItem;
    private readonly InputAction m_Inventory_RotateItem;
    private readonly InputAction m_Inventory_UseItem;
    private readonly InputAction m_Inventory_DropItem;
    public struct InventoryActions
    {
        private @PlayerControls m_Wrapper;
        public InventoryActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @SelectItem => m_Wrapper.m_Inventory_SelectItem;
        public InputAction @RotateItem => m_Wrapper.m_Inventory_RotateItem;
        public InputAction @UseItem => m_Wrapper.m_Inventory_UseItem;
        public InputAction @DropItem => m_Wrapper.m_Inventory_DropItem;
        public InputActionMap Get() { return m_Wrapper.m_Inventory; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InventoryActions set) { return set.Get(); }
        public void SetCallbacks(IInventoryActions instance)
        {
            if (m_Wrapper.m_InventoryActionsCallbackInterface != null)
            {
                @SelectItem.started -= m_Wrapper.m_InventoryActionsCallbackInterface.OnSelectItem;
                @SelectItem.performed -= m_Wrapper.m_InventoryActionsCallbackInterface.OnSelectItem;
                @SelectItem.canceled -= m_Wrapper.m_InventoryActionsCallbackInterface.OnSelectItem;
                @RotateItem.started -= m_Wrapper.m_InventoryActionsCallbackInterface.OnRotateItem;
                @RotateItem.performed -= m_Wrapper.m_InventoryActionsCallbackInterface.OnRotateItem;
                @RotateItem.canceled -= m_Wrapper.m_InventoryActionsCallbackInterface.OnRotateItem;
                @UseItem.started -= m_Wrapper.m_InventoryActionsCallbackInterface.OnUseItem;
                @UseItem.performed -= m_Wrapper.m_InventoryActionsCallbackInterface.OnUseItem;
                @UseItem.canceled -= m_Wrapper.m_InventoryActionsCallbackInterface.OnUseItem;
                @DropItem.started -= m_Wrapper.m_InventoryActionsCallbackInterface.OnDropItem;
                @DropItem.performed -= m_Wrapper.m_InventoryActionsCallbackInterface.OnDropItem;
                @DropItem.canceled -= m_Wrapper.m_InventoryActionsCallbackInterface.OnDropItem;
            }
            m_Wrapper.m_InventoryActionsCallbackInterface = instance;
            if (instance != null)
            {
                @SelectItem.started += instance.OnSelectItem;
                @SelectItem.performed += instance.OnSelectItem;
                @SelectItem.canceled += instance.OnSelectItem;
                @RotateItem.started += instance.OnRotateItem;
                @RotateItem.performed += instance.OnRotateItem;
                @RotateItem.canceled += instance.OnRotateItem;
                @UseItem.started += instance.OnUseItem;
                @UseItem.performed += instance.OnUseItem;
                @UseItem.canceled += instance.OnUseItem;
                @DropItem.started += instance.OnDropItem;
                @DropItem.performed += instance.OnDropItem;
                @DropItem.canceled += instance.OnDropItem;
            }
        }
    }
    public InventoryActions @Inventory => new InventoryActions(this);

    // Weapon
    private readonly InputActionMap m_Weapon;
    private IWeaponActions m_WeaponActionsCallbackInterface;
    private readonly InputAction m_Weapon_Weapon1;
    private readonly InputAction m_Weapon_Weapon2;
    private readonly InputAction m_Weapon_Weapon3;
    private readonly InputAction m_Weapon_Weapon4;
    public struct WeaponActions
    {
        private @PlayerControls m_Wrapper;
        public WeaponActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Weapon1 => m_Wrapper.m_Weapon_Weapon1;
        public InputAction @Weapon2 => m_Wrapper.m_Weapon_Weapon2;
        public InputAction @Weapon3 => m_Wrapper.m_Weapon_Weapon3;
        public InputAction @Weapon4 => m_Wrapper.m_Weapon_Weapon4;
        public InputActionMap Get() { return m_Wrapper.m_Weapon; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(WeaponActions set) { return set.Get(); }
        public void SetCallbacks(IWeaponActions instance)
        {
            if (m_Wrapper.m_WeaponActionsCallbackInterface != null)
            {
                @Weapon1.started -= m_Wrapper.m_WeaponActionsCallbackInterface.OnWeapon1;
                @Weapon1.performed -= m_Wrapper.m_WeaponActionsCallbackInterface.OnWeapon1;
                @Weapon1.canceled -= m_Wrapper.m_WeaponActionsCallbackInterface.OnWeapon1;
                @Weapon2.started -= m_Wrapper.m_WeaponActionsCallbackInterface.OnWeapon2;
                @Weapon2.performed -= m_Wrapper.m_WeaponActionsCallbackInterface.OnWeapon2;
                @Weapon2.canceled -= m_Wrapper.m_WeaponActionsCallbackInterface.OnWeapon2;
                @Weapon3.started -= m_Wrapper.m_WeaponActionsCallbackInterface.OnWeapon3;
                @Weapon3.performed -= m_Wrapper.m_WeaponActionsCallbackInterface.OnWeapon3;
                @Weapon3.canceled -= m_Wrapper.m_WeaponActionsCallbackInterface.OnWeapon3;
                @Weapon4.started -= m_Wrapper.m_WeaponActionsCallbackInterface.OnWeapon4;
                @Weapon4.performed -= m_Wrapper.m_WeaponActionsCallbackInterface.OnWeapon4;
                @Weapon4.canceled -= m_Wrapper.m_WeaponActionsCallbackInterface.OnWeapon4;
            }
            m_Wrapper.m_WeaponActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Weapon1.started += instance.OnWeapon1;
                @Weapon1.performed += instance.OnWeapon1;
                @Weapon1.canceled += instance.OnWeapon1;
                @Weapon2.started += instance.OnWeapon2;
                @Weapon2.performed += instance.OnWeapon2;
                @Weapon2.canceled += instance.OnWeapon2;
                @Weapon3.started += instance.OnWeapon3;
                @Weapon3.performed += instance.OnWeapon3;
                @Weapon3.canceled += instance.OnWeapon3;
                @Weapon4.started += instance.OnWeapon4;
                @Weapon4.performed += instance.OnWeapon4;
                @Weapon4.canceled += instance.OnWeapon4;
            }
        }
    }
    public WeaponActions @Weapon => new WeaponActions(this);
    public interface IPlayerActions
    {
        void OnMouseLook(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnFireSingleBullet(InputAction.CallbackContext context);
        void OnFireMultipleBullets(InputAction.CallbackContext context);
        void OnOpenInventory(InputAction.CallbackContext context);
        void OnInteractPickupItem(InputAction.CallbackContext context);
        void OnSelectItem(InputAction.CallbackContext context);
        void OnCenterCamera(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
    }
    public interface IInventoryActions
    {
        void OnSelectItem(InputAction.CallbackContext context);
        void OnRotateItem(InputAction.CallbackContext context);
        void OnUseItem(InputAction.CallbackContext context);
        void OnDropItem(InputAction.CallbackContext context);
    }
    public interface IWeaponActions
    {
        void OnWeapon1(InputAction.CallbackContext context);
        void OnWeapon2(InputAction.CallbackContext context);
        void OnWeapon3(InputAction.CallbackContext context);
        void OnWeapon4(InputAction.CallbackContext context);
    }
}
