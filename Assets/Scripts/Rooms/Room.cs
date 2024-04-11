using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] GameObject topDoor;
    [SerializeField] GameObject bottomDoor;
    [SerializeField] GameObject leftDoor;
    [SerializeField] GameObject rightDoor;

    public Vector2Int RoomIndex { get; set; }

    public void OpenDoor(Vector2Int direction)
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
}
