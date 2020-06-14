using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Gun")]
public class Gun : ScriptableObject
{
    public float damage;
    public float range;
    public int bulletsLoader;
    public float fireRate;
    public int maxBulletsLoader;
    public float reloadTime;
    public int bulletsStorage;
}
