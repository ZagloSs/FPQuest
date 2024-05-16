using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int enemyCount;

    private Room room;
    private PlayerPosition player;
    private bool enemiesSpawned = false;

    private void Start()
    {
        room = GetComponent<Room>();
        player = FindObjectOfType<PlayerPosition>();
    }

    private void Update()
    {
        if (room.IsPlayerInRoom(player) && !room.completed && !enemiesSpawned)
        {
            SpawnEnemies();
            enemiesSpawned = true;
            room.CloseDoors();
        }

    }
    public void SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Instantiate(enemyPrefab, new Vector2(Random.Range(transform.position.x - 6.5f, transform.position.x + 6.5f), Random.Range(transform.position.y - 2.5f, transform.position.y - 2.5f)), Quaternion.identity);
        }
    }

    public void MarkRoomAsCompleted()
    {
        room.completed = true;
    }
}