using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBehaviour : ProjectileWeaponBehaviour
{
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * weaponData.speed * Time.deltaTime;
    }
}
