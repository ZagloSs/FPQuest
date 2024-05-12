using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] GameObject roomPrefab;
    [SerializeField] GameObject[] fillPrefab;
    [SerializeField] private int maxRooms = 15;
    [SerializeField] private int minRooms = 10;
    [SerializeField] private int gridSizeX = 10;
    [SerializeField] private int gridSizeY = 10;

    [SerializeField] GameObject[] itemPrefabs;
    [SerializeField] GameObject itemPedestal;
    [SerializeField] GameObject bossRoomPrefab;

    private bool hasGeneratedItemRoom = false;
    private bool hasGeneratedBossRoom = false;

    private int roomWidth = 20;
    private int roomHeight = 12;


    private List<GameObject> roomObjects = new List<GameObject>();

    private Queue<Vector2Int> roomQueue = new Queue<Vector2Int>();

    private int[,] roomGrid;

    private int roomCount;

    private bool generationComplete = false;

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

        GameObject fill = Instantiate(fillPrefab[0], roomFill.position, Quaternion.identity, roomFill);

        initialRoom.name = $"Room-{roomCount}";
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

        GameObject fill = Instantiate(fillPrefab[Random.Range(1, fillPrefab.Length)], roomFill.position, Quaternion.identity, roomFill);

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
            newRoom.OpenDoor(Vector2Int.left);
            leftRoom.OpenDoor(Vector2Int.right);
        }
        if (rightRoom != null)
        {
            //Neighbour right
            newRoom.OpenDoor(Vector2Int.right);
            rightRoom.OpenDoor(Vector2Int.left);
        }
        if (bottomRoom != null)
        {
            //Neighbour below
            newRoom.OpenDoor(Vector2Int.down);
            bottomRoom.OpenDoor(Vector2Int.up);
        }
        if (topRoom != null)
        {
            //Neighbour above
            newRoom.OpenDoor(Vector2Int.up);
            topRoom.OpenDoor(Vector2Int.down);
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
        int randomRoomIndex = Random.Range(1, roomObjects.Count);
        GameObject itemRoom = roomObjects[randomRoomIndex];

        // Find the RoomFill object in the room
        Transform roomFill = itemRoom.transform.Find("RoomFill");

        // Clear the room of any existing RoomFill objects
        ClearRoom(roomFill);

        // Instantiate the item prefab in the room with a pedestal
        GameObject item = Instantiate(itemPrefabs[Random.Range(0, itemPrefabs.Length)], itemRoom.transform.position, Quaternion.identity, roomFill);
        item.name = "Item";
        GameObject pedestal = Instantiate(itemPedestal, itemRoom.transform.position, Quaternion.identity, roomFill);
        GameObject fill = Instantiate(fillPrefab[0], roomFill.position, Quaternion.identity, roomFill);

    }

    private void GenerateBossRoom()
    {
        hasGeneratedBossRoom = true;

        // Choose a random room from the existing rooms
        GameObject bossRoom = roomObjects[roomObjects.Count - 1];

        // Find the RoomFill object in the room
        Transform roomFill = bossRoom.transform.Find("RoomFill");

        // Clear the contents of the previous room
        ClearRoom(roomFill);

        // Instantiate the boss room prefab as a child of the room
        GameObject bossRoomInstance = Instantiate(bossRoomPrefab, roomFill.position, Quaternion.identity, roomFill);
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
}
