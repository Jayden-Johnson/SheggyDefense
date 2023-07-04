using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public GameObject playerHealthUI;
    public Slider playerHealthBar;
    Animator anim;
    public float playerHealth;

    public ActionStateManager ActionStateManager;


    public void PlayerTakeDamage(float enemyDamage){
        playerHealth-=enemyDamage;
    }

    void Update()
    {
        if (playerHealth == 0) 
        {
            ActionStateManager.PlayerDeath();
        }  

        playerHealthBar.value = playerHealth;
    }
    public static PlayerHealth instance;

    private void Awake()
    {
        instance = this;
    }
}
