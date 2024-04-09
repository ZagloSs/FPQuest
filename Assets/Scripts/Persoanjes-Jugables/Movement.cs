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
        animator.SetBool("IsWalkingSide", false);
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
            animator.SetBool("IsWalkingSide", true);
        }
        else if(moveX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            animator.SetBool("IsWalkingSide", true);
        }
        else
        {
            animator.SetBool("IsWalkingSide", false);
        }

        if (moveY < 0)
        {
            animator.SetBool("IsWalkingDown", true);
        }
        else if (moveY > 0)
        {
            animator.SetBool("IsWalkingUp", true);
        }
        else
        {
            animator.SetBool("IsWalkingDown", false);
            animator.SetBool("IsWalkingUp", false);
        }
    }
}
