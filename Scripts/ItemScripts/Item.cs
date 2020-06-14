using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        pistol, 
        m4,
        medKit, 
        pistolBullets,
        m4Bullets
    }
    public ItemType itemType;
    private int id;
    public Sprite GetSprite()
    {
       
        switch (itemType)
        {
            default:
            case ItemType.pistol: return ItemAssets.Instance.pistolSprite;
            case ItemType.m4: return ItemAssets.Instance.m4Sprite;
            case ItemType.medKit:return ItemAssets.Instance.medKitSprite;
            case ItemType.pistolBullets:return ItemAssets.Instance.pistolBulletSprite;
            case ItemType.m4Bullets:return ItemAssets.Instance.m4BulletSprite;
        }
    }
    public void SetId(int id)
    {
        this.id = id;
    }
    public int GetId()
    {
        return this.id;
    }

  
}
