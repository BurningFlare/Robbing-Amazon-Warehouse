using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class InteractionPointTrigger : MonoBehaviour
{

    [SerializeField] List<GameObject> interactables = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        interactables.Add(collision.gameObject);
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        interactables.Remove(collision.gameObject);
    }

    public GameObject getClosest()
    {
        if (interactables.Count == 0) return null;
        float closestSqrDist = Vector2.SqrMagnitude(interactables[0].transform.position - transform.position);
        GameObject closest = interactables[0];
        foreach (GameObject obj in interactables)
        {
            float sqrDistance = Vector2.SqrMagnitude(interactables[0].transform.position - transform.position);
            if (sqrDistance < closestSqrDist)
            {
                closestSqrDist = sqrDistance;
                closest = obj;
            }
        }
        return closest;
    }

    public IInteractable getClosestInteractable()
    {
        if (interactables.Count == 0) return null;
        float closestSqrDist = float.MaxValue;
        IInteractable closest = null;
        foreach (GameObject obj in interactables)
        {
            IInteractable interactable = obj.GetComponent<IInteractable>();
            if (interactable != null)
            {
                float sqrDistance = Vector2.SqrMagnitude(obj.transform.position - transform.position);
                if (sqrDistance < closestSqrDist)
                {
                    closestSqrDist = sqrDistance;
                    closest = interactable;
                }
            }
        }
        return closest;
    }
}
