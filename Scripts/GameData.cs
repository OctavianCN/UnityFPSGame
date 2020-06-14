using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class GameData
{
    public float health;
    public float[] positions;
    public List<int> items;
    public bool locked;
    
    public GameData(PlayerHealth ph, PlayerInventory pi,DoorScripts door)
    {
        items = new List<int>();
        health = ph.GetPlayerHealth();
        positions = new float[3];
        positions[0] = ph.transform.position.x;
        positions[1] = ph.transform.position.y;
        positions[2] = ph.transform.position.z;
        if (pi.GetInventory() != null)
        {
            Inventory inv = pi.GetInventory();
            if(inv.GetItemList()!=null)
            {
                List<Item> itemList = inv.GetItemList();
                foreach (Item item in itemList)
                {
                    items.Add(item.GetId());
                  
                }
            }
        }
        locked = door.getLocked();
     
    }
}

