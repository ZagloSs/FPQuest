using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script base para los projectile que creemos 
/// </summary>

public class ProjectileWeaponBehaviour : MonoBehaviour
{
    protected Vector3 direction;
    public float destroyAfterSeconds;

    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;
    }
}
