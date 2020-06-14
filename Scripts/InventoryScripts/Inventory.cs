using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<Item> itemList; 

    public Inventory()
    {
        itemList = new List<Item>();

    }
    
    public void AddItem(Item i)
    {
        itemList.Add(i);
    }
    public List<Item> GetItemList()
    {
        return itemList;
    }
    public void RemoveItem(int id)
    {
        
        foreach(Item item in itemList)
        {
            
            if(item.GetId() == id)
            {
                itemList.Remove(item);
                break;
            }
        }
    }
}
