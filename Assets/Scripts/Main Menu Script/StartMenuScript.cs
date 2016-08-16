using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]

public class StartMenuScript : MonoBehaviour {
    //variables
    public float timer = 45;
    public GameObject moviescreen;
    public MovieTexture movietext;
    public AudioSource audiosource;
    public AudioClip maintheme;
    bool mainthemeloaded = false;

	void Start () {
        //set the audio source to the movie clip and play the movie clip and audio source.
        audiosource.clip = movietext.audioClip;
        movietext.Play();
        audiosource.Play();
	}
	void Update () {
        //run the timer
        timer -= Time.deltaTime;

        //check if the main theme has loaded if not run this
        if (timer <= 0 && mainthemeloaded == false)
        {
            //if the timer is out then load the music for the start screen
            audiosource.clip = maintheme;
            audiosource.Play();
            //close the movie window allowing the player to access the buttons and GUI of start screen
            moviescreen.SetActive(false);
            mainthemeloaded = true;
        }
	}
    //start game button
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    //quit button
    public void Quit()
    {
        Application.Quit();
    }
}
