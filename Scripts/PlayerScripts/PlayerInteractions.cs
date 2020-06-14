using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private float distance = 2f;
    [SerializeField] private GameObject arms;
    [SerializeField] LayerMask ignoreLayer;
    [SerializeField] private GameObject armsGun;
    private bool needToReJoint = false;
    private bool isLetter = false;
    private Transform grabedObject;
    void Update()
    {
        if (PauseMenu.paused == false)
        {
            if (isLetter)
            {
                HandleLetters();
            }
            else
            {
                HandleInteractable();
            }
        }
        
    }
    void InteractWithObject()
    {
        RaycastHit hit;
        Debug.DrawRay(camera.transform.position, camera.transform.forward * distance, Color.red);
        if(Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, distance, ~ignoreLayer))
        {
           
            
            if(hit.transform.tag == "CanInteract")
            { 
                grabedObject = hit.transform;
                arms.GetComponent<FixedJoint>().connectedBody = hit.transform.GetComponent<Rigidbody>();
                isLetter = false;
            }
            if(hit.transform.tag == "Letter")
            {
                grabedObject = hit.transform;
                grabedObject.GetComponent<LettersScript>().ShowLetter();
                //grabedObject.gameObject.SetActive(false);
                isLetter = true;
            }
            if(hit.transform.tag == "Key")
            {
                grabedObject = hit.transform;
                grabedObject.GetComponent<KeyScripts>().Destroy();
            }
        }
    }
    public void HandleLetters()
    {
      
        if (Input.GetKeyDown(KeyCode.Escape))
        { 
            grabedObject.GetComponent<LettersScript>().HideLetter();
            grabedObject.GetComponent<LettersScript>().Destroy();
            isLetter = false;
            
        }
    }
    public void HandleInteractable()
    {
        if (!needToReJoint)
        {

            if ((Input.GetKey(KeyCode.Mouse0)) && (!this.ArmsGun()))
            {
                InteractWithObject();
            }
            else
            {
                if (grabedObject != null)
                {
                    arms.GetComponent<FixedJoint>().connectedBody = null;
                }
            }
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
                needToReJoint = false;
        }
    }
    private bool ArmsGun()
    {
        int nr=0;
        foreach(Transform child in armsGun.transform)
        {
            nr++;
        }
        if (nr == 0)
            return false;
        return true;
    }
    public void SetReJoint(bool x)
    {
        this.needToReJoint = x;
    }

}
