using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Modifiers")]
    [SerializeField] private float damageModifier = 0;
    [SerializeField] private float healthModifier = 0;
    [SerializeField] private float speedModifier = 0f;
    [SerializeField] private float attackSpeedModifier = 0f;

    private string str;

    // M�todo para aplicar los modificadores al jugador
    public void ApplyModifiers(Properties player)
    {
        player.ModifyDamage(damageModifier);
        player.ModifyHealth(healthModifier);
        player.ModifySpeed(speedModifier);
        player.ModifyAttackSpeed(attackSpeedModifier);
        Debug.Log("Modificadores aplicados al jugador: " + damageModifier + " de da�o, " + healthModifier + " de vida, " + speedModifier + " de velocidad, " + attackSpeedModifier + " de velocidad de ataque.");
        
}
    
    // Detectar colisi�n con el jugador
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Colisi�n con el jugador detectada.");

            Properties player = collision.GetComponent<Properties>();
            if (player != null)
            {
                Debug.Log("Componente Properties encontrado.");

                ApplyModifiers(player);
                str = string.Format("A: {0}   Sp: {1}    Hp: {2}   As: {3}", damageModifier, speedModifier, healthModifier, attackSpeedModifier);
                PickUpItem.instance.mostrarCanvasModificacionItem(str);
            }
            // Destruir este objeto
            Destroy(gameObject);
        }
    }

   
}