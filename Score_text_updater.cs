using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score_text_updater : MonoBehaviour
{
    public int score = 0;
    private UnityEngine.UI.Text text_editor_script;

    // Start is called before the first frame update
    void Start()
    {
        //Access text editor component
        text_editor_script = gameObject.GetComponent<UnityEngine.UI.Text>();

    }

    // Update is called once per frame
    void Update()
    {
        //Update score on screen
        text_editor_script.text = "Score: ";
        text_editor_script.text += score.ToString();
    }
}
