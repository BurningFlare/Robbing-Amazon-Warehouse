using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] public Transform interactionPoint;
    [SerializeField] public Player player;
    [SerializeField] private float interactionRadius = 1f;
    [SerializeField] private LayerMask interactionMask;

    private readonly Collider2D[] interactableColliders = new Collider2D[3];
    [SerializeField] private int numFound;

    private void Update()
    {
        numFound = Physics2D.OverlapCircleNonAlloc(interactionPoint.position, interactionRadius, interactableColliders, interactionMask);
        Debug.Log(interactionPoint.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionRadius);
    }
}
