using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword_collider : MonoBehaviour
{
    //Declaring variables
    public GameObject explosion;
    public GameObject[] half_fruit_prefabs;
    public GameObject score_text;
    public int index;
    public Rigidbody rb;            
    public float force_magnitude = 10.0f;
    //Dictionary that stores the fruit names and their indices (to access same index in half_fruit prefabs list)
    Dictionary<string, int> fruit_dict = new Dictionary<string, int>();

    // Start is called before the first frame update
    void Start()
    {
        //Access score text object
        score_text = GameObject.Find("Score_text");
        //Add all fruit names and indices
        fruit_dict.Add("Apple(Clone)", 0);
        fruit_dict.Add("Banana(Clone)", 1);
        fruit_dict.Add("Kiwi(Clone)", 2);
        fruit_dict.Add("lemon(Clone)", 3);
        fruit_dict.Add("lime(Clone)", 4);
        fruit_dict.Add("orange(Clone)", 5);
        fruit_dict.Add("Strawberry(Clone)", 6);

    }

    //Applies force on specific game object in specific direction
    void applyForce(GameObject this_object, Vector3 direction)
    {
        rb = this_object.GetComponent<Rigidbody>();
        rb.AddForce(direction);
    }

    //Determines what happens when an object collides with sword
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bomb")
        {
            //Destory bomb
            Destroy(collision.gameObject);
            //Instantiate explosion prefab in bomb position
            GameObject new_explosion = Instantiate(explosion, collision.transform.position, Quaternion.identity);
            //Destroy explosion prefab 2 seconds later
            Destroy(new_explosion, 2);
            //Remove 5 points from score script
            score_text.GetComponent<Score_text_updater>().score -= 5;

        }
        else if (collision.gameObject.tag == "Fruit")
        {
            //get index of fruit we just sliced
            index = fruit_dict[collision.gameObject.name];

            //get position and rotation of fruit we just sliced
            Vector3 fruit_position = collision.transform.position;
            Quaternion fruit_rotation = collision.transform.rotation;

            //Make some corrections for banana, orange and strawberry
            //They weren't being sliced correctly because the fruit and half-fruit rotations mismatched
            if (collision.gameObject.name == "Banana(Clone)" || collision.gameObject.name == "orange(Clone)")
            {
                fruit_rotation.eulerAngles += new Vector3(0f, 90f, 0f);
            }
            else if (collision.gameObject.name == "Strawberry(Clone)")
            {
                fruit_rotation.eulerAngles += new Vector3(270f, -90f, -90f);
            }

            //Destroy the fruit we sliced
            Destroy(collision.gameObject);

            //Instantiate first half of the fruit using the fruit's position and rotation
            GameObject first_half = Instantiate(half_fruit_prefabs[index], fruit_position, fruit_rotation);

            //Copy first half rotation to the second half rotation
            Quaternion second_half_rotation = first_half.transform.rotation;

            //rotate second half of fruit 180 degrees around y-axis
            second_half_rotation.eulerAngles += new Vector3(0f, 180f, 0f);

            //if strawberry, instantiate second half with rotation angles 90, 180, 0 (again, correcting for rotation mismatch)
            if (index == 6)
            {
                second_half_rotation.eulerAngles = new Vector3(90f, 180f, 0f);
            }

            //Instantiate second half of fruit
            GameObject second_half = Instantiate(half_fruit_prefabs[index], fruit_position, second_half_rotation);

            //If the fruit is a strawberry, make some corrections
            if (index == 6)
            {
                applyForce(first_half, first_half.transform.up * force_magnitude);
                applyForce(second_half, second_half.transform.up * force_magnitude);
            }
            else
            {
                //Apply force to push fruits apart
                applyForce(first_half, first_half.transform.forward * force_magnitude);
                applyForce(second_half, second_half.transform.forward * force_magnitude);
            }
            //increment the score
            score_text.GetComponent<Score_text_updater>().score += 5;
        }
    }
}


