using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] float Despawn;
    [HideInInspector] public WeaponManager weapon;
    public Vector3 dir;
    [HideInInspector] public int decalsSpawned;
    public GameObject decalPrefab;

    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject,Despawn);
    }

    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.GetComponentInParent<EnemyHealth>()){
            EnemyHealth enemyHealth = collision.gameObject.GetComponentInParent<EnemyHealth>();
            enemyHealth.TakeDamage(weapon.damage);
            if(enemyHealth.health<=0 && enemyHealth.isDead == false){
                enemyHealth.isDead = true;
                Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
                rb.AddForce(dir * weapon.enemyKickbackForce,ForceMode.Impulse);
            }
        }
        else {
            //spawn decal
            ContactPoint contact = collision.contacts[0];
            Vector3 decalOffset = contact.normal * 0.01f;
            Quaternion rotation = Quaternion.LookRotation(-contact.normal, Vector3.up);
            GameObject decal = Instantiate(decalPrefab, contact.point + decalOffset, rotation);
            Destroy(decal.gameObject,Despawn);
        }
        Destroy(this.gameObject);
    }
    public void spawnDecal() {
    }
}
