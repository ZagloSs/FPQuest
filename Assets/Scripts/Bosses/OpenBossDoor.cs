using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenBossDoor : MonoBehaviour
{
        public GameObject bossDoor;

        private void Start()
        {
            bossDoor = GameObject.FindGameObjectWithTag("BossDoor");
        }
        void OnDestroy()
        {
            // Verifica que el objeto a activar está asignado
            if (bossDoor != null)
            {
                bossDoor.GetComponent<BoxCollider2D>().enabled = true;
                bossDoor.GetComponent<Animator>().SetBool("Abrir", true);
        }
            else
            {
                Debug.LogWarning("No se ha asignado un objeto a activar.");
            }
        }
}
