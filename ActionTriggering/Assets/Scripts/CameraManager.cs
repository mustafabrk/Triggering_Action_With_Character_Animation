using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject FPS;
    public GameObject TPS;

    public int cameraMode = 0; //0 TPS, 1 FPS

    void Awake()
    {
        FPS.SetActive(false);
        TPS.SetActive(true);
        Debug.Log(cameraMode);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) && cameraMode == 0)
        {
            TPS.SetActive(false);
            FPS.SetActive(true);
            cameraMode = 1;
            Debug.Log("ÝLK AÞAMADASIN");
        }
        else if (Input.GetKeyDown(KeyCode.V) && cameraMode == 1)
        {
            FPS.SetActive(false);
            TPS.SetActive(true);
            cameraMode = 0;
            Debug.Log("ÝKÝNCÝ AÞAMADASIN");
        }
    }
}
