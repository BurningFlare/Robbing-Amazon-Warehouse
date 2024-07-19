using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class InteractionPointTrigger : MonoBehaviour
{

    List<GameObject> interactables = new List<GameObject>();

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
        float closestDist = Vector2.Distance(transform.position, interactables[0].transform.position);
        GameObject closest = interactables[0];
        foreach (GameObject obj in interactables)
        {
            float distance = Vector2.Distance(obj.transform.position, transform.position);
            if (distance < closestDist)
            {
                closestDist = distance;
                closest = obj;
            }
        }
        return closest;
    }

    public IInteractable getClosestInteractable()
    {
        if (interactables.Count == 0) return null;
        float closestDist = float.MaxValue;
        IInteractable closest = null;
        foreach (GameObject obj in interactables)
        {
            IInteractable interactable = obj.GetComponent<IInteractable>();
            if (interactable != null)
            {
                float distance = Vector2.Distance(obj.transform.position, transform.position);
                if (distance < closestDist)
                {
                    closestDist = distance;
                    closest = interactable;
                }
            }
        }
        return closest;
    }
}
