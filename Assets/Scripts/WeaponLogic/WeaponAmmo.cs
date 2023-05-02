using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponAmmo : MonoBehaviour
{
    public Text ammoText;
    public int clipSize;
    public int extraAmmo;

    
    public AudioClip Reloading;
    
    public int currentAmmo;


       
    void Start(){
        currentAmmo = clipSize;
    }    
    
    public void Reload()
    {
        if (extraAmmo >= clipSize)
        {
            int ammoToReload = clipSize - currentAmmo;
            extraAmmo -= ammoToReload;
            currentAmmo += ammoToReload;
        }
        else if (extraAmmo > 0)
        {
            if(extraAmmo+currentAmmo>clipSize)
            {
                int leftOverAmmo = extraAmmo+currentAmmo-clipSize;
                extraAmmo = leftOverAmmo;
                currentAmmo = clipSize;
            }
            else
            {
                currentAmmo += extraAmmo;
                extraAmmo = 0;
            }
        }
    }

}
