using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D _rb;
    public Animator animator;
    public Vector2 moveDir;

    // Para el joystick virtual
    private Joystick movementJoystick;

    private void Start()
    {
        animator.runtimeAnimatorController = GameManager.instance.GetAnimator();
        // Pruebas
        animator.SetBool("IsWalkingSide", false);
        // Fin Pruebas (Borrar Luego)
        _rb = GetComponent<Rigidbody2D>();

        // Obtener el joystick del GameManager
        movementJoystick = GameManager.instance.GetMovementJoystick();
    }

    void Update()
    {
        float moveX = 0f;
        float moveY = 0f;

        // Verificar si es una plataforma móvil o PC
        if (Application.isMobilePlatform && movementJoystick != null)
        {
            // Entrada para móviles usando el joystick virtual
            moveX = movementJoystick.Horizontal;
            moveY = movementJoystick.Vertical;
        }
        else
        {
            // Entrada para PC usando teclado
            moveX = Input.GetAxis("Horizontal");
            moveY = Input.GetAxis("Vertical");
        }

        moveDir = new Vector2(moveX, moveY).normalized;
        _rb.velocity = new Vector2(moveX, moveY) * GetComponent<Properties>().Speed;

        if (moveX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            animator.SetBool("IsWalkingSide", true);
        }
        else if (moveX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
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

    public void death()
    {
        animator.SetBool("IsWalkingDown", false);
        animator.SetBool("IsWalkingSide", false);
        animator.SetBool("IsWalkingUp", false);
    }
}
