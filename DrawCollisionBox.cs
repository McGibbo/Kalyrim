using UnityEngine;
using System.Collections;

public class DrawCollisionBox : MonoBehaviour
{
    public GameObject TopCollisionBox, BottomCollisionBox;
    float leftValue;
    float rightValue;
    float flipedValue = 0.5f;
    const float scaleConstant = 1;

    public void SaveLeftValue(int x)
    {
        leftValue = x;
    }

    public void SaveRightValue(int x)
    {
        rightValue = x;
    }

    public void SpawnCollisionPlatform(int y, bool fliped)
    {
        if (fliped)
            flipedValue = -0.5f;
        else
            flipedValue = 0.5f;
        TopCollisionBox.transform.localScale = new Vector3(rightValue - leftValue + scaleConstant, flipedValue, scaleConstant);
        Instantiate(TopCollisionBox, new Vector2(leftValue + (rightValue - leftValue) / 2, y + (flipedValue / 2)), Quaternion.identity);
        BottomCollisionBox.transform.localScale = new Vector3(rightValue - leftValue + scaleConstant, flipedValue, scaleConstant);
        Instantiate(BottomCollisionBox, new Vector2(leftValue + (rightValue - leftValue) / 2, y - (flipedValue / 2)), Quaternion.identity);
        Debug.Log("Spawned at" + new Vector2(leftValue + (rightValue - leftValue) / 2, y).ToString() + "y = " + y.ToString());
    }
}
