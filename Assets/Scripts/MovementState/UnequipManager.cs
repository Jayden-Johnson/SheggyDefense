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
    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        gun.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.E))
        {
            if (equiped){
            equiped = false;
            rigBuilder.enabled = false; 
            anim.SetLayerWeight(layerIndex, 0f);
            gun.SetActive(false);
            anim.SetBool("CanEmote",true);
            anim.SetBool("Emote",false);
            }
            else{
            equiped = true;
            rigBuilder.enabled = true;
            anim.SetLayerWeight(layerIndex, 1f);
            gun.SetActive(true);
            anim.SetBool("CanEmote",false);
            anim.SetBool("Emote",false);
            }
        }

    
    }
}
