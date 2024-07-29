using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
[RequireComponent(typeof(Collider2D))]
public class MerchBase : MonoBehaviour, IInteractable
{
    private Outline outline;
    [SerializeField] public int weight;
    // honestly I'm not sure where the min and max costs should go since they feel like they definitely should not be included in every instance
    // but if I make them static then they're the same for every single merchbase which is not what I want and they wouldn't be editable in the inspector
    // and then I feel like making them a property getter is kinda adding an extra layer for no reason and then they'd also not be editable unless you serialize the field
    [SerializeField] protected int minCost;
    [SerializeField] protected int maxCost;
    [SerializeField] public int cost;
    public string displayCost;

    private void Awake()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
        if (cost == 0)
        {
            cost = Random.Range(minCost, maxCost + 1); // the + 1 makes it inclusive
        }
        displayCost = (cost - 0.01f).ToString("F2"); // makes the decimal portion 0.99 to trick your monkey brains into thinking it's worth less than it actually is
    }
    
    public void Selected()
    {
        // TODO add gui item that shows the price and weight of the item
        outline.enabled = true;
    }

    public void Deselected()
    {
        outline.enabled = false;
    }

    public bool Interact(Interactor interactor)
    {
        interactor.player.addToInventory(this);
        gameObject.SetActive(false);
        return true;
    }

    public override string ToString()
    {
        return "Type: " + GetType() + ", cost: " + cost + ", weight: " + weight;
    }
}
