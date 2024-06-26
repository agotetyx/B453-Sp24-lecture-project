using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBillionScript : MonoBehaviour
{

    public GameObject bullet;
    public Transform bulletPos;

    public float timer;

    private GameObject enemy;
    private SpriteRenderer spriteRenderer;


    private int health = 100;

    gameManager gm;
    private Color[] colors = { Color.red, Color.green, Color.blue, Color.magenta, Color.yellow }; // Add any colors you want here
    private int currentIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        gm = GetComponent<gameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();


        enemy = GameObject.FindGameObjectWithTag("green");
        
    }

    // Update is called once per frame
    void Update()
    {


        float distance = Vector2.Distance(transform.position, enemy.transform.position);

        if (distance <10 )
        {
            timer += Time.deltaTime;
            if (timer > 2)
            {
                timer = 0;

                
                shoot();

            }

        }
    }


        

        void shoot()
        {
            Instantiate(bullet, bulletPos.position, Quaternion.identity);
        }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("green-bullet"))
        {
            health -= 10;
            changeColor();
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }

        if (other.gameObject.CompareTag("green-base-bullet"))
        {
            health -= 0;
            changeColor();
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void changeColor()
    {
        if (spriteRenderer != null)
        {
            currentIndex = (currentIndex + 1) % colors.Length;
            spriteRenderer.color = colors[currentIndex];
        }
    }
}
