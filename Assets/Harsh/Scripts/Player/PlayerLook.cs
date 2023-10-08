using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float horizontalLookSenstivity = 30.0f;
    public float verticalLookSenstivity = 30.0f;
    
    public GameObject playerCamera;

    private float verticalRotation = 0.0f;
    
    bool bLookDownDelay = false;
    float lookDownDelayTimer = 0.0f;
    float lookDownDelayTime = 0.5f;
    float clampAngle = 80.0f;
    float currentClampAngle = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!bLookDownDelay)
        {
            lookDownDelayTimer += Time.deltaTime;
            if (lookDownDelayTimer >= lookDownDelayTime)
            {
                bLookDownDelay = true;
            }
        }
    }
    
    public void ProcessLook(float horizontalLookInput, float verticalLookInput)
    {
        if (!bLookDownDelay)
        {
            currentClampAngle = 0.0f;
        }
        else
        {
            currentClampAngle = clampAngle;
        }
        verticalRotation -= verticalLookInput * verticalLookSenstivity * Time.deltaTime;
        verticalRotation = Mathf.Clamp(verticalRotation, -80.0f, currentClampAngle);
        
        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0.0f, 0.0f);
        transform.Rotate(Vector3.up * horizontalLookInput * horizontalLookSenstivity * Time.deltaTime);
    }
}
