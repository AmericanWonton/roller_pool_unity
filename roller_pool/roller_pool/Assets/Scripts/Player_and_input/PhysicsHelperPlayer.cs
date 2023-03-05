using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsHelperPlayer : MonoBehaviour
{


    public void objectLerpSpeed(Rigidbody theRigidBody, float maxSpeed)
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
