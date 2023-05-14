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
    
    // Start is called before the first frame update
    void Start()
    {
    }
    public static PlayerHealth instance;
    void Awake(){
        instance = this;
    }
    public void PlayerTakeDamage(float enemyDamage){
        playerHealth-=enemyDamage;
        playerHealthBar.value = playerHealth;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
