using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{

    public float launchStrength = 40f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* Look for collision object and send the object back in the opposite direction */
    void OnCollisionEnter(Collision collision)
    {
        string collideTag = collision.gameObject.tag;
        Rigidbody otherRigidBody = collision.gameObject.GetComponent<Rigidbody>();

        if (collideTag == "Player_Ball")
        {
            Vector3 theForcePosition = (transform.position - collision.gameObject.transform.position);
            otherRigidBody.AddForce(theForcePosition * launchStrength, ForceMode.Impulse);
        }
    }
}
