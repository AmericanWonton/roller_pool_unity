using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject OtherPortal;

    public bool isActive = true;

    public float teleportDistance = 5.0f;

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
        theGameObject.GetComponent<Rigidbody>().velocity.Set(playerVelocity.x, playerVelocity.y, playerVelocity.z);

        Debug.Log("DEBUG: theVelocity is here: " + playerVelocity);
        /*
        Debug.Log("DEBUG: theForward here: " + theForward);
        Debug.Log("DEBUG: thePosition here: " + thePosition);
        Debug.Log("DEBUG: Teleporting here: " + newSpawnPosition);
        */
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
