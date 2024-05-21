using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;



    private Room room;
    RoomManager roomManager;
    private PlayerPosition player;
    private bool enemiesSpawned = false;
    private bool soundOnce = false;

    private void Start()
    {
        
        room = GetComponentInParent<Room>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPosition>();
        roomManager = GameObject.FindGameObjectWithTag("RoomManager").GetComponent<RoomManager>();
    }

    private void Update()
    {
        if (roomManager.generationComplete)
        {
            if (room.IsPlayerInRoom(player) && !room.completed && !enemiesSpawned)
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
        GameManager.instance.spawnedEnemies++;
        Instantiate(enemyPrefab, transform.position, transform.rotation);

    }

    private bool AllEnemiesDefeated()
    {
        return GameManager.instance.spawnedEnemies == 0;
    }
}
