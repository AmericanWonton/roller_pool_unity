using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceMultiplier : MonoBehaviour
{

    private Rigidbody objectRigidBody;
    private float massOffset = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        objectRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        string collideTag = collision.gameObject.tag;
        Rigidbody otherRigidBody = collision.gameObject.GetComponent<Rigidbody>();

        if (collideTag == "Player_Ball")
        {
            PlayerLocomotion playerLocomotion = collision.gameObject.GetComponent<PlayerLocomotion>();
            float objectMass = otherRigidBody.mass;
            float objectVelocity = otherRigidBody.velocity.magnitude;
            float objectForceMultiplier = playerLocomotion.forceMultiplier;

            /* Calculate force amount */
            float theForceApplied = calculateForceAmount(objectMass, objectVelocity, objectForceMultiplier);
            Vector3 theForcePosition = (transform.position - collision.gameObject.transform.position);

            forceApplied(theForceApplied, theForcePosition, ForceMode.Impulse);
        }

        if (collideTag == "A_Ball")
        {
            BallBehavior ballBehavior = collision.gameObject.GetComponent<BallBehavior>();
            float objectMass = otherRigidBody.mass;
            float objectVelocity = otherRigidBody.velocity.magnitude;
            float objectForceMultiplier = ballBehavior.forceMultiplier;

            /* Calculate force amount */
            float theForceApplied = calculateForceAmount(objectMass, objectVelocity, objectForceMultiplier);
            Vector3 theForcePosition = (transform.position - collision.gameObject.transform.position);
        }
    }

    /* This calculates the amount of force we will apply to an object */
    private float calculateForceAmount(float objectMass, float objectVelocity, float objectForceMultiplier)
    {
        float returnedFloat = 0.0f;

        float massRecalculate = objectMass - (objectMass * massOffset);
        float velocityRecalculate = objectVelocity;

        returnedFloat = (massRecalculate * velocityRecalculate) * objectForceMultiplier;


        return returnedFloat;
    }

    private void forceApplied(float forceAmount, Vector3 forcePosition, ForceMode forceModeApplied)
    {
        objectRigidBody.AddForce(forcePosition * forceAmount, forceModeApplied);
    }

}
