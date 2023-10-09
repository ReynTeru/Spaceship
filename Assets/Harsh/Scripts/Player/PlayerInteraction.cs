using System;
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
    public bool bDialogue = false;

    private string[] dialogueLines;
    public string[] initialDialogueLines;
    public GameObject DialogueBox;
    // Start is called before the first frame update
    void Start()
    {
        CurrentInteractionType = InteractionType.None;
        InteractionPrompt.SetActive(false);
        DialogueBox.SetActive(false);
        dialogueLines = initialDialogueLines;
        StartDialogue();
    }

    // Update is called once per frame

    private void Update()
    {
        if (DialogueBox && DialogueBox.activeSelf)
        {
            if (DialogueBox.GetComponent<Dialogue>().bDialougeStopped)
            {
                StopDialogue();
            }
        }
    }

    void LateUpdate()
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
            if (Vector3.Distance(interactableObject.transform.position, interactableLocation) > 0.15f)
            {
                //Debug.Log("Moving");
                interactableObject.transform.position = Vector3.Lerp(interactableObject.transform.position, interactableLocation, Time.deltaTime * 10f);
            }
           
        }
    }
    
    void CheckForInteractable()
    {
        if (!bGrabbed && !bDialogue)
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
    }

    public void InputInteract()
    {
        if (!bDialogue)
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
                            dialogueLines = interactable.GetDialogueLines();
                            StartDialogue();
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
    
    void StartDialogue()
    {
        if (dialogueLines.Length > 0)
        {
            Debug.Log("Starting Dialogue");
            DialogueBox.SetActive(true);
            bDialogue = true; 
            DialogueBox.GetComponent<Dialogue>().lines = null;
            DialogueBox.GetComponent<Dialogue>().lines = dialogueLines;
            DialogueBox.GetComponent<Dialogue>().StartDialogue();
        }
    }
    
    void StopDialogue()
    {
        DialogueBox.SetActive(false);
        bDialogue = false;
        dialogueLines = null;
    }
    
    public void NextLine()
    {
        if (bDialogue)
        {
            DialogueBox.GetComponent<Dialogue>().CallNextLine();
        }
    }
}

public enum InteractionType
{ 
    None,
    Door, 
    Grab,
}
