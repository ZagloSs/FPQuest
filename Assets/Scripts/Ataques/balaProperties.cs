using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balaProperties : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 mousePos;
    private float force = 2f;
    private float timer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(rotation.x, rotation.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,-rot);
    }

    private void Update()
    {
        if(timer > 6f)
        {
            gameObject.SetActive(false);
            timer = 0f;
        }
        timer += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            AudioManager.instance.PlayEnemyHitted();
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            enemy.TomarDaño(Properties.instance.Damage);
            gameObject.SetActive(false);
        }
    }

}
