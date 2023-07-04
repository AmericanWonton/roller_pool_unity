using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject OtherPortal;

    public bool isActive = true;

    public float teleportDistance = 5.0f;
    public float pushStrength = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TeleportObject(GameObject theGameObject)
    {
        Vector3 theForward = OtherPortal.transform.forward;
        Vector3 thePosition = OtherPortal.transform.position; 
        Vector3 newSpawnPosition = OtherPortal.transform.position + (OtherPortal.transform.forward * teleportDistance);

        Vector3 playerVelocity = theGameObject.GetComponent<Rigidbody>().velocity;


        theGameObject.transform.position = newSpawnPosition;
        theGameObject.transform.rotation = OtherPortal.transform.rotation;
        float playerMagnitude = theGameObject.GetComponent<Rigidbody>().velocity.magnitude;
        theGameObject.GetComponent<Rigidbody>().AddForce((theGameObject.transform.forward * (pushStrength * playerMagnitude)), ForceMode.Acceleration);

    }

    void OnCollisionEnter(Collision collision)
    {
        string collisionTag = collision.gameObject.tag;

        switch (collisionTag.ToUpper())
        {
            case "PLAYER_BALL":
                TeleportObject(collision.gameObject);
                break;
            case "A_BALL":
                TeleportObject(collision.gameObject);
                break;
            default:
                //do nothing
                break;
        }
    }
}
