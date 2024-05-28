using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magia : MonoBehaviour
{
    private GameObjectPool pool;
    private bool puedeDisparar = true;
    private float timer = 0f;
    private float bulletcooldown = 1f;
    private float force = 8f;
    private Joystick attackJoystick;

    private void Start()
    {
        pool = GameObject.FindGameObjectWithTag("BP").GetComponent<GameObjectPool>();
        attackJoystick = GameManager.instance.GetAttackJoystick();
    }

    void Update()
    {
        bulletcooldown = Properties.instance.AttSpeed;

        if (!puedeDisparar)
        {
            if (timer > bulletcooldown)
            {
                puedeDisparar = true;
                timer = 0f;
            }

            timer += Time.deltaTime;
        }

        bool isAttacking = Input.GetMouseButton(0) || (Application.isMobilePlatform && attackJoystick != null && (attackJoystick.Horizontal != 0 || attackJoystick.Vertical != 0));

        if (isAttacking && puedeDisparar)
        {
            puedeDisparar = false;
            GameObject bullet = pool.GetInactiveGameObject();

            if (bullet)
            {
                AudioManager.instance.PlayFireball();
                bullet.SetActive(true);
                bullet.transform.position = transform.position;

                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                Vector3 targetPos;

                if (Application.isMobilePlatform && attackJoystick != null)
                {
                    targetPos = new Vector3(attackJoystick.Horizontal, attackJoystick.Vertical, 0) + transform.position;
                }
                else
                {
                    targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }

                Vector3 direction = targetPos - bullet.transform.position;
                Vector3 rotation = bullet.transform.position - targetPos;
                rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
                float rot = Mathf.Atan2(rotation.x, rotation.y) * Mathf.Rad2Deg;
                bullet.transform.rotation = Quaternion.Euler(0, 0, -rot);
            }
        }
    }
}
