using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeuroNode_Controller : MonoBehaviour
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
    public AudioSource Tick;
    public Rigidbody RigidBody;

    public GameObject Mode;
    public GameObject Forward;
    public GameObject Clockwise;
    public GameObject Anticlockwise;
    public GameObject Reverse;
    public GameObject Preset;
    public GameObject Discrete;
    public GameObject Slow;
    public GameObject Med;
    public GameObject Fast;
    public GameObject Max;
    public GameObject Forward_P;
    public GameObject Clockwise_P;
    public GameObject Anticlockwise_P;
    public GameObject Reverse_P;
    public GameObject One_Meter;
    public GameObject Ten_Centimeters;
    public GameObject Five_Meters;
    public GameObject Ninety_Degrees;
    public GameObject Fourty_Five_Degrees;
    public GameObject Ten_Degrees;

    public Text Display;
    public Text SwitchTime;

    public float Speed = 2;
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

        Display.text = Timer.ToString();
        SwitchTime.text = SwitchSpeed.ToString();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Sound.Play();
        }

        void Delay()
        {
            decay -= Time.deltaTime;

            if (decay < 0)
            {
                decay = 0;
                translate = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(Active == false)
            {
                Active = true;

                Debug.Log("Active");

            }

            else if (Input.GetKeyDown(KeyCode.Space))
            {
                if (Active == true)
                {
                    Active = false;

                    Debug.Log("Not Active");
                }
            }
        }



        switch (ActiveState)
        {
            //-------------------------------------------------DISCRETE INPUT---------------------------------------------//

            case ScanAction.Forward:
                {

                    if (Active == true)
                    {
                        //The body of this if statement will cease all angular velocity and apply a forward vector
                        transform.Translate(0, 0, 1 * Speed * Time.deltaTime);
                        Timer = 0;
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        Forward.SetActive(false);
                        Clockwise.SetActive(true);
                        ActiveState = ScanAction.R_Clockwise;
                        Timer = 0;
                        Tick.Play();
                    }

                }
                break;

            case ScanAction.R_Clockwise:
                {
                    if (Active == true)
                    {
                        //The body of this if statement will cease all force applied to the rigidbody and then apply a force on the inverse vector
                        transform.Rotate(0, 10 * Speed * Time.deltaTime, 0);
                        Timer = 0;
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        Clockwise.SetActive(false);
                        Anticlockwise.SetActive(true);
                        ActiveState = ScanAction.R_Anticlockwise;
                        Timer = 0;
                        Tick.Play();
                    }
                }
                break;

            case ScanAction.R_Anticlockwise:
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        //The body of this if statement will cease all force applied to the rigidbody and then apply a force on the inverse vector
                        transform.Rotate(0, -10 * Speed * Time.deltaTime, 0);

                        if (Input.GetKeyUp(KeyCode.Space))
                        {
                            ActiveState = ScanAction.Forward;
                        }
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        Anticlockwise.SetActive(false);
                        Reverse.SetActive(true);
                        ActiveState = ScanAction.Reverse;
                        Timer = 0;
                        Tick.Play();
                    }
                }
                break;

            case ScanAction.Reverse:
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        //The body of this if statement will cease all angular velocity and apply a backward vector
                        transform.Translate(0, 0, -1 * Speed * Time.deltaTime);

                        if (Input.GetKeyUp(KeyCode.Space))
                        {
                            ActiveState = ScanAction.Forward;
                        }
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        Reverse.SetActive(false);
                        Mode.SetActive(true);
                        ActiveState = ScanAction.Mode;
                        Timer = 0;
                        Tick.Play();
                    }
                }
                break;

            //--------------------------------------------------MODE SETTINGS----------------------------------------------//

            case ScanAction.Mode:
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        ActiveState = ScanAction.Presets;
                        Mode.SetActive(false);
                        Preset.SetActive(true);
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        Mode.SetActive(false);
                        Forward.SetActive(true);
                        ActiveState = ScanAction.Forward;
                        Timer = 0;
                        Tick.Play();
                    }
                }
                break;

            case ScanAction.Presets:
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        Preset.SetActive(false);
                        Forward_P.SetActive(true);
                        ActiveState = ScanAction.Forward_P;
                        Sound.Play();
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= 3.0f)
                    {
                        //This will change the Active State to be the next one
                        ActiveState = ScanAction.Slow;
                        Preset.SetActive(false);
                        Slow.SetActive(true);
                        Timer = 0;
                        Tick.Play();
                    }
                }
                break;

            case ScanAction.Slow:
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        Slow.SetActive(false);
                        Forward.SetActive(true);
                        SwitchSpeed = 3.0f;
                        ActiveState = ScanAction.Forward;
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= 3.0f)
                    {
                        //This will change the Active State to be the next one
                        Slow.SetActive(false);
                        Med.SetActive(true);
                        ActiveState = ScanAction.Med;
                        Timer = 0;
                        Tick.Play();
                    }
                }
                break;

            case ScanAction.Med:
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        Med.SetActive(false);
                        Forward.SetActive(true);
                        SwitchSpeed = 2.0f;
                        ActiveState = ScanAction.Forward;
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= 3.0f)
                    {
                        //This will change the Active State to be the next one
                        Med.SetActive(false);
                        Fast.SetActive(true);
                        ActiveState = ScanAction.Fast;
                        Timer = 0;
                        Tick.Play();
                    }
                }
                break;

            case ScanAction.Fast:
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        Fast.SetActive(false);
                        Forward.SetActive(true);
                        SwitchSpeed = 1.0f;
                        ActiveState = ScanAction.Forward;
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= 3)
                    {
                        //This will change the Active State to be the next one
                        Fast.SetActive(false);
                        Max.SetActive(true);
                        ActiveState = ScanAction.Max;
                        Timer = 0;
                        Tick.Play();
                    }
                }
                break;

            case ScanAction.Max:
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        Max.SetActive(false);
                        Forward.SetActive(true);
                        SwitchSpeed = 0.5f;
                        ActiveState = ScanAction.Forward;
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= 3)
                    {
                        Max.SetActive(false);
                        Preset.SetActive(true);
                        //This will change the Active State to be the next one
                        ActiveState = ScanAction.Presets;
                        Timer = 0;
                        Tick.Play();
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
                            Forward_P.SetActive(false);
                            One_Meter.SetActive(true);
                            ActiveState = ScanAction.One_Meter;
                            Timer = 0;
                        }
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        Forward_P.SetActive(false);
                        Clockwise_P.SetActive(true);
                        ActiveState = ScanAction.Clockwise_P;
                        Timer = 0;
                        Tick.Play();
                    }

                }
                break;

            case ScanAction.One_Meter:
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        if (Active == true)
                        {
                            transform.Translate(0, 0, 1 * Time.deltaTime);
                            temp += Time.deltaTime;
                            Timer = 0;

                            if (temp >= 1)
                            {
                                ActiveState = ScanAction.Forward_P;
                                One_Meter.SetActive(false);
                                Forward_P.SetActive(true);
                                Active = false;
                                temp = 0;
                            }
                        }

                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        One_Meter.SetActive(false);
                        Ten_Centimeters.SetActive(true);
                        ActiveState = ScanAction.Ten_Centimeter;
                        Timer = 0;
                        Tick.Play();
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
                                Five_Meters.SetActive(false);
                                Forward_P.SetActive(true);
                                Active = false;
                                temp = 0;
                            }
                        }
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        Five_Meters.SetActive(false);
                        One_Meter.SetActive(true);
                        ActiveState = ScanAction.One_Meter;
                        Timer = 0;
                        Tick.Play();
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
                                Ten_Centimeters.SetActive(false);
                                Forward_P.SetActive(true);
                                Active = false;
                                temp = 0;
                            }
                        }
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        Ten_Centimeters.SetActive(false);
                        Five_Meters.SetActive(true);
                        ActiveState = ScanAction.Five_Meter;
                        Timer = 0;
                        Tick.Play();
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
                            Clockwise_P.SetActive(false);
                            Ninety_Degrees.SetActive(true);
                            Timer = 0;
                        }

                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        Clockwise_P.SetActive(false);
                        Anticlockwise_P.SetActive(true);
                        ActiveState = ScanAction.Anticlockwise_P;
                        Timer = 0;
                        Tick.Play();
                    }

                }
                break;

            case ScanAction.Ninety_R:
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        if (Active == true)
                        {
                            transform.Rotate(0, 20 * Time.deltaTime, 0);
                            temp += Time.deltaTime;
                            Timer = 0;

                            if (temp >= 4.5)
                            {
                                ActiveState = ScanAction.Forward_P;
                                Ninety_Degrees.SetActive(false);
                                Forward_P.SetActive(true);
                                Active = false;
                                temp = 0;
                            }
                        }
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        Ninety_Degrees.SetActive(false);
                        Fourty_Five_Degrees.SetActive(true);
                        ActiveState = ScanAction.Fourty_Five_R;
                        Timer = 0;
                        Tick.Play();
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
                                Fourty_Five_Degrees.SetActive(false);
                                Forward_P.SetActive(true);
                                Active = false;
                                temp = 0;
                                Tick.Play();
                            }
                        }
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        Fourty_Five_Degrees.SetActive(false);
                        Ten_Degrees.SetActive(true);
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
                                Ten_Degrees.SetActive(false);
                                Forward_P.SetActive(true);
                                Active = false;
                                temp = 0;
                            }
                        }
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        Ten_Degrees.SetActive(false);
                        Ninety_Degrees.SetActive(true);
                        ActiveState = ScanAction.Ninety_R;
                        Timer = 0;
                        Tick.Play();
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
                                Anticlockwise_P.SetActive(false);
                                Ninety_Degrees.SetActive(true);
                                Timer = 0;
                            }

                        }
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        Anticlockwise_P.SetActive(false);
                        Reverse_P.SetActive(true);
                        ActiveState = ScanAction.Reverse_P;
                        Timer = 0;
                        Tick.Play();
                    }

                }
                break;

            case ScanAction.Ninety_L:
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        if (Active == true)
                        {
                            transform.Rotate(0, -20 * Time.deltaTime, 0);
                            temp += Time.deltaTime;
                            Timer = 0;

                            if (temp >= 4.5)
                            {
                                ActiveState = ScanAction.Forward_P;
                                Ninety_Degrees.SetActive(false);
                                Forward_P.SetActive(true);
                                Active = false;
                                temp = 0;
                            }
                        }
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        Ninety_Degrees.SetActive(false);
                        Fourty_Five_Degrees.SetActive(true);
                        ActiveState = ScanAction.Fourty_Five_L;
                        Timer = 0;
                        Tick.Play();
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
                                Fourty_Five_Degrees.SetActive(false);
                                Forward_P.SetActive(true);
                                Active = false;
                                temp = 0;
                            }
                        }
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        Fourty_Five_Degrees.SetActive(false);
                        Ten_Degrees.SetActive(true);
                        ActiveState = ScanAction.Ten_L;
                        Timer = 0;
                        Tick.Play();
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
                                Ten_Degrees.SetActive(false);
                                Forward_P.SetActive(true);
                                Active = false;
                                temp = 0;
                            }
                        }
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        Ten_Degrees.SetActive(false);
                        Ninety_Degrees.SetActive(true);
                        ActiveState = ScanAction.Ninety_L;
                        Timer = 0;
                        Tick.Play();
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
                                ActiveState = ScanAction.One_Meter_R;
                                Reverse_P.SetActive(false);
                                One_Meter.SetActive(true);
                                Timer = 0;
                            }

                        }
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        Reverse_P.SetActive(false);
                        Mode.SetActive(true);
                        ActiveState = ScanAction.Mode2;
                        Timer = 0;
                        Tick.Play();
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
                            temp += Time.deltaTime;
                            Timer = 0;

                            if (temp >= 1)
                            {
                                ActiveState = ScanAction.Forward_P;
                                One_Meter.SetActive(false);
                                Forward_P.SetActive(true);
                                Active = false;
                                temp = 0;
                            }
                        }

                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        One_Meter.SetActive(false);
                        Ten_Centimeters.SetActive(true);
                        ActiveState = ScanAction.Ten_Centimeter_R;
                        Timer = 0;
                        Tick.Play();
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
                            temp += Time.deltaTime;
                            Timer = 0;

                            if (temp >= 5)
                            {
                                ActiveState = ScanAction.Forward_P;
                                Five_Meters.SetActive(false);
                                Forward_P.SetActive(true);
                                Active = false;
                                temp = 0;
                            }
                        }
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        Five_Meters.SetActive(false);
                        One_Meter.SetActive(true);
                        ActiveState = ScanAction.One_Meter_R;
                        Timer = 0;
                        Tick.Play();
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
                            temp += Time.deltaTime;
                            Timer = 0;

                            if (temp >= 0.1)
                            {
                                ActiveState = ScanAction.Forward_P;
                                Ten_Centimeters.SetActive(false);
                                Forward_P.SetActive(true);
                                Active = false;
                                temp = 0;
                            }
                        }
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        Ten_Centimeters.SetActive(false);
                        Five_Meters.SetActive(true);
                        ActiveState = ScanAction.Five_Meter_R;
                        Timer = 0;
                        Tick.Play();
                    }

                }
                break;

            //-------------------------------------------MODE2 SETTINGS-----------------------------------------//

            case ScanAction.Mode2:
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        Mode.SetActive(false);
                        Discrete.SetActive(true);
                        ActiveState = ScanAction.Discrete;
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        Mode.SetActive(false);
                        Forward_P.SetActive(true);
                        ActiveState = ScanAction.Forward_P;
                        Timer = 0;
                        Tick.Play();
                    }
                }
                break;

            case ScanAction.Discrete:
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        Discrete.SetActive(false);
                        Forward.SetActive(true);
                        ActiveState = ScanAction.Forward;
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= 3.0f)
                    {
                        //This will change the Active State to be the next one
                        Discrete.SetActive(false);
                        Slow.SetActive(true);
                        ActiveState = ScanAction.Slow2;
                        Timer = 0;
                        Tick.Play();
                    }
                }
                break;

            case ScanAction.Slow2:
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        Slow.SetActive(false);
                        Forward_P.SetActive(true);
                        SwitchSpeed = 3.0f;
                        ActiveState = ScanAction.Forward_P;
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= 3.0f)
                    {
                        //This will change the Active State to be the next one
                        Slow.SetActive(false);
                        Med.SetActive(true);
                        ActiveState = ScanAction.Med2;
                        Timer = 0;
                        Tick.Play();
                    }
                }
                break;

            case ScanAction.Med2:
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        Med.SetActive(false);
                        Forward_P.SetActive(true);
                        SwitchSpeed = 2.0f;
                        ActiveState = ScanAction.Forward_P;
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= 3.0f)
                    {
                        //This will change the Active State to be the next one
                        Med.SetActive(false);
                        Fast.SetActive(true);
                        ActiveState = ScanAction.Fast2;
                        Timer = 0;
                        Tick.Play();
                    }
                }
                break;

            case ScanAction.Fast2:
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        Fast.SetActive(false);
                        Forward_P.SetActive(true);
                        SwitchSpeed = 1.0f;
                        ActiveState = ScanAction.Forward_P;
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= 3)
                    {
                        //This will change the Active State to be the next one
                        Fast.SetActive(false);
                        Max.SetActive(true);
                        ActiveState = ScanAction.Max2;
                        Timer = 0;
                        Tick.Play();
                    }
                }
                break;

            case ScanAction.Max2:
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        Max.SetActive(false);
                        Forward_P.SetActive(true);
                        SwitchSpeed = 0.5f;
                        ActiveState = ScanAction.Forward_P;
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= 3)
                    {
                        //This will change the Active State to be the next one
                        Max.SetActive(false);
                        Discrete.SetActive(true);
                        ActiveState = ScanAction.Discrete;
                        Timer = 0;
                        Tick.Play();
                    }
                }
                break;
        }
    }
}
