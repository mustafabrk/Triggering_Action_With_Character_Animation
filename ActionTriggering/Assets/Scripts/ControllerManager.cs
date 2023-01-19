using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    public GameObject lab;  //Sahne objelerine ula�abilmek i�in bu de�i�ken olu�turuluyor. Sahne objeleri ObjectManager isimli s�n�fta tutuluyor.

    private Animator anim;  //Animasyonlar� �al��t�rmak i�in bunu ekliyoruz.

    private Transform doorTransform;    // Kap�n�n konumunu de�i�tirebilmek i�in transform bilgilerini bu de�i�kene aktar�yoruz. 

    bool touchingTheButton;
    bool trueFalse;
    int loopCounter;

    void Start()
    {
        anim = GetComponent<Animator>();
        doorTransform = lab.GetComponent<ObjectManager>().door.transform;
        touchingTheButton = false;
        loopCounter = 0;
        trueFalse = true;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))    //Bu if blo�unu ayr� olarak Update fonksiyonunda yazmam�n sebebi OnTriggerStay'in saniyede birden �ok kez �al��mas�.
            touchingTheButton = true;       //Saniyede birden �ok kez �al��mas� bazen bast���m�z tu�un alg�lanamamas�na yol a��yor.
    }

    private void OnTriggerStay(Collider other)
    {
        StartCoroutine(DoorOpeningAreaControl(other));
        LihgtOpeningAreaControl(other);
    }

    IEnumerator DoorOpeningAreaControl(Collider a)
    {
        if(a.name == "DoorOpeningArea" && touchingTheButton && loopCounter < 1) //DoorOpeningArea b�lgesinin i�indeysen ve e tu�una bas�yorsan bu blo�a gir
        {
            anim.Play("Petting", -1, 0f);   //Animasyonu oynat
            loopCounter++;  //Sayac� artt�r ki kap�ya s�rekli olarak t�klanamas�n. ��nk� s�rekli t�klanabildi�inde olmas� gereken konumdan git gide uzakla��yor
            doorTransform.position = new Vector3(doorTransform.position.x, doorTransform.position.y, doorTransform.position.z - 1.5f);  //Kap�y� a�ar
            touchingTheButton = false;
            yield return new WaitForSeconds(5); //5 saniye bekle
            doorTransform.position = new Vector3(doorTransform.position.x, doorTransform.position.y, doorTransform.position.z + 1.5f);  //Kap�y� kapat�r
            loopCounter = 0;
        }
    }

    private void LihgtOpeningAreaControl(Collider a)
    {
        if (a.name == "LightOpeningArea" && touchingTheButton)  //LightOpeningArea b�lgesinin i�indeysen ve e tu�una bast�ysan bu blo�a gir
        {
            anim.Play("Petting", -1, 0f);   //Animasyonu oynat
            trueFalse = !trueFalse; //I���� a��ksa kapat, kapal�ysa a�
            lab.GetComponent<ObjectManager>().lambs.SetActive(trueFalse);
            touchingTheButton = false;
        }
    }
}
