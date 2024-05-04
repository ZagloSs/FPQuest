using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D _rb;
    public Animator animator;
    public Vector2 moveDir;




    private void Start()
    {
        animator.runtimeAnimatorController = GameManager.instance.GetAnimator();
        //Pruebas
        animator.SetBool("IsWalkingSide", false);
        //Fin Pruebas (Borrar Luego)
        _rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        
        moveDir = new Vector2(moveX, moveY).normalized;

        _rb.velocity = new Vector2(moveX, moveY) * GetComponent<Properties>().Speed;
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
