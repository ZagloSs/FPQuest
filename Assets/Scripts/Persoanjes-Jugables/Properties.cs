using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Properties : MonoBehaviour
{

    [SerializeField] private ParticleSystem exp1;
    [SerializeField] private AudioClip DeathSound;

    public static Properties instance;
    public float Health;
    public float Damage;
    public float Speed;
    public float AttSpeed;

    public HealthBar healthBar;

    [SerializeField] private List<GameObject> noDeletingWhenDying;

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
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("AudioManager"))
        {
            noDeletingWhenDying.Add(go);
        }


        Dictionary<string, float> properties = GameManager.instance.getProperties();

        if (properties != null)
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

    // Daño Jugador
    public void ModifyDamage(float modifier)
    {
        Damage += modifier;
        Debug.Log("Daño del jugador modificado: " + Damage);
    }

    // Vida Jugador
    public void ModifyHealth(float modifier)
    {
        if (Health + modifier >= MaxHealth)
        {
            Health = MaxHealth;
        }
        else
        {
            Health += modifier;
        }
        Debug.Log("Vida del jugador modificada: " + Health);
        healthBar.setHealth(Health);
    }

    // Velocidad Jugador
    public void ModifySpeed(float modifier)
    {
        Speed += modifier;
        Debug.Log("Velocidad del jugador modificada: " + Speed);
    }

    // Velocidad de Ataque
    public void ModifyAttackSpeed(float modifier)
    {
        if(modifier < 0 && AttSpeed > 0.75)
        {
            AttSpeed += modifier;
            if (AttSpeed < 0.75f)
                AttSpeed = 0.75f;
            Debug.Log("Velocidad de ataque del jugador modificada: " + AttSpeed);
        }
    }

    // Daño enemigo hacia jugador
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && canDamage)
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            if (enemy != null)
            {
                Health -= enemy.damage; // Reducir la vida del jugador
                healthBar.setHealth(Health);
                Debug.Log("Player health: " + Health);
                canDamage = false; // Activar cooldown
                Invoke("ResetCooldown", 1f);
            }
        }
    }

    public void gotHittedByBullet(float damage)
    {
        Health -= damage; // Reducir la vida del jugador
        healthBar.setHealth(Health);
        Debug.Log("Player health: " + Health);
        canDamage = false; // Activar cooldown
        Invoke("ResetCooldown", 1f);
    }
    
    private void ResetCooldown()
    {
        canDamage = true; // Restablecer la capacidad de recibir daño del jugador
    }

    public void Death()
    {
        StartCoroutine(DeadCorrutine());
        GameManager.instance.spawnedEnemies = 0;
    }

    public IEnumerator DeadCorrutine()
    {
        GameObject[] list =  FindObjectsOfType<GameObject>();
        GameObject[] newList = list.Except(noDeletingWhenDying).ToArray();
        
        foreach (GameObject obj in newList)
        {
            obj.SetActive(false);
        }

        AudioManager.instance.stopmusic();
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Movement>().death();
        GetComponent<Movement>().enabled = false;

        yield return new WaitForSeconds(1f);

        GetComponent<SpriteRenderer>().enabled = false;
        AudioManager.instance.playMonsterDeathSound(DeathSound);
        exp1.Play();
        yield return new WaitForSeconds(2f);
        GameManager.instance.backToMenu();
    }
}



