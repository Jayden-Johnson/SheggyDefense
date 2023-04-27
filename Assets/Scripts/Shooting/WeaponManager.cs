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
    public float damage = 20;
    AimStateManager aim;
    
    //audio
    [SerializeField] AudioClip gunShot;
    AudioSource audioSource;

    ActionStateManager actions;
    WeaponRecoil recoil;
    WeaponAmmo ammo;
    WeaponBloom bloom;

    Light muzzleFlashLight;
    ParticleSystem muzzleFlashParticles;
    float lightIntensity;
    [SerializeField] float lightReturnSpeed;
    public float enemyKickbackForce =100;

    // Start is called before the first frame update
    void Start()
    {
        recoil = GetComponent<WeaponRecoil>();
        audioSource = GetComponent<AudioSource>();
        aim = GetComponentInParent<AimStateManager>();
        ammo = GetComponent<WeaponAmmo>();
        bloom = GetComponent<WeaponBloom>();
        muzzleFlashLight = GetComponentInChildren<Light>();
        muzzleFlashParticles = GetComponentInChildren<ParticleSystem>();
        lightIntensity = muzzleFlashLight.intensity;
        muzzleFlashLight.intensity = 0;
        fireRateTimer = fireRate;
        actions = GetComponentInParent<ActionStateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ShouldFire()) Fire();
        muzzleFlashLight.intensity = Mathf.Lerp(muzzleFlashLight.intensity,0,lightReturnSpeed * Time.deltaTime);
        ammo.ammoText.text = "Ammo: " + ammo.currentAmmo + "/" + ammo.extraAmmo;
    }
    bool ShouldFire(){
        fireRateTimer += Time.deltaTime;
        if (fireRateTimer < fireRate) return false;
        if(ammo.currentAmmo==0)return false;
        if(actions.currentState==actions.Reload) return false;
        if(semiAuto&&Input.GetKeyDown(KeyCode.Mouse0)) return true;
        if(!semiAuto&&Input.GetKey(KeyCode.Mouse0)) return true;
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

    void TriggerMuzzleFlash(){
        muzzleFlashParticles.Play();
        muzzleFlashLight.intensity = lightIntensity;
    }
}