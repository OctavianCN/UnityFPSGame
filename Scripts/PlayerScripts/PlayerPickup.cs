using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerPickup : MonoBehaviour
{
    [SerializeField] float distance = 2f;
    [SerializeField] LayerMask ignoreLayer;
    [SerializeField] GameObject pickupObject;
    [SerializeField] Text pickupText;
    [SerializeField] private Camera cam;
    RaycastHit hit;

    private Transform selected;

    void Update()
    {
       if(PauseMenu.paused == false)
        {
            pickupText.gameObject.SetActive(false);
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, distance, ~ignoreLayer))
            {
                pickupText.gameObject.SetActive(true);
                if ((hit.transform.tag == "Pickup") && (Input.GetKeyDown(KeyCode.E)) && (!hit.transform.GetComponent<Pickup>().isPicked))
                {
                    this.PickItem(hit.transform.gameObject);
                }
            }
        }
    }
    public void PickItem(GameObject hit)
    {
        pickupText.gameObject.SetActive(false);
        Item item = new Item();
        item.itemType = hit.GetComponent<Pickup>().GetItemType();
        item.SetId(hit.GetComponent<Pickup>().GetId());
        
        bool succes = this.GetComponent<PlayerInventory>().AddItemToInventory(item);
        
        if (succes)
        {
            hit.transform.parent = this.GetComponent<PlayerInventory>().playerInventoryObject.transform;
            if (hit.transform.gameObject.GetComponent<Pickup>().IsWeapon())
            {
                hit.transform.localPosition = hit.GetComponent<PlayerGunScript>().GetPickupPositions();
                hit.transform.localEulerAngles = hit.GetComponent<PlayerGunScript>().GetPickupRotations();
            }
            hit.GetComponent<Pickup>().isPicked = true;
            hit.SetActive(false);
        }
    }
}
