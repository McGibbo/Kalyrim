using UnityEngine;
using System.Collections;

public class DrawCollisionBox : MonoBehaviour
{
    public GameObject CollisionBox;
    float leftValue;
    float rightValue;
    //DrawLevel draw;

    void Start()
    {
        //draw = GetComponent<DrawLevel>();
        //LoadMap();
    }

    //void LoadMap()
    //{

    //    Color32[] allPixels = draw.levelMap.GetPixels32();
    //    int width = draw.levelMap.width;
    //    int height = draw.levelMap.height;

    //    for (int y = 0; y < height; y++)
    //    {
    //        for (int x = 0; x < width; x++)
    //        {
    //            SpawnTile(allPixels[(y * width) + x], x, y);
    //        }
    //    }
    //}

    public void SaveLeftValue(int x)
    {
        leftValue = x;
    }

    public void SaveRightValue(int x)
    {
        rightValue = x;
    }

    public void SpawnCollisionPlatform(int y)
    {
        CollisionBox.transform.localScale = new Vector3(rightValue - leftValue + 1, 1, 1);
        Instantiate(CollisionBox, new Vector2(leftValue + (rightValue - leftValue) / 2, y), Quaternion.identity);
        Debug.Log("Spawned at" + new Vector2(leftValue + (rightValue - leftValue) / 2, y).ToString() + "y = " + y.ToString());
    }


    //void SpawnTile(Color32 c, int x, int y)
    //{
    //    if (c.a <= 0)
    //        return;

    //    foreach (ColorToPrefab ctp in draw.colorToPrefab)
    //    {
    //        switch (ctp.objectTag)
    //        {
    //            case ColorToPrefab.ObjectTag.None:
    //                return;
    //                break;

    //            case ColorToPrefab.ObjectTag.LeftPlatform:
    //                SaveLeftValue(x);
    //                break;

    //            case ColorToPrefab.ObjectTag.RightPlatform:
    //                SaveRightValue(x);
    //                SpawnCollisionPlatform(y);
                   
    //                break;
    //        }
    //    }
    //}
}
