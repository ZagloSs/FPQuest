using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Random = System.Random;

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

    [Header("Manos")]
    [SerializeField] private GameObject manoIzquierda;
    [SerializeField] private GameObject manoDerecha;

    [Header("Manos Sprites")]
    [SerializeField] private Sprite ManosRecibirDaño;
    [SerializeField] private Sprite ManosAtacar;
    [SerializeField] private Sprite ManosNormal;

    [Header("Explosion")]
    [SerializeField] private ParticleSystem ps;

    [Header("HealthBar")]
    [SerializeField] private Slider HealthBar;

    [Header("Donde aparece el player")]
    [SerializeField] private GameObject spawnPointPlayer;

    Vector3 att1 = new Vector2(0, -5);
    Vector3 att2 = new Vector2(1, -1);
    Vector3 att3 = new Vector2(-1, -1);
    Vector3 att4 = new Vector2(-3, -1);
    Vector3 att5 = new Vector2(3, -1);

   

    private GameObjectPool pool;
    private float timer = 0;
    private SpriteRenderer spriteCara;
    private Vector3 target = new Vector3(0, -5, 0);

    private Vector3 iPosMD;
    private Vector3 iPosMI;
    private GameObject player;
    private bool justEntered = true;

    private void Start()
    {
        spriteCara = GetComponent<SpriteRenderer>(); 
        pool = GetComponent<GameObjectPool>();
        iPosMD = manoDerecha.transform.position;
        iPosMI = manoIzquierda.transform.position;
        AudioManager.instance.playBoss();
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    private void Update()
    {
        if (justEntered)
        {
            player.transform.position = spawnPointPlayer.transform.position;
            justEntered = false;
        }
        if(timer > 5f)
        {
            Random rand = new Random();
            int r = rand.Next(0, 2);
            timer = 0;
            StartCoroutine(r == 1 ? shooting() : ataquesManos()) ;

        }
        timer += Time.deltaTime;

        HealthBar.value = health;
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
        manoDerecha.GetComponent<SpriteRenderer>().sprite = ManosRecibirDaño;
        manoIzquierda.GetComponent<SpriteRenderer>().sprite = ManosRecibirDaño;
        yield return new WaitForSeconds(2f);
        spriteCara.sprite = caraNormal;
        manoDerecha.GetComponent<SpriteRenderer>().sprite = ManosNormal;
        manoIzquierda.GetComponent<SpriteRenderer>().sprite = ManosNormal;
    }

    public IEnumerator Death()
    {
        timer = 20f;
        StopAllCoroutines();
        spriteCara.sprite = caraMuerte;
        yield return new WaitForSeconds(2f);
        ps.Play();
        GetComponent<SpriteRenderer>().enabled = false;
        manoIzquierda.GetComponent<SpriteRenderer>().enabled = false;
        manoDerecha.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(2f);
        endGame();

    }

    public void TomarDaño(float daño)
    {
        StartCoroutine(blinkEffect());
        health -= daño;
        if (health <= 0)
        {
            StartCoroutine(Death());
        }
    }

    public IEnumerator blinkEffect()
    {
        spriteCara.color = new Color(1, 0, 0);
        yield return new WaitForSeconds(0.1f);
        spriteCara.color = Color.white;
    }

    public IEnumerator ataqueMano(GameObject mano, Vector3 moveBack)
    {
        target = GameObject.FindGameObjectWithTag("Player").transform.position;
        mano.GetComponent<SpriteRenderer>().sprite = ManosAtacar;
        yield return new WaitForSeconds(0.5f);
        mano.transform.DOMove(target, 0.5f);
        yield return new WaitForSeconds(1f);
        mano.transform.DOMove(moveBack, 1f);
        yield return new WaitForSeconds(1f);
        mano.GetComponent<SpriteRenderer>().sprite = ManosNormal;
    }

    public IEnumerator ataquesManos()
    {
        StartCoroutine(ataqueMano(manoIzquierda, iPosMI));
        yield return new WaitForSeconds(2f);
        StartCoroutine(ataqueMano(manoDerecha, iPosMD));
    }


    public void endGame()
    {
       Destroy(gameObject);



    }



    public float getDamage()
    {
        return damage;
    }

  



}
