using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int maxEnemyCount;
    [SerializeField] int minEnemyCount;

    private int roomWidth = 20;
    private int roomHeight = 12;

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
            if (room.IsPlayerInRoom(player) && !room.completed && !enemiesSpawned && room.name != "BossRoom" && room.name != "ItemRoom" && room.name != "StartRoom")
            {
                SpawnEnemies();
                enemiesSpawned = true;
                room.CloseDoors();
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
            // Find the RoomFill object in the room
            Transform roomFill = room.transform.Find("RoomFill");

            // Generate a random position within the room
            Vector3 spawnPosition = roomFill.position;
            spawnPosition.x += Random.Range(-roomWidth / 2f + 2f, roomWidth / 2f - 2f);
            spawnPosition.y += Random.Range(-roomHeight / 2f + 2f, roomHeight / 2f - 2f);

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
