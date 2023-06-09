using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class UnequipManager : MonoBehaviour
{
    public bool equippedBeforeEmote;
    public Animator anim;
    public int layerIndex;
    public int layerIndex2;
    public bool equiped = true;
    public RigBuilder rigBuilder;
    public GameObject myGameObject;
    private WeaponClassManager weaponClassManager;

    public MultiAimConstraint rHandAim;
    public MultiAimConstraint bodyAim;

    void Start()
    {
        equiped = false;
        anim = GetComponent<Animator>();
        weaponClassManager = GetComponent<WeaponClassManager>();
        var rigBuilder = myGameObject.GetComponent< RigBuilder >();
        if (equiped != true){
            unEquip();
        }
    }

    void Update()
    {
    }

    public void equip()
    {
        weaponClassManager.weapons[weaponClassManager.currentWeaponIndex].gameObject.SetActive(true);
        rHandAim.weight = 1f;
        bodyAim.weight = 1f;
        anim.SetLayerWeight(layerIndex, 1f);
        anim.SetLayerWeight(layerIndex2, 0f);
        equiped = true;
        rigBuilder.layers[0].active = true;
        anim.SetBool("EmoteBool",false); 
        anim.SetBool("Emote",false);
    }

    public void unEquip()
    {
        if (weaponClassManager.currentWeaponIndex >= 0 && equiped == true){
            weaponClassManager.weapons[weaponClassManager.currentWeaponIndex].gameObject.SetActive(false);
        }
        rHandAim.weight = 0f;
        bodyAim.weight = 0f;
        anim.SetLayerWeight(layerIndex, 0f);
        anim.SetLayerWeight(layerIndex2, 1f);
        equiped = false;
        rigBuilder.layers[0].active = false; 
        anim.SetBool("EmoteBool",true);
        anim.SetBool("Emote",false);
    }

    public static UnequipManager instance;

    public void Awake()
    {
        instance = this;
    }
}
