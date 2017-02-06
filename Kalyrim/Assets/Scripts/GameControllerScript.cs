using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameControllerScript : MonoBehaviour
{

    [SerializeField]
    float shakeAmount;
    GameObject player;
    Camera mainCamera;
    CameraController cameraScript;
    Vector3 oldPlayerPos = new Vector3(0, 0, 0);
    Vector3 newPlayerPos = new Vector3(0, 0, 0);
    float cameraLatencyX = 0;
    float cameraLatencyY = 0;
    [SerializeField]
    float cameraLatencyAmountX;
    [SerializeField]
    float cameraLatencyAmountY;
    float playerSpeedX;
    float playerSpeedY;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = Camera.main;
        cameraScript = mainCamera.GetComponent<CameraController>();
        oldPlayerPos = player.transform.position;
    }


    void FixedUpdate()
    {
        MoveCamera();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            SceneManager.LoadScene(0);
        }
    }

    void MoveCamera()
    {
        if (player.transform.position != oldPlayerPos)
        {
            if (cameraLatencyAmountX < 0.9)
            {
                cameraLatencyAmountX *= 1.01f;
            }
            else if (cameraLatencyAmountX > 1.1)
            {
                cameraLatencyAmountX *= 0.99f;
            }
            else
            {
                cameraLatencyAmountX = 1f;
            }

            newPlayerPos = player.transform.position;
            playerSpeedX = newPlayerPos.x - oldPlayerPos.x;
            playerSpeedY = newPlayerPos.y - oldPlayerPos.y;
            mainCamera.transform.position += new Vector3((newPlayerPos.x - oldPlayerPos.x) * cameraLatencyAmountX, (newPlayerPos.y - oldPlayerPos.y) * cameraLatencyAmountY, 0);
            oldPlayerPos = player.transform.position;
        }

    }


    public void ScreenShake()
    {
        cameraScript.StartScreenShake(playerSpeedY * -1 * shakeAmount);
    }

}