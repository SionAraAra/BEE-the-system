using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 baseScale;
    public Rigidbody2D body;

    public float groundSpeed = 3f;



    
    private float xInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    void Awake()
    {
        baseScale = transform.localScale;
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
            float increment = xInput;
            float newSpeed = Mathf.Clamp(body.linearVelocity.x + increment, -groundSpeed, groundSpeed);
            body.linearVelocity = new Vector2(newSpeed, body.linearVelocity.y);

            // Face direction without "double flipping"
            float dir = Mathf.Sign(xInput);
            transform.localScale = new Vector3(
                Mathf.Abs(baseScale.x) * (dir < 0 ? -1f : 1f),
                baseScale.y,
                baseScale.z
            );
        }else if (xInput == 0)
        {
            body.linearVelocityX = 0;
            body.linearVelocityY = 0;
        }
    }
    


    

    void getInput()
    {
        xInput = Input.GetAxis("Horizontal");

    }


}