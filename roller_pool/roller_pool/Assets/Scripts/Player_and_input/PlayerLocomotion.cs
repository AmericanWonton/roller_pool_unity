using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    InputManager inputManager;

    public Vector3 moveDirection;
    Transform cameraObject;

    public Rigidbody playerRigidBody;
    public bool isOnGround = true;
    public float movementSpeed = 5.0f;
    public float rotationSpeed = 15.0f;
    public float jumpForceModifier = 1500.0f;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerRigidBody = GetComponent<Rigidbody>();
        /* This will scan the scene for the camera tagged 'main camera' */
        cameraObject = Camera.main.transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRigidBody.AddForce(Vector3.up * jumpForceModifier, ForceMode.Impulse);
            isOnGround = false;
        }
    }

    /* This handles all movement for our player */
    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
        HandleJumping();
    }
    //Handles general movement left/right and up/down
    private void HandleMovement() 
    {
        /* Move the character forward, in the direction the camera is faceing, 
        multiplied by up or down, depending on the keys we are hitting.
        
        We start by getting the forward movement,(it can be none, if character isn't pressing up).
        Then, we get right with 'forward movment' plus 'right movement'  */
        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        //All this does is keeps the direction the same, but puts the length to 1
        //moveDirection.Normalize();
        //We want our character to jump, to y is kept in
        //moveDirection.y = 0;
        //moveDirection = moveDirection * movementSpeed;

        Vector3 movementVelocity = moveDirection;
        playerRigidBody.AddForce(movementVelocity * movementSpeed * inputManager.verticalInput);
        //playerRigidBody.velocity = movementVelocity;
    }

    //Handles Rotation...not sure if needed for rollerpool
    private void HandleRotation()
    {
        //0 on all values, starting out
        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        /* This will keep our character facing the same direction they were when they stopped */
        if (targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }

        /* These are used to calculate rotations in Unity. 
        We start by looking towards our camera direction...where we are looking, that's
        where we want to rotate */
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        /* We use Time.deltaTime to accommodate for different framerate speeds.
        We want to rotate at a contstant rate */
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); //Rotation between point a and b

        transform.rotation = playerRotation;
    }

    /* Funciton for handling jumping */
    public void HandleJumping()
    {
        /*
        if (inputManager.jumpInput == true && isOnGround == true)
        {
            Debug.Log("We're jumping...");
            inputManager.jumpInput = false;
            isOnGround = false;
            playerRigidBody.AddForce(Vector3.up * jumpForceModifier, ForceMode.Impulse);
        }
        */
    }

    /* Detect to see if the player has collided with the ground to prevent jumping */
    private void OnCollisionEnter(Collision collision)
    {
        inputManager.jumpInput = true;
        isOnGround = true;
    }


}
