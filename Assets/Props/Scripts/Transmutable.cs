using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
[RequireComponent(typeof(Collider2D))]
public abstract class Transmutable : MonoBehaviour, IInteractable
{
    // this is the class that you will transform into
    [SerializeField] public TransmutationBase transmutation;
    private Outline outline;
    private void Awake()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    public void Selected()
    {
        outline.enabled = true;
    }

    public void Deselected()
    {
        outline.enabled = false; 
    }

    public bool Interact(Interactor interactor)
    {
        return interactor.player.Transmute(transmutation);
    }

    private void OnDestroy()
    {
        foreach (Transform child in transform)
        {
            Destroy(child);
        }
    }
}