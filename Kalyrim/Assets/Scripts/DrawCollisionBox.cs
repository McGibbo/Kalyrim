using UnityEngine;
using System.Collections;

public class DrawCollisionBox : MonoBehaviour
{
    public GameObject
        TopCollisionBox,
        BottomCollisionBox,
        deathCollisionBox,
        rampCollisionBox,
        parent;
    float leftValue;
    float rightValue;
    float flipedValue = 0.5f;
    const float scaleConstant = 1;
    GameObject tempPlatform;


    public void SaveLeftValue(float x, float y)
    {
        leftValue = x;
        //rampCollisionBox.transform.localScale = new Vector3(0.4f, 0.2f, 1);
        //Instantiate(rampCollisionBox, new Vector2(x - .5f, y + .3f), new Quaternion(0, 0, Mathf.PI/2, Quaternion.identity.w));
        //tempPlatform = Instantiate(rampCollisionBox, new Vector2(x - .6f, y + .3f), Quaternion.identity) as GameObject;
        //tempPlatform.transform.localScale = new Vector3(0.4f, 0.2f, 1);
        //tempPlatform.transform.rotation = new Quaternion(0, 0, Mathf.PI / 8, tempPlatform.transform.rotation.w);
        //tempPlatform.transform.parent = parent.transform;
    }

    public void SaveRightValue(float x)
    {
        rightValue = x;
    }

    public void SpawnCollisionPlatform(float y, bool fliped)
    {
        if (fliped)
            flipedValue = -1;
        else
            flipedValue = 1;

        tempPlatform = Instantiate(rampCollisionBox, new Vector2(leftValue - .6f, y + (.3f * flipedValue)), Quaternion.identity) as GameObject;
        tempPlatform.transform.localScale = new Vector3(0.4f, 0.2f, 1);
        tempPlatform.transform.rotation = new Quaternion(0, 0, flipedValue * Mathf.PI / 8, tempPlatform.transform.rotation.w);
        tempPlatform.transform.parent = parent.transform;
        tempPlatform = null;

        tempPlatform = Instantiate(deathCollisionBox, new Vector2(leftValue - .35f, y - (.133f * flipedValue)), Quaternion.identity) as GameObject;
        tempPlatform.transform.localScale = new Vector3(1, 0.5f, 1);
        tempPlatform.transform.parent = parent.transform;
        tempPlatform = null;

        tempPlatform = Instantiate(TopCollisionBox, new Vector2(leftValue + (rightValue - leftValue) / 2, y + (flipedValue / 4)), Quaternion.identity) as GameObject;
        tempPlatform.transform.localScale = new Vector3(rightValue - leftValue + scaleConstant, flipedValue / 2, scaleConstant);
        tempPlatform.transform.parent = parent.transform;
        tempPlatform = null;

        tempPlatform = Instantiate(BottomCollisionBox, new Vector2(leftValue + (rightValue - leftValue) / 2, y - (flipedValue / 4)), Quaternion.identity) as GameObject;
        tempPlatform.transform.localScale = new Vector3(rightValue - leftValue + scaleConstant, flipedValue / 2, scaleConstant);
        tempPlatform.transform.parent = parent.transform;
        tempPlatform = null;
    }
}
