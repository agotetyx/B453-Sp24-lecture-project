using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillionScript : MonoBehaviour
{

    public GameObject bullet;
    public Transform bulletPos;

    private SpriteRenderer spriteRenderer;

    public float timer;

    private GameObject enemy;

    private float health = 100f;

    gameManager gm;

    public BaseScript bs;

    private Color[] colors = { Color.red, Color.green, Color.blue, Color.magenta, Color.yellow }; // Add any colors you want here
    private int currentIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        gm = GetComponent<gameManager>();
        bs = GetComponent<BaseScript>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        enemy = GameObject.FindGameObjectWithTag("red");

    }

    // Update is called once per frame
    void Update()
    {
        //updateRank();

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
        if (other.gameObject.CompareTag("red-bullet"))
        {
            health -= 10;
            changeColor();

            if (health <= 0)
            {

                Destroy(gameObject);
            }
        }
        if (other.gameObject.CompareTag("red-base-bullet"))
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

   /* void updateRank()
    {
        if (bs.baseRank == 2)
        {
            health *= 2.5f;
            timer = timer / 2;
        }
        if (bs.baseRank == 3)
        {
            health *= 2.5f;
            timer = timer / 2;
        }
        if (bs.baseRank == 4)
        {
            health *= 2.5f;
            timer = timer / 2;
        }
        if (bs.baseRank == 5)
        {
            health *= 2.5f;
            timer = timer / 2;
        }
    }*/

}
