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
    // Use this for initialization
    void Start()
    {
        timer = refcooldown;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        refinedupdate();
        buildingcompletefunction();

    }
    public void refinedupdate()
    {
        if (buildingcomplete == true && GameManager.Instance.power >= powerdrain && GameManager.Instance.oil >= oilconsumption)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                GameManager.Instance.power -= powerdrain;
                GameManager.Instance.oil -= oilconsumption;
                GameManager.Instance.money += moneyreward;
                timer = refcooldown;
                Debug.Log("Money has arrived");
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
            Debug.Log("Building complete");
        }
    }
}
