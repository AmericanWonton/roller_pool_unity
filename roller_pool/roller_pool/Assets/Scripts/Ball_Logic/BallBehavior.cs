using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    public float forceMultiplier = 0.01f;
    private float maxBallVelocity = 30.0f;
    public float ballCurrentSpeed;
    private float ballAngularSpeed;
    public float stopVelocitySpeed = 2.5f;
    private Rigidbody ballRigidBody;

    /* Debug values */
    public 
    
    // Start is called before the first frame update
    void Start()
    {
        ballRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        /* Update ball speed */
        ballCurrentSpeed = ballRigidBody.velocity.magnitude;
        ballAngularSpeed = ballRigidBody.angularVelocity.magnitude;
    }

    void FixedUpdate()
    {
        objectLerpSpeed(ballRigidBody, maxBallVelocity);
        BallStoppage();
    }

    /* Keeps the ball from rolling too fast */
    private void objectLerpSpeed(Rigidbody theRigidBody, float maxSpeed)
    {
        if (theRigidBody.velocity.magnitude > maxSpeed)
        {
            theRigidBody.velocity = Vector3.ClampMagnitude(theRigidBody.velocity, maxSpeed);
        }
        if (theRigidBody.angularVelocity.magnitude > maxSpeed)
        {
            theRigidBody.angularVelocity = Vector3.ClampMagnitude(theRigidBody.angularVelocity, maxSpeed);
        }
    }

    /* Prevents the ball from rolling forever */
    private void BallStoppage()
    {
        if (ballCurrentSpeed <= stopVelocitySpeed)
        {
            Debug.Log("DEBUG: We are slowing this object...");
            Vector3 ballCurrentVelocity = ballRigidBody.velocity;
            Vector3 ballCurrentAngularVelocity = ballRigidBody.angularVelocity;
            
            ballRigidBody.velocity = Vector3.SmoothDamp(ballRigidBody.velocity, Vector3.zero, ref ballCurrentVelocity, 0.0f);
            ballRigidBody.angularVelocity = Vector3.SmoothDamp(ballRigidBody.angularVelocity, Vector3.zero, ref ballCurrentAngularVelocity, 0.0f);
        }
    }

}
