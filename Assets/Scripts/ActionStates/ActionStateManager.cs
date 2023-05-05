using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ActionStateManager : MonoBehaviour
{
   [HideInInspector]public ActionBaseState currentState;

    public ReloadState Reload = new ReloadState();
    
    public DefaultState Default = new DefaultState();

    public SwapState Swap = new SwapState();

    public EquipState Equip = new EquipState();

    public UnequipState Unequip = new UnequipState();


    [HideInInspector]public WeaponManager currentWeapon;
    [HideInInspector]public WeaponAmmo ammo;
    AudioSource audioSource;

    [HideInInspector] public Animator anim;

    public MultiAimConstraint rHandAim;
    public TwoBoneIKConstraint lHandIK;

    // Start is called before the first frame update
    void Start()
    {
        SwitchState(Default);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
        if(Input.GetKeyDown(KeyCode.E)){
        if (UnequipManager.instance.equiped == true){
            SwitchState(Unequip);
        }
        if (UnequipManager.instance.equiped == false){
            anim.SetLayerWeight(UnequipManager.instance.layerIndex, 1f);
            SwitchState(Equip);
        }
        }
    }
    public void SwitchState(ActionBaseState state){
        currentState = state;
        currentState.EnterState(this);
    }
    public void WeaponReloaded(){
        ammo.Reload();
        rHandAim.weight = 1;
        lHandIK.weight = 1;
        SwitchState(Default);
    }

    public void ReloadSound(){
        audioSource.PlayOneShot(ammo.Reloading);
    }
    
    public void SetWeapon(WeaponManager weapon){
        currentWeapon = weapon;
        audioSource = weapon.audioSource;
        ammo = weapon.ammo;
    }
}
