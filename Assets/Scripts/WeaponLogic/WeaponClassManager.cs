using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponClassManager : MonoBehaviour
{
    [SerializeField]TwoBoneIKConstraint leftHandIk;
    public Transform recoilFollowPos;
    ActionStateManager actions;

    public WeaponManager[] weapons;
    public int currentWeaponIndex;
    public GameObject weaponPrefab;
    void Start(){
        
    }
    private void Awake(){
        currentWeaponIndex = 0;
        for(int i=0; i< weapons.Length; i++){
            if (i == 0) weapons[i].gameObject.SetActive(true);
            else weapons[i].gameObject.SetActive(false);
        }
        instance = this;
    }

    public void SetCurrentWeapon(WeaponManager weapon){
        if(actions == null) actions = GetComponent<ActionStateManager>();
        leftHandIk.data.target = weapon.leftHandTarget;
        leftHandIk.data.hint = weapon.leftHandHint;
        actions.SetWeapon(weapon);
    }
    public void ChangeWeapon(float direction){
        weapons[currentWeaponIndex].gameObject.SetActive(false);
        if (direction<0){
            if(currentWeaponIndex == 0) currentWeaponIndex = weapons.Length - 1;
            else{
                currentWeaponIndex --;
            } 
        }
        else{
            if(currentWeaponIndex == weapons.Length - 1) currentWeaponIndex = 0;
            else currentWeaponIndex ++;
        }   
        weapons[currentWeaponIndex].gameObject.SetActive(true);
    }

    public void WeaponPutAway(){
        ChangeWeapon(actions.Default.scrollDirection);
    }

    public void WeaponPulledOut(){
        actions.SwitchState(actions.Default);
    }
    public WeaponManager CreateNewWeapon()
    {
        GameObject newWeaponObject = weaponPrefab;
        WeaponManager newWeapon = newWeaponObject.GetComponent<WeaponManager>();
        return newWeapon;
    }

    public void AddNewWeapon()
    {
        WeaponManager newWeapon = CreateNewWeapon();
        WeaponManager[] newWeaponsArray = new WeaponManager[weapons.Length + 1];
        for (int i = 0; i < weapons.Length; i++)
        {
            newWeaponsArray[i] = weapons[i];
        }
        newWeaponsArray[weapons.Length] = newWeapon;
        weapons = newWeaponsArray;
    }
    public static WeaponClassManager instance;
}
