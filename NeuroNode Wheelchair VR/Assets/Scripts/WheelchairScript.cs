using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelchairScript : MonoBehaviour
{
    public GameObject[] pivots;
    public Rigidbody RigidBody;
    public float Speed = 128;
    public float Drag = 1;
    public float AngularDrag = .05f;
    private float temp;
    private Vector3 ForwardVector;

    void Start ()
    {
        RigidBody = GetComponent<Rigidbody>();
	}
	
	void Update ()
    {
        ForwardVector = transform.InverseTransformDirection(RigidBody.velocity); //the inverse transform direction allows for the velocity to be a negative float value.

        if (Input.GetKey(KeyCode.W))
        {
            //The body of this if statement will cease all angular velocity and apply a forward vector
            RigidBody.angularVelocity = Vector3.zero;
            RigidBody.AddForce(transform.forward * Speed * Time.deltaTime, ForceMode.Impulse);
        }

        if (Input.GetKey(KeyCode.S))
        {
            //The body of this if statement will cease all angular velocity and apply a backward vector
            RigidBody.angularVelocity = Vector3.zero;
            RigidBody.AddForce(-transform.forward * Speed * Time.deltaTime, ForceMode.Impulse);
        }

        if (Input.GetKey(KeyCode.A))
        {
            //The body of this if statement will cease all force applied to the rigidbody and then apply a force on the inverse vector
            RigidBody.angularVelocity = new Vector3(0, -1.0f, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            //The body of this if statement will cease all force applied to the rigidbody and then apply a force on the inverse vector
            RigidBody.angularVelocity = new Vector3(0, 1.0f, 0);
        }

        if(ForwardVector.z > 0.0f)
        {
            //the body of this if statement rotates the transform of each element of the pivots array so that the wheels are rotating on a positive vector
            pivots[0].transform.Rotate(Vector3.down * RigidBody.velocity.magnitude);
            pivots[1].transform.Rotate(Vector3.up * RigidBody.velocity.magnitude);
        }
        else if(ForwardVector.z < 0.0f)
        {
            //the body of this if statement rotates the transform of each element of the pivots array so that the wheels are rotating on a negative vector
            pivots[0].transform.Rotate(Vector3.up * RigidBody.velocity.magnitude);
            pivots[1].transform.Rotate(Vector3.down * RigidBody.velocity.magnitude);
        }

        if (Math.Round(RigidBody.angularVelocity.y, 2) > 0.0f)
        {
            //the body of this if statement rotates the transform of each element of the pivots array so that the wheels are rotating on a positive vector
            transform.RotateAround(pivots[1].transform.position, Vector3.up, Speed * RigidBody.angularVelocity.y * Time.deltaTime);
            pivots[0].transform.Rotate(Vector3.down * Speed * RigidBody.angularVelocity.y * Time.deltaTime);
        }
        else if (Math.Round(RigidBody.angularVelocity.y, 2) < 0.0f)
        {
            //the body of this if statement rotates the transform of each element of the pivots array so that the wheels are rotating on a negative vector
            transform.RotateAround(pivots[0].transform.position, Vector3.up, Speed * RigidBody.angularVelocity.y * Time.deltaTime);
            pivots[1].transform.Rotate(Vector3.down * Speed * RigidBody.angularVelocity.y * Time.deltaTime);
        }

        /*transform.GetChild(1).GetChild(4).GetComponent<Rigidbody>().drag = transform.GetChild(1).GetChild(3).GetComponent<Rigidbody>().drag =*/ RigidBody.drag = Drag;
        /*transform.GetChild(1).GetChild(4).GetComponent<Rigidbody>().angularDrag = transform.GetChild(1).GetChild(3).GetComponent<Rigidbody>().angularDrag =*/ RigidBody.angularDrag = AngularDrag;
    }
}
