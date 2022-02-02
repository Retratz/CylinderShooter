using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Vector3 velocity;
    private Rigidbody myRigidBody;
    

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        
    }

    public void Move(Vector3 _velocity) 
    {
        velocity = _velocity;
    }

    public void LookAt(Vector3 lookPoint) //player facing cursor
    {
        // for player to not bend over while trying to face towards cursor
        Vector3 heighCorrected = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
        transform.LookAt(heighCorrected);
    }
    void FixedUpdate()
    {
        myRigidBody.MovePosition(myRigidBody.position + velocity * Time.deltaTime);
    }
}
