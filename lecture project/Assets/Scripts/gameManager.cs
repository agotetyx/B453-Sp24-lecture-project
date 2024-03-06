using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public GameObject red_flag_prefab;
    public GameObject green_flag_prefab;
    private int red_count = 0;
    private int green_count = 0;

    private LineRenderer lineRenderer;
    Vector3 drag_startPos;
    Vector3 drag_endPos;

    public GameObject green_Billion;
    public GameObject red_Billion;
    public bool isPlaced;

    private List <GameObject> green_flags_array = new List<GameObject>();
    private List<GameObject> red_flags_array = new List<GameObject>();

    BaseScript baseScript;
    BaseScript green_baseScript;
    BaseScript red_baseScript;

    public GameObject red_base;
    public GameObject green_base;

    
    // Start is called before the first frame update
    void Start()
    {
        baseScript = GetComponent<BaseScript>();


       

        if (red_base != null)
        {
            // Get the ScriptA component attached to GameObjectA
            red_baseScript = red_base.GetComponent<BaseScript>();

        }

    
        if (green_base != null)
        {
            // Get the ScriptA component attached to GameObjectA
            green_baseScript = green_base.GetComponent<BaseScript>();

        }

    }

    // Update is called once per frame
    void Update()
    {
        flagSpawn();

        
            foreach (GameObject billion in red_baseScript.billions_array)
            {
            //print("move bilions for: " + billion.gameObject.tag);
                MoveBillions(billion);
            }
        

       
            foreach (GameObject billion in green_baseScript.billions_array)
            {
            //print("move bilions for green");
            MoveBillions(billion);
            }
        
        

    }

    void flagSpawn()
    {
        if (Input.GetMouseButtonDown(0) && green_flags_array.Count <= 1)
        {
            SpawnPrefab(green_flag_prefab);
            
            
        }
        // Check if the right mouse button is clicked
        else if (Input.GetMouseButtonDown(1) && red_flags_array.Count <= 1)
        {// Check if the ray hits any collider in the scene
            SpawnPrefab(red_flag_prefab);
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the collider hit belongs to the object we want to detect clicks on
                foreach (GameObject flag in red_flags_array)
                {
                    if (hit.collider.gameObject == red_flag_prefab)
                    {
                        print("red clicked");
                        dragFlag(red_flag_prefab);
                    }
                }
            }
        }


    }
        void SpawnPrefab(GameObject prefab)
        {

            // Get the mouse position in the world
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f; // Set z to 0 to spawn on the same plane as the camera

            // Instantiate the prefab at the mouse position
            GameObject flag = Instantiate(prefab, mousePosition, Quaternion.identity);

            if (flag.tag == "green-flag" && green_flags_array.Count <= 1)
            {
            green_flags_array.Add(flag);
            green_count++;
            //print("green flag array" + green_flags_array.Count);
            }
            if (flag.tag == "red-flag" && red_flags_array.Count <= 1)
            {
            red_flags_array.Add(flag);
            //print("red flag array" + red_flags_array.Count);
            red_count++;
            }

        isPlaced = true;
        //print(isPlaced);
        }

        void dragFlag(GameObject prefab)
        {
      
                if (lineRenderer == null)
                {
                    lineRenderer = gameObject.AddComponent<LineRenderer>();
                }
                lineRenderer.enabled = true;
                lineRenderer.positionCount = 2;
                drag_startPos = prefab.transform.position;
                lineRenderer.SetPosition(0, drag_startPos);
                lineRenderer.useWorldSpace = true;

            

            if (Input.GetMouseButton(0))
            {
                

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // Check if the ray hits any collider in the scene
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the collider hit belongs to the object we want to detect clicks on
                foreach (GameObject flag in green_flags_array)
                {
                    if (hit.collider.gameObject == green_flag_prefab)
                    {
                        print("green clicked");
                        drag_endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        lineRenderer.SetPosition(1, drag_endPos);
                    }
                }

            }

        }

            if (Input.GetMouseButtonUp(0))
            {
                lineRenderer.enabled = false;
                prefab.transform.position = Vector3.MoveTowards(transform.position, drag_endPos, 3 * Time.deltaTime);
        }
        }

    void MoveBillions(GameObject billion)
    {
        Rigidbody2D rb = billion.GetComponent<Rigidbody2D>();
        if (isPlaced)
        {
            //print("is placed" + isPlaced);
            float green_shortestDistance = Mathf.Infinity; // The shortest distance to an enemy
            GameObject green_nearestFlag = null;

            float red_shortestDistance = Mathf.Infinity; // The shortest distance to an enemy
            GameObject red_nearestFlag = null;

            foreach (GameObject flag in green_flags_array)
            {
                if (billion.tag == "green")
                {
                    //print(billion.tag + " flag is detected");
                    float distanceToFlag = Vector3.Distance(billion.transform.position, flag.transform.position);
                    if (distanceToFlag < green_shortestDistance) 
                    {
                        green_shortestDistance = distanceToFlag; 
                        green_nearestFlag = flag;
                        Vector3 direction = (green_nearestFlag.transform.position - billion.transform.position).normalized;
                        float speed = 5.0f; // Adjust this speed as needed
                        billion.transform.Translate(direction * speed * Time.deltaTime);
                        //billion.transform.LookAt(green_nearestFlag.transform);

                        /* float angle = Mathf.Atan2(green_nearestFlag.transform.position.y, green_nearestFlag.transform.position.x) * Mathf.Rad2Deg;

                         // Set the rotation towards the target
                         billion.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);*/

                        Quaternion rotation = Quaternion.LookRotation(flag.transform.position - billion.transform.position, transform.TransformDirection(Vector3.up));
                         billion.transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
                       /* float cross = Vector3.Cross(direction, billion.transform.right).z;
                        rb.angularVelocity = 20 * cross;
                        rb.velocity = transform.right * speed;*/

                    }

                }
            }
            foreach (GameObject flag in red_flags_array)
            {
                if (billion.tag == "red")
                {
                    //print(billion.tag + " flag is detected");
                    float distanceToFlag = Vector3.Distance(billion.transform.position, flag.transform.position);
                    if (distanceToFlag < red_shortestDistance)
                    {
                        red_shortestDistance = distanceToFlag;
                        red_nearestFlag = flag;
                        Vector3 direction = (red_nearestFlag.transform.position - billion.transform.position).normalized;
                        float speed = 5.0f; // Adjust this speed as needed
                        billion.transform.Translate(direction * speed * Time.deltaTime);
                       /* float cross = Vector3.Cross(direction, billion.transform.right).z;
                        rb.angularVelocity = 20 * cross;
                        rb.velocity = transform.right * speed;*/

                        Quaternion rotation = Quaternion.LookRotation(flag.transform.position - billion.transform.position, transform.TransformDirection(Vector3.up));
                        billion.transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
                        //
                        //billion.transform.LookAt(red_nearestFlag.transform);


                        /* float angle = Mathf.Atan2(red_nearestFlag.transform.position.y, red_nearestFlag.transform.position.x) * Mathf.Rad2Deg;

                         // Set the rotation towards the target
                         billion.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);*/

                    }
                }

            }


            //prefab.CompareTag()
        }
    }

    
}

