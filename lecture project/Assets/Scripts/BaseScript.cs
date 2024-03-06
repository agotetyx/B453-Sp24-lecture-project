using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScript : MonoBehaviour
{

    public GameObject Billion;
    public float timer = 2f;
    /*public List<GameObject> green_billions_array = new List<GameObject>();
    public List<GameObject> red_billions_array = new List<GameObject>();*/
    public List<GameObject> billions_array = new List<GameObject>();

    public Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        spawnBillion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnBillion()
    {
        //print("billion spawned");
        GameObject billion = Instantiate(Billion, spawnPoint.transform.position, Quaternion.identity);
        Invoke("spawnBillion", timer);
        billions_array.Add(billion);
        /*if (billion.tag == "green")
        {
            green_billions_array.Add(billion);

            print("green array " + green_billions_array.Count);
        }
        if (billion.tag == "red")
        {
            red_billions_array.Add(billion);
            print("red array " + red_billions_array.Count);
        }*/

    }
}
