using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    public void ChangePlayerPos()
    {
        player.transform.position = new Vector2(transform.position.x, transform.position.y - 2.2f);
    }
}
