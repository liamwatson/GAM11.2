using UnityEngine;
using System.Collections;

public class OilRefine : MonoBehaviour {
    private float timer = 0;
    public float refcooldown = 2;
    public int moneyreward = 50;
    public float buildingtime = 30;
    public float timer2 = 0;
    private bool buildingcomplete = false;
    public int powerdrain = 5;
    public int oilconsumption = 5;
    public bool toggleonoff = true;
    // Use this for initialization
    void Start()
    {
        timer = refcooldown;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        refinedupdate();
        buildingcompletefunction();
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
                timer = refcooldown;
            }
        }
    }
    public void buildingcompletefunction()
    {
        if (buildingcomplete == false)
        {
            timer2 += Time.deltaTime;
            if (timer2 >= buildingtime)
            {
                transform.GetChild(0).gameObject.SetActive(true);
                buildingcomplete = true;
                timer2 = 0;
                GameManager.Instance.Messagefunction("Oil Refinary Complete");
            }
        }
    }
    public void toggleactive()
    {
        if (buildingcomplete == true)
        {
            if (toggleonoff == true)
            {
                GameManager.Instance.Messagefunction("You Turn this building off");
                this.transform.GetChild(0).gameObject.SetActive(false);
                this.transform.GetChild(1).gameObject.SetActive(true);
                toggleonoff = false;
            }
            else if (toggleonoff == false)
            {
                GameManager.Instance.Messagefunction("You Turn this building on");
                this.transform.GetChild(0).gameObject.SetActive(true);
                this.transform.GetChild(1).gameObject.SetActive(false);
                toggleonoff = true;
            }
        }
    }
}
