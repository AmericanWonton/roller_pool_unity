using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingManager : MonoBehaviour
{

    private List<GameObject> objectsCollided;

    void OnCollisionStay(Collision collision)
    {
        string collideTag = collision.gameObject.tag;
        Rigidbody otherRigidBody = collision.gameObject.GetComponent<Rigidbody>();

        if (collideTag == "Player_Ball")
        {
            PlayerLocomotion playerLocomotion = collision.gameObject.GetComponent<PlayerLocomotion>();
            playerLocomotion.inputManager.jumpInput = true;
            playerLocomotion.isOnGround = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        string collideTag = collision.gameObject.tag;
        Rigidbody otherRigidBody = collision.gameObject.GetComponent<Rigidbody>();

        if (collideTag == "Player_Ball")
        {
            //Debug.Log("Player has jumped...");
            PlayerLocomotion playerLocomotion = collision.gameObject.GetComponent<PlayerLocomotion>();
            playerLocomotion.inputManager.jumpInput = false;
            playerLocomotion.isOnGround = false;
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
