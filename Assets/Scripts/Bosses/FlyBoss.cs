using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyBoss : MonoBehaviour
{

    private float timer = 0;
    private GameObject player;
    private Room room;
    [SerializeField] private Slider HealthBar;
    // Update is called once per frame

    private void Start()
    {
        room = GetComponentInParent<Room>();
        player = GameObject.FindGameObjectWithTag("Player");

    }
    void Update()
    {
        if(timer > 4f)
        {
            timer = 0;
            StartCoroutine(accelerate());
        }
        timer += Time.deltaTime;

        HealthBar.value = GetComponent<EnemyController>().vida;
    }


    public IEnumerator accelerate()
    {
       
        GetComponent<FlyMovement>().speed = 3.4f;
        yield return new WaitForSeconds(0.5f);
        GetComponent<FlyMovement>().speed = 2.4f;

    }

}
