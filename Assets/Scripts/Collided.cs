using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collided : MonoBehaviour
{
    public bool collided = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Environment"))
        {
            collided = true;
        }
    }
    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag("Environment")|| collider.gameObject.CompareTag("Prize"))
        {
            collided = true;
        }

    }
    public bool getCollided()
    {
        return collided;
    }
}
