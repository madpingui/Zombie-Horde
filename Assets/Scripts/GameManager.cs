using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private int score;
    public Text scoreText;
    public GameObject lostPanel;

    public Slider expbar;
    public Text levelText;

    public static GameManager Instance { set; get; }

    public void Awake()
    {
        scoreText.text = "Score: " + score.ToString("0");
        Instance = this;
    }

    public void AddScore()
    {
        score += 1;
        scoreText.text = "Score: " + score.ToString("0");
    }

    public void GameOver()
    {
        lostPanel.SetActive(true);
        if(score > PlayerPrefs.GetInt("Hiscore"))
        {
            PlayerPrefs.SetInt("Hiscore", score);
        }
    }

    public void Replay()
    {
        SceneManager.LoadScene("Inicio");
    }

    public void SetPlayerExperience(float percentToLevel, int playerLevel)
    {
        levelText.text = "Level: " + playerLevel;
        expbar.value = percentToLevel;
    }
}
