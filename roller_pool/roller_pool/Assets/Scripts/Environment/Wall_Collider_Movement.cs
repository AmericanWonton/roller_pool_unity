using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_Collider_Movement : MonoBehaviour
{
    public string wallDirection = "horizontal";

    public float movementDistance = 20.0f;
    public float movementSpeed = 5.0f;
    public float minDistanceX=2f;
    public float maxDistanceX=3f;
    public float minDistanceY=2f;
    public float maxDistanceY=3f;
    public float minDistanceZ=2f;
    public float maxDistanceZ=3f;

    // Start is called before the first frame update
    void Start()
    {
        //Get the objects current starting position
        minDistanceX=transform.position.x;
        maxDistanceX=transform.position.x + movementDistance;
        minDistanceY=transform.position.y;
        maxDistanceY=transform.position.y + movementDistance;
        minDistanceZ=transform.position.z;
        maxDistanceZ=transform.position.z + movementDistance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() 
    {
        WallMovement();
        
    }

    /* This controls the movement of the wall, once per frame. 
    The editor needs to manually declare whether the wall is 
        - Vertical
        -Horizontal
        - If neither, it defaults to Horizontal 
    */

    void WallMovement()
    {

        switch (wallDirection.ToLower())
        {
            case "horizontal":
                MoveLeftRight();
                break;
            case "vertical":
                MoveUpDown();
                break;
            case "forward":
                MoveForwardBack();
                break;
            default: 
                //Defaults to horizontal logic
                MoveLeftRight();
                break;
        }
    }

    //Move the object left and right repeatedly
    void MoveLeftRight()
    {
        transform.position = new Vector3((Mathf.PingPong(Time.time * movementSpeed,maxDistanceX-minDistanceX)+minDistanceX), transform.position.y, transform.position.z);
    }

    //Move the object up and down, repeatedly
    void MoveUpDown()
    {
        transform.position = new Vector3(transform.position.x, (Mathf.PingPong(Time.time * movementSpeed,maxDistanceY-minDistanceY)+minDistanceY), transform.position.z);
    }

    //Move the object forward and backward, repeatedly
    void MoveForwardBack()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, (Mathf.PingPong(Time.time * movementSpeed,maxDistanceZ-minDistanceZ)+minDistanceZ));
    }
}
