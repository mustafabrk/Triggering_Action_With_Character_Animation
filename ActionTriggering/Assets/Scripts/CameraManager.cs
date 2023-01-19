using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject FPS;  
    public GameObject TPS;  //Kamera a��lar�n� de�i�tirme i�lemini bu iki objenin setactive de�erini de�i�tirerek yapaca��z

    public Transform playerArmature;
    public Transform playerCapsule;     //Kamera a��s� de�i�tirildi�inde �nceki kamera a��s�n�n pozisyonunu elde etmemiz gerekiyor. Aksi halde kamera a��s� de�i�ti�inde farkl� konumlarda bulunurlar.

    private Vector3 currentPosition;    //�nceki kamera a��s� modunun pozisyon bilgisini tutar
    private Quaternion currentRotation; //�nceki kamera a��s� modunun rotasyon bilgisini tutar

    public int cameraMode = 0; //0 TPS, 1 FPS

    void Awake()
    {
        FPS.SetActive(false);
        TPS.SetActive(true);

        currentPosition = playerArmature.transform.localPosition;
        currentRotation = playerArmature.transform.localRotation;
    }

    void Update()
    {
        if(cameraMode == 0) //Kamera modunun transform bilgilerini s�rekli olarak current de�i�kenlerine aktar�yoruz 
        {
            currentPosition = playerArmature.transform.localPosition;
            currentRotation = playerArmature.transform.localRotation;
        }
        else
        {
            currentPosition = playerCapsule.transform.localPosition;
            currentRotation = playerCapsule.transform.localRotation;
        }

        if (Input.GetKeyDown(KeyCode.V) && cameraMode == 0) //V tu�una bas�ld���nda kamera a��lar�n� de�i�tirmemizi sa�layan if blo�u
        {
            TPS.SetActive(false);
            FPS.SetActive(true);
            cameraMode = 1;
            Debug.Log("FPS Modu");
            playerCapsule.transform.localPosition = currentPosition;    
            playerCapsule.transform.localRotation = currentRotation;    //Mevcut pozisyon ve rotasyon bilgileri yeni kamera modunun bilgilerine e�leniyor
        }
        else if (Input.GetKeyDown(KeyCode.V) && cameraMode == 1)
        {
            FPS.SetActive(false);
            TPS.SetActive(true);
            cameraMode = 0;
            Debug.Log("TPS Modu");
            playerArmature.transform.localPosition = currentPosition;
            playerArmature.transform.localRotation = currentRotation;
        }
    }
}
