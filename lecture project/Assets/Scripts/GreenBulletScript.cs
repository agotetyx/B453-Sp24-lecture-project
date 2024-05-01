using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBulletScript : MonoBehaviour
{
    private GameObject redBillion;
    private Rigidbody2D rb;
    public float force;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        redBillion = GameObject.FindGameObjectWithTag("red");

        Vector3 direction = redBillion.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 5) { Destroy(gameObject); }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("red"))
        {
            Destroy(gameObject);
        }
    }
}
