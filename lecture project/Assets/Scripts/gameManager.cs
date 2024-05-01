using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public GameObject red_flag_prefab;
    public GameObject green_flag_prefab;
    public GameObject blue_flag_prefab;
    public GameObject yellow_flag_prefab;

    private int red_count = 0;
    private int green_count = 0;
    private int blue_count = 0;
    private int yellow_count = 0;

    public LineRenderer lineRenderer;
    Vector3 drag_startPos;
    Vector3 drag_endPos;

    public GameObject green_Billion;
    public GameObject red_Billion;
    public GameObject blue_Billion;
    public GameObject yellow_Billion;
    public bool isPlaced;

    private List <GameObject> green_flags_array = new List<GameObject>();
    private List<GameObject> red_flags_array = new List<GameObject>();
    private List<GameObject> blue_flags_array = new List<GameObject>();
    private List<GameObject> yellow_flags_array = new List<GameObject>();
    BaseScript baseScript;

    BaseScript green_baseScript;
    BaseScript red_baseScript;
    BaseScript blue_baseScript;
    BaseScript yellow_baseScript;

    public GameObject red_base;
    public GameObject green_base;
    public GameObject blue_base;
    public GameObject yellow_base;

    public bool isDragging;

    public GameObject draggedFlag;
    
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


        if (blue_base != null)
        {
            // Get the ScriptA component attached to GameObjectA
            blue_baseScript = blue_base.GetComponent<BaseScript>();

        }


        if (yellow_base != null)
        {
            // Get the ScriptA component attached to GameObjectA
            yellow_baseScript = yellow_base.GetComponent<BaseScript>();

        }

    }

    // Update is called once per frame
    void Update()
    {
        //red_baseScript.billions_array = 
        flagSpawn();
        flagClick();
        
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

        foreach (GameObject billion in blue_baseScript.billions_array)
        {
            //print("move bilions for green");
            MoveBillions(billion);
        }


        foreach (GameObject billion in yellow_baseScript.billions_array)
        {
            //print("move bilions for green");
            MoveBillions(billion);
        }




        if (isDragging)
        {
            dragFlag(draggedFlag);
        }

    }

    void flagSpawn()
    {
        if (Input.GetMouseButtonDown(0) && green_flags_array.Count <= 1 )
        {
            SpawnPrefab(green_flag_prefab);
            
            
        }
        // Check if the right mouse button is clicked
        else if (Input.GetMouseButtonDown(1) && red_flags_array.Count <= 1)
        {// Check if the ray hits any collider in the scene
            SpawnPrefab(red_flag_prefab);
           
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && blue_flags_array.Count <= 1)
        {
            SpawnPrefab(blue_flag_prefab);


        }
        // Check if the right mouse button is clicked
        else if (Input.GetKeyDown(KeyCode.Alpha2) && yellow_flags_array.Count <= 1)
        {// Check if the ray hits any collider in the scene
            SpawnPrefab(yellow_flag_prefab);

        }


    }
    void SpawnPrefab(GameObject prefab)
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        //RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero)
        // Check if the ray hits any collider in the scene
        if (Physics.Raycast(ray, out hit))
        {
            //print("ray shoot");
            if (hit.collider.gameObject.tag == "green-flag" || hit.collider.gameObject.tag == "red-flag"|| hit.collider.gameObject.tag == "blue-flag" || hit.collider.gameObject.tag == "yellow-flag")
            {
                return;

            }


            // Get the mouse position in the world
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f; // Set z to 0 to spawn on the same plane as the camera

            // Instantiate the prefab at the mouse position
            GameObject flag = Instantiate(prefab, mousePosition, Quaternion.identity);
            //flag.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
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

            if (flag.tag == "blue-flag" && blue_flags_array.Count <= 1)
            {
                blue_flags_array.Add(flag);
                blue_count++;
                //print("green flag array" + green_flags_array.Count);
            }
            if (flag.tag == "yellow-flag" && yellow_flags_array.Count <= 1)
            {
                yellow_flags_array.Add(flag);
                //print("red flag array" + red_flags_array.Count);
                yellow_count++;
            }


            isPlaced = true;
            //print(isPlaced);
        }
    }
        void dragFlag(GameObject prefab)
        {
        
                lineRenderer.enabled = true;
                lineRenderer.positionCount = 2;
                drag_startPos = prefab.transform.position;
                lineRenderer.SetPosition(0, drag_startPos);
                drag_endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                drag_endPos.z = 0;
                lineRenderer.SetPosition(1, drag_endPos);
                lineRenderer.useWorldSpace = true;

            if (Input.GetMouseButtonUp(0))
            {
                lineRenderer.enabled = false;
                prefab.transform.position = new Vector3(drag_endPos.x, drag_endPos.y, 0);
            isDragging = false;
            
        }
        }

    void flagClick()
    {
        if (Input.GetMouseButtonDown(0) && !isDragging)
        {
            

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // Check if the ray hits any collider in the scene
            if (Physics.Raycast(ray, out hit))
            {
                print("ray shoot");
                    if (hit.collider.gameObject.tag == "green-flag" || hit.collider.gameObject.tag == "red-flag"|| hit.collider.gameObject.tag == "blue-flag" || hit.collider.gameObject.tag == "yellow-flag")
                    {
                    isDragging = true;
                    draggedFlag = hit.collider.gameObject;
                    print("flag clicked");
                       
                    }
                

            }

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

            float blue_shortestDistance = Mathf.Infinity; // The shortest distance to an enemy
            GameObject blue_nearestFlag = null;

            float yellow_shortestDistance = Mathf.Infinity; // The shortest distance to an enemy
            GameObject yellow_nearestFlag = null;



            foreach (GameObject flag in green_flags_array)
            {
                if (billion.tag == "green")
                {
                    float distanceToFlag = Vector3.Distance(billion.transform.position, flag.transform.position);
                    if (distanceToFlag < green_shortestDistance) 
                    {
                        green_shortestDistance = distanceToFlag; 
                        green_nearestFlag = flag;
                        Vector3 direction = (green_nearestFlag.transform.position - billion.transform.position).normalized;
                        float speed = 5.0f; // Adjust this speed as needed
                        billion.transform.Translate(direction * speed * Time.deltaTime);

                        Quaternion rotation = Quaternion.LookRotation(flag.transform.position - billion.transform.position, transform.TransformDirection(Vector3.up));
                         billion.transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
                       
                    }

                }
            }
            foreach (GameObject flag in red_flags_array)
            {
                if (billion.tag == "red")
                {
                   float distanceToFlag = Vector3.Distance(billion.transform.position, flag.transform.position);
                    if (distanceToFlag < red_shortestDistance)
                    {
                        red_shortestDistance = distanceToFlag;
                        red_nearestFlag = flag;
                        Vector3 direction = (red_nearestFlag.transform.position - billion.transform.position).normalized;
                        float speed = 5.0f; // Adjust this speed as needed
                        billion.transform.Translate(direction * speed * Time.deltaTime);
                      
                        Quaternion rotation = Quaternion.LookRotation(flag.transform.position - billion.transform.position, transform.TransformDirection(Vector3.up));
                        billion.transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
                       
                    }
                }

            }

            foreach (GameObject flag in blue_flags_array)
            {
                if (billion.tag == "blue")
                {
                    float distanceToFlag = Vector3.Distance(billion.transform.position, flag.transform.position);
                    if (distanceToFlag < blue_shortestDistance)
                    {
                        blue_shortestDistance = distanceToFlag;
                        blue_nearestFlag = flag;
                        Vector3 direction = (blue_nearestFlag.transform.position - billion.transform.position).normalized;
                        float speed = 5.0f; // Adjust this speed as needed
                        billion.transform.Translate(direction * speed * Time.deltaTime);

                        Quaternion rotation = Quaternion.LookRotation(flag.transform.position - billion.transform.position, transform.TransformDirection(Vector3.up));
                        billion.transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);

                    }

                }
            }
            foreach (GameObject flag in yellow_flags_array)
            {
                if (billion.tag == "yellow")
                {
                    float distanceToFlag = Vector3.Distance(billion.transform.position, flag.transform.position);
                    if (distanceToFlag < yellow_shortestDistance)
                    {
                        yellow_shortestDistance = distanceToFlag;
                        yellow_nearestFlag = flag;
                        Vector3 direction = (yellow_nearestFlag.transform.position - billion.transform.position).normalized;
                        float speed = 5.0f; // Adjust this speed as needed
                        billion.transform.Translate(direction * speed * Time.deltaTime);

                        Quaternion rotation = Quaternion.LookRotation(flag.transform.position - billion.transform.position, transform.TransformDirection(Vector3.up));
                        billion.transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);

                    }
                }

            }


            //prefab.CompareTag()
        }
    }

    
}

