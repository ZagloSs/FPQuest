using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class mele : MonoBehaviour
{

    [SerializeField] private GameObject meleAttack;
    private Vector3 mousePos; 
    private float timer = 0f;
    private bool puedeAtacar = true;

  
    

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos -transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x)* Mathf.Rad2Deg;
        if(!puedeAtacar)
        {
            if(timer > Properties.instance.AttSpeed)
            {
                timer = 0f;
                puedeAtacar = true;
            }
            timer += Time.deltaTime;
        }
        if (Input.GetMouseButton(0) && puedeAtacar)
        {
            AudioManager.instance.PlayMeleeAtt();
            transform.rotation = Quaternion.Euler(0, 0, rotZ);
            meleAttack.SetActive(true);
            puedeAtacar = false;
        }
    }
}
