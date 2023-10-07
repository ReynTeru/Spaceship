using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabInteraction : MonoBehaviour, IInteract
{
    bool bGrabbed = false;
    GameObject PlayerRef;
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
    }
    
    void IInteract.StopInteract()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        bGrabbed = false;
    }
    
    InteractionType IInteract.GetInteractionType()
    {
        return InteractionType.Grab;
    }
}
