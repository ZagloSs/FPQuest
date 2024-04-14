using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Properties : MonoBehaviour
{

    public float Health;
    public float Damage;
    public float Speed;
    public float AttSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Dictionary<string, float> properties = GameManager.instance.getProperties();

        Health = properties["Health"];
        Damage = properties["Damage"];
        Speed = properties["Speed"];
        AttSpeed = properties["AttSpeed"];
    }
}
