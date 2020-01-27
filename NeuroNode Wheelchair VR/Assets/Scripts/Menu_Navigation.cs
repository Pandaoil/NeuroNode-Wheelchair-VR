using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Navigation : MonoBehaviour
{
    public string theLevel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    // this is for the tradtional VR control that require touch
    public void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(theLevel);
    }

    public void Changescene()
    {
        SceneManager.LoadScene(theLevel);
    }
}
