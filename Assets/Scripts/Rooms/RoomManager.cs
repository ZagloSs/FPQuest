using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] GameObject roomPrefab;
    [SerializeField] GameObject[] fillPrefabLevel1;
    [SerializeField] GameObject[] fillPrefabLevel2;
    [SerializeField] GameObject[] fillPrefabLevel3;

    [SerializeField] private int maxRooms = 15;
    [SerializeField] private int minRooms = 10;
    [SerializeField] private int gridSizeX = 10;
    [SerializeField] private int gridSizeY = 10;

    [SerializeField] GameObject[] itemPrefabs;
    [SerializeField] GameObject itemPedestal;
    [SerializeField] GameObject LvlChangeCanvas;
    [SerializeField] GameObject[] bossRoomPrefabs;

    [SerializeField] GameObject enemyPrefab;

    private bool hasGeneratedItemRoom = false;
    private bool hasGeneratedBossRoom = false;

    private int roomWidth = 20;
    private int roomHeight = 12;

    public int gameLevel = 1;
    public bool enteredBoosExit = false;

    private List<GameObject> roomObjects = new List<GameObject>();

    private Queue<Vector2Int> roomQueue = new Queue<Vector2Int>();

    private int[,] roomGrid;

    private int roomCount;

    public bool generationComplete = false;

    private void Start()
    {
        roomGrid = new int[gridSizeX, gridSizeY];
        roomQueue = new Queue<Vector2Int>();

        Vector2Int initialRoomIndex = new Vector2Int(gridSizeX / 2, gridSizeY / 2);
        StartRoomGenerationFromRoom(initialRoomIndex);
    }

    private void Update()
    {
        if (roomQueue.Count > 0 && roomCount < maxRooms && !generationComplete)
        {
            Vector2Int roomIndex = roomQueue.Dequeue();
            int gridX = roomIndex.x;
            int gridY = roomIndex.y;

            TryGenerateRoom(new Vector2Int(gridX - 1, gridY));
            TryGenerateRoom(new Vector2Int(gridX + 1, gridY));
            TryGenerateRoom(new Vector2Int(gridX, gridY - 1));
            TryGenerateRoom(new Vector2Int(gridX, gridY + 1));
        }
        else if (roomCount < minRooms)
        {
            Debug.Log("Regenerating rooms");
            RegenerateRooms();
        }
        else if (!generationComplete)
        {
            generationComplete = true;
            Debug.Log($"Generation Complete, {roomCount} rooms created");
        }
        if (roomCount >= minRooms && !hasGeneratedItemRoom)
        {
            GenerateItemRoom();
        }

        if (generationComplete && !hasGeneratedBossRoom)
        {
            GenerateBossRoom();
        }
        if (enteredBoosExit)
        {
            enteredBoosExit = false;
            gameLevel++;
            StartCoroutine(cambioDeLvl());
            RegenerateRooms();
        }
    }
    private void StartRoomGenerationFromRoom(Vector2Int roomIndex)
    {
        roomQueue.Enqueue(roomIndex);
        int x = roomIndex.x;
        int y = roomIndex.y;
        roomGrid[x, y] = 1;
        roomCount++;

        var initialRoom = Instantiate(roomPrefab, GetPositionFromGridIndex(roomIndex), Quaternion.identity);

        Transform roomFill = initialRoom.transform.Find("RoomFill");
        switch(gameLevel)
        {
            case 1:
                GameObject fill1 = Instantiate(fillPrefabLevel1[0], roomFill.position, Quaternion.identity, roomFill);
                break;
            case 2:
                GameObject fill2 = Instantiate(fillPrefabLevel2[0], roomFill.position, Quaternion.identity, roomFill);
                break;
            case 3:
                GameObject fill3 = Instantiate(fillPrefabLevel3[0], roomFill.position, Quaternion.identity, roomFill);
                break;
        }

        initialRoom.name = "StartRoom";
        initialRoom.GetComponent<Room>().RoomIndex = roomIndex;
        roomObjects.Add(initialRoom);
    }

    private bool TryGenerateRoom(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;

        if (x < 0 || y < 0 || x >= gridSizeX || y >= gridSizeY)
            return false;

        if (x >= gridSizeX || y >= gridSizeY || x < 0 || y < 0)
            return false;

        if (roomCount >= maxRooms)
            return false;

        if (Random.value < 0.5f && roomIndex != Vector2Int.zero)
            return false;

        if (roomGrid[x, y] != 0)
            return false;

        if (CountAdjacentRooms(roomIndex) > 1)
            return false;

        roomQueue.Enqueue(roomIndex);
        roomGrid[x, y] = 1;
        roomCount++;

        var newRoom = Instantiate(roomPrefab, GetPositionFromGridIndex(roomIndex), Quaternion.identity);

        Transform roomFill = newRoom.transform.Find("RoomFill");

        switch(gameLevel)
        {
            case 1:
                GameObject fill1 = Instantiate(fillPrefabLevel1[Random.Range(1, fillPrefabLevel1.Length)], roomFill.position, Quaternion.identity, roomFill);
                break;
            case 2:
                GameObject fill2 = Instantiate(fillPrefabLevel2[Random.Range(1, fillPrefabLevel2.Length)], roomFill.position, Quaternion.identity, roomFill);
                break;
            case 3:
                GameObject fill3 = Instantiate(fillPrefabLevel3[Random.Range(1, fillPrefabLevel3.Length)], roomFill.position, Quaternion.identity, roomFill);
                break;
        }

        newRoom.GetComponent<Room>().RoomIndex = roomIndex;
        newRoom.name = $"Room-{roomCount}";
        roomObjects.Add(newRoom);

        OpenDoors(newRoom, x, y);
        return true;
    }

    //Clear all rooms and try again
    private void RegenerateRooms()
    {
        foreach (var room in roomObjects)
        {
            Destroy(room);
        }

        roomObjects.Clear();
        roomQueue.Clear();
        roomGrid = new int[gridSizeX, gridSizeY];
        roomQueue.Clear();
        roomCount = 0;
        generationComplete = false;

        Vector2Int initialRoomIndex = new Vector2Int(gridSizeX / 2, gridSizeY / 2);
        StartRoomGenerationFromRoom(initialRoomIndex);
        GameObject player = GameObject.Find("Player");
        player.transform.position = new Vector3(0, 0, 0);
        Camera.main.transform.position = new Vector2(0, 0);
        Camera.main.nearClipPlane = 0;
    }
    void OpenDoors(GameObject room, int x, int y)
    {
        Room newRoom = room.GetComponent<Room>();

        //Neighbours
        Room leftRoom = GetRoomScriptAt(new Vector2Int(x - 1, y));
        Room rightRoom = GetRoomScriptAt(new Vector2Int(x + 1, y));
        Room topRoom = GetRoomScriptAt(new Vector2Int(x, y + 1));
        Room bottomRoom = GetRoomScriptAt(new Vector2Int(x, y - 1));

        //Open doors
        if (leftRoom != null)
        {
            //Neighbour left
            newRoom.SpawnDoor(Vector2Int.left);
            leftRoom.SpawnDoor(Vector2Int.right);
        }
        if (rightRoom != null)
        {
            //Neighbour right
            newRoom.SpawnDoor(Vector2Int.right);
            rightRoom.SpawnDoor(Vector2Int.left);
        }
        if (bottomRoom != null)
        {
            //Neighbour below
            newRoom.SpawnDoor(Vector2Int.down);
            bottomRoom.SpawnDoor(Vector2Int.up);
        }
        if (topRoom != null)
        {
            //Neighbour above
            newRoom.SpawnDoor(Vector2Int.up);
            topRoom.SpawnDoor(Vector2Int.down);
        }
    }

    Room GetRoomScriptAt(Vector2Int roomIndex)
    {
        return roomObjects.Find(r => r.GetComponent<Room>().RoomIndex == roomIndex)?.GetComponent<Room>();
    }

    private int CountAdjacentRooms(Vector2Int roomIndex)
    {
        int count = 0;
        int x = roomIndex.x;
        int y = roomIndex.y;

        if (x > 0 && roomGrid[x - 1, y] != 0) //Left neighbour
            count++;
        if (x < gridSizeX - 1 && roomGrid[x + 1, y] != 0) //Right neighbour
            count++;
        if (y > 0 && roomGrid[x, y - 1] != 0) //Bottom neighbour
            count++;
        if (y < gridSizeY - 1 && roomGrid[x, y + 1] != 0) //Top neighbour
            count++;

        return count;
    }
    private Vector3 GetPositionFromGridIndex(Vector2Int gridIndex)
    {
        int gridX = gridIndex.x;
        int gridY = gridIndex.y;
        return new Vector3(roomWidth *(gridX - gridSizeX / 2), roomHeight * (gridY - gridSizeY / 2));
    }
    private void GenerateItemRoom()
    {
        hasGeneratedItemRoom = true;

        // Choose a random room from the existing rooms
        int randomRoomIndex = Random.Range(1, roomObjects.Count - 1);
        GameObject itemRoom = roomObjects[randomRoomIndex];
        roomObjects[randomRoomIndex].name = "ItemRoom";

        // Find the RoomFill object in the room
        Transform roomFill = itemRoom.transform.Find("RoomFill");

        // Clear the room of any existing RoomFill objects
        ClearRoom(roomFill);

        // Instantiate the item prefab in the room with a pedestal
        GameObject item = Instantiate(itemPrefabs[Random.Range(0, itemPrefabs.Length)], itemRoom.transform.position, Quaternion.identity, roomFill);
        item.name = "Item";
        item.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        GameObject pedestal = Instantiate(itemPedestal, itemRoom.transform.position, Quaternion.identity, roomFill);

        switch(gameLevel)
        {
            case 1:
                GameObject fill1 = Instantiate(fillPrefabLevel1[0], roomFill.position, Quaternion.identity, roomFill);
                break;
            case 2:
                GameObject fill2 = Instantiate(fillPrefabLevel2[0], roomFill.position, Quaternion.identity, roomFill);
                break;
            case 3:
                GameObject fill3 = Instantiate(fillPrefabLevel3[0], roomFill.position, Quaternion.identity, roomFill);
                break;
        }
        

    }

    private void GenerateBossRoom()
    {
        hasGeneratedBossRoom = true;

        // Choose the last room from the existing rooms
        GameObject bossRoom = roomObjects[roomObjects.Count - 1];
        roomObjects[roomObjects.Count - 1].name = "BossRoom";

        // Find the RoomFill object in the room
        Transform roomFill = bossRoom.transform.Find("RoomFill");

        // Clear the contents of the previous room
        ClearRoom(roomFill);

        //Destroy the doors in the room
        Transform doors = bossRoom.transform.Find("Doors");
        Destroy(doors.gameObject);

        // Instantiate the boss room prefab as a child of the room
        GameObject bossRoomInstance = Instantiate(bossRoomPrefabs[gameLevel-1], roomFill.position, Quaternion.identity, roomFill);
        GameObject bossDoor = bossRoom.transform.Find("BossDoor").gameObject;
        bossDoor.SetActive(true);
        bossDoor.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void ClearRoom(Transform room)
    {
        // Remove all RoomFill objects from the room
        foreach (Transform child in room.transform)
        {
            if (child.gameObject.tag == "RoomFill")
            {
                Destroy(child.gameObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Color gizmoColor = Color.green;
        Gizmos.color = gizmoColor;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 position = GetPositionFromGridIndex(new Vector2Int(x, y));
                Gizmos.DrawWireCube(position, new Vector3(roomWidth, roomHeight, 1));
            }
        }
    }

    public IEnumerator cambioDeLvl()
    {
        LvlChangeCanvas.SetActive(true);
        if(roomCount == 2)
        {
            LvlChangeCanvas.GetComponentInChildren<ChangeScene>().changeLvl1();
        }else if(gameLevel == 3)
        {
            LvlChangeCanvas.GetComponentInChildren<ChangeScene>().changeLvl2();
        }
        yield return new WaitForSeconds(2f);
        LvlChangeCanvas.SetActive(false);
    }
}

