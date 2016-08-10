using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {

    //music variables
    public AudioSource music;
    public AudioClip daymusic;
    public AudioClip nightmusic;

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
    public int foodreqper10pop = 1;
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
	}
	
	// Update is called once per frame
	void Update () {
        TimerHour += Time.deltaTime;
        GameTime();
        Nightcycle();
        GuiUpdate();
        nofoodfunction();
        loss();
    }
    void loss()
    {
        if (population <= 0)
        {
            SceneManager.LoadScene(2);
        }
    }
    public void nofoodfunction()
    {
        if (population < 0)
        {
            population = 0;
        }
        if (poptimer >= 0)
        {
            poptimer -= Time.deltaTime;
        }
       else if (poptimer <= 0)
        {
            if (food <= 0)
            {
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
            }
            GameHour += 1;
            TimerHour = 0;
            hourtext = TimerGameobject.GetComponent<Text>();
            hourtext.text = "Time: " + GameHour + ":00";
        }
    }
}
