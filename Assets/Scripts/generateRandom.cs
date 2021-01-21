using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generateRandom : MonoBehaviour
{
    public Rigidbody[] fruit_prefabs; //this creates a field in the inspector called "fruit prefabs"
                                       //so I dragged all the pre-fabs of the fruits and put them in this field
    public float timeInterval;   //How many seconds to wait before cloning
    public float nextTime = 0.0f; //Next time to instantiate a new fruit
    public int index; //index of the fruit to be instantiated
    public float x_coordinate; //x-coordinate of fruit to be instantiated
    public float force_strength; //strength of force on new fruit
    public GameObject player; //to acquire position of player
    public Vector3 object_position; //position of the new fruit
    public float prob; //stores number between 1 and 0 to determine probability of bomb 
    public Rigidbody new_object_prefab; //stores prefab of object to be instantiated
    public Rigidbody bomb; //stores prefab of bomb
    public float distance_from_player = 1.0f;

    private void Start()
    {
        player = GameObject.Find("OVRPlayerController");
        //Idea to use timeInterval and nextTime comes from Unity documentation for Time.time (the example):
        //https://docs.unity3d.com/ScriptReference/Time-time.html
        timeInterval = Random.Range(0.5f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
     
        if (Time.time > nextTime)
        {
            timeInterval = Random.Range(0.5f, 1.0f);
            //update the next time to choose a fruit (chooses random time interval between 0.5 and 1)
            nextTime = Time.time + timeInterval;

            //Choose a random float between 1 and 0 (to determine whether to generate bomb or fruit)
            prob = Random.Range(0.0f, 1.0f);

            //30% chance of choosing a bomb
            if (prob > 0.7f)
            {
                new_object_prefab = bomb;

            }
            //70% chance of choosing fruit
            else
            {
                //Choose a random fruit from the prefabs in the inspector
                index = Random.Range(0, fruit_prefabs.Length);
                new_object_prefab = fruit_prefabs[index];
            }

            //choose random x-coordinate in range (-1, 1)
            x_coordinate = Random.Range(-1.0f, 1.0f);

            //assign position of the object to be 1 unit away from the player and 
            //anywhere between -1 and 1 units away from player on the x-axis
            object_position = player.transform.position + (player.transform.forward * distance_from_player) + (player.transform.right * x_coordinate);

            //Instantiate new object
            //Idea to save prefabs as rigidbodies rather than using getComponent at runtime to access RigidBody and
            //add a force comes from Unity documentation:
            //https://docs.unity3d.com/Manual/InstantiatingPrefabs.html
            //section "Instantiating projectiles & explosions"
            Rigidbody new_object = Instantiate(new_object_prefab, object_position, Quaternion.identity);
            new_object.AddForce(transform.up * force_strength);
            
        }
    }

}


