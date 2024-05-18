using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public static PickUpItem instance;
    private float timer = 0;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (gameObject.GetComponent<Canvas>().enabled)
        {

            if (timer > 2.5f)
            {
                timer = 0;
                gameObject.GetComponent<Canvas>().enabled = false;
            }

            timer += Time.deltaTime;
        }
    }

    public void mostrarCanvasModificacionItem(string str)
    {
        AudioManager.instance.PlayPickUpItem();
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = str;
        gameObject.GetComponent<Canvas>().enabled = true;
        
        gameObject.SetActive(true);
    }
}
