using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    public GameObject lab;  //Sahne objelerine ulaþabilmek için bu deðiþken oluþturuluyor. Sahne objeleri ObjectManager isimli sýnýfta tutuluyor.

    private Animator anim;  //Animasyonlarý çalýþtýrmak için bunu ekliyoruz.

    private Transform doorTransform;    // Kapýnýn konumunu deðiþtirebilmek için transform bilgilerini bu deðiþkene aktarýyoruz. 

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
        if (Input.GetKeyDown(KeyCode.E))    //Bu if bloðunu ayrý olarak Update fonksiyonunda yazmamýn sebebi OnTriggerStay'in saniyede birden çok kez çalýþmasý.
            touchingTheButton = true;       //Saniyede birden çok kez çalýþmasý bazen bastýðýmýz tuþun algýlanamamasýna yol açýyor.
    }

    private void OnTriggerStay(Collider other)
    {
        StartCoroutine(DoorOpeningAreaControl(other));
        LihgtOpeningAreaControl(other);
    }

    IEnumerator DoorOpeningAreaControl(Collider a)
    {
        if(a.name == "DoorOpeningArea" && touchingTheButton && loopCounter < 1) //DoorOpeningArea bölgesinin içindeysen ve e tuþuna basýyorsan bu bloða gir
        {
            anim.Play("Petting", -1, 0f);   //Animasyonu oynat
            loopCounter++;  //Sayacý arttýr ki kapýya sürekli olarak týklanamasýn. Çünkü sürekli týklanabildiðinde olmasý gereken konumdan git gide uzaklaþýyor
            doorTransform.position = new Vector3(doorTransform.position.x, doorTransform.position.y, doorTransform.position.z - 1.5f);  //Kapýyý açar
            touchingTheButton = false;
            yield return new WaitForSeconds(5); //5 saniye bekle
            doorTransform.position = new Vector3(doorTransform.position.x, doorTransform.position.y, doorTransform.position.z + 1.5f);  //Kapýyý kapatýr
            loopCounter = 0;
        }
    }

    private void LihgtOpeningAreaControl(Collider a)
    {
        if (a.name == "LightOpeningArea" && touchingTheButton)  //LightOpeningArea bölgesinin içindeysen ve e tuþuna bastýysan bu bloða gir
        {
            anim.Play("Petting", -1, 0f);   //Animasyonu oynat
            trueFalse = !trueFalse; //Iþýðý açýksa kapat, kapalýysa aç
            lab.GetComponent<ObjectManager>().lambs.SetActive(trueFalse);
            touchingTheButton = false;
        }
    }
}
