using UnityEngine;
using System.Collections;

public class Oilref : MonoBehaviour {
    //private variables
    private float timer = 0;
    private bool buildingcomplete = false;

    //public variables
    public float oilcooldown = 2;
    public float buildingtime = 20;
    public float timer2 = 0;
 
    public int powerdrain = 3;

    public bool toggleonoff = true;

    // Use this for initialization
    void Start()
    {
        //set cooldowns and gameobject status
        timer = oilcooldown;
        transform.GetChild(1).gameObject.SetActive(false);
    }

    void Update()
    {
        //run the functions
        oilupdate();
        buildingcompletefunction();
        if (GameManager.Instance.power <= 0)
        {
            //if the power is 0 then set the smoke off
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    //reward the player each update (cooldown) if the power requirements and building compeleted.
    public void oilupdate()
    {
        if (toggleonoff == true && buildingcomplete == true && GameManager.Instance.power > 0 && GameManager.Instance.oil <= GameManager.Instance.maxoil)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                //update the variables.
                GameManager.Instance.power -= powerdrain;
                GameManager.Instance.reward(4);
                timer = oilcooldown;
            }
        }

    }
    //function that overlooks if the building has been completed
    public void buildingcompletefunction()
    {
        if (buildingcomplete == false)
            timer2 += Time.deltaTime;
        if (timer2 >= buildingtime)
        {
            //when building is finished building then set the appropriate bools and child objects to built state
            transform.GetChild(1).gameObject.SetActive(true);
            buildingcomplete = true;
            timer2 = 0;
            GameManager.Instance.Messagefunction("Oil Drill Complete");
        }
    }

    //function thats toggled when the object is selected
    public void toggleactive()
    {
        if (buildingcomplete == true)
        {
            if (toggleonoff == true)
            {
                //run this when building is on but want to turn it off
                GameManager.Instance.Messagefunction("You Turn this building off");
                this.transform.GetChild(1).gameObject.SetActive(false);
                toggleonoff = false;
            }
            else if (toggleonoff == false && GameManager.Instance.power >0)
            {
                //run this when building is off but want it on
                GameManager.Instance.Messagefunction("You Turn this building on");
                this.transform.GetChild(1).gameObject.SetActive(true);
                toggleonoff = true;
            }
        }
    }
}
