using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public GameObject PlayerCamera;
    public float scrollspeed = 1;
    public float scrollspeedAcc = 0.5f;
    public float maxscrollspeed = 150f;
    public GameObject ClickedObject;
    float basescrollspeed = 20;
    public Vector3 raycasthit;

    public static PlayerController Instance;

    void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
        float basescrollspeed = scrollspeed;
    }
	
	// Update is called once per frame
	void Update () {
        Movement();
        mousescroll();
        Mouseclick();
	}

    private void Mouseclick()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BuildingManager.Instance.buildingbeingplaced = false;
        }

            if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            raycasthit = hitInfo.point;
            if (BuildingManager.Instance.buildingbeingplaced == true && hitInfo.transform.gameObject.tag != "Building")
            {
                BuildingManager.Instance.placebuilding(raycasthit);
            }
            else if (BuildingManager.Instance.buildingbeingplaced == false)
            {
                if (hit)
                {
                    Debug.Log("Left Click hit: " + hitInfo.transform.gameObject.name);
                    if (hitInfo.transform.gameObject.name == "HQ")
                    {
                        Debug.Log("Hit HQ");
                        hitInfo.collider.gameObject.GetComponent<BuildingManager>().HQMENUCLICKED();
                    }
                    if (hitInfo.transform.gameObject.name == "ResearchCenter(Clone)" && GameManager.Instance.researchcentercomplete == true)
                    {
                        Debug.Log("Hit Research center");
                        BuildingManager.Instance.RESEARCHMENUCLICKED();
                    }
                    else if (hitInfo.transform.gameObject.tag == "Building")
                    {
                        hitInfo.transform.gameObject.SendMessageUpwards("toggleactive");
                        Debug.Log("you toggle the building on/off");
                    }
                    else
                    {
                        Debug.Log("Hit somthing but not the HQ");
                    }
                }
                else
                {
                    Debug.Log("No hit");
                }
            }
        }
    }

    private void mousescroll()
    {
        var input = Input.GetAxis("Mouse ScrollWheel");
        if (input > 0f && transform.position.y >= 50)
        {
            Debug.Log("scroll up");
            transform.Translate(Vector3.forward * Time.deltaTime * 300);
        }
        else if (input < 0f && transform.position.y <= 130)
        {
            Debug.Log("scroll down");
            transform.Translate(Vector3.back * Time.deltaTime * 300);
        }
    }

    private void Movement()
    {

        if (Input.GetKey("a") && transform.position.x >= 133 && Input.GetKey("s") && transform.position.z >= 77)
        {
            transform.Translate(Vector3.left * (Time.deltaTime * scrollspeed)/1.5f);
            transform.Translate(Vector3.down * (Time.deltaTime * scrollspeed)/ 1.5f);
            if (scrollspeed <= maxscrollspeed)
                scrollspeed += scrollspeedAcc;
        }
        else if (Input.GetKey("a")  && transform.position.x >= 133 && Input.GetKey("w") && transform.position.z <= 373)
        {
            transform.Translate(Vector3.left * (Time.deltaTime * scrollspeed) / 1.5f);
            transform.Translate(Vector3.up * (Time.deltaTime * scrollspeed) / 1.5f);
            if (scrollspeed <= maxscrollspeed)
                scrollspeed += scrollspeedAcc;
        }
        else if (Input.GetKey("w") && transform.position.z <= 373 && Input.GetKey("d") && transform.position.x <= 370)
        {
            transform.Translate(Vector3.up * (Time.deltaTime * scrollspeed) / 1.5f);
            transform.Translate(Vector3.right * (Time.deltaTime * scrollspeed) / 1.5f);
            if (scrollspeed <= maxscrollspeed)
                scrollspeed += scrollspeedAcc;
        }
        else if (Input.GetKey("d") && transform.position.x <= 370 && Input.GetKey("s") && transform.position.z >= 77)
        {
            transform.Translate(Vector3.right * (Time.deltaTime * scrollspeed) / 1.5f);
            transform.Translate(Vector3.down * (Time.deltaTime * scrollspeed) / 1.5f);
            if (scrollspeed <= maxscrollspeed)
                scrollspeed += scrollspeedAcc;
        }
        
        else if (Input.GetKey("a") && transform.position.x >= 133)
        {
            transform.Translate(Vector3.left * Time.deltaTime * scrollspeed);
            if (scrollspeed <= maxscrollspeed)
                scrollspeed += scrollspeedAcc;
        }
        else if (Input.GetKey("d") && transform.position.x <= 370)
        {
            transform.Translate(Vector3.right * Time.deltaTime * scrollspeed);
            if (scrollspeed <= maxscrollspeed)
                scrollspeed += scrollspeedAcc;
        }
        else if (Input.GetKey("w") && transform.position.z <= 373)
        {
            transform.Translate(Vector3.up * Time.deltaTime * scrollspeed);
            if (scrollspeed <= maxscrollspeed)
                scrollspeed += scrollspeedAcc;
        }
        
        else if (Input.GetKey("s") && transform.position.z >= 77)
        {
            transform.Translate(Vector3.down * Time.deltaTime * scrollspeed);
            if (scrollspeed <= maxscrollspeed)
                scrollspeed += scrollspeedAcc;
        }
        else {
            scrollspeed = basescrollspeed;
        }
    }
}
