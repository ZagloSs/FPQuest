using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float vida;
    public GameObject player; // Buscar al jugador
    public float speed = 3f; // Velocidad del enemigo
    public int damage = 10; // Daño al jugador
    public float knockbackForce = 100f; // Retroceso
    public float attackCooldown = 1f; // Tiempo de espera entre ataques

    private SpriteRenderer spriteRenderer; // Referencia al componente SpriteRenderer del enemigo
    private float lastAttackTime; // Momento del último ataque
    private bool isPlayerInRange = false; // Indica si el jugador está en rango de ataque

    private void Start()
    {
        // Buscar el objeto del jugador por etiqueta
        player = GameObject.FindWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>(); // Obtener el componente SpriteRenderer del enemigo
        lastAttackTime = -attackCooldown; // Iniciar el tiempo de último ataque en un valor negativo
    }

    private void Update()
    {
        if (isPlayerInRange)
        {
            // Movimiento del enemigo hacia el jugador
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            if (direction.x > 0) // Mirando hacia la derecha
                spriteRenderer.flipX = false;
            else if (direction.x < 0) // Mirando hacia la izquierda
                spriteRenderer.flipX = true;

            // Verificar si el tiempo de cooldown ha pasado para atacar
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                Attack();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject == player)
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject == player)
        {
            isPlayerInRange = false;
        }
    }

    private void Attack()
    {
        // Reducir la vida del jugador
        Properties playerProperties = player.GetComponent<Properties>();
        if (playerProperties != null)
        {
            playerProperties.ModifyHealth(-damage); // Aplicar daño negativo al jugador
        }

        // Actualizar el tiempo del último ataque
        lastAttackTime = Time.time;

        // Aplicar retroceso al jugador
        Vector2 knockbackDirection = (player.transform.position - transform.position).normalized;
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            playerRb.AddForce(knockbackDirection * knockbackForce);
        }
    }

    public void TomarDaño(float daño)
    {
        vida -= daño;
        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }
}
