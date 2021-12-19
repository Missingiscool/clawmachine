using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveClaw : MonoBehaviour
{
    public float speed = 10.0f;
    public float dropSpeed = 10.0f;
    private float horizontalInput;
    private float verticalInput;

    public int position;
    private bool clawable;
    private Vector3 startPos;


    // Start is called before the first frame update
    void Start()
    {
        position = 0;
        clawable = true;
        startPos = transform.position;
    }
    // Update is called once per frame
    void Update()
    {

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");


        //start clawing
        if (Input.GetKey(KeyCode.Space) && clawable)
        {
            clawable = false;
            position = 1;
        }

        //drop 1
        if(position == 1 && !clawable)
        {
            transform.position += Vector3.down * Time.deltaTime * dropSpeed;
            print("claw drop now");
        }
        //close claw 2
        if (position == 2 && !clawable)
        {
            print("claw close now");
            position = 3;
        }
        //raise claw
        if (position == 3 && !clawable)
        {
            print("claw lifting now");
            if (transform.position.y < startPos.y)
            {
                transform.position += Vector3.up * Time.deltaTime * dropSpeed;
                print(startPos.y.ToString() + " " + transform.position.y.ToString());
            }
            else
            {
                position = 4;
            }
        }
        //move claw back to start
        if (position == 4 && !clawable)
        {
            print("claw moving back to starting position");
            if (transform.position.x > startPos.x)
            {
                transform.position += Vector3.left * Time.deltaTime * speed;
                //print(transform.position.x.ToString());
            }
            else if (transform.position.z > startPos.z)
            {
                transform.position += Vector3.back * Time.deltaTime * speed;
                //print(transform.position.z.ToString());
            }
            else
            {
                position = 5;
            }
        }
        //open claw
        if (position == 5 && !clawable)
        {
            print("claw opening now");

            position = 0;
            clawable = true;
        }

        if (clawable) { 
            transform.position += Vector3.forward * Time.deltaTime * speed * verticalInput;
            transform.position += Vector3.right * Time.deltaTime * speed * horizontalInput;
        }

        //transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);

        //transform.Translate(Vector3.right * Time.deltaTime * turnSpeed * horizontalInput);

    }

    void OnCollisionEnter(Collision collision)
    {
        print("collision detected");
        if(position == 1)
        {
            position = 2;
        }
    }
    private void DropClaw()
    {
        //drop claw down
        print("claw dropped");
        CloseClaw();
    }

    private void CloseClaw()
    {
        //close claw
        print("claw closed");
        LiftClaw();
    }

    private void LiftClaw()
    {
        print("claw lifted");
        ResetPosition();
    }

    private void ResetPosition()
    {
        print("claw back to starting position");
        OpenClaw();
    }

    private void OpenClaw()
    {
        print("claw opened");
        clawable = true;
    }
}
