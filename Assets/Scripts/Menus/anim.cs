using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class anim : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject intro;

    private float timer = 0f;



    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<SpriteRenderer>().enabled = false;
        player.GetComponent<Movement>().enabled = false;
        canvas.SetActive(false);
        intro.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (intro)
        {
            if (timer > 2f && intro.GetComponent<SpriteRenderer>().color.r > 0)
            {
                intro.GetComponent<SpriteRenderer>().color = new Color((intro.GetComponent<SpriteRenderer>().color.r) - ((200 * Time.deltaTime)/255),
                                                                       (intro.GetComponent<SpriteRenderer>().color.g) - ((200 * Time.deltaTime) / 255),
                                                                       (intro.GetComponent<SpriteRenderer>().color.b) - ((200 * Time.deltaTime) / 255));
             
               

            }else if(timer > 4f && intro.GetComponent<SpriteRenderer>().color.r <= 0)
            {
                player.GetComponent<SpriteRenderer>().enabled = true;
                player.GetComponent<Movement>().enabled = true;
                canvas.SetActive(true) ;

                intro.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, (intro.GetComponent<SpriteRenderer>().color.a) - ((200 * Time.deltaTime) / 255));
                if(timer > 5f)
                {
                    intro.SetActive(false);
                    timer = 0;
                }
            }
           
            timer += Time.deltaTime;
        }
    }
}
