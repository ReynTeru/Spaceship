using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabInteraction : MonoBehaviour, IInteract
{
    bool bGrabbed = false;
    GameObject PlayerRef;
    float LetGoImpulse = 5f;
    public bool bIsFinal = false;
    
    public bool bHasInteracted = false;
    public bool bIsClipboard = false;
    
    public string[] dialogueLines;
    // Start is called before the first frame update
    void Start()
    {
        PlayerRef = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (bGrabbed)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(-PlayerRef.transform.forward), Time.deltaTime * 10.0f);
        }
    }
    
    void IInteract.Interact()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        bGrabbed = true;
        if (bIsClipboard)
        {
            GameObject.FindWithTag("DoorManager").GetComponent<DoorManager>().AddClipboard();
        }
        
    }
    
    void IInteract.StopInteract()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.GetComponent<Rigidbody>().AddForce(PlayerRef.transform.forward * 5.0f, ForceMode.Impulse);
        Vector3 fRandom = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        //fRandom = fRandom.normalized;
        
        gameObject.GetComponent<Rigidbody>().AddTorque(fRandom* LetGoImpulse, ForceMode.Impulse);
     
        bGrabbed = false;
        gameObject.layer = default;
        bHasInteracted = true;
    }
    
    InteractionType IInteract.GetInteractionType()
    {
        return InteractionType.Grab;
    }
    
    string[] IInteract.GetDialogueLines()
    {
        return dialogueLines;
    }
    
    bool IInteract.GetIsFinal()
    {
        return bIsFinal;
    }
}
