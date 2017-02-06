using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CameraController : MonoBehaviour
{

    [SerializeField]
    float shakeSpeed;
    [SerializeField]
    float shakeDuration;
    float cameraShakeAmount;
    float shakeDurationPrivate = 0;
    bool shakeUp = true;
    float originalYPos;
    [SerializeField]
    bool shakeCamera;
    bool resetPosition = false;

    void Update()
    {
        ScreenShake();
    }

    public void StartScreenShake(float shakeAmount)
    {
        shakeDurationPrivate = shakeDuration;
        cameraShakeAmount = shakeAmount;
        originalYPos = transform.position.y;
        resetPosition = true;
    }

    void ScreenShake()
    {
        if (shakeDurationPrivate > 0 && shakeCamera)
        {
            shakeDurationPrivate -= Time.deltaTime;
            transform.position += new Vector3(0, shakeSpeed, 0);
            if (transform.position.y > originalYPos + cameraShakeAmount || transform.position.y < originalYPos - cameraShakeAmount)
            {
                shakeSpeed *= -1;
            }
        }
        else if (resetPosition)
        {
            transform.position = new Vector3(transform.position.x, originalYPos, transform.position.z);
            resetPosition = false;
        }
    }

}
