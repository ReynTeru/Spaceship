using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController;
    Vector3 playerVelocity;
    public float playerSpeed = 5.0f;
    
    public float gravityValue = -9.81f;
    
    public float jumpHeight = 3.0f;

    private Vector3 movementDirection;
    public GameObject bobTarget;
    public float bobFactor = 0.5f;
    public float bobSpeed = 10f;
    float timer = 0.0f;
    Vector3 bobTargetOriginalPosition;
    public GameObject playerCamera;

    private float cameraOriginalFOV;
    public float cameraZoomedFOV = 30.0f;
    float zoomSpeed = 5.0f;
    bool bZoomed = false;
    
    public float PushForce = 10.0f;
    
    bool bGrabbed = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        bobTargetOriginalPosition = bobTarget.transform.localPosition;
        cameraOriginalFOV = playerCamera.GetComponent<Camera>().fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        bGrabbed = gameObject.GetComponent<PlayerInteraction>().bGrabbed;
        if (bZoomed)
        {
            playerCamera.GetComponent<Camera>().fieldOfView = Mathf.Lerp(playerCamera.GetComponent<Camera>().fieldOfView, cameraZoomedFOV, Time.deltaTime * zoomSpeed);
        }
        else
        {
            playerCamera.GetComponent<Camera>().fieldOfView = Mathf.Lerp(playerCamera.GetComponent<Camera>().fieldOfView, cameraOriginalFOV, Time.deltaTime * zoomSpeed);
        }
        HeadBob();
    }
    
    public void PrecessMovement(float longitudinalInput, float lateralInput)
    {
        if (!bGrabbed)
        {
            movementDirection = new Vector3(lateralInput, 0.0f, longitudinalInput);
            characterController.Move(transform.TransformDirection(movementDirection) * playerSpeed * Time.deltaTime);
        }
        
        playerVelocity.y += gravityValue * Time.deltaTime;

        if (characterController.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        characterController.Move(playerVelocity * Time.deltaTime);
    }
    
    public void ProcessJump()
    {
        if (characterController.isGrounded && !bGrabbed)
        {
            playerVelocity.y = 0f;
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2f * gravityValue);
        }
    }
    
    void HeadBob()
    {
        if (characterController.isGrounded && movementDirection.magnitude > 0 && !bGrabbed)
        {
            timer += Time.deltaTime * bobSpeed;
            bobTarget.transform.localPosition = new Vector3(0.0f, Mathf.Sin(timer) * bobFactor + bobTargetOriginalPosition.y, 0.0f);
            //Debug.Log(Mathf.Sin(timer));
        }
        else
        {
            timer = 0.0f;
            bobTarget.transform.localPosition = Vector3.Lerp(bobTarget.transform.localPosition, bobTargetOriginalPosition, Time.deltaTime * bobSpeed);
        }
    }
    
    public void Zoom()
    {
        bZoomed = true;
    }

    public void StopZoom()
    {
        bZoomed = false;
    }
}
