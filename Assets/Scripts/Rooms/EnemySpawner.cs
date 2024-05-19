using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int maxEnemyCount;
    [SerializeField] int minEnemyCount;

    private Room room;
    RoomManager roomManager;
    private PlayerPosition player;
    private bool enemiesSpawned = false;
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    private void Start()
    {
        room = GetComponent<Room>();
        player = FindObjectOfType<PlayerPosition>();
        roomManager = GameObject.FindGameObjectWithTag("RoomManager").GetComponent<RoomManager>();
    }

    private void Update()
    {
        if (roomManager.generationComplete)
        {
            if (room.IsPlayerInRoom(player) && !room.completed && !enemiesSpawned && room.name != "BossRoom" && room.name != "ItemRoom")
            {
                SpawnEnemies();
                enemiesSpawned = true;
                room.CloseDoors();
            }
            else if (room.name == "BossRoom" && room.name == "ItemRoom")
            {
                room.OpenDoors();
            }

            if (enemiesSpawned && AllEnemiesDefeated())
            {
                room.OpenDoors();
                room.completed = true;
            }
        }
    }

    public void SpawnEnemies()
    {
        int enemyCount = Random.Range(minEnemyCount, maxEnemyCount + 1);
        for (int i = 0; i < enemyCount; i++)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(transform.position.x - 6.5f, transform.position.x + 6.5f), Random.Range(transform.position.y - 2.5f, transform.position.y + 2.5f));
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            spawnedEnemies.Add(enemy);
        }
    }

    private bool AllEnemiesDefeated()
    {
        spawnedEnemies.RemoveAll(enemy => enemy == null);  // Remove null references (destroyed enemies)
        return spawnedEnemies.Count == 0;
    }
}
