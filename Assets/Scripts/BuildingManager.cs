using UnityEngine;
using System.Collections;

public class BuildingManager : MonoBehaviour {
    //variables
    public GameObject HQmenu;
    public GameObject Researchmenu;
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
    public GameObject powerplant;

    //singleton variable
    public static BuildingManager Instance;
    //light variables
    public GameObject l1;
    public GameObject l2;
    public GameObject l4;

    //singleton on awake
    void Awake()
    {
        Instance = this;
    }
	// Update is called once per frame
	void Update () {
        //turns the lights on the HQ on when its night and off when its morning
        if (GameManager.Instance.GameHour >= 22 && GameManager.Instance.power > 0 || GameManager.Instance.GameHour < 7 && GameManager.Instance.power >0)
        {
            l1.SetActive(true);
            l2.SetActive(true);
            l4.SetActive(true);
        }
        else
        {
            l1.SetActive(false);
            l2.SetActive(false);
            l4.SetActive(false);
        }
        
	}

    //if the HQ is clicked bring up the buy menu and update the bool of menuopen
    public void HQMENUCLICKED()
    {
        HQmenu.SetActive(true);
        GameManager.Instance.Menuopen = true;
    }
    //close the buy menu
    public void HQMENUCLOSE()
    {
        HQmenu.SetActive(false);
        GameManager.Instance.Menuopen = false;
    }
    //open the reasearch menu
    public void RESEARCHMENUCLICKED()
    {
        Researchmenu.SetActive(true);
        GameManager.Instance.Menuopen = true;
    }
    //close the research menu
    public void RESEARCHMENUCLOSE()
    {
        Researchmenu.SetActive(false);
        GameManager.Instance.Menuopen = false;
    }
    //if the relating buy button is pressed and the player has enough money, it will set the menuopen bool to false,
    //change the vaurable building placed so the next place the player clicks will build the building, turn the buy menu off
    //if the player does not ahve enough money it will tell them via the messagefunction
    public void Buyfarm()
    {
        if (GameManager.Instance.money >= 650)
        {
            buildingbeingplaced = true;
            buildingtoplace = 1;
            HQmenu.SetActive(false);
            GameManager.Instance.Menuopen = false;
        }
        else
        {
            GameManager.Instance.Messagefunction("You cant afford this");
        }
    }
    //if the relating buy button is pressed and the player has enough money, it will set the menuopen bool to false,
    //change the vaurable building placed so the next place the player clicks will build the building, turn the buy menu off
    //if the player does not ahve enough money it will tell them via the messagefunction
    public void BuySolarpanel()
    {
        if (GameManager.Instance.money >= 250)
        {

            buildingbeingplaced = true;
            buildingtoplace = 2;
            HQmenu.SetActive(false);
            GameManager.Instance.Menuopen = false;
        }
        else
        {
            GameManager.Instance.Messagefunction("You cant afford this");
        }
    }
    //if the relating buy button is pressed and the player has enough money, it will set the menuopen bool to false,
    //change the vaurable building placed so the next place the player clicks will build the building, turn the buy menu off
    //if the player does not ahve enough money it will tell them via the messagefunction
    public void Buyenergystorage()
    {
        if (GameManager.Instance.money >= 300)
        {
            buildingbeingplaced = true;
            buildingtoplace = 3;
            HQmenu.SetActive(false);
            GameManager.Instance.Menuopen = false;
        }
        else
        {
            GameManager.Instance.Messagefunction("You cant afford this");
        }
    }
    //if the relating buy button is pressed and the player has enough money, it will set the menuopen bool to false,
    //change the vaurable building placed so the next place the player clicks will build the building, turn the buy menu off
    //if the player does not ahve enough money it will tell them via the messagefunction
    public void Buyfoodstorage()
    {
        if (GameManager.Instance.money >= 200)
        {
            buildingbeingplaced = true;
            buildingtoplace = 4;
            HQmenu.SetActive(false);
            GameManager.Instance.Menuopen = false;
        }
        else
        {
            GameManager.Instance.Messagefunction("You cant afford this");
        }
    }
    //if the relating buy button is pressed and the player has enough money, it will set the menuopen bool to false,
    //change the vaurable building placed so the next place the player clicks will build the building, turn the buy menu off
    //if the player does not ahve enough money it will tell them via the messagefunction
    public void BuyOilMine()
    {
        if (GameManager.Instance.money >= 500)
        {

            buildingbeingplaced = true;
            buildingtoplace = 5;
            HQmenu.SetActive(false);
            GameManager.Instance.Menuopen = false;
        }
        else
        {
            GameManager.Instance.Messagefunction("You cant afford this");
        }
    }
    //if the relating buy button is pressed and the player has enough money, it will set the menuopen bool to false,
    //change the vaurable building placed so the next place the player clicks will build the building, turn the buy menu off
    //if the player does not ahve enough money it will tell them via the messagefunction
    public void Buyref()
    {
        if (GameManager.Instance.money >= 750)
        {

            buildingbeingplaced = true;
            buildingtoplace = 6;
            HQmenu.SetActive(false);
            GameManager.Instance.Menuopen = false;
        }
        else
        {
            GameManager.Instance.Messagefunction("You cant afford this");
        }
    }
    //if the relating buy button is pressed and the player has enough money, it will set the menuopen bool to false,
    //change the vaurable building placed so the next place the player clicks will build the building, turn the buy menu off
    //if the player does not ahve enough money it will tell them via the messagefunction
    public void Buyhouse()
    {
        if (GameManager.Instance.money >= 400)
        {
            buildingbeingplaced = true;
            buildingtoplace = 7;
            HQmenu.SetActive(false);
            GameManager.Instance.Menuopen = false;
        }
        else
        {
            GameManager.Instance.Messagefunction("You cant afford this");
        }
    }
    //if the relating buy button is pressed and the player has enough money, it will set the menuopen bool to false,
    //change the vaurable building placed so the next place the player clicks will build the building, turn the buy menu off
    //if the player does not ahve enough money it will tell them via the messagefunction
    public void Buyresearch()
    {
        if (GameManager.Instance.money >= 1500)
        {
            buildingbeingplaced = true;
            buildingtoplace = 8;
            HQmenu.SetActive(false);
            GameManager.Instance.Menuopen = false;
        }
        else
        {
            GameManager.Instance.Messagefunction("You cant afford this");
        }
    }
    //if the relating buy button is pressed and the player has enough money, it will set the menuopen bool to false,
    //change the vaurable building placed so the next place the player clicks will build the building, turn the buy menu off
    //if the player does not ahve enough money it will tell them via the messagefunction
    public void BuyPowerplant()
    {
        if (GameManager.Instance.money >= 1200)
        {
            buildingbeingplaced = true;
            buildingtoplace = 9;
            HQmenu.SetActive(false);
            GameManager.Instance.Menuopen = false;
        }
        else
        {
            GameManager.Instance.Messagefunction("You cant afford this");
        }
    }
    //this function when ran determins the value of a variable and then will place the building based on that, a vector3 is argued in for creation spot
    public void placebuilding(Vector3 clickedinput)
    {
        //the rest of the of statments in this function then
        //check the buildingtoplace variable, updates the players money
        //instantiates the required building based on the buildingtoplace variable
        //then sets the buildingplaced bool to false (as the building building cylce is complete)
        //it then plays the audio of the building being constructed
        if (buildingtoplace == 1)
        {
            GameManager.Instance.money -= 650;
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
            GameManager.Instance.researchcenterbuilt = true;
            researchcenterbuild = true; 
            buildingbeingplaced = false;

        }
        if (buildingtoplace == 9)
        {
            GameManager.Instance.money -= 1200;
            clicked = clickedinput;
            Instantiate(powerplant, clicked, transform.rotation);
            buildingbeingplaced = false;
        }
        AudioSource.PlayClipAtPoint(constructionsound, clicked, 5f);
    }
}
