using UnityEngine;
using System.Collections;

[System.Serializable]
public class ColorToPrefabBG
{
    public Color32 color;
    public GameObject prefab;
}

public class DrawBackground : MonoBehaviour
{
    public Texture2D levelMap;
    public ColorToPrefabBG[] colorToPrefab;

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
                
                Instantiate(ctp.prefab, new Vector3(x * 15 - 10, y * 15 - 10, 5), ctp.prefab.transform.rotation);
                return;
            }
        }
        Debug.LogError("Map: " + levelMap.name.ToString() + "\n Incorrect Color: " + c.ToString() + "\n Position: (" + x.ToString() + "x, " + y.ToString() + "y) from the bottom left of the screen");
    }
}