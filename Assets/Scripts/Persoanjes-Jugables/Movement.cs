using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody2D _rb;
    public Animator animator;

    

    private void Start()
    {
        //Pruebas 
        animator.SetBool("IsWalkingSide", true);
        //FIn Pruebas (Borrar Luego)
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        _rb.velocity = new Vector2(moveX, moveY) * speed;
        if (moveX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

    }


        
}
