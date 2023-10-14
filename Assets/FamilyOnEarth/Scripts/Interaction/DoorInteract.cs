using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoorInteract : MonoBehaviour,IInteract
{
    InteractionType interactionType = InteractionType.Door;
    public MeshRenderer meshRenderer;
    
    public GameObject[] Clipboards;
    
    Material material;
    bool bOpen = false;
    bool bAllClipboards = false;
    
    float NotAllClipboardsTimer = 0.0f;
    public float NotAllClipboardsTime = 1.0f;
    bool bNotAllClipboards = false;
    
    public TextMeshProUGUI textComponent;
    [SerializeField] Color InitialColor = new Color(191,0,8);
    [SerializeField] Color FinalColor = new Color(0,191,32);
    Color TargetColor;
    
    int FramesToWait = 5;
    int FramesWaited = 0;
    bool bOpenedOnce = false;
   public bool bIsLastDoor = false;
   public GameObject ClipboardImage;
   
   
    
    // Start is called before the first frame update
    void Start()
    {
        if (meshRenderer)
        {
            material = meshRenderer.material;
        }
        textComponent.color = InitialColor;
        TargetColor = FinalColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (bNotAllClipboards)
        {
            NotAllClipboardsTimer += Time.deltaTime;

            if (textComponent.color == InitialColor)
            {
                TargetColor = FinalColor;
            }
            else if (textComponent.color == FinalColor)
            {
                TargetColor = InitialColor;
            }
            
            FramesWaited++;
            if (FramesWaited > FramesToWait)
            {
                textComponent.color = TargetColor;
                FramesWaited = 0;
            }
            
            
            if (NotAllClipboardsTimer > NotAllClipboardsTime)
            {
                TargetColor = InitialColor;
                FramesWaited = 0;
                textComponent.color = InitialColor;
                bNotAllClipboards = false;
                NotAllClipboardsTimer = 0.0f;
            }
        }
        
        bAllClipboards = CheckAllClipboards();
        //Debug.Log(bAllClipboards);
    }
    
    void IInteract.Interact()
    {
  
        if (bOpen)
        {
            gameObject.GetComponent<Animator>().SetBool("bOpen", false);
            material.SetColor("_EmissionColor",new Color(191,0,8));
            bOpen = false;
        }
        else if (!bAllClipboards)
        {
            bNotAllClipboards = true;
        }
        else if (bAllClipboards)
        {
            gameObject.GetComponent<Animator>().SetBool("bOpen", true);
            material.SetColor("_EmissionColor",new Color(0,191,32));
            bOpen = true;
            if (!bOpenedOnce)
            {
                GameObject.FindWithTag("DoorManager").GetComponent<DoorManager>().DoorOpened();
                bOpenedOnce = true;
            }
           
        }
        if (bIsLastDoor)
        {
            textComponent.gameObject.SetActive(false);
            ClipboardImage.SetActive(false);
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
    
    bool IInteract.GetIsFinal()
    {
        return false;
    }
    
    bool CheckAllClipboards()
    {
        foreach (GameObject clipboard in Clipboards)
        {
            if (!clipboard.GetComponent<GrabInteraction>().bHasInteracted)
            {
                return false;
            }
        }
        return true;
    }
    
}
