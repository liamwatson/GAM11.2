using UnityEngine;
using System.Collections;

public class OilRefine : MonoBehaviour {
    //private variables
    private bool buildingcomplete = false;
    private float timer = 0;

    //public variables
    public float refcooldown = 2;
    public float buildingtime = 30;
    public float timer2 = 0;

    public int moneyreward = 50;
    public int powerdrain = 5;
    public int oilconsumption = 5;

    public bool toggleonoff = true;

    void Start()
    {
        //variables set to false when building is placed
        timer = refcooldown;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
    }

    void Update()
    {
        //run these functions every frame
        refinedupdate();
        buildingcompletefunction();

        //check if the building is on or off and update the particle and power signs accordingly
        if (GameManager.Instance.power <= 0 && toggleonoff == true && buildingcomplete == true || GameManager.Instance.oil <= 4 && toggleonoff == true && buildingcomplete == true)
        {
            this.transform.GetChild(0).gameObject.SetActive(false);
            this.transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (GameManager.Instance.power > 0 && toggleonoff == true && buildingcomplete == true || GameManager.Instance.oil > 4 && toggleonoff == true && buildingcomplete == true)
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
            this.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
    //this runs the amount of reward for the player and updates the power ect
    public void refinedupdate()
    {
        if (toggleonoff == true && buildingcomplete == true && GameManager.Instance.power >= powerdrain && GameManager.Instance.oil >= oilconsumption)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                GameManager.Instance.power -= powerdrain;
                GameManager.Instance.oil -= oilconsumption;
                GameManager.Instance.money += moneyreward;
                GameManager.Instance.totalmoney += moneyreward;
                timer = refcooldown;
            }
        }
    }

    //function that dictates if the building is complete and built.
    public void buildingcompletefunction()
    {
        if (buildingcomplete == false)
        {
            timer2 += Time.deltaTime;
            if (timer2 >= buildingtime)
            {
                //change building to complete and update the text to the player
                transform.GetChild(0).gameObject.SetActive(true);
                buildingcomplete = true;
                timer2 = 0;
                GameManager.Instance.Messagefunction("Oil Refinary Complete");
            }
        }
    }
    //function that toggles the building on and off
    public void toggleactive()
    {
        if (buildingcomplete == true)
        {
            if (toggleonoff == true)
            {
                //toggle the building off and update the object and player message.
                GameManager.Instance.Messagefunction("You Turn this building off");
                this.transform.GetChild(0).gameObject.SetActive(false);
                this.transform.GetChild(1).gameObject.SetActive(true);
                toggleonoff = false;
            }
            else if (toggleonoff == false)
            {
                //toggle the building on and update the object and player message.
                GameManager.Instance.Messagefunction("You Turn this building on");
                this.transform.GetChild(0).gameObject.SetActive(true);
                this.transform.GetChild(1).gameObject.SetActive(false);
                toggleonoff = true;
            }
        }
    }
}
