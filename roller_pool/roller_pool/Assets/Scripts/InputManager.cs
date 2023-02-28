using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    public Vector2 movement;

    public float verticalInput;
    public float horizontalInput;

    /* When the game object attached to this script is enabled, 
    we will be doing logic */
    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();


            playerControls.PlayerMovement.Movement.performed += i => movement = i.ReadValue<Vector2>();
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
        //Handle Jump INput()
        //Handle Start Menu Input();
    }
    private void HandleMovementInput(){
        verticalInput = movement.y;
        horizontalInput = movement.x;
    }
}
