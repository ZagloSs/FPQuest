using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;

public class Magia : MonoBehaviour
{
    [SerializeField] private GameObject bala;
    [SerializeField] private GameObjectPool pool;
    private bool puedeDisparar = true;
    private float timer = 0f;
    private float bulletcooldown = 1f;
    


    // Update is called once per frame
    void Update()
    {
        bulletcooldown = Properties.instance.AttSpeed;
        if (!puedeDisparar)
        {
            if(timer > bulletcooldown)
            {
                puedeDisparar = true;
                timer = 0f;
            }

            timer += Time.deltaTime;
        }

        if (Input.GetMouseButton(0) && puedeDisparar)
        {
            puedeDisparar = false;
            GameObject bullet = pool.GetInactiveGameObject();
            if (bullet)
            {
                AudioManager.instance.PlayFireball();
                bullet.SetActive(true);
                bullet.transform.position = transform.position;
            }
        }
    }
}
