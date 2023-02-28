using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //Object our camera will follow
    public Transform targetTransform;
    private Vector3 cameraFollowVelocity = Vector3.zero;
    public float cameraFollowSpeed = 0.2f;

    private void Awake()
    {
        //Get the transform of the player
        targetTransform = FindObjectOfType<PlayerManager>().transform;
    }
    public void FollowTarget()
    {
        /*
        Used to move something 'softly' from one location to another...
        like a camera to a player. So, every frame, our this will be called to move our camera to our player's position
        */
        Vector3 targetPosition = Vector3.SmoothDamp
        (transform.position, targetTransform.position, ref cameraFollowVelocity, cameraFollowSpeed);

        transform.position = targetPosition;
    }
}
