using UnityEngine;
using System.Collections;

[System.Serializable]
public class ColorToPrefab
{
    public Color32 color;
    public GameObject prefab;
    public ObjectTag objectTag;
    public enum ObjectTag
    {
        None,
        LeftPlatform,
        RightPlatform,
        Crystal
    }
    public bool fliped;
}

[ExecuteInEditMode]
public class DrawLevel : MonoBehaviour
{
    GameObject tempPlatform;
    public Texture2D levelMap;
    public GameObject middleMapParent;
    public ZMap zMap;
    public enum ZMap
    {
        Back,
        Middle,
        Front
    }
    public ColorToPrefab[] colorToPrefab;
    DrawCollisionBox dcb;
    float flipedValue = 1;
    int mapZPos;
    int crystalCount;

    void Start()
    {
        dcb = GetComponent<DrawCollisionBox>();
        PlayerPrefs.SetInt("Crystals", 0);
        LoadMap();

    }


    //void EmptyMap()
    //{
    //    while(transform.childCount > 0)
    //    {
    //        Transform c = transform.GetChild(0);
    //        c.SetParent(null);
    //        Destroy(c.gameObject);
    //    }
    //}

    void LoadMap()
    {
        //EmptyMap();

        Color32[] allPixels = levelMap.GetPixels32();
        int width = levelMap.width;
        int height = levelMap.height;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                SpawnTile(allPixels[(y * width) + x], x + 0.5f, y + 0.5f);
            }
        }
    }

    void SpawnTile(Color32 c, float x, float y)
    {
        if (c.a <= 0)
            return;

        foreach (ColorToPrefab ctp in colorToPrefab)
        {
            if (c.Equals(ctp.color))
            {
                if (ctp.fliped)
                {
                    flipedValue = -1;
                }
                else
                {
                    flipedValue = 1;
                }

                switch (zMap)
                {
                    case ZMap.Back:
                        mapZPos = 1;
                        break;

                    case ZMap.Middle:
                        mapZPos = 0;
                        break;
                    case ZMap.Front:
                        mapZPos = -1;
                        break;
                }

                tempPlatform = Instantiate(ctp.prefab, new Vector3(x, y, mapZPos), Quaternion.identity) as GameObject;
                tempPlatform.transform.localScale = new Vector3(tempPlatform.transform.localScale.x, tempPlatform.transform.localScale.y * flipedValue, tempPlatform.transform.localScale.z);
                tempPlatform.transform.parent = middleMapParent.transform;

                switch (ctp.objectTag)
                {
                    case ColorToPrefab.ObjectTag.None:
                        return;

                    case ColorToPrefab.ObjectTag.LeftPlatform:
                        dcb.SaveLeftValue(x, y);
                        break;

                    case ColorToPrefab.ObjectTag.RightPlatform:
                        dcb.SaveRightValue(x);
                        dcb.SpawnCollisionPlatform(y, ctp.fliped);
                        break;

                    case ColorToPrefab.ObjectTag.Crystal:
                        PlayerPrefs.SetInt("Crystals", PlayerPrefs.GetInt("Crystals") + 1);
                        break;
                }
                return;
            }

        }
        Debug.LogError("Map: " + levelMap.name.ToString() + " " + zMap.ToString() + "\n Incorrect Color: " + c.ToString() + "\n Position: (" + x.ToString() + "x, " + y.ToString() + "y) from the bottom left of the screen");

    }

    public int GetCrystalCount()
    {
        return crystalCount;
    }


}
