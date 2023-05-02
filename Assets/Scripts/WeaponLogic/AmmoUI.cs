using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    public int maxAmmo = 100;
    public int currentAmmo = 20;
    public int mag = 80;
    public float fireRate = 0.5f;
    public float reloadTime = 1.5f;
    public Text ammoText;

    private bool canFire = true;

    void Start()  
    {
        Cursor.lockState = CursorLockMode.Locked; 
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) 
        {
            Fire();
        }

        if (Input.GetKeyDown(KeyCode.R)) 
        {
            Reload();
        }

        UpdateAmmoDisplay();
    }

    public void Fire()
    {
        if (currentAmmo > 0 && canFire)
        {
            currentAmmo--;
            canFire = false;
            Invoke("ResetCanFire", fireRate);
        }
    }

    public void Reload()
    {
        if (currentAmmo < maxAmmo)
        {
            currentAmmo += mag;
            maxAmmo -= mag;
            Invoke("ResetCanFire", reloadTime);
        }
    }

    private void ResetCanFire()
    {
        canFire = true;
    }

    void UpdateAmmoDisplay()
    {
        if (ammoText != null)
        {
            ammoText.text = "Ammo: " + currentAmmo + "/" + maxAmmo;
        }
    }
}
