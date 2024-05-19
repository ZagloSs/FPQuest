using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float vida;
    public float speed = 3f; // Velocidad Enemigo
    public int damage = 10; // Daño a jugador
    public float knockbackForce = 100f; //Retroceso

    private GameObject player; // Bucar al player
    private SpriteRenderer spriteRenderer; // Referencia al componente SpriteRenderer del enemigo


    private void Start()
    {
        // Buscar el objeto del jugador por etiqueta
        player = GameObject.FindWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>(); // Obtener el componente SpriteRenderer del enemigo

    }
    private void Update()
    {
        if (player)
        {
            // Movimiento del enemigo hacia el jugador
            Vector2 direction = (player.transform.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);

            if (direction.x > 0) // Mirando hacia la derecha
                spriteRenderer.flipX = false;
            else if (direction.x < 0) // Mirando hacia la izquierda
                spriteRenderer.flipX = true;
        }
    }

    public void TomarDaño(float daño)
    {
        vida -= daño;
        if(vida<=0)
        {
            AudioManager.instance.PlayFlyDeath();
            Destroy(gameObject);
        }
    }
    
    
}
