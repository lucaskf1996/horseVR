using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private static GameManager _instance;

    // public float timeSpeed;
    // public float gameSpeed;
    // public float alterSpeed;
    // public int playerLives;
    // public int playerScore;
    // public int highscore;
    // public bool lostLife;
    // public bool gainedLife;
    public bool move;
    
    public static GameManager GetInstance()
    {
        if(_instance == null)
        {
            _instance = new GameManager();
        }

        return _instance;
    }

    private GameManager()
    {
        move = false;
    }

    // public void Reset()
    // {
    //     SaveHighscore();
    //     playerLives = 2;
    //     playerScore = 0;
    // }

    // public void SaveHighscore()
    // {
        
    //     if(playerScore > highscore) highscore = playerScore;
    //     PlayerPrefs.SetInt("Highscore", highscore);
    // }

    // public int LoadHighscore()
    // {
    //     if(!PlayerPrefs.HasKey("Highscore")){
    //         PlayerPrefs.SetInt("Highscore", 0);
    //     }
    //     highscore = PlayerPrefs.GetInt("Highscore");
    //     return highscore;
    // }
}