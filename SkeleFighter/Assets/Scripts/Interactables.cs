using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string promptMessage;
    public bool useEvents;
    public void baseInteract()
    {
        if (useEvents)
            GetComponent<InteractionEvents>().onInteract.Invoke();
        Interact();
    }
    protected virtual void Interact()
    {

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
