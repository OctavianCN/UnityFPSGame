using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Ui_Inventory : MonoBehaviour
{
    private Inventory inventory;
    [SerializeField] private Transform itemSlotContainer;
    [SerializeField] private Transform itemSlotTemplate;
    public void SetInventory(Inventory i)
    {
        
        foreach (Transform child in itemSlotContainer)
        {
            if (child != itemSlotTemplate)
            {
                Destroy(child.gameObject);
            }
        }
        
        this.inventory = new Inventory();
        foreach (Item item in i.GetItemList())
            this.inventory.AddItem(item);
        this.RefreshInventoryItems();
       
    }
    private void RefreshInventoryItems()
    {
        int x = 0;
        int y = 0;
        float itemSlotCellSizeHeight = 245f;
        float itemSlotCellSizeWidth = 280;
        foreach(Transform child in itemSlotContainer)
        {
            if(child != itemSlotTemplate)
            {
                Destroy(child.gameObject);
            }
        }
      
        foreach (Item item in inventory.GetItemList())
        {
            
            RectTransform itemSlotRect = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRect.gameObject.SetActive(true);
            itemSlotRect.localPosition = new Vector2(x * itemSlotCellSizeWidth, -y * itemSlotCellSizeHeight);
            Image image = itemSlotRect.Find("PickupImage").GetComponent<Image>();
            if (item.GetSprite() != null)
            {
                image.sprite = item.GetSprite();
                itemSlotRect.Find("PickupImage").GetComponent<ItemId>().SetItemId(item.GetId());
            }
            x++;
            
            if (x > 4)
            {
                x = 0;
                y++;
            }
        }
      
    }
}
