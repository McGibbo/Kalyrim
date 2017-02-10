using UnityEngine;
using System.Collections;

[System.Serializable]
public class PanelScript : MonoBehaviour {

    public bool moveRight = false;

    private Vector3 startPositon;
    [SerializeField]
    private Vector3 endPosition;
    [SerializeField]
    private Vector3 speed;
    RectTransform rect;

    void Awake ()
    {
        rect = GetComponent<RectTransform>();
        startPositon = rect.transform.localPosition;
	}


	void Update () {
        MovePanel();
        
    }

    public void PanelMoveRight()
    {
        if (moveRight == false)
            moveRight = true;
        else
            moveRight = false;
        
    }

    public void PanelMoveLeft()
    {
        if (moveRight)
        moveRight = false;
    }

    void MovePanel()
    {
        if (moveRight)
        {
            if (rect.transform.localPosition.x < endPosition.x)
            {
                rect.transform.localPosition += new Vector3(speed.x, speed.y, speed.z);
            }
            else if (rect.transform.localPosition.x > endPosition.x)
            {
                rect.transform.localPosition = new Vector3(endPosition.x, startPositon.y, startPositon.z);
                Debug.Log(rect.transform.localPosition);
            }
        }
        else
        {
            if (rect.transform.localPosition.x > startPositon.x)
            {
                rect.transform.localPosition += new Vector3(-speed.x, -speed.y, -speed.z);
            }
            else
            {
                rect.transform.localPosition = new Vector3(startPositon.x, startPositon.y, startPositon.z);
            }
        }
    }
}
