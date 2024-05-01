using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaseScript : MonoBehaviour
{

    public GameObject Billion_rank1;
    public GameObject Billion_rank2;
    public GameObject Billion_rank3;
    public GameObject Billion_rank4;
    public GameObject Billion_rank5;
    public float timer = 2f;
    /*public List<GameObject> green_billions_array = new List<GameObject>();
    public List<GameObject> red_billions_array = new List<GameObject>();*/
    public List<GameObject> billions_array = new List<GameObject>();

    public Transform spawnPoint;
    // Start is called before the first frame update

    public Slider slider;
    public Slider XPslider;

    public float health = 100f;
    public float xp = 0f;
    public int baseRank = 1;
    public float maxXP = 100f;

    public BaseScript[] enemyBase;

    bool isDestroyed = false;

    public TextMeshProUGUI rankText;
    void Start()
    {
        spawnBillion();
        
    }

    // Update is called once per frame
    void Update()
    {
        updateSlider();
        rankText.text = baseRank.ToString();
    }

    public void spawnBillion()
    {
        //print("billion spawned");
        if (baseRank == 1)
        {
            GameObject billion = Instantiate(Billion_rank1, spawnPoint.transform.position, Quaternion.identity);
            Invoke("spawnBillion", timer);
            billions_array.Add(billion);
        }
        if (baseRank == 2)
        {
            GameObject billion = Instantiate(Billion_rank2, spawnPoint.transform.position, Quaternion.identity);
            Invoke("spawnBillion", timer);
            billions_array.Add(billion);
        }
        if (baseRank == 3)
        {
            GameObject billion = Instantiate(Billion_rank3, spawnPoint.transform.position, Quaternion.identity);
            Invoke("spawnBillion", timer);
            billions_array.Add(billion);
        }
        if (baseRank == 4)
        {
            GameObject billion = Instantiate(Billion_rank4, spawnPoint.transform.position, Quaternion.identity);
            Invoke("spawnBillion", timer);
            billions_array.Add(billion);
        }
        if (baseRank == 5)
        {
            GameObject billion = Instantiate(Billion_rank5, spawnPoint.transform.position, Quaternion.identity);
            Invoke("spawnBillion", timer);
            billions_array.Add(billion);
        }


    }

    void decreaseHealth()
    {
        health -= 10f;

        if (health <= 0)
        {
            Destroy(gameObject);
            isDestroyed = true;
        }

        
    }

    void increaseXP()
    {
        xp += 20;

        if (xp >= maxXP)
        {
            baseRank++;
            xp = 0;
            maxXP *= 2;
            print("maxxp" + maxXP);
            XPslider.maxValue = maxXP;
            

            print(baseRank.ToString());
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
       /* if (gameObject.tag == "green")
        {*/
            foreach (BaseScript enemy in enemyBase)
            {
                if (other.gameObject == enemy)
                    //.CompareTag("red-bullet") || other.gameObject.CompareTag("red-base-bullet") || other.gameObject.CompareTag("blue-bullet") || other.gameObject.CompareTag("blue-base-bullet") || other.gameObject.CompareTag("yellow-bullet") || other.gameObject.CompareTag("yellow-base-bullet"))
                {
                    decreaseHealth();
                    if (isDestroyed)
                    {
                        enemy.increaseXP();
                    }

                }
            //}
        }

       /* if (gameObject.tag == "red")
        {
            if (other.gameObject.CompareTag("green-bullet") || other.gameObject.CompareTag("green-base-bullet"))
            {
                decreaseHealth();
                //print("red health" + health);
                if (isDestroyed)
                {
                    enemyBase.increaseXP();}
            }

        //}*/
    }

    void updateSlider()
    {
        slider.value = health;
        XPslider.value = xp;
        
    }
}
