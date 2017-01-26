using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CameraScript : MonoBehaviour {

    [SerializeField]float shakeSpeed;
    [SerializeField]float shakeDuration;
    float cameraShakeAmount;
    float shakeDurationPrivate = 0;
    bool shakeUp = true;
    float originalYPos;
    [SerializeField]bool shakeCamera;

    void Start()
    {
        
    }

    void Update () {
        screenShake();
	}

    public void StartScreenShake(float shakeAmount)
    {
        shakeDurationPrivate = shakeDuration;
        cameraShakeAmount = shakeAmount;
        originalYPos = transform.position.y;
    }

    void screenShake()
    {
        if (shakeDurationPrivate > 0)
        {
            shakeDurationPrivate -= Time.deltaTime;
            transform.position += new Vector3(0, shakeSpeed, 0);
            if (transform.position.y > originalYPos + cameraShakeAmount || transform.position.y < originalYPos - cameraShakeAmount)
            {
                shakeSpeed *= -1;
            }
        }
        else
        {
            //transform.position = new Vector3(transform.position.x, originalYPos, transform.position.z);
        }
    }

}
