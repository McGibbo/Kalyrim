using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class ScreenCoverScript : MonoBehaviour {

    bool startScreenCover = false;
    [SerializeField]float screenCoverSpeed;

	void Update ()
    {
        CoverScreenUpdate();
	}

    void CoverScreenUpdate()
    {
        if (startScreenCover)
        {
            GetComponent<Image>().color += new Color(0, 0, 0, screenCoverSpeed);
        }
       
    }

    public void CoverScreen()
    {
        startScreenCover = true;
    }
}
