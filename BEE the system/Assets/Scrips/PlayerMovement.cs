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

    
    //private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*animator = GetComponentInChildren<Animator>();
        animator.SetBool("WakeUp", true);*/
    }

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
        }
    }
    


    

    void getInput()
    {
        xInput = Input.GetAxis("Horizontal");

    }


}