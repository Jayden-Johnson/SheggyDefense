using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoTextManager : MonoBehaviour
{
    public WeaponClassManager weaponClassManager;
    public UnequipManager unequipManager;

    public GameObject weaponIconObj;
    public Text ammoTextObj;

    public Sprite M4Icon;
    public Sprite pistolIcon;
    public GameObject pistol;
    public GameObject m4;


    void Start() 
    {

    }

    void Update()
    {
        if(pistol.activeInHierarchy == true) 
        {
            switchIcon(pistolIcon);
        } 
        if(m4.activeInHierarchy == true) 
        {
            switchIcon(M4Icon);
        }
        if(!unequipManager.equiped) 
        {
            ammoTextObj.text = " ";
            weaponIconObj.SetActive(false);
        }
    }

    void switchIcon(Sprite weaponIcon) {
        weaponIconObj.gameObject.GetComponent<Image>().sprite = weaponIcon;
        weaponIconObj.SetActive(true);
    }

    public static AmmoTextManager instance;

    private void Awake()
    {
        instance = this;
    }
}
