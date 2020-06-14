using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DoorScripts : MonoBehaviour
{ 
    [SerializeField] private bool locked = true;
    [SerializeField] private GameObject key;
    [SerializeField] private GameObject doorUI;
    [SerializeField] private Animator doorAnim;
    private GameObject doorMessageUI;
    private GameObject doorStateUI;
    private float textStateTime = 2.0f;
    void Awake()
    {
        foreach(Transform child in doorUI.transform)
        {
            if(child.name == "DoorMessageUI")
            {
                doorMessageUI = child.gameObject;
            }
            if(child.name == "DoorState")
            {
                doorStateUI = child.gameObject;
            }
        }
    }
    public GameObject getKey()
    {
        return key;
    }
    public void UnlockDoor()
    {
        locked = false;   
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            doorMessageUI.SetActive(true);
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if((other.transform.tag == "Player") && (Input.GetKeyDown(KeyCode.E)))
        {
            StartCoroutine(this.DoorStateText());
           
        }
    }
    private IEnumerator DoorStateText()
    {
        doorStateUI.SetActive(true);
        doorMessageUI.SetActive(false);
        if (locked == true)
        {
            string doorLocked = "Locked";
            doorStateUI.GetComponent<Text>().text = doorLocked;
        }
        else
        {
            string doorUnlocked = "Unlocked";
            doorStateUI.GetComponent<Text>().text = doorUnlocked;
            doorAnim.SetTrigger("Unlocked");
        }
        yield return new WaitForSeconds(textStateTime);
        doorStateUI.SetActive(false);
        if(this.locked == false)
            this.GetComponent<Collider>().enabled = false;
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            doorMessageUI.SetActive(false);
        }
    }
    public Animator getAnimator()
    {
        return doorAnim;
    }
    public bool getLocked()
    {
        return this.locked;
    }
    
}
