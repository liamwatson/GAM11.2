using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    //public variables
    public GameObject PlayerCamera;
    public GameObject ClickedObject;

    public float scrollspeed = 1;
    public float scrollspeedAcc = 0.5f;
    public float maxscrollspeed = 150f;
    public float basescrollspeed = 20;

    public Vector3 raycasthit;
    public static PlayerController Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start ()
    {
        //makesure that the base scroll speed is set to the scrollspeed at the start.
        float basescrollspeed = scrollspeed;
    }
	
	// Update is called once per frame
	void Update () {
        //run these functions in the update
        Movement();
        mousescroll();
        Mouseclick();
	}

    //function that is ran all the time
    private void Mouseclick()
    {
        //if the escape key is pressed cancel the building placement
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BuildingManager.Instance.buildingbeingplaced = false;
        }
        //if the left mouse button is pressed run this 
        if (Input.GetMouseButtonDown(0))
        {
            //check if the menu is open first, if it is then do nothing (this stops buildings being clicked when the menu is open)
            if (GameManager.Instance.Menuopen == false)
            {
                //if the menu is not open then create the new raycast
                RaycastHit hitInfo = new RaycastHit();

                //check if the raycast hit anything and fire from camera to mouse postion
                bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
                //set where the raycast hit to a vector3
                raycasthit = hitInfo.point;

                //if a building is going to be placed and the player isnt clicking on a building tagged collider then run this
                if (BuildingManager.Instance.buildingbeingplaced == true && hitInfo.transform.gameObject.tag != "Building")
                {
                    //run the function in building manager and send the vector3 coords
                    BuildingManager.Instance.placebuilding(raycasthit);
                }

                //if the building is not ready to be placed then run this
                else if (BuildingManager.Instance.buildingbeingplaced == false)
                {
                    //if the ray cast actually hits somthing
                    if (hit)
                    {
                        //if it hits a object with name HQ run this
                        if (hitInfo.transform.gameObject.name == "HQ")
                        {
                            //if HQ is hit then open the menu
                            hitInfo.collider.gameObject.GetComponent<BuildingManager>().HQMENUCLICKED();
                        }
                        //if it hits a gameobject with name research center(clone) < this had to be added otherwise didnt work, when unity instantiateds it adds clone to end
                        if (hitInfo.transform.gameObject.name == "ResearchCenter(Clone)" && GameManager.Instance.researchcentercomplete == true)
                        {
                            //if this ran it will open the research menu
                            BuildingManager.Instance.RESEARCHMENUCLICKED();
                        }
                        //if a building with tag building is clicked it will run the function on the object
                        else if (hitInfo.transform.gameObject.tag == "Building")
                        {
                            //run function on object hit
                            hitInfo.transform.gameObject.SendMessageUpwards("toggleactive");
                        }
                    }
                    //nothing hit
                    else
                    {
                        Debug.Log("No hit");
                    }
                }
            }
        }
    }

    //function that controlls the moust in / out scroll
    private void mousescroll()
    {
        var input = Input.GetAxis("Mouse ScrollWheel");
        //scroll in max zoom
        if (input > 0f && transform.position.y >= 50)
        {
            //will scroll in on every mouse roll
            transform.Translate(Vector3.forward * Time.deltaTime * 300);
        }
        //scroll out
        else if (input < 0f && transform.position.y <= 130)
        {
            //will scroll out on every mouse roll
            transform.Translate(Vector3.back * Time.deltaTime * 300);
        }
    }

    //this function overlooks the movement
    private void Movement()
    {
        //check the input given, this will move the camera down and left in corraspondance with the maximum amount
        if (Input.GetKey("a") && transform.position.x >= 133 && Input.GetKey("s") && transform.position.z >= 77)
        {
            transform.Translate(Vector3.left * (Time.deltaTime * scrollspeed)/1.5f);
            transform.Translate(Vector3.down * (Time.deltaTime * scrollspeed)/ 1.5f);
            //this will increase the speed of the scroll (acceleration)
            if (scrollspeed <= maxscrollspeed)
                scrollspeed += scrollspeedAcc;
        }
        //check the input given, this will move the camera up and left in corraspondance with the maximum amount
        else if (Input.GetKey("a")  && transform.position.x >= 133 && Input.GetKey("w") && transform.position.z <= 373)
        {
            transform.Translate(Vector3.left * (Time.deltaTime * scrollspeed) / 1.5f);
            transform.Translate(Vector3.up * (Time.deltaTime * scrollspeed) / 1.5f);
            //this will increase the speed of the scroll (acceleration)
            if (scrollspeed <= maxscrollspeed)
                scrollspeed += scrollspeedAcc;
        }
        //check the input given, this will move the camera up and right in corraspondance with the maximum amount
        else if (Input.GetKey("w") && transform.position.z <= 373 && Input.GetKey("d") && transform.position.x <= 370)
        {
            transform.Translate(Vector3.up * (Time.deltaTime * scrollspeed) / 1.5f);
            transform.Translate(Vector3.right * (Time.deltaTime * scrollspeed) / 1.5f);
            //this will increase the speed of the scroll (acceleration)
            if (scrollspeed <= maxscrollspeed)
                scrollspeed += scrollspeedAcc;
        }
        //check the input given, this will move the camera down in corraspondance with the maximum amount
        else if (Input.GetKey("d") && transform.position.x <= 370 && Input.GetKey("s") && transform.position.z >= 77)
        {
            transform.Translate(Vector3.right * (Time.deltaTime * scrollspeed) / 1.5f);
            transform.Translate(Vector3.down * (Time.deltaTime * scrollspeed) / 1.5f);
            //this will increase the speed of the scroll (acceleration)
            if (scrollspeed <= maxscrollspeed)
                scrollspeed += scrollspeedAcc;
        }
        //check the input given, this will move the camera left in corraspondance with the maximum amount
        else if (Input.GetKey("a") && transform.position.x >= 133)
        {
            transform.Translate(Vector3.left * Time.deltaTime * scrollspeed);
            //this will increase the speed of the scroll (acceleration)
            if (scrollspeed <= maxscrollspeed)
                scrollspeed += scrollspeedAcc;
        }
        //check the input given, this will move the camera right in corraspondance with the maximum amount
        else if (Input.GetKey("d") && transform.position.x <= 370)
        {
            transform.Translate(Vector3.right * Time.deltaTime * scrollspeed);
            //this will increase the speed of the scroll (acceleration)
            if (scrollspeed <= maxscrollspeed)
                scrollspeed += scrollspeedAcc;
        }
        //check the input given, this will move the camera up in corraspondance with the maximum amount
        else if (Input.GetKey("w") && transform.position.z <= 373)
        {
            transform.Translate(Vector3.up * Time.deltaTime * scrollspeed);
            //this will increase the speed of the scroll (acceleration)
            if (scrollspeed <= maxscrollspeed)
                scrollspeed += scrollspeedAcc;
        }
        //check the input given, this will move the camera down in corraspondance with the maximum amount
        else if (Input.GetKey("s") && transform.position.z >= 77)
        {
            transform.Translate(Vector3.down * Time.deltaTime * scrollspeed);
            //this will increase the speed of the scroll (acceleration)
            if (scrollspeed <= maxscrollspeed)
                scrollspeed += scrollspeedAcc;
        }
        //when the player lets go it sets the speed (acceleration) back to base amount
        else {
            scrollspeed = basescrollspeed;
        }
    }
}
