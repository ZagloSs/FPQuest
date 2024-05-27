using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balaProperties : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 mousePos;
    [SerializeField] private ParticleSystem ps;

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
            enemy.TomarDaño(Properties.instance.Damage);

            gameObject.SetActive(false);

  
            
        }
        if (collision.gameObject.CompareTag("Boss"))
        {
            AudioManager.instance.PlayEnemyHitted();
            FBProperties enemy = collision.gameObject.GetComponent<FBProperties>();
            enemy.TomarDaño(Properties.instance.Damage);

            gameObject.SetActive(false);



        }



        if (collision.gameObject.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
        }
    }


}
