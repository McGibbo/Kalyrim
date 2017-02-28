using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


[System.Serializable]
public class UIGameController : MonoBehaviour {

    [SerializeField]Text deathCounterText;
    [SerializeField]Text crystalCounterText;
    [SerializeField]int crystalAmount;
    [SerializeField]Image coverUp;
    [SerializeField]GameObject loadingScreen;
    [SerializeField]float coverUpSpeed;

    int crystalTotalAmountPrivate = 0;
    int crystalCounter = 0;
    int deathCounter = 0;

    bool removeCover = true;

    void Awake()
    {
        ChangeCrystalCounter(0);
    }

	void Update ()
    {
	    if (removeCover && coverUp.color.a > 0)
        {
            coverUp.color -= new Color(0, 0, 0, coverUpSpeed);
        }
        else if (removeCover == false && coverUp.color.a < 255)
        {
            coverUp.color += new Color(0, 0, 0, coverUpSpeed);
        }
        if (coverUp.color.a >= 1)
        {
            Debug.Log("HEJ");
            loadingScreen.SetActive(true);
            SceneManager.LoadScene(2);
        }   
	}

    public void GetCrystalTotalAmount(int crystalTotalAmount)
    {
        crystalTotalAmountPrivate = crystalTotalAmount;
    }

    public void ChangeCrystalCounter(int amount)
    {
        crystalCounter += amount;
        crystalCounterText.text = crystalCounter + "/" + crystalAmount;
    }

    public void ChangeDeathCounter(int amount)
    {
        deathCounter += amount;
        deathCounterText.text = "Deaths: " + deathCounter;
    }

    public void AddCoverUp()
    {
        removeCover = false;
    }

    public void RemoveCoverUp()
    {
        removeCover = true;
    }
}
