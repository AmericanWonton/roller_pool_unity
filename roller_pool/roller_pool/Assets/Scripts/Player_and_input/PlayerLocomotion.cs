using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    public InputManager inputManager;
    PhysicsHelperPlayer physicsHelperPlayer;

    public Vector3 moveDirection;
    Transform cameraObject;

    public Rigidbody playerRigidBody;
    public bool isOnGround = true;
    private float maxCharacterVelocity = 30.0f;
    public float movementSpeed = 15.0f;
    public float rotationSpeed = 15.0f;
    public float jumpForceModifier = 10.0f;
    public float smoothTimeStop = 0.3F;
    public float stopVelocitySpeed = 5.0f;
    public float angularDragNormal = 0.05f;

    public float forceMultiplier = 0.01f;
    private PhysicMaterial playerPhysicalMaterial;
    public float playerDynamicFriction = 0.6f;
    public float airSpeedDecrease = 5.0f;

    /* DEBUG Variables */
    public float horizontalInput;
    public float verticalInput;
    public float playerCurrentSpeed;
    public float AngularSpeed;
    public float DecreaseSpeedAmount = 0.1f;
    public float DecreaseSpeedTime = 5.5f;

    private void Awake()
    {
        physicsHelperPlayer = GetComponent<PhysicsHelperPlayer>();
        inputManager = GetComponent<InputManager>();
        playerRigidBody = GetComponent<Rigidbody>();
        playerPhysicalMaterial = GetComponent<SphereCollider>().material;
        /* This will scan the scene for the camera tagged 'main camera' */
        cameraObject = Camera.main.transform;
        
    }

    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRigidBody.AddForce(Vector3.up * jumpForceModifier, ForceMode.Impulse);
        }
        
        //DEBUG
        playerCurrentSpeed = playerRigidBody.velocity.magnitude;
        AngularSpeed = playerRigidBody.angularVelocity.magnitude;
        horizontalInput = inputManager.horizontalInput;
        verticalInput = inputManager.verticalInput;
    }

    /* This handles all movement for our player */
    public void HandleAllMovement()
    {
        KeepVelocityTame();
        HandleMovement();
        HandleRotation();
        HandleJumping();
        BallStoppage();
    }

    //Checks to see if ball is on the ground 

    //Keeps the velocity below a certain speed for this object
    private void KeepVelocityTame()
    {
        physicsHelperPlayer.objectLerpSpeed(playerRigidBody, maxCharacterVelocity);
    }

    //Handles general movement left/right and up/down
    private void HandleMovement() 
    {
        /* Move the character forward, in the direction the camera is faceing, 
        multiplied by up or down, depending on the keys we are hitting.
        
        We start by getting the forward movement,(it can be none, if character isn't pressing up).
        Then, we get right with 'forward movment' plus 'right movement'  */
        moveDirection = (cameraObject.forward * inputManager.verticalInput) + (cameraObject.right * inputManager.horizontalInput);
        //moveDirection = cameraObject.forward * inputManager.verticalInput;
        //moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        //All this does is keeps the direction the same, but puts the length to 1
        //moveDirection.Normalize();
        //We want our character to jump, to y is kept in
        moveDirection.y = 0;
        //moveDirection = moveDirection * movementSpeed;

        Vector3 movementVelocity = moveDirection;
        
        /* Add a lessened amount of force if player is in the air; if not, make it normal force */
        if (isOnGround)
        {
            playerRigidBody.AddForce(movementVelocity * movementSpeed);
        } else {
            float newMovementSpeed = movementSpeed / airSpeedDecrease;
            playerRigidBody.AddForce(movementVelocity * newMovementSpeed);
        }
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

        /* We can un-comment this for other games...*/
        //transform.rotation = playerRotation;
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

    /* Brings the ball to a stop quicker. Player must not be touching 
    controls */
    private void BallStoppage()
    {
        if ((inputManager.verticalInput == 0 && inputManager.horizontalInput == 0) && (playerCurrentSpeed <= stopVelocitySpeed) && (playerCurrentSpeed > 0.0f))
        {
            /* Slow the ball to a stop with friction */
            GetComponent<Rigidbody>().angularDrag = 10.0f;
        } else {
            //Set ball dynamic friction back to what it was after stopping it
            GetComponent<Rigidbody>().angularDrag = angularDragNormal;
        }
    }


}
