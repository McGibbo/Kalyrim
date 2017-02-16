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
    [SerializeField]
    Vector2 maxSize;

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
            if (rect.sizeDelta.y < maxSize.y)
            {
                rect.sizeDelta += new Vector2(speed.x, speed.y);
                rect.transform.localPosition += new Vector3(-speed.x/2, -speed.y/2, -speed.z/2);
            }
            else if (rect.sizeDelta.y > maxSize.y)
            {
                rect.sizeDelta = maxSize;
            }
        }
        else
        {
            if (rect.sizeDelta.y > 0)
            {
                rect.sizeDelta += new Vector2(-speed.x, -speed.y);
                rect.transform.localPosition += new Vector3(speed.x / 2, speed.y / 2, speed.z / 2);
            }
            else if (rect.sizeDelta.y < 0)
            {
                rect.sizeDelta = new Vector2(rect.sizeDelta.x,0);
            }
        }
    }
}
