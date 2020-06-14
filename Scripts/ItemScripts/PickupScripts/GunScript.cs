using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunScript : MonoBehaviour
{
    [SerializeField] protected float damage;
    [SerializeField] protected float range;
    [SerializeField] protected Animator anim;
    [SerializeField] protected GameObject impactEffect;
    [SerializeField] protected AudioSource gunfireSound;
    [SerializeField] protected float fireRate;
    [SerializeField] protected int maxBulletsLoader;
    [SerializeField] protected float reloadTime;
    [SerializeField] protected ParticleSystem muzzleFlash;
    [SerializeField] protected int bulletsLoader;
    [SerializeField] protected int bulletsStorage;
    [SerializeField] private Gun gun;
    protected float nextTimeToFire = 0f;
    protected bool isReloading =false;
    void Start()
    {
        damage = gun.damage;
        range = gun.range;
        fireRate = gun.fireRate;
        maxBulletsLoader = gun.maxBulletsLoader;
        reloadTime = gun.reloadTime;
        bulletsLoader = gun.bulletsLoader;
        bulletsStorage = gun.bulletsStorage;
    }
    void Update()
    {
        if (isReloading)
        {
            return;
        }
        if ((bulletsLoader <= 0) && (bulletsStorage > 0)&&(bulletsLoader<maxBulletsLoader))
        {
            if(anim !=null)
                anim.SetTrigger("Reload");
            StartCoroutine(this.Reload());
            return;
        }    
    }

    protected IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        if (bulletsLoader > 0)
        {
           
            bulletsStorage -= (maxBulletsLoader - bulletsLoader);
            bulletsLoader = maxBulletsLoader;
            if (bulletsStorage < 0)
            {
                bulletsLoader += bulletsStorage;
                bulletsStorage = 0;
            }
            
            
        }
        else
        {
            if (bulletsStorage >= maxBulletsLoader)
            {
                bulletsStorage -= maxBulletsLoader;
                bulletsLoader = maxBulletsLoader;

            }
            else
            {
                bulletsLoader = bulletsStorage;
                bulletsStorage = 0;

            }
        }
        isReloading = false;
           
        

    }

    public void Shoot()
    {
        RaycastHit hit;
        muzzleFlash.Play();
        gunfireSound.Play();
        bool hitted = Physics.Raycast(this.transform.position, this.transform.forward, out hit, range);
        if (hitted)
        {
            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
        if (anim != null)
            anim.SetTrigger("Fire");
        bulletsLoader--;
    }
    public int getBulletsLoader()
    {
        return bulletsLoader;
    }
    public float getNextTimeToFire()
    {
        return nextTimeToFire;
    }
    public void setNextTimeToFire(float time)
    {
        nextTimeToFire = time;
    }
    public void AddAmmo(int amount)
    {
        bulletsStorage += amount; 
    }
    
    public void SetReloading(bool value)
    {
        isReloading = value;
    }
   
}
