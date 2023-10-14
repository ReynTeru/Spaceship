using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessInput : MonoBehaviour
{
    InputMapping inputMapping;
    InputMapping.MovementMapActions movementMapActions;
    PlayerLook playerLook;
    PlayerMovement playerMovement;
    PlayerInteraction playerInteraction;
  
    
    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        inputMapping = new InputMapping();
        movementMapActions = inputMapping.MovementMap;
        playerLook = GetComponent<PlayerLook>();
        playerMovement = GetComponent<PlayerMovement>();
        playerInteraction = GetComponent<PlayerInteraction>();
        movementMapActions.Jump.started +=ctx=> playerMovement.ProcessJump();
        movementMapActions.Zoom.started += ctx => playerMovement.Zoom();
        movementMapActions.Zoom.canceled += ctx => playerMovement.StopZoom();
        movementMapActions.Interact.started += ctx => playerInteraction.InputInteract();
        movementMapActions.NextDialog.started += ctx => playerInteraction.NextLine();
    }

    void Start()
    {
        playerLook.ProcessLook(0f, 0f);
    }
    // Update is called once per frame
    void Update()
    {
        playerMovement.PrecessMovement(movementMapActions.LongMovement.ReadValue<float>(), movementMapActions.LateralMovement.ReadValue<float>());
        playerLook.ProcessLook(movementMapActions.LookHorizontal.ReadValue<float>(), movementMapActions.LookVertical.ReadValue<float>());
    }
    
    void OnEnable()
    {
        movementMapActions.Enable();
    }
    void OnDisable()
    {
        movementMapActions.Disable();
    }
}
