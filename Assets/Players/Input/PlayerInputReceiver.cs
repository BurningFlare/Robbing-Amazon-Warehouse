using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReceiver : MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset playerControls;

    // add strings for the names of all of the input mappings
    [Header("Action Map References")]
    [SerializeField] private string playerBasicActionMapName = "Player";
    [SerializeField] private string playerHotbarActionMapName = "HotbarTransforming";
    [SerializeField] private string playerInteractActionMapName = "Interact";

    // strings for actions
    [Header("Action Name References")]
    [SerializeField] private string move = "Move";
    [SerializeField] private string jump = "Jump";

    [SerializeField] private string interact = "Interact";

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction interactAction;

    // members to be read by player for input values
    public Vector2 MoveInput { get; private set; }
    public bool JumpTriggered { get; private set; }
    public bool InteractTriggered { get; private set; }

    // only want a single instance of the input handler
    public static PlayerInputReceiver Instance { get; private set; }

    private void Awake()
    {
        // set the single instance to this if it's the only instance, otherwise destroy the current instance
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }

       // bind the actions to the correct object within the input action asset
        moveAction = playerControls.FindActionMap(playerBasicActionMapName).FindAction(move);
        jumpAction = playerControls.FindActionMap(playerBasicActionMapName).FindAction(jump);
        jumpAction = playerControls.FindActionMap(playerInteractActionMapName).FindAction(interact);
        // setup event handling for inputs, change the values to be read
        RegisterInputActions();
    }

    // this feels really backwards ngl but it's what I found on youtube so...
    void RegisterInputActions()
    {
        // handle move inputs
        moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => MoveInput = Vector2.zero;

        // handle jump inputs
        jumpAction.performed += context => JumpTriggered = true;
        jumpAction.canceled += context => JumpTriggered = false;

        // handle interact inputs
        interactAction.performed += context => InteractTriggered = true;
        interactAction.canceled += context => InteractTriggered = false;
    }

    // enable and disable actions as necessary
    private void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
        interactAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
        interactAction.Disable();
    }
}
