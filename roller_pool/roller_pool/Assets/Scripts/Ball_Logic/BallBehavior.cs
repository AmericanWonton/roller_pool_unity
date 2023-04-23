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
    public float ballDynamicFriction = 0.6f;
    private Rigidbody ballRigidBody;

    private Collider theCollider;

    /* Debug values */
    
    // Start is called before the first frame update
    void Start()
    {
        ballRigidBody = GetComponent<Rigidbody>();
        theCollider = GetComponent<Collider>();
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
        
    }

}
