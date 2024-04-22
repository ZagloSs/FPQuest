using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Modifiers")]
    [SerializeField] private float damageModifier = 0;
    [SerializeField] private float healthModifier = 0;
    [SerializeField] private float speedModifier = 0f;
    [SerializeField] private float attackSpeedModifier = 0f;

    // Método para aplicar los modificadores al jugador
    public void ApplyModifiers(Properties player)
    {
        player.ModifyDamage(damageModifier);
        player.ModifyHealth(healthModifier);
        player.ModifySpeed(speedModifier);
        player.ModifyAttackSpeed(attackSpeedModifier);
        Debug.Log("Modificadores aplicados al jugador: " + damageModifier + " de daño, " + healthModifier + " de vida, " + speedModifier + " de velocidad, " + attackSpeedModifier + " de velocidad de ataque.");
    }

    // Detectar colisión con el jugador
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Colisión con el jugador detectada.");

            Properties player = collision.GetComponent<Properties>();
            if (player != null)
            {
                Debug.Log("Componente Properties encontrado.");

                ApplyModifiers(player);
            }

            // Destruir este objeto
            Destroy(gameObject);
        }
    }
}