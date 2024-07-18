using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public string InteractionPrompt { get; }
    public Sprite InteractionSprite { get; }

    public void Selected();

    public bool Interact();
}