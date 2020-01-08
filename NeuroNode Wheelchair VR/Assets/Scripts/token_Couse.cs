using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class token_Couse : MonoBehaviour
{
    public bool Goal = false;

    public int Tokens;

    public float time;
    public float Timer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Token"))
        {
            Tokens += 1;
            Destroy(other.gameObject);

            if(Tokens == 5)
            {
                Goal = true;
            }
        }
    }

    private void Update()
    {
        Timer += Time.deltaTime;
        Debug.Log(Tokens);

        if(Goal == true)
        {
            Debug.Log("FINISHED THE TOKEN COURSE");
            time += Time.deltaTime;
        }

        if (time >= 10.0f)
        {
            //Scene Switcher go here when we have a Main Menu
            SceneManager.LoadScene("MainMenu");
            Debug.Log("Back to Menu");
        }
    }


}
