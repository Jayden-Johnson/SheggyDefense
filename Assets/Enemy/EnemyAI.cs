using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Animator anim;
    public Animator animPlayer;
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask Ground, Player;
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    private float attackRange;
    public float AttackRange;
    public bool playerInAttackRange;
    private CharacterController theGuy;
    public float enemyDamage = 20;

    private void Awake(){
        agent = GetComponent<NavMeshAgent>();
        theGuy = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        attackRange = theGuy.radius + AttackRange;
        player = GameObject.Find("TheGuy").transform;
    }
    
    

    void Update(){
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, Player);
        if(playerInAttackRange) AttackPlayer();
        if(!playerInAttackRange) ChasePlayer();
    }
    public void ChasePlayer(){
        anim.SetBool("Walking",true);
        agent.SetDestination(player.position);
    }
    public void AttackPlayer(){
        anim.SetBool("Walking",false);
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        if (!alreadyAttacked){
           alreadyAttacked = true;
           Invoke (nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    public void Attacked(){
        animPlayer.SetTrigger("Damaged");
        PlayerHealth.instance.PlayerTakeDamage(enemyDamage);
        if (PlayerHealth.instance.playerHealth == 0) animPlayer.SetTrigger("Death");
    }
    public void ResetAttack(){
        alreadyAttacked = false;
    }
}
