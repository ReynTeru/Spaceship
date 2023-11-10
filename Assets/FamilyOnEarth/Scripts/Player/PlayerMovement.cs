using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    //CharacterController characterController;
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
    bool bDialogue = false;
    bool bIsFinal = false;
    
    private EMovementState movementState = EMovementState.NormalGravity;
    public EMovementState GetMovementState()
    {
        return movementState;
    }
    
    bool bIsGettingUp = false;
    float gettingUpTimer = 0.0f;
    [SerializeField] float gettingUpTime = 2.5f;
    Vector3 PositionAfterGettingUp;
    Vector3 PositionBeforeGettingUp;
    Quaternion RotationBeforeGettingUp;
    Quaternion RotationAfterGettingUp;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        //characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        bobTargetOriginalPosition = bobTarget.transform.localPosition;
        cameraOriginalFOV = playerCamera.GetComponent<Camera>().fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        bDialogue = gameObject.GetComponent<PlayerInteraction>().bDialogue;
        bGrabbed = gameObject.GetComponent<PlayerInteraction>().bGrabbed;
        if (bDialogue || bGrabbed)
        {
            SwitchMovementState(EMovementState.Interacting);
        }
        else if (movementState == EMovementState.Interacting)
        {
            SwitchMovementState(EMovementState.NormalGravity);
        }
        bIsFinal = gameObject.GetComponent<PlayerInteraction>().bWon;
        if (bZoomed)
        {
            playerCamera.GetComponent<Camera>().fieldOfView = Mathf.Lerp(playerCamera.GetComponent<Camera>().fieldOfView, cameraZoomedFOV, Time.deltaTime * zoomSpeed);
        }
        else
        {
            playerCamera.GetComponent<Camera>().fieldOfView = Mathf.Lerp(playerCamera.GetComponent<Camera>().fieldOfView, cameraOriginalFOV, Time.deltaTime * zoomSpeed);
        }
        HeadBob();
        
        if (bIsGettingUp)
        {
            gettingUpTimer += Time.deltaTime;
            float gettingUpPercentage = gettingUpTimer / gettingUpTime;
            Debug.Log(gettingUpPercentage);
            transform.rotation = Quaternion.Lerp(RotationBeforeGettingUp, RotationAfterGettingUp, gettingUpPercentage);
            transform.position = Vector3.Lerp(PositionBeforeGettingUp, PositionAfterGettingUp, gettingUpPercentage);
            if (gettingUpTimer >= gettingUpTime)
            {
                gettingUpTimer = 0.0f;
                bIsGettingUp = false;
            }
        }
    }
    
    public void PrecessMovement(float longitudinalInput, float lateralInput)
    {
        switch (movementState)
        {
            case EMovementState.NormalGravity:
            {
                if (!bIsFinal && !bIsGettingUp)
                {
                    movementDirection = new Vector3(lateralInput, 0.0f, longitudinalInput);
                    rb.velocity = transform.TransformDirection(movementDirection * playerSpeed * Time.deltaTime);
                }
        
                playerVelocity.y += gravityValue * Time.deltaTime;

                if (IsGrounded() && playerVelocity.y < 0)
                {
                    playerVelocity.y = 0f;
                }
                rb.velocity += playerVelocity;
            } 
                break;

            case EMovementState.Interacting:
            {
                playerVelocity.y += gravityValue * Time.deltaTime;

                if (IsGrounded() && playerVelocity.y < 0)
                {
                    playerVelocity.y = 0f;
                }
                rb.velocity += playerVelocity;
            }
                break;
            case EMovementState.GravityLess:
            {
                
            }
                break;
        }
    }
    
    public void ProcessJump()
    {
        switch (movementState)
        {
            case EMovementState.NormalGravity:
            {
                if (IsGrounded() && !bIsFinal)
                {
                    playerVelocity.y = 0f;
                    playerVelocity.y += Mathf.Sqrt(jumpHeight * -2f * gravityValue);
                }
            } 
                break;

            case EMovementState.Interacting:
            {
                
            }
                break;
            case EMovementState.GravityLess:
            {
                
            }
                break;
        }
    }
    
    void HeadBob()
    {
        switch (movementState)
        {
            case EMovementState.NormalGravity:
            {
                if (IsGrounded() && movementDirection.magnitude > 0 && !bIsGettingUp)
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
                break;

            case EMovementState.Interacting:
            {
                
            }
                break;
            case EMovementState.GravityLess:
            {
                
            }
                break;
        }
    }
    
    bool IsGrounded()
    {
        Debug.DrawRay(transform.position, Vector3.down*((gameObject.GetComponent<CapsuleCollider>().height/2) + 0.1f), Color.red);
        return Physics.Raycast(transform.position, Vector3.down, (gameObject.GetComponent<CapsuleCollider>().height/2) + 0.1f);
    }
    
    public void Zoom()
    {
        bZoomed = true;
    }

    public void StopZoom()
    {
        bZoomed = false;
    }
    
    public void SwitchMovementState(EMovementState newState)
    {
        switch (newState)
        {
            case EMovementState.NormalGravity:
            {
                switch(movementState)
                {
                    case EMovementState.Interacting:
                    {
                       
                    }
                        break;
                    case EMovementState.GravityLess:
                    {
                       rb.constraints = RigidbodyConstraints.FreezeRotation;
                       bIsGettingUp = true;
                       
                       if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 100f))
                       {
                            PositionAfterGettingUp = hit.point + Vector3.up * (gameObject.GetComponent<CapsuleCollider>().height/2 + 0.1f);
                       }
                       else
                       {
                           transform.position = PositionAfterGettingUp;
                       }
                       PositionBeforeGettingUp = transform.position; 
                       RotationBeforeGettingUp = transform.rotation;
                       Vector3 fUp = Vector3.up;
                       Vector3 fForward = Vector3.Cross(transform.right, fUp);
                       RotationAfterGettingUp = Quaternion.LookRotation(fForward, fUp);
                    }
                        break;
                }
            }
                break;
            case EMovementState.Interacting:
            {
                
            }
                break;
            case EMovementState.GravityLess:
            {
                switch (movementState)
                {
                    case EMovementState.NormalGravity:
                    {
                        rb.constraints = RigidbodyConstraints.None;
                    }
                        break;
                    case EMovementState.Interacting:
                    {
                        
                    }
                        break;
                }
                //rb.AddForce(Vector3.up * 10.0f, ForceMode.Impulse);
            }
                break;
        }
        movementState = newState;
        Debug.Log(movementState);
    }
}

public enum EMovementState
{
    NormalGravity,
    Interacting,
    GravityLess,
}