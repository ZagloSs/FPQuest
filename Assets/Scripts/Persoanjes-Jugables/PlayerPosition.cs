using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    [SerializeField] int roomIndexX;
    [SerializeField] int roomIndexY;

    public Vector2Int RoomIndex
    {
        get { return new Vector2Int(roomIndexX, roomIndexY); }
        set { roomIndexX = value.x; roomIndexY = value.y; }
    }
}