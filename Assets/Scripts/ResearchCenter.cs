using UnityEngine;
using System.Collections;

public class ResearchCenter : MonoBehaviour {

    //private variables declaration
    private float timer = 0;
    private bool buildingcomplete = false;

    //public variables declaration
    public float timercoolcooldown = 2;
    public float buildingtime = 20;
    public float timer2 = 0;

    public int reward = 3;
    public int powerdrain = 5;

    // Use this for initialization
    void Start()
    {
        //run these at start.
        timer = timercoolcooldown;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    void Update()
    {
        //run these functions every frame
        functionupdate();
        buildingcompletefunction();

    }

    //this function makes sure the research center is using power.
    public void functionupdate()
    {
        if (buildingcomplete == true && GameManager.Instance.power > powerdrain)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                //changes the power of the gamemanager and then resets the timer
                GameManager.Instance.power -= powerdrain;
                timer = timercoolcooldown;
            }
        }
    }

    //this function dictates when the building is complete
    public void buildingcompletefunction()
    {
        if (buildingcomplete == false)
            timer2 += Time.deltaTime;
        if (timer2 >= buildingtime)
        {
            //this changes the booleans so the building can be places, closes the buy menu and sets the child object to active
            // to give the impression its built
            GameManager.Instance.researchcentercomplete = true;
            transform.GetChild(0).gameObject.SetActive(true);
            buildingcomplete = true;
            timer2 = 0;
            GameManager.Instance.Messagefunction("Research Center Complete");
        }
    }
}

