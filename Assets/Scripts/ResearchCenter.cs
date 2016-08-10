using UnityEngine;
using System.Collections;

public class ResearchCenter : MonoBehaviour {
    private float timer = 0;
    public float timercoolcooldown = 2;
    public int reward = 3;
    public float buildingtime = 20;
    public float timer2 = 0;
    private bool buildingcomplete = false;
    public int powerdrain = 5;
    // Use this for initialization
    void Start()
    {
        timer = timercoolcooldown;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        functionupdate();
        buildingcompletefunction();

    }
    public void functionupdate()
    {
        if (buildingcomplete == true && GameManager.Instance.power > powerdrain)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                GameManager.Instance.power -= powerdrain;
                //GameManager.Instance.oil += reward;
                timer = timercoolcooldown;
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

