using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class endmanager : MonoBehaviour {
    private int endresult;
    public GameObject losspic;
    public GameObject textobj;
    public Text text;

	// Use this for initialization
	void Start () {
        endresult = PlayerPrefs.GetInt("wincond");
        if (endresult == 1)
        {
            text = textobj.GetComponent<Text>();
            text.text = "Humanity is Saved!";
        }
        else if (endresult == 2)
        {
            text = textobj.GetComponent<Text>();
            text.text = "Humanity is Extinct!";
            losspic.SetActive(true);
        }
    }
    public void mainmenu()
    {
        SceneManager.LoadScene(0);
    }

    public void exit()
    {
        Application.Quit();
    }
}
