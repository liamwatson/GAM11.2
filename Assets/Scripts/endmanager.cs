using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class endmanager : MonoBehaviour {
    //variables
    private int endresult;
    public GameObject losspic;
    public GameObject textobj;
    public Text text;

	// Use this for initialization
	void Start () {
        //load a playerpref to see what the result was from the main game
        endresult = PlayerPrefs.GetInt("wincond");
        if (endresult == 1)
        {
            //if the player pref is 1 then the player wins the game
            text = textobj.GetComponent<Text>();
            text.text = "Humanity is Saved!";
        }
        else if (endresult == 2)
        {
            //if end result is 2 then the player looses the game
            text = textobj.GetComponent<Text>();
            text.text = "Humanity is Extinct!";
            losspic.SetActive(true);
        }
    }
    //button to load the main menu
    public void mainmenu()
    {
        SceneManager.LoadScene(0);
    }
    //button the exit the game
    public void exit()
    {
        Application.Quit();
    }
}
