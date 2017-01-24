using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameControllerScript : MonoBehaviour
{

    [SerializeField]float shakeAmount;
    GameObject player;
    Camera mainCamera;
    CameraScript cameraScript;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = Camera.main;
        cameraScript = mainCamera.GetComponent<CameraScript>();
    }


    void Update()
    {
        MoveCamera();
    }

    void MoveCamera()
    {
        if (player.transform.position.x > mainCamera.transform.position.x)
        {
            mainCamera.transform.position = new Vector3(player.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
        }
    }

    public void ScreenShake(float playerYSpeed)
    {
        Debug.Log("GameController");
        cameraScript.StartScreenShake(playerYSpeed * 100000000000 * shakeAmount);
        Debug.Log(playerYSpeed * 100000000000 * shakeAmount);
    }

}
