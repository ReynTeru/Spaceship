using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteract : MonoBehaviour,IInteract
{
    InteractionType interactionType = InteractionType.Door;
    public MeshRenderer meshRenderer;
    Material material;
    bool bOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        if (meshRenderer)
        {
            material = meshRenderer.material;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void IInteract.Interact()
    {
        if (bOpen)
        {
            gameObject.GetComponent<Animator>().SetBool("bOpen", false);
            material.SetColor("_EmissionColor",new Color(191,0,8));
            bOpen = false;
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("bOpen", true);
            material.SetColor("_EmissionColor",new Color(0,191,32));
            bOpen = true;
        }
    }
    
    void IInteract.StopInteract()
    {
        gameObject.GetComponent<Animator>().SetBool("bOpen", false);
        material.SetColor("_EmissionColor",new Color(191,0,8));
    }
    
    InteractionType IInteract.GetInteractionType()
    {
        return interactionType;
    }
    
    string[] IInteract.GetDialogueLines()
    {
        return null;
    }
}
