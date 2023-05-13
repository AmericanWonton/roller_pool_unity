using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_Collider_Movement : MonoBehaviour
{
    public string wallDirection;
    private Vector3 startingPosition;

    public float movementDistance = 20.0f;
    public float movementSpeed = 5.0f;
    private string currentDirectionMoving = "left";
    public float min=2f;
    public float max=3f;

    // Start is called before the first frame update
    void Start()
    {
        //Get the objects current starting position
        startingPosition = transform.position;
        min=transform.position.x;
        max=transform.position.x + movementDistance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() 
    {
        //WallMovement();
        //transform.position = new Vector3(Mathf.PingPong(Time.time * movementSpeed, movementDistance), startingPosition.y, startingPosition.z);
        transform.position =new Vector3((Mathf.PingPong(Time.time* movementSpeed,max-min)+min), transform.position.y, transform.position.z);
    }

    /* This controls the movement of the wall, once per frame. 
    The editor needs to manually declare whether the wall is 
        - Vertical
        -Horizontal
        - If neither, it defaults to Horizontal 
    */

    void WallMovement()
    {
        Debug.Log(startingPosition.ToString());

        switch (wallDirection.ToLower())
        {
            case "horizontal":

                break;
            case "vertical":

                break;
            default: 
                //Defaults to horizontal logic
                break;
        }
    }

    //Move the object left and right repeatedly
    void MoveLeftRight()
    {
        if (currentDirectionMoving.ToLower() == "left")
        {
            if (transform.position.x >= startingPosition.x + movementDistance)
            {
                currentDirectionMoving = "right";
            } else 
            {
                Vector3 newPosition = new Vector3(transform.position.x + 1.0f, transform.position.y, transform.position.z);
                transform.Translate(newPosition * Time.deltaTime * movementSpeed);
            }
        } else 
        {
            if (transform.position.x >= startingPosition.x + movementDistance)
            {
                currentDirectionMoving = "right";
            } else 
            {
                Vector3 newPosition = new Vector3(transform.position.x + 1.0f, transform.position.y, transform.position.z);
                transform.Translate(newPosition * Time.deltaTime * movementSpeed);
            }
        }
    }

}
