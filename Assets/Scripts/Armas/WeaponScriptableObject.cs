using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponScriptableObject", menuName ="ScriptableObjects/Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    [SerializeField]
    GameObject prefab;
    public GameObject Prefab { get => prefab; private set => prefab = value; }

    //Stats Basicas para las armas

    [SerializeField]
    float damage;
    public float Damage { get => damage; private set => damage = value; }
    
    [SerializeField]
    float speed;
    public float Speed { get => speed; private set => speed = value; }
    
    [SerializeField]
    float cooldownDuration;
    public float CooldownDuration { get => cooldownDuration; private set => cooldownDuration = value; }
    
    [SerializeField]
    int pierce;
    public int Pierce { get => pierce; private set => pierce = value; }

}
