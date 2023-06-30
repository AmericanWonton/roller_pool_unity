using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    InputManager inputManager;
    //Object our camera will follow
    public Transform targetTransform;
    public Transform cameraPivot; //The object the camera uses to pivot 
    public Transform cameraTransform; //The transform of the actual camera obejct in the scene
    public LayerMask collisionLayers; //The layers we want our camera to collide with
    private float defaultPosition; 
    private Vector3 cameraFollowVelocity = Vector3.zero;
    private Vector3 cameraVectorPosition;
    public float cameraInterpolateTime = 0.1f;
    public float cameraCollisionOffset = 0.4f; //How much the camera will jump off of objects it's colliding with
    public float minimumCollisionOffset = 0.1f;
    public float cameraCollisionRadius = 0.1f;
    public float cameraFollowSpeed = 0.1f;
    public float cameraLookSpeed = 2.0f;
    public float cameraPivotSpeed = 2.0f;

    public float lookAngle; //Camera Looking Up
    public float pivotAngle; //Camera looking left/right
    public float minimumPivotAngle = -20.0f; //Minimum amount of pivot for the camera
    public float maximumPivotAngle = 25.0f; //Maximum amount of pivot for the camera

    private void Awake()
    {
        //Get the transform of the player
        targetTransform = FindObjectOfType<PlayerManager>().transform;
        inputManager = FindObjectOfType<InputManager>();
        cameraTransform = Camera.main.transform;
        defaultPosition = cameraTransform.localPosition.z;
    }

    /* Handles all movement inputs of the camera */
    public void HandleAllCameraMovement()
    {
        FollowTarget();
        RotateCamera();
        HandleCameraCollisions();
    }

    /* This is used for having the camera follow the player */
    private void FollowTarget()
    {
        /*
        Used to move something 'softly' from one location to another...
        like a camera to a player. So, every frame, our this will be called to move our camera to our player's position
        */
        Vector3 adjusted_targetTransform = new Vector3(targetTransform.position.x, targetTransform.position.y, (targetTransform.position.z));
        Vector3 targetPosition = Vector3.SmoothDamp
        (transform.position, adjusted_targetTransform, ref cameraFollowVelocity, cameraFollowSpeed);

        transform.position = targetPosition;
    }

    /* used for rotating the camera */
    private void RotateCamera()
    {
        Vector3 rotation;
        Quaternion targetRotation;

        lookAngle = lookAngle + (inputManager.cameraInputX * cameraLookSpeed);
        pivotAngle = pivotAngle - (inputManager.cameraInputY * cameraPivotSpeed);

        /* Clamping the limit of how much we can rotate on Y axis.
        Clamp means it has to be at least something...but no more than something */
        pivotAngle = Mathf.Clamp(pivotAngle, minimumPivotAngle, maximumPivotAngle);
        /* Below does this
        1.) sets a rotation variable to zero
        2.) Sets the rotation of y, then x axis to the look angle.
        3.) Does calculation to get the rotation of the last rotation to the rotation we want to 
        rotate to
        */
        rotation = Vector3.zero;
        rotation.y = lookAngle;
        targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);

        /* Make sure you use 'localRotation'...that's the object's local 
        rotation, not the rotation in the world space */
        cameraPivot.localRotation = targetRotation;

    }
    
    /* Handles camera collisions. This will
    push the camera forward or backward if it hits an object */
    private void HandleCameraCollisions()
    {
        float targetPosition = defaultPosition;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - cameraPivot.position;
        direction.Normalize();

        /* spheracast creates a sphere around an object.
        It creates this with a radius of cameraCollisionRadius. It fires it in the
        direction we made above, we use this RayCast 'hit' variable to store what this 
        cast hit. We can do stuff with it*/
        if (Physics.SphereCast
        (cameraPivot.transform.position, cameraCollisionRadius, direction, 
        out hit, Mathf.Abs(targetPosition), collisionLayers))
        {   
            /* This is the distance between our camera pivot position and the thing 
            that we hit */
            float distance = Vector3.Distance(cameraPivot.position, hit.point);
            targetPosition =- (distance - cameraCollisionOffset);
        }


        if (Mathf.Abs(targetPosition) < minimumCollisionOffset)
        {
            targetPosition = targetPosition - minimumCollisionOffset;
        }

        cameraVectorPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, cameraInterpolateTime);
        cameraTransform.localPosition = cameraVectorPosition;
    }
}
