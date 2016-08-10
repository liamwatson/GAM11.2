using UnityEngine;
using System.Collections;

public class Farm : MonoBehaviour {
    private float timer = 0;
    public float foodcooldown = 3;
    public int foodreward = 4;
    public float buildingtime = 15;
    public float timer2 = 0;
    private bool buildingcomplete = false;
    public int powerdrain = 1;

	// Use this for initialization
	void Start () {
        timer = foodcooldown;
        transform.GetChild(0).gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        foodupdate();
        buildingcompletefunction();

    }
    public void foodupdate()
    {
        if(buildingcomplete == true && GameManager.Instance.power >= powerdrain && GameManager.Instance.food <= GameManager.Instance.maxfood && GameManager.Instance.GameHour >= 7 && GameManager.Instance.GameHour < 22)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                GameManager.Instance.food += foodreward;
                GameManager.Instance.power -= powerdrain;
                timer = foodcooldown;
                Debug.Log("food has arrived");
            }
        }
    }
    public void buildingcompletefunction()
    {
        if(buildingcomplete == false)
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
