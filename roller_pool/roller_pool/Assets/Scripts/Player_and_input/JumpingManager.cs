using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingManager : MonoBehaviour
{

    private List<GameObject> objectsCollided;


    /* Count how many objects are entering this floor */
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Object is on floor: " + other.gameObject.GetHashCode().ToString());
        objectsCollided.Add(other.gameObject);
    }

    /* Remove object currently colliding; the player might have jumped or something */
    void OnTriggerExit(Collider other)
    {
        Debug.Log("Object is in air: " + other.gameObject.GetHashCode().ToString());
        objectsCollided.Remove(other.gameObject);
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
