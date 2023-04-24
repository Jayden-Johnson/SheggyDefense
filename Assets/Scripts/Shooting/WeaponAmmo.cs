using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponAmmo : MonoBehaviour
{
    public int clipSize;
    public int extraAmmo;
    public AudioClip Reloading;

    public Text ammoDisplay;
    
    [HideInInspector] public int currentAmmo;
    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = clipSize;
        UpdateammoDisplay();
    }

    public void Reload(){
        if (extraAmmo >= clipSize){
            int ammoToReload = clipSize - currentAmmo;
            extraAmmo -= ammoToReload;
            currentAmmo+= ammoToReload;
        }
        else if (extraAmmo > 0){
            if(extraAmmo+currentAmmo>clipSize){
                int leftOverAmmo = extraAmmo+currentAmmo-clipSize;
                extraAmmo = leftOverAmmo;
                currentAmmo = clipSize;
            }
            else{
                currentAmmo += extraAmmo;
                extraAmmo = 0;
            }
        }
        
    }
    private void UpdateammoDisplay()
    {
        ammoDisplay.text = string.Format("{0}/{1}", currentAmmo, extraAmmo);
    }
}
