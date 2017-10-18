using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public Text highScore;

	void Start ()
    {
        HighScoreFunction();
	}
	

	void Update ()
    {
		
	}

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    void HighScoreFunction()
    {
        highScore.text = PlayerPrefs.GetInt("HighScore").ToString();
    }
}
