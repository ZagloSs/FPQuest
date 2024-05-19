using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balaProperties : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 mousePos;

    private float timer = 0f;

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
            enemy.TomarDa�o(Properties.instance.Damage);
            gameObject.SetActive(false);

            //Reiniciamos los valores a 0
            transform.rotation = Quaternion.Euler(0,0,0);
            transform.position = Vector3.zero;
            rb.velocity = Vector2.zero;
        }
    }

}