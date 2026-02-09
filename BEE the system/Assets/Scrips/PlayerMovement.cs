using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public Rigidbody2D body;

    public float groundSpeed = 3f;



    
    private float xInput;

    
    //private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*animator = GetComponentInChildren<Animator>();
        animator.SetBool("WakeUp", true);*/
    }

    // Update is called once per frame

    void Update()
    {
        getInput();

    }

    private void FixedUpdate()
    {
        
        MoveWithInput();
        
    }

    void MoveWithInput()
    {
        if (Mathf.Abs(xInput) > 0)
        {
            float increment = xInput ;
            float newSpeed = Mathf.Clamp(body.linearVelocity.x + increment, -groundSpeed, groundSpeed);
            body.linearVelocity = new Vector2(newSpeed, body.linearVelocity.y);
                    
            //Changing looking direction
            float direction = Mathf.Sign(xInput);
            transform.localScale = new Vector3(direction, transform.localScale.y, transform.localScale.z);
        }
        
                
    }
    


    

    void getInput()
    {
        xInput = Input.GetAxis("Horizontal");

    }


}