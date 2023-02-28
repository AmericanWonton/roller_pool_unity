using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    /* This script is used to handle all functionality for our player */
    InputManager inputManager;
    CameraManager cameraManager;
    PlayerLocomotion playerLocomotion;
    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        cameraManager = FindObjectOfType<CameraManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    private void Update()
    {
        inputManager.HandleAllInputs();
    }

    /* We're using fixed update to handle Ridgidbodys...
    it handles much nicer. It's a Unity specific rule... */
    private void FixedUpdate()
    {
        playerLocomotion.HandleAllMovement();
    }

    /*  This calls AFTER the frame has ended */
    private void LateUpdate()
    {
        cameraManager.FollowTarget();
    }

}
