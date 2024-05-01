using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class green_base_shoot : MonoBehaviour
{
    public float range;
    private GameObject billion;
    bool detected = false;
    Vector2 direction;

    public GameObject turret;
    public GameObject bullet;
    public float fireRate;
    float nextTimeToFire = 0;

    public Transform shootPoint;

    public float force = 5f;
    
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
        billion = GameObject.FindGameObjectWithTag("red");

        if (billion != null)
        {
            Vector2 targetPos = billion.transform.position;

            direction = targetPos - (Vector2)transform.position;

            turret.transform.right = direction;
            if (Time.time > nextTimeToFire)
            {
                nextTimeToFire = (Time.time + 1) / fireRate;
                //shoot();
            }
        }
        
    }

    void shoot()
    {
        GameObject bulletIns = Instantiate(bullet, shootPoint.position, Quaternion.identity);
        bulletIns.GetComponent<Rigidbody2D>().AddForce(direction * force);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
