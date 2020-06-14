using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentOpenScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> objects;
    [SerializeField] private Animator anim;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject pickup;
    private bool opened = false;

    void OnTriggerStay(Collider other)
    {
        
        if((other.gameObject.tag == "Player") && (Input.GetKeyDown(KeyCode.Mouse0)))
        {
            RaycastHit hit;
          
            if (Physics.Raycast(cam.transform.position,cam.transform.forward,out hit))
            {
               
                if(hit.transform.tag == "OpenDoor")
                {
                    if(this.opened == false)
                    {
                        this.Open();
                    }
                    else
                    {
                        this.Closed();
                    }
                }
            }
        }
    }
    private void Open()
    {
        this.opened = true;
        anim.SetTrigger("Open"); 
    }
    private void Closed()
    {
        this.opened = false;
        anim.SetTrigger("Close");
    }
    public void ShowObjects()
    {
        if (objects != null)
        {
            if (objects.Count != 0)
            {
                foreach (GameObject obj in objects)
                {
                    if (obj.GetComponent<Rigidbody>() != null)
                    {
                        obj.GetComponent<Rigidbody>().isKinematic = true;
                    }
                    obj.SetActive(true);
                }
            }
        }
    }
    public void HideObject()
    {
        if (objects != null)
        {
            if (objects.Count != 0)
            {
                foreach (GameObject obj in objects)
                {
                    if (obj.transform.parent != pickup.transform)
                    {
                        objects.Remove(obj);
                        if (obj.GetComponent<Rigidbody>() != null)
                        {
                            obj.GetComponent<Rigidbody>().isKinematic = false;
                        }
                    }
                    else
                    {
                        obj.SetActive(false);
                    }
                }
            }
        }
    }
}
