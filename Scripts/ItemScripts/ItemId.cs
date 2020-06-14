using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemId : MonoBehaviour
{
    private int itemId;

    public void SetItemId(int id)
    {
        itemId = id;
    }
    public int GetItemId()
    {
        return itemId;
    }
   
}
