using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //The player's Rigidbody
    private Rigidbody playerRb;
    //The forward direction object of the player
    private GameObject focalPoint;
    //Debug testing
    private GameObject playerFocalPoint;
    //speed this player can travel
    public float speed = 5.0f;
    //Height of jump
    public float jumpForce;

    //Debug for forward input
    public float debugForwardInput = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //Set RigidBody and focal point
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("PlayerFocalPoint");
        playerFocalPoint = GameObject.Find("PlayerBall");
    }

    // Update is called once per frame
    void Update()
    {
        //Movie foward/backward
        bool fart = Input.GetKeyDown(KeyCode.W);

        //Move forward Button
        if (Input.GetKeyDown(KeyCode.W))
        {
            moveForward("forward");
        } 

        //Move backward Button
        if (Input.GetKeyDown(KeyCode.D))
        {
            moveForward("backward");
        }

        //Jump Button
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump();
        }
    }

    //Move forward/Backward
    void moveForward (string direction)
    {
        float forwardInput = Input.GetAxis("Vertical");
        debugForwardInput = forwardInput; //Debug

        switch (direction)
        {
            case "forward":
                //Moving forward
                Debug.Log("We are moving forward");
                playerRb.AddForce(playerFocalPoint.transform.forward * speed * forwardInput);
                break;
            case "backward":
                //Moving backward
                Debug.Log("We are moving backward");
                playerRb.AddForce(playerFocalPoint.transform.forward * speed * forwardInput);
                break;
            default:
                //Wrong direction input!
                Debug.Log("Wrong direction input given: " + direction);
                break;
        }
        
    }
    //Player Jump
    private void jump ()
    {
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    //Move left/right
}
