using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class FBProperties : MonoBehaviour
{
    [Header("Propiedades")]
    [SerializeField] private float health;
    [SerializeField] private float damage;

    [Header("Sprites de la Cara")]
    [SerializeField] private Sprite caraNormal;
    [SerializeField] private Sprite caraRecibirDaño;
    [SerializeField] private Sprite caraDisparando;
    [SerializeField] private Sprite caraMuerte;

    [Header("Desde donde dispara")]
    [SerializeField] private GameObject puntoDeDisparo;

    private bool isShooting = false;
    Vector3 att1 = new Vector2(0, -5);
    Vector3 att2 = new Vector2(1, -1);
    Vector3 att3 = new Vector2(-1, -1);
    Vector3 att4 = new Vector2(-3, -1);
    Vector3 att5 = new Vector2(3, -1);

    private GameObjectPool pool;
    private float timer = 0;
    private SpriteRenderer spriteCara;

    private void Start()
    {
        spriteCara = GetComponent<SpriteRenderer>(); 
        pool = GetComponent<GameObjectPool>();
        StartCoroutine(shooting());
    }

    private void Update()
    {
        
        if(timer > 6f)
        {
            timer = 0;
            StartCoroutine(shooting());
        }
        timer += Time.deltaTime;

        //Cuando le quites la mitad de la vida va a hacer una carita de dolor
        if(health <= health * 0.5)
        {
            StartCoroutine(auch());
        }
    }

    public IEnumerator shooting()
    {
        spriteCara.sprite = caraDisparando;
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.3f);
            shoot(att1); shoot(att2); shoot(att3); shoot(att4); shoot(att5);
        }
        spriteCara.sprite = caraNormal;
    }

    public void shoot(Vector3 att)
    {
        isShooting = true;

           
        GameObject bullet = pool.GetInactiveGameObject();
        if (bullet)
        {
            bullet.transform.position = puntoDeDisparo.transform.position;
            bullet.SetActive(true);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = att.normalized * 5f;
            Vector3 rotation = bullet.transform.position - att;
            float rot = Mathf.Atan2(rotation.x, rotation.y) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0, 0, -rot);
        }
    }

    //Carita de dolor
    public IEnumerator auch()
    {
        spriteCara.sprite = caraRecibirDaño;
        yield return new WaitForSeconds(2f);
        spriteCara.sprite = caraNormal;
    }

  



}
