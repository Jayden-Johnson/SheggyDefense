using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class UnequipManager : MonoBehaviour
{
    public bool equippedBeforeEmote;
    public Animator anim;
    public int layerIndex;
    public RigBuilder rigBuilder;
    public bool equiped = true;
    public List<GameObject> guns;

    private GameObject lastEnabledGun; // add this variable to keep track of the last enabled gun
    private WeaponClassManager weaponClassManager;

    void Start()
    {
        anim = GetComponent<Animator>();
        weaponClassManager = GetComponent<WeaponClassManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (equiped)
            {
                unEquip();
            }
            else
            {
                equip();
            }
        }
    }

    public void equip()
    {
        rigBuilder.enabled = true;
        anim.SetLayerWeight(layerIndex, 1f);

        if (lastEnabledGun != null) // enable the last enabled gun if it exists
        {
            lastEnabledGun.SetActive(true);
        }
        else // otherwise, enable the first gun in the list
        {
            guns[0].SetActive(true);
            lastEnabledGun = guns[0];
        }

        equiped = true;
        anim.SetBool("CanEmote", false);
        anim.SetBool("Emote", false);
        weaponClassManager.enabled = false;
    }

    public void unEquip()
    {
        rigBuilder.enabled = false;
        anim.SetLayerWeight(layerIndex, 0f);
        foreach (GameObject gun in guns)
        {
            gun.SetActive(false);
        }
        equiped = false;
        anim.SetBool("CanEmote", true);
        anim.SetBool("Emote", false);
        weaponClassManager.enabled = true;
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
