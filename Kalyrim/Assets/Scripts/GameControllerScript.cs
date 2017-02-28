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
    float playerSpeedX;
    float playerSpeedY;
    float cameraLatencyX = 0;
    float cameraLatencyY = 0;
    [SerializeField]
    float cameraLatencyAmountX;
    [SerializeField]
    float cameraLatencyAmountY;
    List<Vector3> crystalList;
    public GameObject gameObj;
    UIGameController uIGameController;
    Vector3 currentSpawnPos;
    EditmodeInformation editmodeInformation;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        uIGameController = GameObject.FindGameObjectWithTag("GameCanvas").GetComponent<UIGameController>();
        oldPlayerPos = player.transform.position;
        crystalList = new List<Vector3>();
        //editmodeInformation = GameObject.FindGameObjectWithTag("EditorInformationGatherer").GetComponent<EditmodeInformation>();
        
    }
    void FixedUpdate()
    {
        MoveCamera();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            StartOverLevel();
        }
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        if (cameraScript == null)
        {
            cameraScript = mainCamera.GetComponent<CameraController>();
        }
    }

    public void StartOverLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void CollectCrystal(Vector3 coll)
    {
        crystalList.Add(coll);
        uIGameController.ChangeCrystalCounter(1);
    }

    public void ChangeDeathCounter()
    {
        uIGameController.ChangeDeathCounter(1);
    }

    public void FinishLevel()
    {
        uIGameController.AddCoverUp();
    }

    public void EndLevel()
    {

    }


    public void SpawnCrystals()
    {
        for (int i = 0; i < crystalList.Count; i++)
        {
            if(crystalList[i].x > currentSpawnPos.x)
            {
                Instantiate(gameObj, crystalList[i], Quaternion.identity);
                uIGameController.ChangeCrystalCounter(-1);
            }
        }
        crystalList = new List<Vector3>();
    }

    public void GetCurrentSpawnPos(Vector3 spawnPos)
    {
        currentSpawnPos = spawnPos;
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
            mainCamera.transform.position += new Vector3((playerSpeedX) * cameraLatencyAmountX, (playerSpeedY) * cameraLatencyAmountY, 0);
            oldPlayerPos = player.transform.position;
        }
    }

    public void ScreenShake()
    {
        //cameraScript.StartScreenShake(playerSpeedY * -1 * shakeAmount);
    }

}