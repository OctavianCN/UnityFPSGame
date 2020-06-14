using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private Ui_Inventory uiInventory;
    [SerializeField] private Image playerTarget;
    public GameObject playerArms;
    public GameObject playerInventoryObject;
    private Inventory inventory;
    private bool inventoryActive;
    private int maxItems = 15;
    public static bool inInventory=false;

    void Start()
    {
        if (inventory == null)
        {
            inventory = new Inventory();
            uiInventory.SetInventory(inventory);
            inventoryActive = false;
            uiInventory.gameObject.SetActive(false);
        }
       
    }
    void Update()
    {
        if(PauseMenu.paused==false)
            this.InventoryHandle();
    }
    public Inventory GetInventory()
    {
        return inventory;
    }
    public void InventoryHandle()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            
            if (inventoryActive == false)
            {
                inventoryActive = true;
                uiInventory.gameObject.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f;
                PlayerInventory.inInventory = true;
            }
            else
            {
                inventoryActive = false;
                uiInventory.gameObject.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1f;
                PlayerInventory.inInventory = false;

            }
        }
    }
    public bool AddItemToInventory(Item item)
    {
        bool success = false;
        if (inventory != null)
        {
            if (this.inventory.GetItemList().Count < this.maxItems)
            {
                this.inventory.AddItem(item);
                this.uiInventory.SetInventory(inventory);
                success = true;
            }
        }
        else
        {
            
            inventory = new Inventory();
            uiInventory.SetInventory(inventory);
            inventoryActive = false;
            Debug.Log("0");
            uiInventory.gameObject.SetActive(false);
            this.inventory.AddItem(item);
            Debug.Log("1");
            this.uiInventory.SetInventory(inventory);
            Debug.Log("2");
            success = true;
        }
        return success;
    }
    public void DropItem() // se apeleaza in inventar cand apesi pe butonul de drop
    {
        int itemId = EventSystem.current.currentSelectedGameObject.transform.parent.GetComponentInChildren<ItemId>().GetItemId();
        GameObject pickups = GameObject.Find("Pickups");
        Transform arm = this.transform.Find("Main Camera").transform.Find("Arms");
        Transform invent = this.transform.Find("Inventory");
        Transform posDrop = this.transform.Find("DropPosition");
        bool isDropped = false;
        float dropForce = 10f;
        foreach (Transform item in invent)
        {
            if (item.GetComponent<Pickup>().GetId() == itemId)
            {
                item.position = posDrop.position;
                item.rotation = posDrop.rotation;
                item.transform.parent = pickups.transform;
                item.GetComponent<Pickup>().isEquipped = false;
                item.GetComponent<Pickup>().isPicked = false;
                item.gameObject.SetActive(true);
                inventory.RemoveItem(item.GetComponent<Pickup>().GetId());
                uiInventory.SetInventory(inventory);
                isDropped = true;
            }
        }
        if (isDropped == false)
        {
            foreach (Transform item in arm)
            {
                if (item.GetComponent<Pickup>().GetId() == itemId)
                {
                    if (item.GetComponent<Pickup>().IsWeapon())
                    {
                        item.GetComponent<GunScript>().SetReloading(false);
                        item.GetComponent<PlayerGunScript>().EnableAmmoDisplay(false);
                        item.GetComponent<PlayerGunScript>().EnableCrosshair(false);
                        playerTarget.gameObject.SetActive(true);
                    }
                    /*item.position = posDrop.position;*/
                    item.rotation = posDrop.rotation;
                    item.transform.parent = pickups.transform;
                    item.GetComponent<Pickup>().isEquipped = false;
                    item.GetComponent<Pickup>().isPicked = false;
                    item.GetComponent<Rigidbody>().isKinematic = false;
                    item.GetComponent<Rigidbody>().AddForce(dropForce * this.transform.Find("Main Camera").transform.forward);
                    inventory.RemoveItem(item.GetComponent<Pickup>().GetId());
                    uiInventory.SetInventory(inventory);
                    isDropped = true;
                }
            }
            
        }
    }
    public void ChooseItem() //se apeleaza in inventar cand apesi pe buton de select
    {
        int itemId = EventSystem.current.currentSelectedGameObject.transform.parent.GetComponentInChildren<ItemId>().GetItemId();
        Transform invent = this.transform.Find("Inventory");
        Transform arm = this.transform.Find("Main Camera").transform.Find("Arms");
        bool selectedIsWeapon = false;
        foreach (Transform item in invent)
        {
            if (item.GetComponent<Pickup>().GetId() == itemId)
            {
                if (item.GetComponent<Pickup>().IsWeapon())
                {
                    selectedIsWeapon = true;
                }
            }
        }
        if (selectedIsWeapon)
        {
            foreach (Transform item in arm)
            {

                if (item.GetComponent<Pickup>().IsWeapon())
                {
                    item.GetComponent<GunScript>().SetReloading(false);
                    item.GetComponent<PlayerGunScript>().EnableAmmoDisplay(false);
                    item.GetComponent<PlayerGunScript>().EnableCrosshair(false);
                    item.GetComponent<Rigidbody>().isKinematic = false;
                }
                item.gameObject.SetActive(false);
                item.GetComponent<Pickup>().isEquipped = false;
                item.transform.parent = invent;
            }
        }
        foreach (Transform item in invent)
        {
            if (item.GetComponent<Pickup>().GetId() == itemId)
            {
                if (item.GetComponent<Pickup>().IsWeapon())
                {
                    item.transform.parent = arm;
                    item.transform.localPosition = item.GetComponent<PlayerGunScript>().GetPickupPositions();
                    item.transform.localEulerAngles = item.GetComponent<PlayerGunScript>().GetPickupRotations();
                    item.GetComponent<Rigidbody>().isKinematic = true;
                    item.GetComponent<Pickup>().isEquipped = true;
                    item.gameObject.SetActive(true);
                    playerTarget.gameObject.SetActive(false);
                    item.GetComponent<PlayerGunScript>().EnableCrosshair(true);
                    item.GetComponent<PlayerGunScript>().EnableAmmoDisplay(true);
                }
                else
                {
                  playerTarget.gameObject.SetActive(true);
                  if(item.GetComponent<Pickup>().GetItemType() == Item.ItemType.medKit)
                  {

                        if (this.GetComponentInParent<PlayerHealth>().GetPlayerHealth() < this.GetComponentInParent<PlayerHealth>().GetMaxHealth())
                        {
                            this.GetComponentInParent<PlayerHealth>().SetPlayerHealth(item.GetComponent<MedKit>().GiveHealth(this.GetComponentInParent<PlayerHealth>().GetPlayerHealth(), this.GetComponentInParent<PlayerHealth>().GetMaxHealth()));
                            
                        }
                  }
                  bool succes = false;
                  switch (item.GetComponent<Pickup>().GetItemType())
                  {
                        default:
                        case Item.ItemType.medKit:
                            if (this.GetComponentInParent<PlayerHealth>().GetPlayerHealth() < this.GetComponentInParent<PlayerHealth>().GetMaxHealth())
                            {
                                this.GetComponentInParent<PlayerHealth>().SetPlayerHealth(item.GetComponent<MedKit>().GiveHealth(this.GetComponentInParent<PlayerHealth>().GetPlayerHealth(), this.GetComponentInParent<PlayerHealth>().GetMaxHealth()));
                                succes = true;
                            }
                            break;
                        case Item.ItemType.pistolBullets:
                            if (arm.GetComponentInChildren<Pickup>()!= null)
                            {
                                if (arm.GetComponentInChildren<Pickup>().GetItemType() == Item.ItemType.pistol)
                                {
                                    arm.GetComponentInChildren<GunScript>().AddAmmo(item.GetComponent<AmmoScript>().GiveAmmo());
                                    succes = true;
                                }
                            }
                            break;
                        case Item.ItemType.m4Bullets:
                            if (arm.GetComponentInChildren<Pickup>() != null)
                            {
                                   if (arm.GetComponentInChildren<Pickup>().GetItemType() == Item.ItemType.m4)
                                    {
                                    arm.GetComponentInChildren<GunScript>().AddAmmo(item.GetComponent<AmmoScript>().GiveAmmo());
                                    succes = true;

                                    }
                            }
                            break;

                    }
                    if (succes==true)
                    {
                        inventory.RemoveItem(item.GetComponent<Pickup>().GetId());
                        uiInventory.SetInventory(inventory);
                        Destroy(item.gameObject);
                    }

                }
            }

        }
    }
    public int GetMaxItems()
    {
        return maxItems;
    }
}
