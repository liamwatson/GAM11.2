using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {
    //message variables
    public GameObject MessageGo;
    public Text messagetext;

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

    public Text FoodText;
    public Text PowerText;
    public Text OilText;
    public Text MoneyText;
    public Text PopulationText;
    public Text MaxPopulationText;

    //singleton declaration
    public static GameManager Instance;

    void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
        music.clip = daymusic;
        music.PlayDelayed(1);
        poptimer = poplosstimercooldown;
        reasearchicon.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
        TimerHour += Time.deltaTime;
        GameTime();
        Nightcycle();
        GuiUpdate();
        nofoodfunction();
        loss();
        researchcenterbuiltfunction();
        if(power <= 0)
            GameManager.Instance.Messagefunction("Warning no power, Production stopped");
    }

    public void Messagefunction(string messagetodisplay)
    {
        messagetext = MessageGo.GetComponent<Text>();
        messagetext.text = messagetodisplay;
    }

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

    public void researchcenterbuiltfunction()
    {
        if (researchcenterbuilt == true && rbran == false)
        {
            researchcentericon.SetActive(false);
            rbran = true;
        }
}
    void win()
    {
        if (population >= 1500)
        {
            PlayerPrefs.SetInt("wincond", 1);
            SceneManager.LoadScene(2);
        }
    }
    void loss()
    {
        if (population <= 0)
        {
            PlayerPrefs.SetInt("wincond", 2);
            SceneManager.LoadScene(2);
        }
    }
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
                    population -= ((population / 100) * (poplosspercent));
                    food = 0;
                    poptimer = poplosstimercooldown;
                }
                else if (population <= 100)
                {
                    population -= 10;
                    food = 0;
                    poptimer = poplosstimercooldown;
                }
            }
        }
    }
    

    public void Nightcycle()
    {
        if (GameHour >= nighttime)
        {
            if (isnight == false && nightcyclefinished == false)
            {
                if (musicchanged == false)
                {
                    music.clip = nightmusic;
                    music.PlayDelayed(1);
                    musicchanged = true;
                }
                if (sun.intensity > 0.15f)
                {
                    sun.intensity -= 0.00050f;
                }
                else
                {
                    Messagefunction("Its Night!");
                    musicchanged = false;
                    isnight = true;
                    nightcyclefinished = true;
                }
            }
        }
        if (GameHour >= morningtime && GameHour < morningtime + 1 && nightcyclefinished == true)
        {
            if (musicchanged == false)
            {
                music.clip = daymusic;
                music.PlayDelayed(1);
                musicchanged = true;
            }
                if (sun.intensity < 1)
            {
                sun.intensity += 0.00050f;
            }
            else
            {
                Messagefunction("Its Morning!");
                musicchanged = false;
                isnight = false;
                nightcyclefinished = false;
            }
        }
    }
    public void GuiUpdate()
    {
        FoodText = FoodGameobject.GetComponent<Text>();
        FoodText.text = "Food: " + food;

        MoneyText = MoneyGameobject.GetComponent<Text>();
        MoneyText.text = "$: " + money;

        PowerText = PowerGameobject.GetComponent<Text>();
        PowerText.text = "Power: " + power;

        OilText = OilGameobject.GetComponent<Text>();
        OilText.text = "Oil: " + oil;

        PopulationText = Populationgameobject.GetComponent<Text>();
        PopulationText.text = "Pop: " + population;

        MaxPopulationText = MaxPopulationgameobject.GetComponent<Text>();
        MaxPopulationText.text = "Max: " + maxpopulation;
    }
    public void GameTime()
    {
        if (GameHour >= 24.5f)
        {
            Messagefunction("Tax Recieved: " + "$" + population * taxmodifier);
            money += population * taxmodifier;
            GameHour = 0;
            hourtext = TimerGameobject.GetComponent<Text>();
            hourtext.text = "Time: " + GameHour + ":00";
        }
        if (TimerHour >= TimePerHour)
        {
            if (power > 0 )
               { 
                  power -= powerusedperhour;
               }
            if (food > 0)
            {
                food -= foodreqper10pop * (population / 10);
                Messagefunction("Food Consumed: " + foodreqper10pop * (population / 10));
            }
            GameHour += 1;
            TimerHour = 0;
            hourtext = TimerGameobject.GetComponent<Text>();
            hourtext.text = "Time: " + GameHour + ":00";
        }
    }
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
    public void researchtax()
    {
        if (money >= 1500)
        {
            taxmodifier += 2;
            Reasearch2.SetActive(false);
            money -= 500;
            Messagefunction("Tax Increased");
        }
    }
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
