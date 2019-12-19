using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBar_Controller : MonoBehaviour
{
    [SerializeField]
    public enum ScanAction
    {
        Mode,
        Mode2,
        Forward,
        R_Clockwise,
        R_Anticlockwise,
        Reverse,
        Presets,
        Discrete,
        Slow,
        Slow2,
        Med,
        Med2,
        Fast,
        Fast2,
        Max,
        Max2,
        Forward_P,
        Clockwise_P,
        Anticlockwise_P,
        Reverse_P,
        Five_Meter,
        One_Meter,
        Ten_Centimeter,
        Ninety_L,
        Fourty_Five_L,
        Ten_L,
        Ninety_R,
        Fourty_Five_R,
        Ten_R,
        One_Meter_R,
        Five_Meter_R,
        Ten_Centimeter_R
    }

    public GameObject[] pivots;
    public AudioSource Sound;
    public Rigidbody RigidBody;
    

    public float Speed = 70;
    public float Drag = 1;
    public float AngularDrag = 0.05f;
    public float Timer = 0;
    public float SwitchSpeed = 3.0f;
    private float temp;
    public float decay;


    public bool Active;
    public bool translate = false;

    private Vector3 ForwardVector;

    public ScanAction ActiveState = ScanAction.Forward;

    private void Start()
    {
        RigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //the inverse transform direction allows for the velocity
        ForwardVector = transform.InverseTransformDirection(RigidBody.velocity);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Sound.Play();
        }

        void Delay()
        {
            decay -= Time.deltaTime;
            
            if(decay < 0)
            {
                decay = 0;
                translate = false;
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Active = true;

            if (Active == true)
            {
                Timer = 0;
            }

            Debug.Log("Pressing Space Bar");
        }
        else
        {
            Active = false;
            Debug.Log("Not Pressing Space Bar");
        }

        switch (ActiveState)
        {
            //-------------------------------------------------DISCRETE INPUT---------------------------------------------//

            case ScanAction.Forward:
                {
                    
                    if (Input.GetKey(KeyCode.Space))
                    {
                        //The body of this if statement will cease all angular velocity and apply a forward vector
                        RigidBody.angularVelocity = Vector3.zero;
                        RigidBody.AddForce(transform.forward * Speed * Time.deltaTime, ForceMode.Impulse);
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        ActiveState = ScanAction.R_Clockwise;
                        Timer = 0;
                    }
                    
                }
                break;

            case ScanAction.R_Clockwise:
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        //The body of this if statement will cease all force applied to the rigidbody and then apply a force on the inverse vector
                        RigidBody.angularVelocity = new Vector3(0, 1.0f, 0);

                        if (Input.GetKeyUp(KeyCode.Space))
                        {
                            ActiveState = ScanAction.Forward;
                        }
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        ActiveState = ScanAction.R_Anticlockwise;
                        Timer = 0;
                    }
                }
                break;

            case ScanAction.R_Anticlockwise:
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        //The body of this if statement will cease all force applied to the rigidbody and then apply a force on the inverse vector
                        RigidBody.angularVelocity = new Vector3(0, -1.0f, 0);

                        if (Input.GetKeyUp(KeyCode.Space))
                        {
                            ActiveState = ScanAction.Forward;
                        }
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        ActiveState = ScanAction.Reverse;
                        Timer = 0;
                    }
                }
                break;

            case ScanAction.Reverse:
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        //The body of this if statement will cease all angular velocity and apply a backward vector
                        RigidBody.angularVelocity = Vector3.zero;
                        RigidBody.AddForce(-transform.forward * Speed * Time.deltaTime, ForceMode.Impulse);

                        if (Input.GetKeyUp(KeyCode.Space))
                        {
                            ActiveState = ScanAction.Forward;
                        }
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        ActiveState = ScanAction.Mode;
                        Timer = 0;
                    }
                }
                break;

            //--------------------------------------------------MODE SETTINGS----------------------------------------------//

            case ScanAction.Mode:
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        ActiveState = ScanAction.Presets;
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        ActiveState = ScanAction.Forward;
                        Timer = 0;
                    }
                }
                break;

            case ScanAction.Presets:
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        ActiveState = ScanAction.Forward_P;
                        Sound.Play();
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= 3.0f)
                    {
                        //This will change the Active State to be the next one
                        ActiveState = ScanAction.Slow;
                        Timer = 0;
                    }
                }
                break;

            case ScanAction.Slow:
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        SwitchSpeed = 3.0f;
                        ActiveState = ScanAction.Mode;
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= 3.0f)
                    {
                        //This will change the Active State to be the next one
                        ActiveState = ScanAction.Med;
                        Timer = 0;
                    }
                }
                break;

            case ScanAction.Med:
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        SwitchSpeed = 2.0f;
                        ActiveState = ScanAction.Mode;
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= 3.0f)
                    {
                        //This will change the Active State to be the next one
                        ActiveState = ScanAction.Fast;
                        Timer = 0;
                    }
                }
                break;

            case ScanAction.Fast:
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        SwitchSpeed = 1.0f;
                        ActiveState = ScanAction.Mode;
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= 3)
                    {
                        //This will change the Active State to be the next one
                        ActiveState = ScanAction.Max;
                        Timer = 0;
                    }
                }
                break;
           
            case ScanAction.Max:
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        SwitchSpeed = 0.5f;
                        ActiveState = ScanAction.Mode;
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= 3)
                    {
                        //This will change the Active State to be the next one
                        ActiveState = ScanAction.Slow;
                        Timer = 0;
                    }
                }
                break;

            //-----------------------------------------------PRESET INPUT-------------------------------------------------//

            case ScanAction.Forward_P:
                {


                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        decay = 0.01f;
                        translate = true;
                        Delay();

                        if (decay == 0 && !translate)
                        {
                            ActiveState = ScanAction.One_Meter;
                            Timer = 0;
                        }
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        ActiveState = ScanAction.Clockwise_P;
                        Timer = 0;
                    }

                }
                break;

            case ScanAction.One_Meter:
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        if(Active == true)
                        {
                            transform.Translate(0, 0, 1 * Time.deltaTime);
                            temp += Time.deltaTime;
                            Timer = 0;

                            if (temp >= 1)
                            {
                                ActiveState = ScanAction.Forward_P;
                                Active = false;
                                temp = 0;
                            }
                        }

                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        ActiveState = ScanAction.Ten_Centimeter;
                        Timer = 0;
                    }

                }
                break;

            case ScanAction.Five_Meter:
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        if (Active == true)
                        {
                            transform.Translate(0, 0, 1 * Time.deltaTime);
                            temp += Time.deltaTime;
                            Timer = 0;

                            if (temp >= 5)
                            {
                                ActiveState = ScanAction.Forward_P;
                                Active = false;
                                temp = 0;
                            }
                        }
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        ActiveState = ScanAction.One_Meter;
                        Timer = 0;
                    }

                }
                break;

            case ScanAction.Ten_Centimeter:
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        if (Active == true)
                        {
                            transform.Translate(0, 0, 1 * Time.deltaTime);
                            temp += Time.deltaTime;
                            Timer = 0;

                            if (temp >= 0.1)
                            {
                                ActiveState = ScanAction.Forward_P;
                                Active = false;
                                temp = 0;
                            }
                        }
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        ActiveState = ScanAction.Five_Meter;
                        Timer = 0;
                    }

                }
                break;

            case ScanAction.Clockwise_P:
                {

                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        decay = 0.015f;
                        translate = true;
                        Delay();

                        if (decay == 0 && !translate)
                        {
                            ActiveState = ScanAction.Ninety_R;
                            Timer = 0;
                        }

                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        ActiveState = ScanAction.Anticlockwise_P;
                        Timer = 0;
                    }

                }
                break;

            case ScanAction.Ninety_R:
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        if (Active == true)
                        {
                            transform.Rotate(0, 10 * Time.deltaTime, 0 );
                            temp += Time.deltaTime;
                            Timer = 0;

                            if (temp >= 9)
                            {
                                ActiveState = ScanAction.Forward_P;
                                Active = false;
                                temp = 0;
                            }
                        }
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        ActiveState = ScanAction.Fourty_Five_R;
                        Timer = 0;
                    }

                }
                break;

            case ScanAction.Fourty_Five_R:
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        if (Active == true)
                        {
                            transform.Rotate(0, 10 * Time.deltaTime, 0);
                            temp += Time.deltaTime;
                            Timer = 0;

                            if (temp >= 4.5f)
                            {
                                ActiveState = ScanAction.Forward_P;
                                Active = false;
                                temp = 0;
                            }
                        }
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        ActiveState = ScanAction.Ten_R;
                        Timer = 0;
                    }

                }
                break;

            case ScanAction.Ten_R:
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        if (Active == true)
                        {
                            transform.Rotate(0, 10 * Time.deltaTime, 0);
                            temp += Time.deltaTime;
                            Timer = 0;

                            if (temp >= 1)
                            {
                                ActiveState = ScanAction.Forward_P;
                                Active = false;
                                temp = 0;
                            }
                        }
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        ActiveState = ScanAction.Ninety_R;
                        Timer = 0;
                    }

                }
                break;

            case ScanAction.Anticlockwise_P:
                {

                    if (Input.GetKeyDown(KeyCode.Space))
                    {

                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            decay = 0.015f;
                            translate = true;
                            Delay();

                            if (decay == 0 && !translate)
                            {
                                ActiveState = ScanAction.Ninety_L;
                                Timer = 0;
                            }

                        }
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        ActiveState = ScanAction.Reverse_P;
                        Timer = 0;
                    }

                }
                break;

            case ScanAction.Ninety_L:
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        if (Active == true)
                        {
                            transform.Rotate(0, -10 * Time.deltaTime, 0);
                            temp += Time.deltaTime;
                            Timer = 0;

                            if (temp >= 9)
                            {
                                ActiveState = ScanAction.Forward_P;
                                Active = false;
                                temp = 0;
                            }
                        }
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        ActiveState = ScanAction.Fourty_Five_L;
                        Timer = 0;
                    }

                }
                break;

            case ScanAction.Fourty_Five_L:
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        if (Active == true)
                        {
                            transform.Rotate(0, -10 * Time.deltaTime, 0);
                            temp += Time.deltaTime;
                            Timer = 0;

                            if (temp >= 4.5f)
                            {
                                ActiveState = ScanAction.Forward_P;
                                Active = false;
                                temp = 0;
                            }
                        }
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        ActiveState = ScanAction.Ten_L;
                        Timer = 0;
                    }

                }
                break;

            case ScanAction.Ten_L:
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        if (Active == true)
                        {
                            transform.Rotate(0, -10 * Time.deltaTime, 0);
                            temp += Time.deltaTime;
                            Timer = 0;

                            if (temp >= 1)
                            {
                                ActiveState = ScanAction.Forward_P;
                                Active = false;
                                temp = 0;
                            }
                        }
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        ActiveState = ScanAction.Ninety_L;
                        Timer = 0;
                    }

                }
                break;

            case ScanAction.Reverse_P:
                {

                    if (Input.GetKeyDown(KeyCode.Space))
                    {

                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            decay = 0.015f;
                            translate = true;
                            Delay();

                            if (decay == 0 && !translate)
                            {
                                ActiveState = ScanAction.One_Meter;
                                Timer = 0;
                            }

                        }
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        ActiveState = ScanAction.Mode2;
                        Timer = 0;
                    }

                }
                break;

            case ScanAction.One_Meter_R:
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        if (Active == true)
                        {
                            transform.Translate(0, 0, -1 * Time.deltaTime);
                            Timer = 0;

                            if (temp >= 1)
                            {
                                ActiveState = ScanAction.Forward_P;
                                Active = false;
                                temp = 0;
                            }
                        }

                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        ActiveState = ScanAction.Ten_Centimeter;
                        Timer = 0;
                    }

                }
                break;

            case ScanAction.Five_Meter_R:
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        if (Active == true)
                        {
                            transform.Translate(0, 0, -1 * Time.deltaTime);
                            Timer = 0;

                            if (temp >= 5)
                            {
                                ActiveState = ScanAction.Forward_P;
                                Active = false;
                                temp = 0;
                            }
                        }
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        ActiveState = ScanAction.One_Meter;
                        Timer = 0;
                    }

                }
                break;

            case ScanAction.Ten_Centimeter_R:
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        if (Active == true)
                        {
                            transform.Translate(0, 0, -1 * Time.deltaTime);
                            Timer = 0;

                            if (temp >= 0.1)
                            {
                                ActiveState = ScanAction.Forward_P;
                                Active = false;
                                temp = 0;
                            }
                        }
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        ActiveState = ScanAction.Five_Meter_R;
                        Timer = 0;
                    }

                }
                break;

            //-------------------------------------------MODE2 SETTINGS-----------------------------------------//

            case ScanAction.Mode2:
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        ActiveState = ScanAction.Discrete;
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        ActiveState = ScanAction.Forward_P;
                        Timer = 0;
                    }
                }
                break;

            case ScanAction.Discrete:
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        ActiveState = ScanAction.Forward;
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= 3.0f)
                    {
                        //This will change the Active State to be the next one
                        ActiveState = ScanAction.Slow2;
                        Timer = 0;
                    }
                }
                break;

            case ScanAction.Slow2:
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        SwitchSpeed = 3.0f;
                        ActiveState = ScanAction.Mode2;
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= 3.0f)
                    {
                        //This will change the Active State to be the next one
                        ActiveState = ScanAction.Med2;
                        Timer = 0;
                    }
                }
                break;

            case ScanAction.Med2:
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        SwitchSpeed = 2.0f;
                        ActiveState = ScanAction.Mode2;
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= 3.0f)
                    {
                        //This will change the Active State to be the next one
                        ActiveState = ScanAction.Fast2;
                        Timer = 0;
                    }
                }
                break;

            case ScanAction.Fast2:
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        SwitchSpeed = 1.0f;
                        ActiveState = ScanAction.Mode2;
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= 3)
                    {
                        //This will change the Active State to be the next one
                        ActiveState = ScanAction.Max2;
                        Timer = 0;
                    }
                }
                break;

            case ScanAction.Max2:
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        SwitchSpeed = 0.5f;
                        ActiveState = ScanAction.Mode2;
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= 3)
                    {
                        //This will change the Active State to be the next one
                        ActiveState = ScanAction.Slow2;
                        Timer = 0;
                    }
                }
                break;
        }   
    }
}
