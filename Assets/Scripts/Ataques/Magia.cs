using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;

public class Magia : MonoBehaviour
{

    [SerializeField] private GameObjectPool pool;
    private bool puedeDisparar = true;
    private float timer = 0f;
    private float bulletcooldown = 1f;
    private float force = 2f;



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

                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 direction = mousePos - bullet.transform.position;
                Vector3 rotation = bullet.transform.position - mousePos;
                rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
                float rot = Mathf.Atan2(rotation.x, rotation.y) * Mathf.Rad2Deg;
                bullet.transform.rotation = Quaternion.Euler(0, 0, -rot);
            }
        }
    }
}
