using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartMenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
       
	}

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Credits()
    {

    }
    public void Quit()
    {
        Application.Quit();
    }
}
