using System.Collections.Generic;
using System.IO;
using UnityEditor.EditorTools;
using UnityEngine;
using static RoomManager;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance { get; private set; }
    private string saveFilePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "savegame.json");
        
    }

    public bool SaveFileExists()
    {
        return File.Exists(saveFilePath);
    }

    public void SaveGame(RoomManager roomManager, PlayerPosition player, Properties playerProperties)
    {
        RoomData[,] mapData = roomManager.GetMapData();

        GameState gameState = new GameState
        {
            level = roomManager.CurrentLevel,
            completedRooms = roomManager.GetCompletedRooms(),
            playerRoom = player.RoomIndex,
            playerStats = new PlayerStats
            {
                health = playerProperties.Health,
                damage = playerProperties.Damage,
                speed = playerProperties.Speed,
                attackSpeed = playerProperties.AttSpeed,
                atttype = GameObject.FindGameObjectWithTag("Player").GetComponent<tipoDeAtaque>().tipoAtaque

    },
            mapData = mapData
        };

        string json = JsonUtility.ToJson(gameState);
        File.WriteAllText(saveFilePath, json);

        Debug.Log("Game saved.");
    }

    public void LoadGame(/*RoomManager roomManager, PlayerPosition playerPos,*/ GameObject player)
    {
        if (SaveFileExists())
        {
            string json = File.ReadAllText(saveFilePath);
            GameState gameState = JsonUtility.FromJson<GameState>(json);

            //roomManager.CurrentLevel = gameState.level;
            //roomManager.SetCompletedRooms(gameState.completedRooms);
            // playerPos.RoomIndex = JsonUtility.FromJson<GameState>(json).playerRoom;
            player.GetComponent<Properties>().Health = gameState.playerStats.health;
            player.GetComponent<Properties>().Damage = gameState.playerStats.damage;
            player.GetComponent<Properties>().Speed = gameState.playerStats.speed;
            player.GetComponent<Properties>().AttSpeed = gameState.playerStats.attackSpeed;

            //roomManager.LoadMapData(gameState.mapData);

            Debug.Log("Game loaded.");
        }
        else
        {
            Debug.LogWarning("No save file found.");
        }
    }
}