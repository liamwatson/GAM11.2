using UnityEngine;
using System.Collections;

public class house : MonoBehaviour {
    //private variables
    private float timer = 0;
    private float timer3 = 0;
    private bool buildingcomplete = false;

    //public variables
    public float popcooldown = 3;
    public float buildingtime = 15;
    public float timer2 = 0;

    public int powerdrain = 1;

    public bool toggleonoff = true;

    // Use this for initialization
    void Start()
    {
        //start of the game update the appropriate objects and variables
        timer = popcooldown;
        GameManager.Instance.maxpopulation += 50;
        GameManager.Instance.houseamountai += 1;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    void Update()
    {
        //run functions and child objects that give visual display to the player
        popupdate();
        buildingcompletefunction();
        if (GameManager.Instance.power <= 0 && toggleonoff == true)
        {
            this.transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (GameManager.Instance.power > 0 && toggleonoff == true)
        {
            this.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
    //main house function that runs if the player has power and building is on and complete
    public void popupdate()
    {
        //if there is no power but the building is complete OR if the building is complete and turned off then run this
        if (GameManager.Instance.power <= 0 && buildingcomplete == true || buildingcomplete == true && toggleonoff == false)
        {
            timer3 -= Time.deltaTime;
            if (timer3 <= 0)
            {
                //10% chance of killing a pop every cycle
                float randomnumber = Random.Range(0, 10);
                if (randomnumber <= 3)
                {
                    GameManager.Instance.population -= 1;
                    //update and let the player know a person has died
                    GameManager.Instance.Messagefunction("A civilian died of Radiation");
                }
                //reset the timer
                timer3 = popcooldown;
            }
        }
        //if building has power food and is on
        if (toggleonoff == true && buildingcomplete == true && GameManager.Instance.power >= powerdrain && GameManager.Instance.population <= GameManager.Instance.maxpopulation - GameManager.Instance.popreward)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                if (GameManager.Instance.food > 0)
                {
                    //50% chance of spawning a pop
                    int rnd = Random.Range(0, 10);
                    if (rnd <= 5)
                    {
                        GameManager.Instance.reward(2);
                    }
                }
                //take the power from the building every cycle
                GameManager.Instance.power -= powerdrain;
                timer = popcooldown;
            }
        }
        //if the building is on and complete still take power even if pop is full
        else if (toggleonoff == true && buildingcomplete == true && GameManager.Instance.power >= powerdrain && GameManager.Instance.population >= GameManager.Instance.maxpopulation)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                GameManager.Instance.power -= powerdrain;
                timer = popcooldown;
            }
        }
    }
    //function that controlls if the building is complete
    public void buildingcompletefunction()
    {
        if (buildingcomplete == false)
            timer2 += Time.deltaTime;
        if (timer2 >= buildingtime)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            buildingcomplete = true;
            timer2 = 0;
            //let the player know the building is complete
            GameManager.Instance.Messagefunction("House Complete!");
        }
    }
    //function that is used by the ray cast to toggle the building on and off
    public void toggleactive()
    {
        if (buildingcomplete == true)
        {
            if (toggleonoff == true)
            {
                //turn the building and objects off
                GameManager.Instance.Messagefunction("You Turn this building off");
                this.transform.GetChild(1).gameObject.SetActive(true);
                toggleonoff = false;
            }
            else if (toggleonoff == false)
            {
                //turn the building and objects on
                GameManager.Instance.Messagefunction("You Turn this building on");
                this.transform.GetChild(1).gameObject.SetActive(false);
                toggleonoff = true;
            }
        }
    }
}
