using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] float timeToDestroy;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>= timeToDestroy) Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision collision){
        Destroy(this.gameObject);
    }
}
