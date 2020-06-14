using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGunScript : GunScript
{
    [SerializeField] private GameObject crosshair;
    [SerializeField] private Text ammoDisplay;
    [SerializeField] private Vector3 pickUpPositions;
    [SerializeField] private Vector3 pickUpRotations;
    [SerializeField] private Camera cam;
    // Start is called before the first frame update

    void Update()
    {
        WeaponDisplay();
        if (isReloading)
        {
            return;
        }
        if (((bulletsLoader <= 0) || (Input.GetKeyDown(KeyCode.R))) && (bulletsStorage > 0) && (bulletsLoader < maxBulletsLoader))
        {
            anim.SetTrigger("Reload");
            StartCoroutine(this.Reload());
            return;
        }
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            if ((this.GetComponent<Pickup>().isPicked) && (bulletsLoader > 0))
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                this.Shoot();
            }
        }
    }
    void WeaponDisplay()
    {
        if (this.GetComponentInParent<Pickup>().isEquipped)
        {
            this.GetComponentInParent<Animator>().SetBool("Picked", true);
            this.GetComponent<Light>().enabled = false;
            ammoDisplay.gameObject.SetActive(true);
            ammoDisplay.text = this.bulletsLoader.ToString() + "/" + this.bulletsStorage.ToString();
        }
      
    }
    protected new void Shoot()
    {
        RaycastHit hit;
        muzzleFlash.Play();
        gunfireSound.Play();
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Debug.Log(hit.collider.gameObject.tag);
            if (hit.collider.gameObject.tag == "Enemy")
                hit.collider.gameObject.GetComponent<EnemyGuard>().GetDamage(damage);
        }
        if (anim != null)
            anim.SetTrigger("Fire");
        bulletsLoader--;
    }
    public void EnableCrosshair(bool value)
    {
        crosshair.SetActive(value);
    }
    public void EnableAmmoDisplay(bool value)
    {
        ammoDisplay.gameObject.SetActive(value);
    }
    public Vector3 GetPickupPositions()
    {
        return pickUpPositions;
    }
    public Vector3 GetPickupRotations()
    {
        return pickUpRotations;
    }
}
