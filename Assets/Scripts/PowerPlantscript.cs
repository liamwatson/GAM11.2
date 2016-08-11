using UnityEngine;
using System.Collections;

public class PowerPlant : MonoBehaviour {
    private float timer = 0;
    public float cooldown = 1;
    public float buildingtime = 75;
    public float timer2 = 0;
    private bool buildingcomplete = false;
    public int oilconsumption = 1;
    // Use this for initialization
    void Start()
    {
        timer = cooldown;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        powerplantupdate();
        buildingcompletefunction();

    }
    public void powerplantupdate()
    {
        if (buildingcomplete == true && GameManager.Instance.oil >= oilconsumption && GameManager.Instance.power >0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                GameManager.Instance.reward(3);
                GameManager.Instance.oil -= oilconsumption;
                timer = cooldown;
            }
        }
    }
    public void buildingcompletefunction()
    {
        if (buildingcomplete == false)
            timer2 += Time.deltaTime;
        if (timer2 >= buildingtime)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            buildingcomplete = true;
            timer2 = 0;
            GameManager.Instance.Messagefunction("Power Plant Complete!");
            
        }
    }
}
