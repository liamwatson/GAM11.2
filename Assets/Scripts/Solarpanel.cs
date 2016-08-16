using UnityEngine;
using System.Collections;

public class Solarpanel : MonoBehaviour {

    //declare private variables
    private float timer = 0;

    //declare public variables
    public float powercooldown = 2;
    public float buildingtime = 10;
    public float timer2 = 0;

    private bool buildingcomplete = false;

    // Use this for initialization
    void Start()
    {
        timer = powercooldown;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //run these functions every frame
        powerupdate();
        buildingcompletefunction();
    }

    //main function, this will check if the building is complete, if its day and reward the player with power
    public void powerupdate()
    {
        if (buildingcomplete == true && GameManager.Instance.GameHour >= 7 && GameManager.Instance.GameHour < 22 && GameManager.Instance.power <= GameManager.Instance.maxpower)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                GameManager.Instance.reward(3);
                timer = powercooldown;
            }
        }
    }

    //function initially used in order to decide when the building is complete
    public void buildingcompletefunction()
    {
        if (buildingcomplete == false)
            timer2 += Time.deltaTime;
        if (timer2 >= buildingtime)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            buildingcomplete = true;
            timer2 = 0;
            GameManager.Instance.Messagefunction("Solar Panel Complete");
        }
    }
}

