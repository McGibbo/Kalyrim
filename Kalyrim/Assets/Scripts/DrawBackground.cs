using UnityEngine;
using System.Collections;

[System.Serializable]
public class ColorToPrefabBG
{
    public Color32 color;
    public GameObject prefab;
}
[ExecuteInEditMode]
public class DrawBackground : MonoBehaviour
{
    public Texture2D levelMap;
    public GameObject parent;
    public float xPos, yPos, zPos;
    public ColorToPrefabBG[] colorToPrefab;

    GameObject tempPlatform;

    void Start()
    {
        LoadMap();
    }

    void LoadMap()
    {
        Color32[] allPixels = levelMap.GetPixels32();
        int width = levelMap.width;
        int height = levelMap.height;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                SpawnTile(allPixels[(y * width) + x], x, y);
            }
        }
    }

    void SpawnTile(Color32 c, float x, float y)
    {
        foreach (ColorToPrefabBG ctp in colorToPrefab)
        {
            if (c.Equals(ctp.color))
            {

                tempPlatform = Instantiate(ctp.prefab, new Vector3(x * 15 + xPos, y * 15 + yPos, zPos), ctp.prefab.transform.rotation) as GameObject;
                tempPlatform.transform.parent = parent.transform;
                return;
            }
        }
        Debug.LogError("Map: " + levelMap.name.ToString() + "\n Incorrect Color: " + c.ToString() + "\n Position: (" + x.ToString() + "x, " + y.ToString() + "y) from the bottom left of the screen");
    }
}