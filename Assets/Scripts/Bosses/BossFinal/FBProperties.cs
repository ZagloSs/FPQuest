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

    [Header("SFX")]
    [SerializeField] private AudioClip huh;
    [SerializeField] private AudioClip explosion;

    [Header("Sprites de la Cara")]
    [SerializeField] private Sprite caraNormal;
    [SerializeField] private Sprite caraRecibirDaņo;
    [SerializeField] private Sprite caraDisparando;
    [SerializeField] private Sprite caraMuerte;

    [Header("Desde donde dispara")]
    [SerializeField] private GameObject puntoDeDisparo;

    [Header("Manos")]
    [SerializeField] private GameObject manoIzquierda;
    [SerializeField] private GameObject manoDerecha;

    [Header("Manos Sprites")]
    [SerializeField] private Sprite ManosRecibirDaņo;
    [SerializeField] private Sprite ManosAtacar;
    [SerializeField] private Sprite ManosNormal;

    [Header("Explosion")]
    [SerializeField] private ParticleSystem ps;

    [Header("HealthBar")]
    [SerializeField] private Slider HealthBar;

    [Header("CanvasPosMortem")]
    [SerializeField] private Canvas canvasPosMortem;

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

    private bool isAlive = true;


    private void Start()
    {
        spriteCara = GetComponent<SpriteRenderer>(); 
        pool = GetComponent<GameObjectPool>();


        iPosMD = manoDerecha.transform.position;
        iPosMI = manoIzquierda.transform.position;

        AudioManager.instance.playBoss();

    }

    private void Update()
    {

        if (timer > 5f && isAlive)
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
            Vector3 rotation = att - bullet.transform.position;
            float rot = Mathf.Atan2(rotation.x, rotation.y) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0, 0, rot);
        }
    }

    //Carita de dolor
    public IEnumerator auch()
    {
        spriteCara.sprite = caraRecibirDaņo;
        manoDerecha.GetComponent<SpriteRenderer>().sprite = ManosRecibirDaņo;
        manoIzquierda.GetComponent<SpriteRenderer>().sprite = ManosRecibirDaņo;
        yield return new WaitForSeconds(2f);
        spriteCara.sprite = caraNormal;
        manoDerecha.GetComponent<SpriteRenderer>().sprite = ManosNormal;
        manoIzquierda.GetComponent<SpriteRenderer>().sprite = ManosNormal;
    }

    public IEnumerator Death()
    {
        isAlive = false;
        StopCoroutine(ataquesManos());
        StopCoroutine(shooting());

        spriteCara.sprite = caraMuerte;
        AudioManager.instance.playMonsterDeathSound(huh);
        yield return new WaitForSeconds(2f);
        AudioManager.instance.playMonsterDeathSound(explosion);
        ps.Play();
        GetComponent<SpriteRenderer>().enabled = false;
        manoIzquierda.GetComponent<SpriteRenderer>().enabled = false;
        manoDerecha.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(2f);
        endGame();

    }

    public void TomarDaņo(float daņo)
    {
        StartCoroutine(blinkEffect());
        health -= daņo;
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
       Instantiate(canvasPosMortem);
       Destroy(gameObject);

    }



    public float getDamage()
    {
        return damage;
    }

  



}
