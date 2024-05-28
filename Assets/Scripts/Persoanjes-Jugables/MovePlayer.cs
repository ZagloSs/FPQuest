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


    public IEnumerator ChangePlayerPos()
    {
        if (player != null)
        {
            Debug.Log("ChangePlayerPos called");
            player.transform.position = new Vector3(transform.position.x, transform.position.y - 2.2f, player.transform.position.z);
            yield return null;
            Debug.Log("Player position changed to: " + player.transform.position);
        }
        else
        {
            Debug.LogError("Player object is not assigned.");
        }
    }
}
