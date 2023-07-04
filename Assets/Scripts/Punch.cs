using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    public GameObject fist;
    ActionStateManager action;
    void Start(){
        fist.SetActive(false);
    }
    void Update(){
    }

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("enemy")){
            EnemyHealth enemyHealth = other.GetComponentInParent<EnemyHealth>();
            enemyHealth.TakeMeleeDamage();
        }
    }
    public void punchEvent(){
        fist.SetActive(true);
    } 
    public void nonPunchEvent(){
        fist.SetActive(false);
    }
}