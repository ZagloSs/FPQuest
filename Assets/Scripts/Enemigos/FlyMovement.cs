using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FlyMovement : MonoBehaviour
{

    public float speed = 1f; // Velocidad Enemigo
    private GameObject player; // Bucar al player
    private SpriteRenderer spriteRenderer; // Referencia al componente SpriteRenderer del enemigo
    private bool isPlayerInRange = false;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>(); // Obtener el componente SpriteRenderer del enemigo
        player = GameObject.FindWithTag("Player");
    }
    private void Update()
    {
        if (player && isPlayerInRange)
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


}
