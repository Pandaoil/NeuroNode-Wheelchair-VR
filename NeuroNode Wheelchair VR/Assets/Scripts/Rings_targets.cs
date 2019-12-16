using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rings_targets : MonoBehaviour
{
    public GameObject ringManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        ringManager.GetComponent<ring_Course>().NextRing();
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }
}
