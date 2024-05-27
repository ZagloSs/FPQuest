using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManosProperties : MonoBehaviour
{
    private float damage;

    private void Start()
    {
        damage = GetComponentInParent<FBProperties>().getDamage();
    }
    public float getDamage()
    {
        return damage;
    }
    
}
