using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ring_Course : MonoBehaviour
{
    public GameObject[] nextPoint;
    public int ringcheck = 0;
    public float time;

    // Update is called once per frame
    void Update()
    {
        if(ringcheck == nextPoint.Length)
        {

            // Goal Audio Trigger Goes Here
            Debug.Log("FINISHED THE RING COURSE!!!");

            //Start Count Down to return to Menu
            time += Time.deltaTime;
            ringcheck++;
        }

        if (time >= 10.0f)
        {
            //Scene Switcher go here when we have a Main Menu
            SceneManager.LoadScene("Main Menu");
            Debug.Log("Back to Menu");
        }
    }

    public void NextRing()
    {
        // check if there is a next ring in course
        if (ringcheck <= nextPoint.Length)
        {
            Debug.Log("next");

            // Turns off current ring in course
            nextPoint[ringcheck].SetActive(false);

            // Increase ring counter
            ringcheck++;

            if (ringcheck < nextPoint.Length)
            {
                // Turns on next ring in course
                nextPoint[ringcheck].SetActive(true);

            }
            Debug.Log(nextPoint.Length);

        }
       
        
    }
}
