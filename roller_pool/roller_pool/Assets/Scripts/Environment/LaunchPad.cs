using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPad : MonoBehaviour
{
    public float launchStrength = 50f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* Look for collision object and apply force if needed */
    void OnCollisionEnter(Collision collision)
    {
        string collideTag = collision.gameObject.tag;
        Rigidbody otherRigidBody = collision.gameObject.GetComponent<Rigidbody>();

        if (collideTag == "Player_Ball")
        {
            otherRigidBody.AddForce(Vector3.up * launchStrength, ForceMode.Impulse);
        }
    }
}
