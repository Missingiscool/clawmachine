using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveClaw : MonoBehaviour
{
    public float speed = 10.0f;
    public float dropSpeed = 10.0f;
    private float horizontalInput;
    private float verticalInput;

    public int state;
    public Text text;
    private bool clawable;
    private Vector3 startPos;


    // Start is called before the first frame update
    void Start()
    {
        text.text = "Start";
        state = 0;
        clawable = true;
        startPos = transform.position;
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");


        //start clawing
        if (Input.GetKey(KeyCode.Space) && clawable)
        {
            clawable = false;
            state = 1;
        }

        //drop 1
        if(state == 1 && !clawable)
        {
            transform.position += Vector3.down * Time.deltaTime * dropSpeed;
            printText("claw drop now");
        }
        //close claw 2
        if (state == 2 && !clawable)
        {
            printText("claw close now");
            CloseClaw();
        }
        //raise claw
        if (state == 3 && !clawable)
        {
            printText("claw lifting now");
            if (transform.position.y < startPos.y)
            {
                transform.position += Vector3.up * Time.deltaTime * dropSpeed;
                //print(startPos.y.ToString() + " " + transform.position.y.ToString());
            }
            else
            {
                state = 4;
            }
        }
        //move claw back to start
        if (state == 4 && !clawable)
        {
            printText("claw moving back to starting position");
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
                state = 5;
            }
        }
        //open claw
        if (state == 5 && !clawable)
        {
            printText("claw opening now");
            OpenClaw();
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
        printText("collision detected");
        if(state == 1)
        {
            state = 2;
        }
    }
    private void DropClaw()
    {
        //drop claw down
        printText("claw dropped");
        CloseClaw();
    }

    private void CloseClaw()
    {
        //close claw
        printText("claw closed");
        state = 3;
        //LiftClaw();
    }

    private void LiftClaw()
    {
        printText("claw lifted");
        ResetPosition();
    }

    private void ResetPosition()
    {
        printText("claw back to starting position");
        OpenClaw();
    }

    private void OpenClaw()
    {
        printText("claw opened");
        state = 0;
        clawable = true;
    }

    private void printText(string s)
    {
        text.text = s;
    }
}
