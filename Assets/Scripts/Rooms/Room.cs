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

    public Vector2Int RoomIndex { get; set; }

    public void Start()
    {
        fillRoomParent.transform.GetChild(Random.Range(0,2)).gameObject.SetActive(true);
    }
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
