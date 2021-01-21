using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliding_floor : MonoBehaviour
{
    //Destroys object within 5 seconds of hitting the floor
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            Destroy(gameObject, 5);
        }
    }
}
    