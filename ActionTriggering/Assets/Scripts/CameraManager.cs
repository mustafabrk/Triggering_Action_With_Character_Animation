using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject FPS;  
    public GameObject TPS;  //Kamera açýlarýný deðiþtirme iþlemini bu iki objenin setactive deðerini deðiþtirerek yapacaðýz

    public Transform playerArmature;
    public Transform playerCapsule;     //Kamera açýsý deðiþtirildiðinde önceki kamera açýsýnýn pozisyonunu elde etmemiz gerekiyor. Aksi halde kamera açýsý deðiþtiðinde farklý konumlarda bulunurlar.

    private Vector3 currentPosition;    //Önceki kamera açýsý modunun pozisyon bilgisini tutar
    private Quaternion currentRotation; //Önceki kamera açýsý modunun rotasyon bilgisini tutar

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
        if(cameraMode == 0) //Kamera modunun transform bilgilerini sürekli olarak current deðiþkenlerine aktarýyoruz 
        {
            currentPosition = playerArmature.transform.localPosition;
            currentRotation = playerArmature.transform.localRotation;
        }
        else
        {
            currentPosition = playerCapsule.transform.localPosition;
            currentRotation = playerCapsule.transform.localRotation;
        }

        if (Input.GetKeyDown(KeyCode.V) && cameraMode == 0) //V tuþuna basýldýðýnda kamera açýlarýný deðiþtirmemizi saðlayan if bloðu
        {
            TPS.SetActive(false);
            FPS.SetActive(true);
            cameraMode = 1;
            Debug.Log("FPS Modu");
            playerCapsule.transform.localPosition = currentPosition;    
            playerCapsule.transform.localRotation = currentRotation;    //Mevcut pozisyon ve rotasyon bilgileri yeni kamera modunun bilgilerine eþleniyor
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
