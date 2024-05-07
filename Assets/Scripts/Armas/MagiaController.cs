using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagiaController : WeaponController
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedMagic = Instantiate(weaponData.prefab);
        spawnedMagic.transform.position = transform.position;
        spawnedMagic.GetComponent<MagicBehaviour>().DirectionChecker(pm.lastMovedVector);
    }
}
