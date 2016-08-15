﻿using UnityEngine;
using System.Collections;

public class Storage : MonoBehaviour {
    public int powerstoragereward = 50;
    public int foodstoragereward = 50;

    // Use this for initialization
    void Start()
    {
        if (this.gameObject.name == "Power Storage(Clone)")
        {
            GameManager.Instance.maxpower += powerstoragereward;
        }
        if (this.gameObject.name == "Food Storage(Clone)")
        {
            GameManager.Instance.maxfood += foodstoragereward;
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
}
