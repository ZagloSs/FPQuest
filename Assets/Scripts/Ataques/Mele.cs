using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mele : MonoBehaviour
{
    [SerializeField] private GameObject meleAttack;
    private Vector3 targetPos;
    private float timer = 0f;
    private bool puedeAtacar = true;
    private Joystick attackJoystick;

    private void Start()
    {
        attackJoystick = GameManager.instance.GetAttackJoystick();
    }

    void Update()
    {
        if (Application.isMobilePlatform && attackJoystick != null)
        {
            targetPos = new Vector3(attackJoystick.Horizontal, attackJoystick.Vertical, 0);
        }
        else
        {
            targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        Vector3 rotation = targetPos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        if (!puedeAtacar)
        {
            if (timer > Properties.instance.AttSpeed)
            {
                timer = 0f;
                puedeAtacar = true;
            }
            timer += Time.deltaTime;
        }

        if ((Input.GetMouseButton(0) || (Application.isMobilePlatform && attackJoystick != null && (attackJoystick.Horizontal != 0 || attackJoystick.Vertical != 0))) && puedeAtacar)
        {
            AudioManager.instance.PlayMeleeAtt();
            transform.rotation = Quaternion.Euler(0, 0, rotZ);
            meleAttack.SetActive(true);
            puedeAtacar = false;
        }
    }
}