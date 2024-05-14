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

    public bool completed = false;

    public Vector2Int RoomIndex { get; set; }

    public bool IsPlayerInRoom(PlayerPosition player)
    {
        return player.RoomIndex == RoomIndex;
    }

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
    }
    public void CloseDoors()
    {
        topDoor.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Salas/Puertas_0");
        bottomDoor.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Salas/Puertas_0");
        leftDoor.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Salas/Puertas_0");
        rightDoor.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Salas/Puertas_0");
    }
    public void OpenDoors()
    {
        topDoor.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Salas/Puertas_1");
        bottomDoor.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Salas/Puertas_1");
        leftDoor.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Salas/Puertas_1");
        rightDoor.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Salas/Puertas_1");
    }

    public void RoomCompleted()
    {
        completed = true;
        GetComponent<EnemySpawner>().MarkRoomAsCompleted();
        OpenDoors();
    }
}