using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemiesLeftUI : MonoBehaviour
{
    public Text enemLeft;
    
    void Update()
    {
        enemLeft.text = EnemySpawn.instance.enemyCounter.ToString() + " Enemies Remaining";
    }
}
