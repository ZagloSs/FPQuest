using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Estadisticas Armas")]
    public WeaponScriptableObject weaponData;
    //public GameObject prefab;
    //public float damage;
    //public float speed;
    //public float cooldownDuration;
    //public int pierce;
    float currentCooldown;
    protected Movement pm;

    protected virtual void Start()
    {
        pm = FindObjectOfType<Movement>();
        currentCooldown =weaponData.cooldownDuration;
    }

    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        if(currentCooldown <= 0f) 
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        currentCooldown = weaponData.cooldownDuration;
    }
}
