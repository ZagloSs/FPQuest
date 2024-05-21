using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class OjoBehaviour : MonoBehaviour
{

    private GameObjectPool pool;
    private Animator animator;
    private float timer = 0f;
    private bool canTp = true;
    private GameObject player;

    private void Start()
    {
        pool = GetComponent<GameObjectPool>();
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (canTp)
        {
            if(timer > 3f)
            {
                timer = 0f;
                StartCoroutine(tp());
            }
            timer += Time.deltaTime;
        }
    }

    public IEnumerator tp()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        animator.SetBool("Abrir", false);
        canTp = false;
        yield return new WaitForSeconds(1f);
        animator.SetBool("Abrir", true);
        GetComponent<BoxCollider2D>().enabled = true;
        StartCoroutine(canShoot());
        canTp=true;
    }

    public IEnumerator canShoot()
    {
        yield return new WaitForSeconds(1.5f);
        shoot();
    }
    
    public void shoot()
    {
        GameObject bullet = pool.GetInactiveGameObject();
        if (bullet)
        {
            bullet.SetActive(true);
            bullet.transform.position = transform.position;

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Vector3 playerPos = player.transform.position;
            Vector3 direction = playerPos - bullet.transform.position;
            Vector3 rotation = bullet.transform.position - playerPos;
            rb.velocity = new Vector2(direction.x, direction.y).normalized * 2f;
            float rot = Mathf.Atan2(rotation.x, rotation.y) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0, 0, -rot);
        }
    }
}
