using UnityEngine;
using System.Collections;

public class Solarpanel : MonoBehaviour {

    private float timer = 0;
    public float powercooldown = 2;
    public int powerreward = 3;
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
        powerupdate();
        buildingcompletefunction();

    }
    public void powerupdate()
    {
        if (buildingcomplete == true && GameManager.Instance.GameHour >= 7 && GameManager.Instance.GameHour < 22 && GameManager.Instance.power <= GameManager.Instance.maxpower)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                GameManager.Instance.power += powerreward;
                timer = powercooldown;
                Debug.Log("power has arrived");
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

