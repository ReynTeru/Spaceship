using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWin : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject TargetA;
    public GameObject TargetC;
    public GameObject Planet;
    
    GameObject Target;
    float transitionSpeed = 1.0f;
    
    public bool bWon = false;
    bool bTransitionComplete = false;
    float FinalFov = 120f;
    public GameObject FinalDialogue;
    
    
    
    void Start()
    {
        Target = TargetA;
    }

    private void Update()
    {
        if (bWon && !bTransitionComplete)
        {
            transform.position = Vector3.Lerp(transform.position, Target.transform.position, Time.deltaTime * transitionSpeed);
            
            if (Target == TargetC)
            {
                transform.LookAt(Planet.transform.position);
                //gameObject.GetComponent<Camera>().fieldOfView = Mathf.Lerp(gameObject.GetComponent<Camera>().fieldOfView, FinalFov, Time.deltaTime * transitionSpeed);
            }
            else
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Target.transform.rotation, Time.deltaTime * transitionSpeed);
            }
            if (Vector3.Distance(transform.position, Target.transform.position) < 0.05f)
            {
                if (Target == TargetA)
                {
                    Target = TargetC;
                }
                else if (Target == TargetC)
                {
                    bTransitionComplete = true;
                    FinalDialogue.SetActive(true);
                    FinalDialogue.GetComponent<FinalDialogue>().StartDialogue();
                }
            }
        }
    }
}
