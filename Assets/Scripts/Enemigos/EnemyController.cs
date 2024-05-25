using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float vida;
    [SerializeField] private AudioClip deathSound;

    private Color initialColor;
    public float damage = 10; // Daño a jugador
    public float knockbackForce = 16f; //Retroceso

    [SerializeField] private ParticleSystem ps;
   
    private SpriteRenderer spriteRenderer; // Referencia al componente SpriteRenderer del enemigo
  
    
    private void Start()
    {

        // Buscar el objeto del jugador por etiqueta
        spriteRenderer = GetComponent<SpriteRenderer>(); // Obtener el componente SpriteRenderer del enemigo
        initialColor = spriteRenderer.color;
       
    }

    
    public void TomarDaño(float daño)
    {
        StartCoroutine(blinkEffect());
        vida -= daño;
        if (vida <= 0)
        {
            if(GameManager.instance.spawnedEnemies>0)
                GameManager.instance.spawnedEnemies--;
            StartCoroutine(Death());
        }
    }

    public IEnumerator Death()
    {
        
        AudioManager.instance.playMonsterDeathSound(deathSound);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Animator>().enabled = false;
        ps.Play();
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);
    }

    public IEnumerator blinkEffect()
    {
        spriteRenderer.color = new Color(1 , 0, 0);
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = initialColor;
    }
}
