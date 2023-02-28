using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    InputManager inputManager;
    //Object our camera will follow
    public Transform targetTransform;
    public Transform cameraPivot; //The object the camera uses to pivot 
    private Vector3 cameraFollowVelocity = Vector3.zero;
    public float cameraFollowSpeed = 0.2f;
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
    }

    /* Handles all movement inputs of the camera */
    public void HandleAllCameraMovement()
    {
        FollowTarget();
        RotateCamera();
    }

    /* This is used for having the camear follow the player */
    private void FollowTarget()
    {
        /*
        Used to move something 'softly' from one location to another...
        like a camera to a player. So, every frame, our this will be called to move our camera to our player's position
        */
        Vector3 targetPosition = Vector3.SmoothDamp
        (transform.position, targetTransform.position, ref cameraFollowVelocity, cameraFollowSpeed);

        transform.position = targetPosition;
    }

    /* used for rotating the camera */
    private void RotateCamera()
    {
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
        Vector3 rotation = Vector3.zero;
        rotation.y = lookAngle;
        Quaternion targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);

        /* Make sure you use 'localRotation'...that's the object's local 
        rotation, not the rotation in the world space */
        cameraPivot.localRotation = targetRotation;

    }
}
