using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    //fire Rate
    [SerializeField]float fireRate;
    float fireRateTimer;
    [SerializeField] bool semiAuto;

    //bullet properties

    [SerializeField] GameObject bullet;
    [SerializeField] Transform barrelPos;
    [SerializeField] float bulletVelocity;
    [SerializeField] int bulletsPerShot;

    //Variables
    AimStateManager aim;
    ActionStateManager actions;
    WeaponRecoil recoil;
    [HideInInspector] public WeaponAmmo ammo;
    WeaponBloom bloom;
    
    //audio
    [SerializeField] AudioClip gunShot;
    [HideInInspector] public AudioSource audioSource;

    //muzzleflash
    Light muzzleFlashLight;
    ParticleSystem muzzleFlashParticles;
    float lightIntensity;
    [SerializeField] float lightReturnSpeed;

    //enemy related stuff
    public float enemyKickbackForce =100;
    public float damage = 20;
    //weaponswap
    public Transform leftHandTarget, leftHandHint;
    WeaponClassManager weaponClass;

    // Start is called before the first frame update
    void Start()
    {
        aim = GetComponentInParent<AimStateManager>();
        bloom = GetComponent<WeaponBloom>();
        muzzleFlashLight = GetComponentInChildren<Light>();
        muzzleFlashParticles = GetComponentInChildren<ParticleSystem>();
        lightIntensity = muzzleFlashLight.intensity;
        muzzleFlashLight.intensity = 0;
        fireRateTimer = fireRate;
        actions = GetComponentInParent<ActionStateManager>();
    }

    private void OnEnable(){
        if(weaponClass == null){
            weaponClass = GetComponentInParent<WeaponClassManager>();
            ammo = GetComponent<WeaponAmmo>();
            audioSource = GetComponent<AudioSource>();
            recoil = GetComponent<WeaponRecoil>(); 
            recoil.recoilFollowPos = weaponClass.recoilFollowPos;
        }
        weaponClass.SetCurrentWeapon(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(ShouldFire()) Fire();
        muzzleFlashLight.intensity = Mathf.Lerp(muzzleFlashLight.intensity,0,lightReturnSpeed * Time.deltaTime);
        ammo.ammoText.text = ammo.currentAmmo + "/" + ammo.extraAmmo;
    }
    bool ShouldFire(){
        fireRateTimer += Time.deltaTime;
        if (fireRateTimer < fireRate) return false;
        if(ammo.currentAmmo==0)return false;
        if(actions.currentState==actions.Reload) return false;
        if(actions.currentState == actions.Swap) return false;
        if(actions.currentState == actions.Equip) return false;
        if(actions.currentState == actions.Unequip) return false;
        if(semiAuto&&Input.GetKeyDown(KeyCode.Mouse0) && InputManger.instance.canInput) return true;
        if(!semiAuto&&Input.GetKey(KeyCode.Mouse0) && InputManger.instance.canInput) return true;
        return false;
    }
    public void Fire(){
        fireRateTimer = 0;
        barrelPos.LookAt(aim.aimPos);
        audioSource.PlayOneShot(gunShot);
        recoil.triggerRecoil();
        TriggerMuzzleFlash();
        ammo.currentAmmo --;
        barrelPos.LookAt(aim.aimPos);
        barrelPos.localEulerAngles = bloom.BloomAngle(barrelPos);
        for(int i = 0; i<bulletsPerShot; i ++){
            GameObject currentBullet = Instantiate(bullet, barrelPos.position,barrelPos.rotation);
            bullet bulletScript = currentBullet.GetComponent<bullet>();
            bulletScript.weapon = this;
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(barrelPos.forward * bulletVelocity,ForceMode.Impulse);
        }
    }

    public void TriggerMuzzleFlash(){
        muzzleFlashParticles.Play();
        muzzleFlashLight.intensity = lightIntensity;
        
    }
    
public static WeaponManager instance;

    private void Awake()
    {
        instance = this;
    }
}