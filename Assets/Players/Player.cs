using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [Header("Information")]
    [SerializeField] private string propName;

    // This should be guaranteed to be normalized
    private Vector2 moveDirection;

    // Components
    private Interactor interactor;
    private Camera mainCamera;
    private PlayerInputReceiver playerInputReceiver;
    [SerializeField] public TransmutationBase currentTransmutation;

    private void Awake()
    {
        interactor = GetComponent<Interactor>();
        mainCamera = Camera.main;
        playerInputReceiver = GetComponent<PlayerInputReceiver>();
        if (currentTransmutation == null)
        {
            currentTransmutation = GetComponent<TransmutationBase>();

            // TODO make default transmutation base the player one
            //currentTransmutation = new TransmutationBase(
            //currentTransmutation.transform.SetParent(transform, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetMovementInput();
    }

    private void FixedUpdate()
    {
        currentTransmutation.HandleMovement(moveDirection);
    }

    void GetMovementInput()
    {
        moveDirection = playerInputReceiver.MoveInput;
        moveDirection.Normalize();
    }

    public void handleInteractionInput()
    {
        interactor.handleInteractionInput();
    }

    public bool Transmute(TransmutationBase transmutation)
    {
        if (transmutation.name == currentTransmutation.name)
        {
            return false;
        }

        Vector2 oldPosition = currentTransmutation.transform.position;

        Destroy(currentTransmutation.gameObject);

        currentTransmutation = Instantiate(transmutation, oldPosition, Quaternion.identity, transform).GetComponent<TransmutationBase>();

        interactor.handleTransmutationChanged();

        // TODO modify health to be equivalent
        // TODO play cool transmutation animation
        // TODO replace final transmutation in hotbar
        // TODO handle transmutation cooldown
        return true;
    }
}