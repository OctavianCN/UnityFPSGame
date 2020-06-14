using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoScript : MonoBehaviour
{
    [SerializeField] private int amount;
    [SerializeField] private Ammo ammo;
    void Start()
    {
        amount = ammo.amount;
    }
    public int GiveAmmo()
    {
        return amount;
    }
}
