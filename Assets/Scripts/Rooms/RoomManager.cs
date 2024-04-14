using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] GameObject roomPrefab;
    [SerializeField] private int maxRooms = 15;
    [SerializeField] private int minRooms = 10;
    [SerializeField] private int gridSizeX = 10;
    [SerializeField] private int gridSizeY = 10;

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
    }
    private void StartRoomGenerationFromRoom(Vector2Int roomIndex)
    {
        roomQueue.Enqueue(roomIndex);
        int x = roomIndex.x;
        int y = roomIndex.y;
        roomGrid[x, y] = 1;
        roomCount++;
        var initialRoom = Instantiate(roomPrefab, GetPositionFromGridIndex(roomIndex), Quaternion.identity);
        initialRoom.name = $"Room-{roomCount}";
        initialRoom.GetComponent<Room>().RoomIndex = roomIndex;
        roomObjects.Add(initialRoom);
    }

    private bool TryGenerateRoom(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;

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
        if (x > 0 && roomGrid[x - 1, y] != 0)
        {
            //Neighbour left
            newRoom.OpenDoor(Vector2Int.left);
            leftRoom.OpenDoor(Vector2Int.right);
        }
        if (x < gridSizeX - 1 && roomGrid[x + 1, y] != 0)
        {
            //Neighbour right
            newRoom.OpenDoor(Vector2Int.right);
            rightRoom.OpenDoor(Vector2Int.left);
        }
        if (y > 0 && roomGrid[x, y - 1] != 0)
        {
            //Neighbour below
            newRoom.OpenDoor(Vector2Int.down);
            bottomRoom.OpenDoor(Vector2Int.up);
        }
        if (x < gridSizeY - 1 && roomGrid[x, y + 1] != 0)
        {
            //Neighbour above
            newRoom.OpenDoor(Vector2Int.up);
            topRoom.OpenDoor(Vector2Int.down);
        }
    }

    Room GetRoomScriptAt(Vector2Int roomIndex)
    {
       GameObject room = roomObjects.Find(r => r.GetComponent<Room>().RoomIndex == roomIndex);
        if (room != null)
            return room.GetComponent<Room>();
        else
            return null;
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