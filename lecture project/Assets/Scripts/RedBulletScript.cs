using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBulletScript : MonoBehaviour
{
    private GameObject greenBillion;
    private Rigidbody2D rb;
    public float force;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        greenBillion = GameObject.FindGameObjectWithTag("green");

        Vector3 direction = greenBillion.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 5) { Destroy(gameObject); }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("green"))
            {
            Destroy(gameObject);
        }
    }
}
