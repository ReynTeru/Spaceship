using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionDistance = 2.0f;
    public GameObject interactableLocationTarget;
    
    public Vector3 interactableLocation;
    InteractionType CurrentInteractionType;
    bool bInteractionPrompt = false;
    
    public GameObject InteractionPrompt;
    IInteract interactable;
    GameObject interactableObject;
    public GameObject CameraRef;
    
    public bool bGrabbed = false;
    // Start is called before the first frame update
    void Start()
    {
        CurrentInteractionType = InteractionType.None;
        InteractionPrompt.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        interactableLocation = interactableLocationTarget.transform.position;
        CheckForInteractable();

        if (CurrentInteractionType != InteractionType.None && !bInteractionPrompt)
        {
            InteractionPrompt.SetActive(true);
            bInteractionPrompt = true;
        }
        else if (CurrentInteractionType == InteractionType.None && bInteractionPrompt)
        {
            InteractionPrompt.SetActive(false);
            bInteractionPrompt = false;
        }

        if (bGrabbed)
        {
            if (Vector3.Distance(interactableObject.transform.position, interactableLocation) > 0.1f)
            {
                interactableObject.transform.position = Vector3.Lerp(interactableObject.transform.position, interactableLocation, Time.deltaTime * 5.0f);
            }
           
        }
    }
    
    void CheckForInteractable()
    {
        Ray ray = new Ray(CameraRef.transform.position, CameraRef.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactionDistance, LayerMask.GetMask("Interact")))
        {
            interactable = hit.collider.gameObject.GetComponent<IInteract>();
            if (interactable != null)
            {
                interactableObject = hit.collider.gameObject;
                CurrentInteractionType = interactable.GetInteractionType();
            }
            else
            {
                CurrentInteractionType = InteractionType.None;
            }
        }
        else
        {
            CurrentInteractionType = InteractionType.None;
        }
        
    }

    public void InputInteract()
    {
        if (bInteractionPrompt && CurrentInteractionType != InteractionType.None && interactable != null)
        {
            switch (CurrentInteractionType)
            {  
                case InteractionType.Door:
                    interactable.Interact();
                    break;
                case InteractionType.Grab:
                    if (bGrabbed)
                    {
                        interactable.StopInteract();
                        bGrabbed = false;
                    }
                    else
                    {
                        interactable.Interact();
                        bGrabbed = true;
                    }
                    break;
                default:
                    break;
                
            }
        }
    }
}

public enum InteractionType
{ 
    None,
    Door, 
    Grab,
}
