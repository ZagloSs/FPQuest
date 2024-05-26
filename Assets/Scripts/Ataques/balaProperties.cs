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
            enemy.TomarDa�o(Properties.instance.Damage);

            StartCoroutine(disable());

  
            
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            StartCoroutine(disable());
        }
    }

    public IEnumerator disable()
    {
        ps.Play();
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }


}
