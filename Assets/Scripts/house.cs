using UnityEngine;
using System.Collections;

public class house : MonoBehaviour {
    private float timer = 0;
    private float timer3 = 0;
    public float popcooldown = 3;
    public float buildingtime = 15;
    public float timer2 = 0;
    private bool buildingcomplete = false;
    public int powerdrain = 1;
    public bool toggleonoff = true;

    // Use this for initialization
    void Start()
    {
        timer = popcooldown;
        GameManager.Instance.maxpopulation += 50;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        popupdate();
        buildingcompletefunction();
        if (GameManager.Instance.power <= 0 && toggleonoff == true)
        {
            this.transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (GameManager.Instance.power > 0 && toggleonoff == true)
        {
            this.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
    public void popupdate()
    {
        if (GameManager.Instance.power <= 0 && buildingcomplete == true || buildingcomplete == true && toggleonoff == false)
        {
            timer3 -= Time.deltaTime;
            if (timer3 <= 0)
            {
                float randomnumber = Random.Range(0, 10);
                Debug.Log(randomnumber);
                if (randomnumber <= 1.0f)
                {
                    GameManager.Instance.population -= 2;
                    GameManager.Instance.Messagefunction("A civilian died of Radiation");
                }
                timer3 = popcooldown;
            }
        }
        if (toggleonoff == true && buildingcomplete == true && GameManager.Instance.power >= powerdrain && GameManager.Instance.population <= GameManager.Instance.maxpopulation - GameManager.Instance.popreward)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                if (GameManager.Instance.food > 0)
                {
                    int rnd = Random.Range(0, 10);
                    if (rnd <= 5)
                    {
                        GameManager.Instance.reward(2);
                    }
                }
                GameManager.Instance.power -= powerdrain;
                timer = popcooldown;
            }
        }
        else if (toggleonoff == true && buildingcomplete == true && GameManager.Instance.power >= powerdrain && GameManager.Instance.population >= GameManager.Instance.maxpopulation)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                GameManager.Instance.power -= powerdrain;
                timer = popcooldown;
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
            GameManager.Instance.Messagefunction("House Complete!");
        }
    }
    public void toggleactive()
    {
        if (buildingcomplete == true)
        {
            if (toggleonoff == true)
            {
                GameManager.Instance.Messagefunction("You Turn this building off");
                this.transform.GetChild(1).gameObject.SetActive(true);
                toggleonoff = false;
            }
            else if (toggleonoff == false)
            {
                GameManager.Instance.Messagefunction("You Turn this building on");
                this.transform.GetChild(1).gameObject.SetActive(false);
                toggleonoff = true;
            }
        }
    }
}
