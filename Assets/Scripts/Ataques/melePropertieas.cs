using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class melePropertieas : MonoBehaviour
{
    private float timer = 0f;


    void Update()
    {
        if(gameObject && timer > 0.2f)
        {
            timer = 0f;
            gameObject.SetActive(false);
        }
        timer += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            AudioManager.instance.PlayEnemyHitted();
            enemy.TomarDaño(Properties.instance.Damage);
        }
    }

}
