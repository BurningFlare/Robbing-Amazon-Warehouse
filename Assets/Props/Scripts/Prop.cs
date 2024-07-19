using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Prop : MonoBehaviour, IInteractable
{
    [SerializeField] public TransmutationBase transmutation;

    public void Selected()
    {

    }

    public bool Interact(Interactor interactor)
    {
        return interactor.player.Transmute(transmutation);
    }
}