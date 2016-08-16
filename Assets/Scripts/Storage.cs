using UnityEngine;
using System.Collections;

public class Storage : MonoBehaviour {

    //variables for the amount that will be stored
    public int powerstoragereward = 50;
    public int foodstoragereward = 50;

    
    void Start()
    {
        //combining both the storage buildings into one class that is checking the name of the object and then will give different
        //storage amounts
        if (this.gameObject.name == "Power Storage(Clone)")
        {
            //increase the variables on the gamemanger
            GameManager.Instance.maxpower += powerstoragereward;
        }
        if (this.gameObject.name == "Food Storage(Clone)")
        {
            GameManager.Instance.maxfood += foodstoragereward;
        }
    }
}
