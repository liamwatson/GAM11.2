using UnityEngine;
using System.Collections;

public class PowerPlant : MonoBehaviour
{
    //declare private variables
    private float timer = 0;
    private bool buildingcomplete = false;

    //declare public variables
    public float cooldown = 1;
    public float buildingtime = 75;
    public float timer2 = 0;
   
    public int oilconsumption = 1;

    public bool toggleonoff = true;

    
    void Start()
    {
        //set the timer to the cooldown when the building is placed
        timer = cooldown;
        //make sure the childobject is not active
        transform.GetChild(0).gameObject.SetActive(false);
    }

    void Update()
    {
        //run the functions every frame
        powerplantupdate();
        buildingcompletefunction();
    }

    //main function, checks if the building has the requirements and then will produce power.
    public void powerplantupdate()
    {
        if (toggleonoff == true && buildingcomplete == true && GameManager.Instance.oil >= oilconsumption && GameManager.Instance.power > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                //reward the player
                GameManager.Instance.reward(3);
                GameManager.Instance.oil -= oilconsumption;
                timer = cooldown;
            }
        }
    }

    //building complete function that oversees the building being finalised and finished
    public void buildingcompletefunction()
    {
        if (buildingcomplete == false)
            timer2 += Time.deltaTime;
        if (timer2 >= buildingtime)
        {
            //sets booleans and sets child object to true (visably different)
            transform.GetChild(0).gameObject.SetActive(true);
            buildingcomplete = true;
            timer2 = 0;
            GameManager.Instance.Messagefunction("Power Plant Complete!");
        }
    }

    //this building can be clicked and activated and decativated
    public void toggleactive()
    {
        if (buildingcomplete == true)
        {
            if (toggleonoff == true)
            {
                //if the building is on and clicked run this, decativates the building and sets child power simble to true
                GameManager.Instance.Messagefunction("You Turn this building off");
                this.transform.GetChild(1).gameObject.SetActive(true);
                toggleonoff = false;
            }
            else if (toggleonoff == false)
            {
                //if the building is off then it turns it on
                GameManager.Instance.Messagefunction("You Turn this building on");
                this.transform.GetChild(1).gameObject.SetActive(false);
                toggleonoff = true;
            }
        }
    }
}