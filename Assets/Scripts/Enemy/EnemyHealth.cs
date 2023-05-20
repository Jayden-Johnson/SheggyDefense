using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour
{
    public GameObject healthUI;
    public Slider slider;
    RagdollManager ragdollManager;
    public float health;
    public float despawnTime = 15f;
    EnemyAI enemyAI;
    private Animator animator;
    public PointsManager pointsManager;

    [HideInInspector]public bool isDead;


    void Start(){
        ragdollManager = GetComponent<RagdollManager>();
        enemyAI = GetComponent<EnemyAI>();
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage){
        health-=damage;
        slider.value = health;
        
        if(health<=0 && !isDead)
        {
            EnemyDeath();
        } 
    }
    public void EnemyDeath() {
        ragdollManager.TriggerRagdoll();
        Invoke("Despawn",despawnTime);
        animator.runtimeAnimatorController = null;
        enemyAI.enabled = false;
        Destroy(slider);
        Destroy(healthUI);
        isDead = true;
        pointsManager.pointBalance += 30;
    }
    void Despawn(){

        Destroy(gameObject);
    }
}
