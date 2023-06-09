using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawn : MonoBehaviour
{
    
    public Waves[] waves;
    private int xPos;
    private int zPos;
    [HideInInspector]public int currentWave;
    public float CountDown;
    private bool readyCountDown;
    [HideInInspector] public int enemyCounter;

    

    // Start is called before the first frame update
    void Start()
    {
        readyCountDown = true;
        for(int i = 0; i < waves.Length; i++){
            waves[i].enemiesLeft = waves[i].enemies.Length;
        }
        enemyCounter = waves[currentWave].enemiesLeft;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWave >= waves.Length){
            Debug.Log("Indeed deeds");
            return;
        }

        if(readyCountDown == true){
            CountDown -= Time.deltaTime;
        }

        if (CountDown <= 0){
            readyCountDown = false;
            CountDown = waves[currentWave].timeToNextWave;
            StartCoroutine(Spawn());
        }

        if (waves[currentWave].enemiesLeft == 0){

            currentWave ++;
            readyCountDown = true;

            if(currentWave < waves.Length){
                enemyCounter = waves[currentWave].enemiesLeft;
            }
        }
    }
    
    private IEnumerator Spawn(){
        if(currentWave< waves.Length){
            for(int i = 0; i < waves[currentWave].enemies.Length; i ++){
                float xPos = Random.Range(-305,-497);
                float zPos = Random.Range(-305,-315);
                Instantiate(waves[currentWave].enemies[i], new Vector3(xPos, 2, zPos), Quaternion.identity);
                yield return new WaitForSeconds(waves[currentWave].timeToNextEnemy);
            }
        }
        
    }
    public static EnemySpawn instance;

    private void Awake()
    {
        instance = this;
    }
}

[System.Serializable]
public class Waves {
    [HideInInspector] public int enemiesLeft;
    public EnemyIndex[] enemies;
    public float timeToNextEnemy;
    public float timeToNextWave;
}



