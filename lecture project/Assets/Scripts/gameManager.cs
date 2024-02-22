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
    private bool isPlaced;

    List <GameObject> green_flags_array = new List<GameObject>();
    List<GameObject> red_flags_array = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        flagSpawn();
        //MoveBillions(red_Billion);
        //MoveBillions(green_Billion);

        
    }

    void flagSpawn()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnPrefab(green_flag_prefab);
            green_count++;
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
                        dragFlag(green_flag_prefab);
                    }
                }
               
            }
        }
        // Check if the right mouse button is clicked
        else if (Input.GetMouseButtonDown(1))
        {// Check if the ray hits any collider in the scene
            SpawnPrefab(red_flag_prefab);
            green_count++;
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

            if (flag.tag == "green-flag")
        {
            green_flags_array.Add(flag);
        }
        if (flag.tag == "red-flag")
        {
            red_flags_array.Add(flag);
        }

        isPlaced = true;
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
                drag_endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                lineRenderer.SetPosition(1, drag_endPos);
            }

            if (Input.GetMouseButtonUp(0))
            {
                lineRenderer.enabled = false;
                prefab.transform.position = Vector3.MoveTowards(transform.position, drag_endPos, 3 * Time.deltaTime);
        }
        }

    void MoveBillions(GameObject prefab)
    {
        if (isPlaced)
        {
            print("is placed" + isPlaced);
            
            if (prefab.tag == "green")
            {
                print(prefab.tag + " flag is detected");
                //GameObject [] green_flags_array = GameObject.FindGameObjectsWithTag("green");
                prefab.transform.position += Vector3.MoveTowards(prefab.transform.position, green_flags_array[0].transform.position, 1 * Time.deltaTime);
            
            }

            if (prefab.tag == "red")
            {
                print(prefab.tag + " flag is detected");
                //GameObject[] red_flags_array = GameObject.FindGameObjectsWithTag("red");
                prefab.transform.position += Vector3.MoveTowards(prefab.transform.position, red_flags_array[0].transform.position, 1 * Time.deltaTime);
            }


            //prefab.CompareTag()
        }
    }

    
}

