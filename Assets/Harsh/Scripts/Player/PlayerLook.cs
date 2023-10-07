using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float horizontalLookSenstivity = 30.0f;
    public float verticalLookSenstivity = 30.0f;
    
    public GameObject playerCamera;

    private float verticalRotation = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        verticalRotation = 45f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ProcessLook(float horizontalLookInput, float verticalLookInput)
    {
        verticalRotation -= verticalLookInput * verticalLookSenstivity * Time.deltaTime;
        verticalRotation = Mathf.Clamp(verticalRotation, -80.0f, 80.0f);
        
        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0.0f, 0.0f);
        transform.Rotate(Vector3.up * horizontalLookInput * horizontalLookSenstivity * Time.deltaTime);
    }
}
