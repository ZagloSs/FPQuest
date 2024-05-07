using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script base para los projectile que creemos 
/// </summary>

public class ProjectileWeaponBehaviour : MonoBehaviour
{
    public WeaponScriptableObject weaponData;

    protected Vector3 direction;
    public float destroyAfterSeconds;

    //Stats de ahora
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;
    protected int currentPierce;


    private void Awake()
    {
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentCooldownDuration = weaponData.CooldownDuration;
        currentPierce = weaponData.Pierce;
    }
    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;

        float dirx = direction.x;
        float diry = direction.y;

        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;

        //Cambiar la direccion del sprite segun el personaje

        if (dirx < 0 && diry == 0)//Izquierda
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
        }
        else if (dirx == 0 && diry < 0) //Abajo
        {
            scale.y = scale.y * -1;
        }
        else if (dirx == 0 && diry > 0) //Arriba
        {
            scale.x = scale.x * -1;
        }
        else if (dir.x > 0 && dir.y > 0) //Derecha Arriba
        {
            rotation.z = 0f;
        }
        else if (dir.x > 0 && dir.y < 0) //Derecha Abajo
        {
            rotation.z = -90f;
        }
        else if (dir.x < 0 && dir.y > 0)
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
            rotation.z = -90f;
        }
        else if (dir.x < 0 && dir.y < 0)
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
            rotation.z = 0f;
        }

        transform.localScale = scale;
        transform.rotation = Quaternion.Euler(rotation);

    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            EnemyController enemy = col.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TomarDaño(currentDamage);
                Destroy(gameObject); // Optionally destroy the projectile upon hitting an enemy
            }
        }
    }
}
