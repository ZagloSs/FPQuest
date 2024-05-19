using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tipoDeAtaque : MonoBehaviour
{
    private Magia magia;
    [SerializeField] private GameObject melee;
    private float tipoAtaque;
    // Start is called before the first frame update
    void Start()
    {
        magia = GetComponent<Magia>();
        tipoAtaque = GameManager.instance.getProperties()["AttType"];

        if(tipoAtaque == 1f)
        {
            magia.enabled = false;
            melee.SetActive(true);
        }
        else
        {
            magia.enabled = true;
            melee.SetActive(false);
        }
    }

   
}
