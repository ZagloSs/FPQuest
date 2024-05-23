using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] GameObject fillRoomParent;
    [SerializeField] GameObject topDoor;
    [SerializeField] GameObject bottomDoor;
    [SerializeField] GameObject leftDoor;
    [SerializeField] GameObject rightDoor;

    public GameObject[] doors;  // Array de puertas de la habitación

    public bool completed = false;  // Estado de la habitación

    public Vector2Int RoomIndex { get; set; }

    public void SpawnDoor(Vector2Int direction)
    {

        if (direction == Vector2Int.up)
        {
            topDoor.SetActive(true);
        }
        else if (direction == Vector2Int.down)
        {
            bottomDoor.SetActive(true);
        }
        else if (direction == Vector2Int.left)
        {
            leftDoor.SetActive(true);
        }
        else if (direction == Vector2Int.right)
        {
            rightDoor.SetActive(true);
        }
        OpenDoors();
    }
    // Método para cerrar las puertas
    public void CloseDoors()
    {
        foreach (GameObject door in doors)
        {
            door.GetComponent<Animator>().SetBool("Abrir", false);
            door.GetComponent<BoxCollider2D>().enabled = false;  // Activa el collider para cerrar la puerta
        }
    }

    // Método para abrir las puertas
    public void OpenDoors()
    {

        foreach (GameObject door in doors)
        {
            door.GetComponent<Animator>().SetBool("Abrir", true);
            door.GetComponent<BoxCollider2D>().enabled = true;  // Desactiva el collider para abrir la puerta
        }
    }

    // Verificar si el jugador está en la habitación
    public bool IsPlayerInRoom(PlayerPosition player)
    {
        return GetComponent<Collider2D>().bounds.Contains(player.transform.position);
    }

}