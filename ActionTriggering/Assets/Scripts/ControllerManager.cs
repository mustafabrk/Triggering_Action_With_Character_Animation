using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    public GameObject lab;
    public GameObject player;

    private Animator anim;

    private Transform doorTransform;

    bool touchingTheButton;
    bool trueFalse;
    int loopCounter;
    int cameraMode;

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
        cameraMode = player.gameObject.GetComponent<CameraManager>().cameraMode;

        if (Input.GetKeyDown(KeyCode.E))
            touchingTheButton = true;
    }

    private void OnTriggerStay(Collider other)
    {
        StartCoroutine(DoorOpeningAreaControl(other));
        LihgtOpeningAreaControl(other);
    }

    IEnumerator DoorOpeningAreaControl(Collider a)
    {
        if(a.name == "DoorOpeningArea" && touchingTheButton && loopCounter < 1 && cameraMode == 0)
        {
            anim.Play("Petting", -1, 0f);
            loopCounter++;
            doorTransform.position = new Vector3(doorTransform.position.x, doorTransform.position.y, doorTransform.position.z - 1.5f);
            touchingTheButton = false;
            yield return new WaitForSeconds(5);
            doorTransform.position = new Vector3(doorTransform.position.x, doorTransform.position.y, doorTransform.position.z + 1.5f);
            loopCounter = 0;
        }
        else if(a.name == "DoorOpeningArea" && touchingTheButton && loopCounter < 1 && cameraMode == 1)
        {
            loopCounter++;
            doorTransform.position = new Vector3(doorTransform.position.x, doorTransform.position.y, doorTransform.position.z - 1.5f);
            touchingTheButton = false;
            yield return new WaitForSeconds(5);
            doorTransform.position = new Vector3(doorTransform.position.x, doorTransform.position.y, doorTransform.position.z + 1.5f);
            loopCounter = 0;
        }
    }

    private void LihgtOpeningAreaControl(Collider a)
    {
        if (a.name == "LightOpeningArea" && touchingTheButton && cameraMode == 0)
        {
            anim.Play("Petting", -1, 0f);
            trueFalse = !trueFalse;
            lab.GetComponent<ObjectManager>().lambs.SetActive(trueFalse);
            touchingTheButton = false;
        }
        else if (a.name == "LightOpeningArea" && touchingTheButton && cameraMode == 1)
        {
            trueFalse = !trueFalse;
            lab.GetComponent<ObjectManager>().lambs.SetActive(trueFalse);
            touchingTheButton = false;
        }
    }
}
