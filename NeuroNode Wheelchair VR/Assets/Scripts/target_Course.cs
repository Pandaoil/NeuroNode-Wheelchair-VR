using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class target_Course : MonoBehaviour
{
    public bool Goal = false;
    public float time;

    private void OnTriggerEnter(Collider other)
    {
        Goal = true;
    }

    private void Update()
    {
        if(Goal == true)
        {
            // Goal Audio Trigger Goes Here
            Debug.Log("FINISHED THE TARGET COURSE!!!");

            //Start Count Down to return to Menu
            time += Time.deltaTime;
        }

        if(time >= 10.0f)
        {
         //Scene Switcher go here when we have a Main Menu
         Debug.Log("Back to Menu");
        }        
    }
}
