using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    PlayerLocomotion playerLocomotion;
    public Vector2 movement;
    public Vector2 cameraInput;

    public float cameraInputX;
    public float cameraInputY;

    public float verticalInput;
    public float horizontalInput;
    public bool jumpInput = true;

    /* When the game object attached to this script is enabled, 
    we will be doing logic */
    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            /* If you move any of those keys, those values will be fed to 'movement' and 'camerainput' variables */
            playerControls.PlayerMovement.Movement.performed += i => movement = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            /* Jump Button Controls */
            playerControls.PlayerActions.Jump.performed += i => jumpInput = true;
        }

        playerControls.Enable();

    }

    //If our controller input is disabled, we turn off player controls
    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleJumpInput();
        //Handle Start Menu Input();
    }
    private void HandleMovementInput(){
        /* Charater movement */
        verticalInput = movement.y;
        horizontalInput = movement.x;

        /* Camera Movement */
        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;
    }

    /* Handles Jumping */
    private void HandleJumpInput()
    {
        if (jumpInput == true)
        {
            /* We don't want to jump twice, set this to false */
            jumpInput = false;
        }
    }
}
