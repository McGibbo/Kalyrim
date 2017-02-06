﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class ColorToPrefab
{
    public Color32 color;
    public GameObject prefab;
}

public class DrawLevel : MonoBehaviour
{
    public Texture2D levelMap;
    public ColorToPrefab[] colorToPrefab;

	void Start () {
        LoadMap();
	}

    void EmptyMap()
    {
        while(transform.childCount > 0)
        {
            Transform c = transform.GetChild(0);
            c.SetParent(null);
            Destroy(c.gameObject);
        }
    }

    void LoadMap()
    {
        EmptyMap();

        Color32[] allPixels = levelMap.GetPixels32();
        int width = levelMap.width;
        int height = levelMap.height;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                SpawnTile(allPixels[(y * width) + x], x, y);
            }
        }

    }

    void SpawnTile(Color32 c, int x, int y)
    {
        if (c.a <= 0)
            return;

        foreach(ColorToPrefab ctp in colorToPrefab)
        {
            if (c.Equals(ctp.color))
            {
                Instantiate(ctp.prefab, new Vector3(x, y, 0), Quaternion.identity);
                return;
            }
        }
        Debug.LogError("Incorrect Color: " + c.ToString() + "\n Position: (" + x.ToString() + "x, " + y.ToString() + "y) from the bottom left of the screen");
        
    }


}