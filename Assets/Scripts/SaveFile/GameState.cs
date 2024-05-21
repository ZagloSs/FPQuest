using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RoomManager;

[System.Serializable]
public class GameState
{
    public int level;
    public List<Vector2Int> completedRooms;
    public Vector2Int playerRoom;
    public PlayerStats playerStats;
    public RoomData[,] mapData;
}

[System.Serializable]
public class PlayerStats
{
    public float health;
    public float damage;
    public float speed;
    public float attackSpeed;
}

[System.Serializable]
public class RoomData
{
    public Vector2Int roomIndex;
    public bool isCompleted;
    public string roomType; // "StartRoom", "ItemRoom", "BossRoom", etc.
}
