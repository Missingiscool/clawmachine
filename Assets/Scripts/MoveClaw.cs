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
    public GameObject claw;
    public Collider claw1;
    public Collider claw2;
    public Collider claw3;
    public Collided claw1c;
    public Collided claw2c;
    public Collided claw3c;

    private int state;
    public Text text;
    private bool clawable;
    bool checkit = true;
    bool closing = false;
    private Vector3 startPos;

    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        text.text = "Start";
        state = 0;
        clawable = true;
        startPos = transform.position;
        anim = claw.GetComponent<Animator>();
        claw1c = claw1.GetComponent<Collided>();
        claw2c = claw2.GetComponent<Collided>();
        claw3c = claw3.GetComponent<Collided>();
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
            checkit = true;
            state = 1;
        }

        if (state == 1 && checkit && !clawable)
        {
            claw1c.collided = false;
            claw2c.collided = false;
            claw3c.collided = false;
            checkit = false;
        }

        //drop 1
        if (state == 1 && !clawable)
        {
            transform.position += Vector3.down * Time.deltaTime * dropSpeed;
            printText("claw drop now");
        }

        if (state == 1)
        {
            if (claw1c.getCollided() || claw2c.getCollided() || claw3c.getCollided())
            {
                state = 2;
            }
        }

        //close claw 2
        if (state == 2 && !clawable)
        {
            printText("claw close now");
            
            //TODO:
            CloseClaw();
        }
        //claw closed 3
        if (state == 3 && !clawable && closing)
        {
            if (AnimatorIsPlaying("Close"))
            {
                closing = false;
                transform.position += Vector3.down * Time.deltaTime * dropSpeed;
                state = 4;
            }
            else
            {
                transform.position += Vector3.down * Time.deltaTime * dropSpeed;
            }
             
        }
        //raise claw 4
        if (state == 4 && !clawable)
        {
            //printText(AnimatorIsPlaying().ToString());
            if (transform.position.y < startPos.y)
            {
                transform.position += Vector3.up * Time.deltaTime * dropSpeed;
                //print(startPos.y.ToString() + " " + transform.position.y.ToString());
            }
            else
            {
                state = 5;
            }
        }
        //move claw back to start 5
        if (state == 5 && !clawable)
        {
            printText("claw moving back to starting position");
            if (transform.position.z > startPos.z)
            {
                transform.position += Vector3.back * Time.deltaTime * speed;
                //print(transform.position.z.ToString());
            }
            else if (transform.position.x > startPos.x)
            {
                transform.position += Vector3.left * Time.deltaTime * speed;
                //print(transform.position.x.ToString());
            }
            else
            {
                state = 6;
            }
        }
        //open claw
        if (state == 6 && !clawable)
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


    private void DropClaw()
    {
        //drop claw down
        printText("claw dropped");
        CloseClaw();
    }

    bool AnimatorIsPlaying()
    {
        return anim.GetCurrentAnimatorStateInfo(0).length >
               anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    bool AnimatorIsPlaying(string stateName)
    {
        return AnimatorIsPlaying() && anim.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }

    private void CloseClaw()
    {
        //close claw

        //printText(anim.GetClipCount().ToString());

        anim.Play("Close");
        closing = true;

        //printText("claw closed");
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

        anim.Play("Open");

        printText("claw opened");
        state = 0;
        clawable = true;
    }

    private void printText(string s)
    {
        text.text = s;
    }
}
