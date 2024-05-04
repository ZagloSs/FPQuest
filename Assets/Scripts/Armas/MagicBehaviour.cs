using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBehaviour : ProjectileWeaponBehaviour
{
    MagiaController mc;
    protected override void Start()
    {
        base.Start();
        mc = FindObjectOfType<MagiaController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * mc.speed * Time.deltaTime;
    }
}
