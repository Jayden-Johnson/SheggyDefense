using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] float timeToDestroy;
    [HideInInspector] public WeaponManager weapon;
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject,timeToDestroy);
    }

    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.GetComponentInParent<EnemyHealth>()){
            EnemyHealth enemyHealth = collision.gameObject.GetComponentInParent<EnemyHealth>();
            enemyHealth.TakeDamage(weapon.damage);
            if(enemyHealth.health<=0 && enemyHealth.isDead == false){
                Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
                rb.AddForce(dir * weapon.enemyKickbackForce,ForceMode.Impulse);
                enemyHealth.isDead = true;
            }
        }
        else {
            //spawn decal
            ContactPoint contact = collision.contacts[0];
            Vector3 decalOffset = contact.normal * 0.01f;
            Quaternion rotation = Quaternion.LookRotation(-contact.normal, Vector3.up);
            GameObject decal = Instantiate(decalPreFab, contact.point + decalOffset, rotation);
        }
        Destroy(this.gameObject);
    }
    public void spawnDecal() {}
}
