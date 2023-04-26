using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    RagdollManager ragdollManager;
    [SerializeField]float health;
    void Start(){
        ragdollManager = GetComponent<RagdollManager>();
    }
    public void TakeDamage(float damage){
        health-=damage;
        if(health<=0) EnemyDeath();
    }
    void EnemyDeath(){
        ragdollManager.TriggerRagdoll();
    
}
}
