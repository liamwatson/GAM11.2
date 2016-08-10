using UnityEngine;
using System.Collections;

public class house : MonoBehaviour {
    private float timer = 0;
    public float popcooldown = 3;
    public int popreward = 2;
    public float buildingtime = 15;
    public float timer2 = 0;
    private bool buildingcomplete = false;
    public int powerdrain = 1;

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

    }
    public void popupdate()
    {   
        if (buildingcomplete == true && GameManager.Instance.power >= powerdrain && GameManager.Instance.population <= GameManager.Instance.maxpopulation - popreward)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                if (GameManager.Instance.food > 0)
                {
                    GameManager.Instance.population += popreward;
                }
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
            Debug.Log("Building complete");
        }
    }
}
