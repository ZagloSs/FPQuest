using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class MeordeadorBoss : MonoBehaviour
{
    private GameObjectPool pool;
    private GameObject player;
    [SerializeField] private Slider HealthBar;

    private float timer = 0;

    void Start()
    {
        pool = GetComponent<GameObjectPool>();
        player = GameObject.FindWithTag("Player");
    }


    void Update()
    {
        if(timer > 6f)
        {
            timer = 0;
            StartCoroutine(attack());
        }
        timer += Time.deltaTime;

        HealthBar.value = GetComponent<EnemyController>().vida;

    }


    private IEnumerator attack()
    {
        shoot();
        yield return new WaitForSeconds(0.2f);
        shoot();
        yield return new WaitForSeconds(0.2f);
        shoot();
        yield return new WaitForSeconds(1f);
        shoot();
        yield return new WaitForSeconds(0.2f);
        shoot();
        yield return new WaitForSeconds(0.2f);
        shoot();
        yield return new WaitForSeconds(1f);
        shoot();
        yield return new WaitForSeconds(0.2f);
        shoot();
        yield return new WaitForSeconds(0.2f);
        shoot();
    }


    private void shoot()
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
