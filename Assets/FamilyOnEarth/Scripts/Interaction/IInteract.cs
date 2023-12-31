using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteract
{
    public void Interact();
    public void StopInteract();
    InteractionType GetInteractionType();
    
    string[] GetDialogueLines();
    
    bool GetIsFinal();
}
