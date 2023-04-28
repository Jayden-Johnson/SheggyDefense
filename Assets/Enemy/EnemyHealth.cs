using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    RagdollManager ragdollManager;
    public float health;
    public float despawnTime = 15f;
    [HideInInspector]public bool isDead;
    void Start(){
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        ragdollManager = GetComponent<RagdollManager>();
    }
    public void TakeDamage(float damage){
        health-=damage;
        if(health<=0) EnemyDeath();
    }
    void EnemyDeath(){
        ragdollManager.TriggerRagdoll();
        Invoke("Despawn", despawnTime);
   }
   void Despawn(){
    Destroy(gameObject);
   }
}
