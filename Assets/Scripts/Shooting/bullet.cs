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
        }
        Destroy(this.gameObject);
    }
}
