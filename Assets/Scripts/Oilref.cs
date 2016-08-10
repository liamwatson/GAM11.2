﻿using UnityEngine;
using System.Collections;

public class Oilref : MonoBehaviour {
    private float timer = 0;
    public float oilcooldown = 2;
    public int oilreward = 3;
    public float buildingtime = 20;
    public float timer2 = 0;
    private bool buildingcomplete = false;
    public int powerdrain = 3;
    // Use this for initialization
    void Start()
    {
        timer = oilcooldown;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        oilupdate();
        buildingcompletefunction();

    }
    public void oilupdate()
    {
        if (buildingcomplete == true && GameManager.Instance.power > powerdrain && GameManager.Instance.oil <= GameManager.Instance.maxoil)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                GameManager.Instance.power -= powerdrain;
                GameManager.Instance.oil += oilreward;
                timer = oilcooldown;
                Debug.Log("Oil has arrived");
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
