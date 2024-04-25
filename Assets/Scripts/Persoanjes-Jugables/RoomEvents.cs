using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomEvents : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            Debug.Log("Colision con puerta");
            switch (collision.gameObject.name)
            {
                case "TopDoor":
                    mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y + 12, mainCamera.transform.position.z);
                    this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 5, this.transform.position.z);
                    break;
                case "BottomDoor":
                    mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y - 12, mainCamera.transform.position.z);
                    this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 5, this.transform.position.z);
                    break;
                case "LeftDoor":
                    mainCamera.transform.position = new Vector3(mainCamera.transform.position.x - 20, mainCamera.transform.position.y, mainCamera.transform.position.z);
                    this.transform.position = new Vector3(this.transform.position.x - 5, this.transform.position.y, this.transform.position.z);

                    break;
                case "RightDoor":
                    mainCamera.transform.position = new Vector3(mainCamera.transform.position.x + 20, mainCamera.transform.position.y, mainCamera.transform.position.z);
                    this.transform.position = new Vector3(this.transform.position.x + 5, this.transform.position.y + 5, this.transform.position.z);

                    break;
            }
        }
    }
}
