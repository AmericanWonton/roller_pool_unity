using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bumper : MonoBehaviour
{

    struct BumperPadTarget
    {
        public float ContactTime;
        public Vector3 ContactVelocity;
    }

    [SerializeField] float LaunchDelay = 0.1f;
    [SerializeField] float LaunchForce = 5.0f;
    [SerializeField] float BallLaunchForce = 100.0f;
    [SerializeField] ForceMode LaunchMode = ForceMode.Impulse;
    [SerializeField] float ImpactVelocityScaleMax = 2f;
    [SerializeField] float ImpactVelocityScale = 0.05f;
    [SerializeField] float MaxDistortionWeight = 0.25f;


    private void FixedUpdate()
    {

    }


    /* Look for collision object and send the object back in the opposite direction */
    void OnCollisionEnter(Collision collision)
    {
        string collisionTag = collision.gameObject.tag;

        switch (collisionTag.ToUpper())
        {
            case "PLAYER_BALL":
                Launch(collision.rigidbody, collision.relativeVelocity, collisionTag);
                break;
            case "A_BALL":
                Launch(collision.rigidbody, collision.relativeVelocity, collisionTag);
                break;
            default:
                //do nothing
                break;
        }

    }

    void OnCollisionExit(Collision collision)
    {

    }

    void Launch(Rigidbody targetRB, Vector3 contactVelocity, string theTag)
    {
        Vector3 launchVector = transform.up;

        Vector3 distortionVector = transform.forward * Vector3.Dot(contactVelocity, transform.forward) +
                                    transform.right * Vector3.Dot(contactVelocity, transform.right);

        /* normalize will lessen the impact of large values but add to the values of small values */                          
        launchVector = (launchVector + MaxDistortionWeight * distortionVector.normalized).normalized;

        //project the velocity on the jump axis
        float contactProjection = Vector3.Dot(contactVelocity, transform.up);
        if (contactProjection < 0)
        {
            //We don't want this to be negative, thats where this function comes in
            //Scale up the launch vector based on how fast we hit,(within a limit)
            launchVector *= Mathf.Min(ImpactVelocityScaleMax, 1f + Mathf.Abs(contactProjection * ImpactVelocityScale));
        }

        switch (theTag.ToUpper())
        {
            case "PLAYER_BALL":
                targetRB.AddForce(launchVector * BallLaunchForce, LaunchMode);
                break;
            case "A_BALL":
                targetRB.AddForce(launchVector * LaunchForce, LaunchMode);
                break;
            default:
                //do nothing
                break;
        }
    }
}
