using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCollisionHandler : MonoBehaviour
{
    // Define an enum for door types
    public enum DoorType { Top, Bottom, Left, Right }

    // Create a dictionary to map door names to their types
    private readonly Dictionary<string, DoorType> doorTypes = new Dictionary<string, DoorType>()
    {
        { "TopDoor", DoorType.Top },
        { "BottomDoor", DoorType.Bottom },
        { "LeftDoor", DoorType.Left },
        { "RightDoor", DoorType.Right }
    };

    // Define animation durations and speeds
    public float cameraAnimationDuration = 1f;
    public float cameraMovementSpeed = 2f;
    public float playerMovementSpeed = 5f;

    private Camera mainCamera;
    private Transform playerTransform;

    void Start()
    {
        mainCamera = Camera.main;
        playerTransform = transform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            Debug.Log("Collision with door");
            DoorType doorType;
            if (doorTypes.TryGetValue(collision.gameObject.name, out doorType))
            {
                // Create an animation for the camera movement
                StartCoroutine(AnimateCameraMovement(doorType));

                // Move the player towards the door
                StartCoroutine(MovePlayerTowardsDoor(doorType));
            }
        }
    }

    private IEnumerator AnimateCameraMovement(DoorType doorType)
    {
        float startTime = Time.time;
        Vector3 startPosition = mainCamera.transform.position;
        Vector3 endPosition = GetEndPosition(doorType);

        while (Time.time - startTime < cameraAnimationDuration)
        {
            float t = (Time.time - startTime) / cameraAnimationDuration;
            mainCamera.transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }

        mainCamera.transform.position = endPosition;
    }

    private IEnumerator MovePlayerTowardsDoor(DoorType doorType)
    {
        Vector3 endPosition = GetPlayerEndPosition(doorType);

        while (playerTransform.position != endPosition)
        {
            playerTransform.position = endPosition;
            yield return null;
        }
    }

    private Vector3 GetEndPosition(DoorType doorType)
    {
        switch (doorType)
        {
            case DoorType.Top:
                return new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y + 12, mainCamera.transform.position.z);
            case DoorType.Bottom:
                return new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y - 12, mainCamera.transform.position.z);
            case DoorType.Left:
                return new Vector3(mainCamera.transform.position.x - 20, mainCamera.transform.position.y, mainCamera.transform.position.z);
            case DoorType.Right:
                return new Vector3(mainCamera.transform.position.x + 20, mainCamera.transform.position.y, mainCamera.transform.position.z);
            default:
                return mainCamera.transform.position;
        }
    }

    private Vector3 GetPlayerEndPosition(DoorType doorType)
    {
        switch (doorType)
        {
            case DoorType.Top:
                return new Vector3(playerTransform.position.x, playerTransform.position.y + 5, playerTransform.position.z);
            case DoorType.Bottom:
                return new Vector3(playerTransform.position.x, playerTransform.position.y - 5, playerTransform.position.z);
            case DoorType.Left:
                return new Vector3(playerTransform.position.x - 5, playerTransform.position.y, playerTransform.position.z);
            case DoorType.Right:
                return new Vector3(playerTransform.position.x + 5, playerTransform.position.y, playerTransform.position.z);
            default:
                return playerTransform.position;
        }
    }
}