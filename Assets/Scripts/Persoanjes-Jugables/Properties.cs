using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Properties : MonoBehaviour
{
    public static Properties instance;
    public float Health;
    public float Damage;
    public float Speed;
    public float AttSpeed;

    public HealthBar healthBar;


    private float MaxHealth;
    private bool canDamage = true;
    private void Awake()
    {
        if(!instance)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Dictionary<string, float> properties = GameManager.instance.getProperties();

        if(properties != null)
        {


            Health = properties["Health"];
            MaxHealth = properties["Health"];
            Damage = properties["Damage"];
            Speed = properties["Speed"];
            AttSpeed = properties["AttSpeed"];

            healthBar.setMaxValue(Health);

            Debug.Log("Valores de propiedades obtenidos con éxito.");
            Debug.Log("Health: " + Health);
            Debug.Log("Damage: " + Damage);
            Debug.Log("Speed: " + Speed);
            Debug.Log("Attack Speed: " + AttSpeed);
        }
        else
        {
            Debug.LogError("¡Error al obtener los valores de propiedades del GameManager!");
        }

    }

    //Daño Jugador
    public void ModifyDamage(float modifier)
    {
        Damage += modifier;
        Debug.Log("Daño del jugador modificado: " + Damage);

    }

    //Vida Jugador
    public void ModifyHealth(float modifier)
    {
        if(Health + modifier >= MaxHealth)
        {
            Health += (MaxHealth - Health);
        }
        else
        {
            Health += modifier;
        }
        Debug.Log("Vida del jugador modificada: " + Health);
        healthBar.setHealth(Health);
       

    }

    //Velocidad Jugador
    public void ModifySpeed(float modifier)
    {
        Speed += modifier;
        Debug.Log("Velocidad del jugador modificada: " + Speed);

    }

    //Velocidad de Ataque
    public void ModifyAttackSpeed(float modifier)
    {
        AttSpeed += modifier;
        Debug.Log("Velocidad de ataque del jugador modificada: " + AttSpeed);

    }

    //Daño enemigo hacia jugador
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy") && canDamage)
        {
            EnemyController Enemy = collision.gameObject.GetComponent<EnemyController>();
            if (Enemy != null)
            {
                Health -= Enemy.damage; // Reducir la vida del jugador
                healthBar.setHealth(Health);
                Debug.Log("Player health: " + Health);
                canDamage = false; // Activar cooldown
                Invoke("ResetCooldown", 1f);
            }
        }
    }

    private void ResetCooldown()
    {
        canDamage = true; // Reestablecer la capacidad de hacer daño al jugador
    }
}
