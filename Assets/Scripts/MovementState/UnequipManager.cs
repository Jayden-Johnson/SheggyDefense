using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Animations;


public class UnequipManager : MonoBehaviour
{
    public Animator anim; 
    public int layerIndex;
    public RigBuilder rigBuilder;
    public bool equiped = true;
    public GameObject gun;

    public bool equippedBeforeEmote;

    // Start is called before the first frame update
    void Start()
    {
        gun.SetActive(true);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
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
        gun.SetActive(true);
        equiped = true;
        anim.SetBool("CanEmote",false);
        anim.SetBool("Emote",false);
    }

    public void unEquip() 
    {
        rigBuilder.enabled = false; 
        anim.SetLayerWeight(layerIndex, 0f);
        gun.SetActive(false);
        equiped = false;
        anim.SetBool("CanEmote", true);
        anim.SetBool("Emote",false);
    }
    
    //this makes it possible to reference unequipmanager in other scripts
    public static UnequipManager instance;

    private void Awake()
    {
        instance = this;
    }
    
}
