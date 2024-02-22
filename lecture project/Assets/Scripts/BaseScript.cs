using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScript : MonoBehaviour
{

    public GameObject Billion;
    public float timer = 2f;
    // Start is called before the first frame update
    void Start()
    {
        spawnBillion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnBillion()
    {
        //print("billion spawned");
        Instantiate(Billion, transform.position, Quaternion.identity);
        Invoke("spawnBillion", timer);
    }
}
