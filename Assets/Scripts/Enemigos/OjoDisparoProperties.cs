using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OjoDisparo : MonoBehaviour
{

    [SerializeField] private float damage;
    private Rigidbody2D rb;
    [SerializeField] private ParticleSystem ps;

    private float timer = 0f;

    private void Update()
    {
        if (timer > 6f)
        {
            gameObject.SetActive(false);
            timer = 0f;
        }
        timer += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Colided");
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.PlayEnemyHitted();
            Properties.instance.gotHittedByBullet(damage);

            gameObject.SetActive(false);
        }
    }

  

}
