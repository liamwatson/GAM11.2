using UnityEngine;
using System.Collections;

public class Farm : MonoBehaviour {
    //private variables
    private float timer = 0;
    private bool buildingcomplete = false;

    //public variables
    public float foodcooldown = 3;
    public float buildingtime = 15;
    public float timer2 = 0;
    
    public int powerdrain = 1;

	void Start () {
        //set the initilisation of game objects
        timer = foodcooldown;
        transform.GetChild(1).gameObject.SetActive(false);
        GameManager.Instance.farmsamountai += 1;
	}
	void Update () {
        //run the functions every frame
        foodupdate();
        buildingcompletefunction();

    }
    //if its day and the farm has power and hasnt reached full amount it will produce food and add it to the game manager
    public void foodupdate()
    {
        if(buildingcomplete == true && GameManager.Instance.power >= powerdrain && GameManager.Instance.food <= GameManager.Instance.maxfood && GameManager.Instance.GameHour >= GameManager.Instance.morningtime && GameManager.Instance.GameHour < GameManager.Instance.nighttime)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                //run the reward function (rewards amounts of resources based on the int argued in)
                GameManager.Instance.reward(1);
                //drains power every cycle
                GameManager.Instance.power -= powerdrain;
                timer = foodcooldown;
            }
        }
    }
    // controlls the building completed, this will use a timer to tell how long the bulding has been placed and then will activate it
    public void buildingcompletefunction()
    {
        //this makes sure that the timer is not ran after the building is complete (efficiency)
        if(buildingcomplete == false)
        timer2 += Time.deltaTime;
        if (timer2 >= buildingtime)
        {
            //once the building is complete update the objects and bools accordingly
            transform.GetChild(1).gameObject.SetActive(true);
            buildingcomplete = true;
            timer2 = 0;
            //update the player that the farm has finished
            GameManager.Instance.Messagefunction("Farm Complete");
        }
    }
}
