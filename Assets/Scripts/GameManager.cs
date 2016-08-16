using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {
    //message variables
    public GameObject MessageGo;
    public Text messagetext;
    //no power variables
    private bool hastriggerd = false;

    //pause variables
    private bool ispaused = false;
    public GameObject pausedgo;

    //music variables
    public AudioSource music;
    public AudioClip daymusic;
    public AudioClip nightmusic;

    //research center variables
    public GameObject researchcentericon;
    public bool researchcenterbuilt = false;
    private bool rbran = false;
    public bool researchcentercomplete = false;
    public GameObject reasearchicon;

    public GameObject Reasearch1;
    public GameObject Reasearch2;
    public GameObject Reasearch3;
    public GameObject Reasearch4;
    public GameObject Reasearch5;
    public GameObject Reasearch6;
    public GameObject Reasearch7;
    public GameObject Reasearch8;

    //timer variables
    public GameObject TimerGameobject;
    public Text hourtext;
    public float TimePerHour = 20;
    public float TimerHour = 0;
    public int powerusedperhour = 2;
    public int nighttime = 20;
    public int morningtime = 7;
    public bool musicchanged = false;
    private bool isnight = false;
    private bool nightcyclefinished = false;
    public int GameHour = 8;
    public Light sun;
    public float powerpercent;

    //reward variables
    public int foodreward = 1;
    public int popreward = 1;
    public int powerreward = 1;
    public int oilreward = 1;

    //no food variables
    public float poplosstimercooldown = 3;
    private float poptimer;
    public int poplosspercent = 10;

    //gui and resrouce variables
    public GameObject FoodGameobject;
    public GameObject MoneyGameobject;
    public GameObject PowerGameobject;
    public GameObject OilGameobject;
    public GameObject Populationgameobject;
    public GameObject MaxPopulationgameobject;

    //resource variables
    public int money = 1000;
    public int food = 25;
    public int maxfood = 100;
    public int power = 0;
    public int maxpower = 100;
    public int oil = 0;
    public int maxoil = 100;
    public int population = 0;
    public int maxpopulation = 0;
    public int foodreqper10pop = 2;
    public int taxmodifier = 3;

    //menu open variable
    public bool Menuopen = false;
    
    //text variables
    public Text FoodText;
    public Text PowerText;
    public Text OilText;
    public Text MoneyText;
    public Text PopulationText;
    public Text MaxPopulationText;

    //singleton declaration
    public static GameManager Instance;

    //singlton declaration
    void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
        //play the morning music and delay it by one second, update the variables 
        music.clip = daymusic;
        music.PlayDelayed(1);
        poptimer = poplosstimercooldown;
        reasearchicon.SetActive(false);
    }

    void Update() {
        //run all these functions every frame
        TimerHour += Time.deltaTime;
        GameTime();
        Nightcycle();
        GuiUpdate();
        nofoodfunction();
        loss();
        researchcenterbuiltfunction();
        minmaxcapfunction();
        win();
        //check if there is power and update the player
        if (power <= 0 && hastriggerd == false)
        { 
            GameManager.Instance.Messagefunction("Warning no power");
            hastriggerd = true;
        }
       else if (power > 0 && hastriggerd == true)
            hastriggerd = false;

    }
    //GUI quit button
    public void quitbutton()
    {
        Application.Quit();
    }

    // a function added towards the end to clamp the min and max amount for GUI purposes
    private void minmaxcapfunction()
    {
        if (food < 0)
            food = 0;

        if (oil < 0)
            oil = 0;

        if (power < 0)
            power = 0;

        if (food > maxfood)
            food = maxfood;

        if (oil > maxoil)
            oil = maxoil;

        if (power > maxpower)
            power = maxpower;
    }
    // a function implemented to display messages to the player, it takes a string and displays it
    public void Messagefunction(string messagetodisplay)
    {
        messagetext = MessageGo.GetComponent<Text>();
        messagetext.text = messagetodisplay;
    }
    // this function switches between paused and not altering Time.timeScale
    public void pausefunction()
    {
        Debug.Log(ispaused);
        if (ispaused == false)
        {
            Time.timeScale = 0;
            pausedgo.SetActive(true);
            ispaused = true;
        }
       else if (ispaused == true)
        {
            Time.timeScale = 1;
            pausedgo.SetActive(false);
            ispaused = false;
        }
    }
    // this makes sure that only one research center can be built, once it is it will turn off the icon GUI so the 
    //player cant see it and will stop the if statment from running (efficency)
    public void researchcenterbuiltfunction()
    {
        if (researchcenterbuilt == true && rbran == false)
        {
            researchcentericon.SetActive(false);
            rbran = true;
        }
    }   
    //win condition for the player if the pop reaches 1500+ then set the playerpref and load the end level
    void win()
    {
        if (population >= 1500)
        {
            PlayerPrefs.SetInt("wincond", 1);
            SceneManager.LoadScene(2);
        }
    }
    //lose condition, if the pop reaches 0 or less then update the player pref and goto end level
    void loss()
    {
        if (population <= 0)
        {
            PlayerPrefs.SetInt("wincond", 2);
            SceneManager.LoadScene(2);
        }
    }

    //if the player has no food then punich the players popilation
    public void nofoodfunction()
    {
        if (poptimer >= 0)
        {
            poptimer -= Time.deltaTime;
        }
       else if (poptimer <= 0)
        {
            if (food <= 0)
            {
                Messagefunction("Your Population is Starving");
                if (population > 100)
                {
                    //if population is over 100 then players will lose a % of its pop per cycle
                    population -= ((population / 100) * (poplosspercent));
                    food = 0;
                    poptimer = poplosstimercooldown;
                }
                else if (population <= 100)
                {
                    //random amount of people die between 0 and 10 when the pop is lower that 100
                    int rnddie = Random.Range(0, 10);
                    population -= rnddie;
                    food = 0;
                    poptimer = poplosstimercooldown;
                }
            }
        }
    }
    
    //this controls the light and darkness for the game giving the effect of day night
    public void Nightcycle()
    {
        if (GameHour >= nighttime)
        {
            if (isnight == false && nightcyclefinished == false)
            {
                if (musicchanged == false)
                {
                    //chances music to night music
                    music.clip = nightmusic;
                    music.PlayDelayed(1);
                    musicchanged = true;
                }
                //checks the intensity of sun and modifies it until its at the required dark amount
                if (sun.intensity > 0.05f)
                {
                    sun.intensity -= 0.002f;
                }
                else
                {
                    //once finished updates to night for the purposes of day kicking back in
                    Messagefunction("Its Night!");
                    musicchanged = false;
                    isnight = true;
                    nightcyclefinished = true;
                }
            }
        }
        //begins the morning cycle
        if (GameHour >= morningtime && GameHour < morningtime + 1 && nightcyclefinished == true)
        {
            if (musicchanged == false)
            {
                //changes music to morning music
                music.clip = daymusic;
                music.PlayDelayed(1);
                musicchanged = true;
            }
            //sets the sun intencity untill its required amount reached
                if (sun.intensity < 1)
            {
                sun.intensity += 0.002f;
            }
            else
            {
                //then morning bools are kicked in and its morning
                Messagefunction("Its Morning!");
                musicchanged = false;
                isnight = false;
                nightcyclefinished = false;
            }
        }
    }
    // this function runs and updates all the text on the GUI and displays it
    public void GuiUpdate()
    {
        FoodText = FoodGameobject.GetComponent<Text>();
        FoodText.text = "Food: " + food;

        MoneyText = MoneyGameobject.GetComponent<Text>();
        MoneyText.text = "$: " + money;

        //power percent basically compairs the max and the current energy to display a % value.
        powerpercent = ((float)power / (float)maxpower)*100;
        PowerText = PowerGameobject.GetComponent<Text>();
        PowerText.text = "Power: " + powerpercent.ToString("n2") + "%";

        OilText = OilGameobject.GetComponent<Text>();
        OilText.text = "Oil: " + oil;

        PopulationText = Populationgameobject.GetComponent<Text>();
        PopulationText.text = "Pop: " + population;

        MaxPopulationText = MaxPopulationgameobject.GetComponent<Text>();
        MaxPopulationText.text = "Max: " + maxpopulation;
    }

    //function that controlls the in game hour (24 hour clock)
    public void GameTime()
    {
        //checks if the day is complete
        if (GameHour >= 24.5f)
        {
            //if it is start back at 0 (looks like a 24 hour clock)
            GameHour = 0;
            hourtext = TimerGameobject.GetComponent<Text>();
            //display it as a 24 hour clock
            hourtext.text = "Time: " + GameHour + ":00";
        }
        //checks if the timer per hour has been met, if it has update the hour
        if (TimerHour >= TimePerHour)
        {
            if (power > 0 )
               { 
                  power -= powerusedperhour;
               }
            if (food > 0)
            {
                //updates the food based on teh population (consumption)
                food -= foodreqper10pop * (population / 10);
                //the function below used to display how much food was being used and still works
                //but i replaced the amount of food consumed with the amount of tax generated every hour
                //Messagefunction("Food Consumed: " + foodreqper10pop * (population / 10));
            }
            //displays the amount of income (tax) the player recieves per in game hour in relation to pop
            Messagefunction("Tax Recieved: " + "$" + (population / 10) * taxmodifier);
            money += (population / 10) * taxmodifier;
            GameHour += 1;
            TimerHour = 0;
            hourtext = TimerGameobject.GetComponent<Text>();
            hourtext.text = "Time: " + GameHour + ":00";
        }
    }
    //reward function used with prefabs inorder to simplify the rewarded resource, when this function is called
    //it is called with an int that dictates what reward the player gets
    public void reward(int whatreward)
    {
        if (whatreward == 1)
        {
            food += foodreward;
        }
        if (whatreward == 2)
        {
            population += popreward;
        }
        if (whatreward == 3)
        {
            power += powerreward;
        }
        if (whatreward == 4)
        {
            oil += oilreward;
        }
    }
    //research that is ran when the button on the reasearch GUI is pressed
    //this function also closes the GUI button too making it so it cant be bought more than once and displays
    //to the user what has been bought and updates the money of the player
    public void researchpowerconsumption()
    {
        if (money >= 500)
        {
            powerusedperhour = powerusedperhour / 2;
            Reasearch1.SetActive(false);
            money -= 500;
            Messagefunction("HQ will now use less power");
        }
    }
    //research that is ran when the button on the reasearch GUI is pressed
    //this function also closes the GUI button too making it so it cant be bought more than once and displays
    //to the user what has been bought and updates the money of the player
    public void researchtax()
    {
        if (money >= 1500)
        {
            taxmodifier += 2;
            Reasearch2.SetActive(false);
            money -= 1500;
            Messagefunction("Tax Increased");
        }
    }
    //research that is ran when the button on the reasearch GUI is pressed
    //this function also closes the GUI button too making it so it cant be bought more than once and displays
    //to the user what has been bought and updates the money of the player
    public void researchsurvival()
    {
        if (money >= 1300)
        {
            poplosstimercooldown += 2;
            Reasearch3.SetActive(false);
            money -= 1300;
            Messagefunction("Better Starvation Survival Researched");
        }
    }
    //research that is ran when the button on the reasearch GUI is pressed
    //this function also closes the GUI button too making it so it cant be bought more than once and displays
    //to the user what has been bought and updates the money of the player
    public void unlockpowerplan()
    {
        if (money >= 2000)
        {
            Reasearch4.SetActive(false);
            reasearchicon.SetActive(true);
            money -= 2000;
            Messagefunction("Power Plant Unlocked");
        }
    }
    //research that is ran when the button on the reasearch GUI is pressed
    //this function also closes the GUI button too making it so it cant be bought more than once and displays
    //to the user what has been bought and updates the money of the player
    public void increasefoodresearch()
    {
        if (money >= 1000)
        {
            Reasearch5.SetActive(false);
            foodreward += 1;
            money -= 1000;
            Messagefunction("Researched Better Food Growing");
        }
    }
    //research that is ran when the button on the reasearch GUI is pressed
    //this function also closes the GUI button too making it so it cant be bought more than once and displays
    //to the user what has been bought and updates the money of the player
    public void increasepowerresearch()
    {
        if (money >= 1300)
        {
            Reasearch6.SetActive(false);
            powerreward += 1;
            money -= 1300;
            Messagefunction("Researched Increased Power Generation");
        }
    }
    //research that is ran when the button on the reasearch GUI is pressed
    //this function also closes the GUI button too making it so it cant be bought more than once and displays
    //to the user what has been bought and updates the money of the player
    public void increasepopgrowth()
    {
        if (money >= 1300)
        {
            Reasearch7.SetActive(false);
            popreward += 1;
            money -= 1300;
            Messagefunction("Researched Higher Population Growth");
        }
    }
    //research that is ran when the button on the reasearch GUI is pressed
    //this function also closes the GUI button too making it so it cant be bought more than once and displays
    //to the user what has been bought and updates the money of the player
    public void increaseoilreward()
    {
        if (money >= 900)
        {
            Reasearch8.SetActive(false);
            oilreward += 1;
            money -= 900;
            Messagefunction("Researched Better Oil Mining");
        }
    }

}
