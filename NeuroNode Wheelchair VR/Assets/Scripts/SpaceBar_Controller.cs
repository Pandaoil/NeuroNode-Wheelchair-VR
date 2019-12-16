using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBar_Controller : MonoBehaviour
{
    [SerializeField]
    public enum ScanAction
    {
        Forward,
        R_Clockwise,
        R_Anticloise,
        Reverse,
        Mode,
        Slow,
        Med,
        Fast,
        Max
    }

    public GameObject[] pivots;
    public Rigidbody RigidBody;

    public float Speed = 70;
    public float Drag = 1;
    public float AngularDrag = 0.05f;
    public float Timer = 0;
    public float SwitchSpeed = 3.0f;
    private float temp;

    public bool Active;

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

        if (Input.GetKey(KeyCode.Space))
        {
            Active = true;

            if(Active == true)
            {
                Timer = 0;
            }
        }
        else
        {
            Active = false;
        }

        switch (ActiveState)
        {
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
                    }

                    Timer += Time.deltaTime;

                    if (Timer >= SwitchSpeed)
                    {
                        //This will change the Active State to be the next one
                        ActiveState = ScanAction.R_Anticloise;
                        Timer = 0;
                    }
                }
                break;

            case ScanAction.R_Anticloise:
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        //The body of this if statement will cease all force applied to the rigidbody and then apply a force on the inverse vector
                        RigidBody.angularVelocity = new Vector3(0, -1.0f, 0);
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

            case ScanAction.Mode:
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        ActiveState = ScanAction.Slow;
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

            case ScanAction.Slow:
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        SwitchSpeed = 3.0f;
                        ActiveState = ScanAction.Forward;
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
                        ActiveState = ScanAction.Forward;
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
                        ActiveState = ScanAction.Forward;
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
                        ActiveState = ScanAction.Forward;
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
        }
    }
}
