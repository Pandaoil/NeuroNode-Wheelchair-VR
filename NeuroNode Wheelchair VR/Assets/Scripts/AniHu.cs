using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniHu : MonoBehaviour
{
    Animator anime;
    float CurPos;
    
    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
        CurPos = transform.position.z;
    }

    //Update is called once per frame
    void Update()
    {
        //if (Input.GetButton("Fire1"))
        //{
        //    gameObject.transform.Translate(Vector3.forward);
        //    Debug.Log("Holding Left Mouse Button");
        //}
        //else
        //{
        //    Debug.Log(" Not Holding Left Mouse Button");
        //}

        if (CurPos != transform.position.z)
        {
            anime.SetBool("IsMoving", true);
            CurPos = transform.position.z;
        }
        else
        {
            anime.SetBool("IsMoving", false);
        }
    }
}
