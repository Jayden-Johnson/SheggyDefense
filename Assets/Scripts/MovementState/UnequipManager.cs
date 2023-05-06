using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class UnequipManager : MonoBehaviour
{
    public bool equippedBeforeEmote;
    public Animator anim;
    public int layerIndex;
    public bool equiped = true;
    public List<GameObject> guns;
    public RigBuilder rigBuilder;
    private GameObject lastEnabledGun; 
    public GameObject myGameObject;
    private WeaponClassManager weaponClassManager;

    public MultiAimConstraint rHandAim;
    public MultiAimConstraint bodyAim;
    void Start()
    {
        equiped = true;
        anim = GetComponent<Animator>();
        weaponClassManager = GetComponent<WeaponClassManager>();
        var rigBuilder = myGameObject.GetComponent< RigBuilder >();
    }

    void Update()
    {
        
    }

    public void equip()
    {
        rHandAim.weight = 1f;
        bodyAim.weight = 1f;
        anim.SetLayerWeight(layerIndex, 1f);
        if (lastEnabledGun != null)
        {
            lastEnabledGun.SetActive(true);
        }
        else
        {
            guns[0].SetActive(true);
            lastEnabledGun = guns[0];
        }

        equiped = true;
        weaponClassManager.enabled = true;
        rigBuilder.layers[0].active = true;
        anim.SetBool("EmoteBool",false); 
        anim.SetBool("Emote",false);
    }

    public void unEquip()
    {
        rHandAim.weight = 0f;
        bodyAim.weight = 0f;
        anim.SetLayerWeight(layerIndex, 0f);
        foreach (GameObject gun in guns)
        {
            gun.SetActive(false);
        }
        equiped = false;
        weaponClassManager.enabled = false;
        rigBuilder.layers[0].active = false; 
        anim.SetBool("EmoteBool",true);
        anim.SetBool("Emote",false);
    }

    public static UnequipManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void SetLastEnabledGun(GameObject gun) // add this method to set the last enabled gun
    {
        lastEnabledGun = gun;
    }
}
