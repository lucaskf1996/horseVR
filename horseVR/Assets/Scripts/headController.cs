using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class headController : MonoBehaviour
{
    GameManager gm;
    wallController walls;
    private Text CanvasHelperLives;
    private string[] horse = new string[6];

    // Start is called before the first frame update
    void Start()
    {
        CanvasHelperLives = GameObject.FindWithTag("CanvasLives").GetComponent<Text>();
        gm = GameManager.GetInstance();
        walls = GameObject.Find("Walls").GetComponent<wallController>();
        horse[5] = "";
        horse[4] = "H";
        horse[3] = "HO";
        horse[2] = "HOR";
        horse[1] = "HORS";
        horse[0] = "HORSE";
    }

    void OnTriggerEnter(Collider col)
    {
        if(gm.gameState == GameManager.GameState.GAME){
            if(col.gameObject.tag == "obstacle"){
                gm.LifeList[gm.playerIndex-1]--;
                CanvasHelperLives.text = updateScoreBoard();
                gm.ChangePlayer();
                walls.ResetWallPos();

            }
            else if(col.gameObject.tag == "Finish"){
                gm.ChangePlayer();
                walls.ResetWallPos();
            }
        }
    }

    // Update is called once per frame
    public string updateScoreBoard()
    {  
        string scoreBoard = "";
        for(int i = 1; i<=gm.maxPlayers; i++){
            scoreBoard += "Player " + i + "  " + horse[gm.LifeList[i-1]+1] + "\n";
        }
        return scoreBoard;
    }
}
