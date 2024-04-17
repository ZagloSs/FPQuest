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

        if(properties != null)
        {

            Health = properties["Health"];
            Damage = properties["Damage"];
            Speed = properties["Speed"];
            AttSpeed = properties["AttSpeed"];

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

    //Daño Jugador
    public void ModifyDamage(float modifier)
    {
        Damage += modifier;
        Debug.Log("Daño del jugador modificado: " + Damage);

    }

    //Vida Jugador
    public void ModifyHealth(float modifier)
    {
        Health += modifier;
        Debug.Log("Vida del jugador modificada: " + Health);

    }

    //Velocidad Jugador
    public void ModifySpeed(float modifier)
    {
        Speed += modifier;
        Debug.Log("Velocidad del jugador modificada: " + Speed);

    }

    //Velocidad de Ataque
    public void ModifyAttackSpeed(float modifier)
    {
        AttSpeed += modifier;
        Debug.Log("Velocidad de ataque del jugador modificada: " + AttSpeed);

    }
}
