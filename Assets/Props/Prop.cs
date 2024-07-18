using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Prop : MonoBehaviour, IInteractable
{
    public abstract TransmutationBase transmutation {
        get;
    }

    public void Selected()
    {

    }

    public bool Interact(Interactor interactor)
    {
        return interactor.player.Transmute(transmutation);
    }
}