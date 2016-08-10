using UnityEngine;
using System.Collections;

public class BuildingManager : MonoBehaviour {
    public GameObject HQmenu;
    public Vector3 clicked;
    public bool buildingbeingplaced = false;
    public int buildingtoplace = 0;
    public bool researchcenterbuild = false;
    //sound variable
    public AudioClip constructionsound;

    //building variables
    public GameObject farm;
    public GameObject solarpanel;
    public GameObject foodstorage;
    public GameObject energystorage;
    public GameObject OilMiner;
    public GameObject OilRefinery;
    public GameObject house;
    public GameObject researchcenter;

    public static BuildingManager Instance;
    //light variables
    public GameObject l1;
    public GameObject l2;
    public GameObject l3;
    public GameObject l4;

    void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.Instance.GameHour >= 22 && GameManager.Instance.power > 0 || GameManager.Instance.GameHour < 7 && GameManager.Instance.power >0)
        {
            l1.SetActive(true);
            l2.SetActive(true);
            l3.SetActive(true);
            l4.SetActive(true);
        }
        else
        {
            l1.SetActive(false);
            l2.SetActive(false);
            l3.SetActive(false);
            l4.SetActive(false);
        }
        
	}

    public void HQMENUCLICKED()
    {
        HQmenu.SetActive(true);
    }
    public void HQMENUCLOSE()
    {
        HQmenu.SetActive(false);
    }
   
    public void Buyfarm()
    {
        if (GameManager.Instance.money >= 350)
        {
            
            buildingbeingplaced = true;
            buildingtoplace = 1;
            HQmenu.SetActive(false);
        }
    }
    public void BuySolarpanel()
    {
        if (GameManager.Instance.money >= 250)
        {

            buildingbeingplaced = true;
            buildingtoplace = 2;
            HQmenu.SetActive(false);
        }
    }
    public void Buyenergystorage()
    {
        if (GameManager.Instance.money >= 300)
        {
            buildingbeingplaced = true;
            buildingtoplace = 3;
            HQmenu.SetActive(false);
        }
    }
    public void Buyfoodstorage()
    {
        if (GameManager.Instance.money >= 200)
        {
            buildingbeingplaced = true;
            buildingtoplace = 4;
            HQmenu.SetActive(false);
        }
    }
    public void BuyOilMine()
    {
        if (GameManager.Instance.money >= 500)
        {

            buildingbeingplaced = true;
            buildingtoplace = 5;
            HQmenu.SetActive(false);
        }
    }
    public void Buyref()
    {
        if (GameManager.Instance.money >= 750)
        {

            buildingbeingplaced = true;
            buildingtoplace = 6;
            HQmenu.SetActive(false);
        }
    }
    public void Buyhouse()
    {
        if (GameManager.Instance.money >= 400)
        {
            buildingbeingplaced = true;
            buildingtoplace = 7;
            HQmenu.SetActive(false);
        }
    }
    public void Buyresearch()
    {
        if (GameManager.Instance.money >= 1500)
        {
            buildingbeingplaced = true;
            buildingtoplace = 8;
            HQmenu.SetActive(false);
        }
    }
    public void placebuilding(Vector3 clickedinput)
    {
        if (buildingtoplace == 1)
        {
            GameManager.Instance.money -= 350;
            clicked = clickedinput;
            Instantiate(farm, clicked, transform.rotation);
            buildingbeingplaced = false;
        }
        if (buildingtoplace == 2)
        {
            GameManager.Instance.money -= 250;
            clicked = clickedinput;
            Instantiate(solarpanel, clicked, transform.rotation);
            buildingbeingplaced = false;
        }
        if (buildingtoplace == 3)
        {
            GameManager.Instance.money -= 300;
            clicked = clickedinput;
            Instantiate(energystorage, clicked, transform.rotation);
            buildingbeingplaced = false;
        }
        if (buildingtoplace == 4)
        {
            GameManager.Instance.money -= 200;
            clicked = clickedinput;
            Instantiate(foodstorage, clicked, transform.rotation);
            buildingbeingplaced = false;
        }
        if (buildingtoplace == 5)
        {
            GameManager.Instance.money -= 500;
            clicked = clickedinput;
            Instantiate(OilMiner, clicked, transform.rotation);
            buildingbeingplaced = false;
        }
        if (buildingtoplace == 6)
        {
            GameManager.Instance.money -= 750;
            clicked = clickedinput;
            Instantiate(OilRefinery, clicked, transform.rotation);
            buildingbeingplaced = false;
        }
        if (buildingtoplace == 7)
        {
            GameManager.Instance.money -= 400;
            clicked = clickedinput;
            Instantiate(house, clicked, transform.rotation);
            buildingbeingplaced = false;
        }
        if (buildingtoplace == 8)
        {
            GameManager.Instance.money -= 1500;
            clicked = clickedinput;
            Instantiate(researchcenter, clicked, transform.rotation);
            researchcenterbuild = true; 
            buildingbeingplaced = false;

        }
        AudioSource.PlayClipAtPoint(constructionsound, clicked, 5f);
    }
}
