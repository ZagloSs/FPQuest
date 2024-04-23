using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject player; // Bucar al player
    public float speed = 3f; // Velocidad Enemigo
    public int damage = 10; // Daño a jugador
    public float knockbackForce = 100f; //Retroceso

    private bool canDamage = true; // Controla si el enemigo puede hacer daño al jugador
    private SpriteRenderer spriteRenderer; // Referencia al componente SpriteRenderer del enemigo


    private void Start()
    {
        // Buscar el objeto del jugador por etiqueta
        player = GameObject.FindWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>(); // Obtener el componente SpriteRenderer del enemigo

    }
    private void Update()
    {
        if (player != null)
        {
            // Movimiento del enemigo hacia el jugador
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            if (direction.x > 0) // Mirando hacia la derecha
                spriteRenderer.flipX = false;
            else if (direction.x < 0) // Mirando hacia la izquierda
                spriteRenderer.flipX = true;
        }
    }

    
}
