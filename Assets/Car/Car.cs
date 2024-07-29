using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class Car : MonoBehaviour, IInteractable
{
    private Outline outline;
    private void Awake()
    {
        outline = GetComponent<Outline>();
    }
    public void Deselected()
    {
        outline.enabled = false;
    }

    public bool Interact(Interactor interactor)
    {
        return GameManager.Instance.endDay();
    }

    public void Selected()
    {
        outline.enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
