using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] public Transform interactionPoint;
    [SerializeField] public Player player;
    [SerializeField] private LayerMask interactionMask;

    private InteractionPointTrigger interactionPointTrigger;

    private IInteractable currentSelection;

    private void Awake()
    {
        if (player == null)
        {
            player = gameObject.GetComponent<Player>();
        }
        if (interactionPoint == null)
        {
            interactionPoint = player.currentTransmutation.transform.Find("interactionPoint");
        }
        if (interactionMask < 1)
        {
            interactionMask = LayerMask.NameToLayer("Interactable");
        }
        interactionPointTrigger = interactionPoint.GetComponent<InteractionPointTrigger>();
        currentSelection = null;
        RegisterEventBindings();
    }

    private void RegisterEventBindings()
    {
        Player.onTransmutation += handleTransmutationChanged;
    }

    private void Update()
    {
        // TODO maybe need to filter out the type of interactable that the player is currently using
        IInteractable selection = interactionPointTrigger.getClosestInteractable();
        if (selection != currentSelection)
        {
            if (currentSelection != null)
            {
                currentSelection.Deselected();
            }

            currentSelection = selection;
            
            if (selection != null)
            {
                selection.Selected();
            }
        }
    }

    public void handleInteractionInput()
    {
        if (currentSelection != null)
        {
            currentSelection.Interact(this);
        }
    }

    public void handleTransmutationChanged(TransmutationBase transmutation)
    {
        if (currentSelection != null)
        {
            currentSelection.Deselected(); 
            currentSelection = null;
        }

        interactionPoint = player.currentTransmutation.transform.Find("interactionPoint");
        if (interactionPoint == null)
        {
            Debug.LogWarning("Current transmutation does not provide an interaction point");
            interactionPointTrigger = null;
        } else
        {
            interactionPointTrigger = interactionPoint.GetComponent<InteractionPointTrigger>();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionPoint.GetComponent<CircleCollider2D>().radius);
    }
}
