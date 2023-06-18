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

    Dictionary<Rigidbody, BumperPadTarget> Targets = new Dictionary<Rigidbody, BumperPadTarget>();

    List<Rigidbody> TargetsToClear = new List<Rigidbody>();


    private void FixedUpdate()
    {
        //Check for targets to launch
        float thresholdTime = Time.timeSinceLevelLoad - LaunchDelay;
        foreach(var kvp in Targets)
        {
            if (kvp.Value.ContactTime >= thresholdTime)
            {
                Launch(kvp.Key, kvp.Value.ContactVelocity);
                TargetsToClear.Add(kvp.Key);
            }
        }

        foreach(var target in TargetsToClear)
        {
            Targets.Remove(target);
            TargetsToClear.Clear();
        }
    }


    /* Look for collision object and send the object back in the opposite direction */
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"{collision.gameObject.name}");
        Rigidbody rb;
        if (collision.gameObject.TryGetComponent<Rigidbody>(out rb))
        {
            Targets[rb] = new BumperPadTarget() { ContactTime = Time.timeSinceLevelLoad, ContactVelocity = collision.relativeVelocity} ;
        }
    }

    void OnCollisionExit(Collision collision)
    {

    }

    void Launch(Rigidbody targetRB, Vector3 contactVelocity)
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

        if (targetRB.CompareTag("Player_Ball"))
        {
            targetRB.AddForce(launchVector * BallLaunchForce, LaunchMode);
        } else 
        {
            targetRB.AddForce(launchVector* LaunchForce, LaunchMode);
        }
    }
}
