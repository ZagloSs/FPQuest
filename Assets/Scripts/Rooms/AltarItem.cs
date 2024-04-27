using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarItem : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<Animator>().SetBool("Energy", false);
        }
    }
}
