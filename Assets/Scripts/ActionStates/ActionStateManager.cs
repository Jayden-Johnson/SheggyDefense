using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.SceneManagement;

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
    UnequipManager unequipManager;
    Movement movement;
        
    public Animator anim;
    public Animator animPlayer;

    public MultiAimConstraint rHandAim;
    public TwoBoneIKConstraint lHandIK;

    public bool playerDead = false;
    public PlayerHealth PlayerHealth;

    public GameObject gameOverScreen;
    public GameObject gameUI;

    public AimStateManager aimStateManager;
    public float meleeDamage = 50;
    public GameObject fist;

    public static ActionStateManager instance;

    void Awake(){
        instance = this;
    }
    void Start()
    {
        unequipManager = GetComponent<UnequipManager>();
        movement = GetComponent<Movement>();
        SwitchState(Default);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
        if (Input.GetKeyDown(KeyCode.Mouse0)&& !aimStateManager.lookingAtShop && InputManger.instance.canInput){
            anim.SetTrigger("Punch");
        }
        if(Input.GetKeyDown(KeyCode.E) && unequipManager.enabled == true && !aimStateManager.lookingAtShop && InputManger.instance.canInput){
            if (UnequipManager.instance.equiped == true){
                SwitchState(Unequip);
            }
            if (UnequipManager.instance.equiped == false && !aimStateManager.lookingAtShop && WeaponClassManager.instance.weapons.Length > 0){
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
    public void PlayerDeath(){
        if(playerDead == false) 
        {
            InputManger.instance.canInput = false;
            UnequipManager.instance.unEquip();
            unequipManager.enabled = false;
            movement.enabled = false;
            animPlayer.SetTrigger("Death");

            playerDead = true;
            PlayerHealth.playerHealth = 0;
            Debug.Log("player dead");
               
            gameOverScreen.SetActive(true);
            gameUI.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
        }
    }
    
    public void Respawn() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}


