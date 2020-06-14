using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Pickup : MonoBehaviour
{
    [SerializeField] private Item.ItemType type;
    [SerializeField] private Text pickupText;
    private static int setId;
    [SerializeField]private int id = 0;
    public bool isPicked = false;
    public bool isEquipped = false;

    void Awake()
    {
        setId = 0;
    }
    void Start()
    {
        //id = setId;
        setId++;
    }
    void LateUpdate()
    {
        if (isPicked)
        {
            pickupText.gameObject.SetActive(false);
        }
    }
    public Item.ItemType GetItemType()
    {
        return type;
    }
    
    public bool IsWeapon()
    {
        if (type == Item.ItemType.pistol || type == Item.ItemType.m4)
            return true;
        else
            return false;
    }

    public int GetId()
    {
        return id;
    }
}
