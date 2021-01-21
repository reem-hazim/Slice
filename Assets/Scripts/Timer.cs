    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    //variable to access the text editor script on the timer text object
    private UnityEngine.UI.Text text_editor_script;
    //timer interval (currently set to 60 seconds)
    public float timeInterval = 60f;
    //Current time 
    public float timeKeeper;

    // Start is called before the first frame update
    void Start()
    {
        //Access the text editor script to modify the text displayed on screen
        text_editor_script = gameObject.GetComponent<UnityEngine.UI.Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //Update timeInterval with current time
        timeKeeper = timeInterval - Time.time;
        //Change timeKeeper to an integer (to avoid displaying decimal points on screen)
        int timeKeeper_Int = (int)timeKeeper;
        //Update text on text editor script
        text_editor_script.text = "Time: ";
        text_editor_script.text += timeKeeper_Int.ToString();
        //Load end_game scene when timer reaches 0
        if (timeKeeper <= 0)
        {
            SceneManager.LoadScene("end_game");
        }
    }
}
