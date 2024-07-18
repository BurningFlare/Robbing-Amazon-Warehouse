using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [Header("Information")]
    [SerializeField] private string propName;

    // These two should be guaranteed to be normalized
    private Vector2 moveDirection;
    private Vector2 mostRecentMoveDirection;

    // Components
    private Interactor interactor;
    private Camera mainCamera;
    private PlayerInputReceiver playerInputReceiver;
    [SerializeField] private TransmutationBase currentTransmutation;

    private void Awake()
    {
        interactor = GetComponent<Interactor>();
        mainCamera = Camera.main;
        playerInputReceiver = GetComponent<PlayerInputReceiver>();
        if (currentTransmutation == null)
        {
            // TODO make default transmutation base the player one
            //currentTransmutation = new TransmutationBase(
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetMovementInput();
        UpdateInteractorPosition();
    }

    private void FixedUpdate()
    {
        currentTransmutation.HandleMovement(moveDirection);
    }

    void GetMovementInput()
    {
        moveDirection = playerInputReceiver.MoveInput;
        moveDirection.Normalize();
        if (moveDirection.magnitude >= 0.1)
        {
            mostRecentMoveDirection = moveDirection;
        }
    }
    
    void UpdateInteractorPosition()
    {
        interactor.interactionPoint.localPosition = mostRecentMoveDirection;
    }

    public bool Transmute(TransmutationBase transmutation)
    {
        // TODO modify health to be equivalent
        // TODO play cool transmutation animation
        currentTransmutation = transmutation;
        // TODO replace final transmutation in hotbar
        // TODO handle transmutation cooldown
        return true;
    }
}