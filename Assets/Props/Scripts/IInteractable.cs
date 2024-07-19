using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    // TODO change these to the prompt props should get when they pop up
    public string InteractionPrompt => "hi";
    public Sprite InteractionSprite => Sprite.Create(Texture2D.whiteTexture, new Rect(0, 0, 10, 10), Vector2.zero);

    public void Selected();

    public bool Interact(Interactor interactor);
}