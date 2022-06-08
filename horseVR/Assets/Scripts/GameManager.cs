using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;

public class GameManager
{
    private GameObject Walls;
    private static GameManager _instance;
    private Text CanvasHelperPlayerIndex;
    private Text CanvasHelperAction;
    private Text CanvasHelperLives;
    private GameObject CanvasEnd;
    public int maxPlayers;
    public int playerIndex;
    private int lifes = 2;
    public delegate void ChangeStateDelegate();
    public static ChangeStateDelegate changeStateDelegate;
    public bool startGame;
    public bool resetGame;
    private int winIndex;
    public float multiplier = 1f;
    public GameObject Block;
    // public string selectedObjName;
    public enum GameState {PLAYERSELECT, OBJSELECT,XYSELECT,ZSELECT, SIDESELECT,ROTATESELECT, GAME, END};
    public GameState gameState { get; private set; }
    public struct ObjCordinates {
        public string type;
        public Vector2 XY;
        public float Z;
        public Quaternion rotate;
        public GameObject prefab;

        public ObjCordinates(string type, Vector2 XY,float Z,Quaternion rotate, GameObject prefab) { 
            this.prefab = prefab;
            this.type = type;
            this.XY = XY;
            this.Z = Z;
            this.rotate = rotate;
        }
    }
    public List<ObjCordinates> ObjCorList;
    public List<int> LifeList;

    public ObjCordinates tempObjCor;

    public static GameManager GetInstance()
    {
        if(_instance == null)
        {
            _instance = new GameManager();
        }
        return _instance;
    }
    public void ChangeState(GameState nextState)
    {
        if(nextState == GameState.GAME){
            CanvasHelperAction.text = "Press Trigger to Start";
        }
        else if(nextState == GameState.OBJSELECT){
            CanvasHelperAction.text = "Select Obstacle";
        }
        else if(nextState == GameState.SIDESELECT){
            CanvasHelperAction.text = "Select Obstacle's Side";
        }
        else if(nextState == GameState.XYSELECT){
            CanvasHelperAction.text = "Select Obstacle's XY ";
        }
        else if(nextState == GameState.ROTATESELECT){
            CanvasHelperAction.text = "Select Obstacle's Rotation";
        }
        else if(nextState == GameState.ZSELECT){
            CanvasHelperAction.text = "Select Obstacle's Z";
        }
        else if(nextState == GameState.END){
            CanvasHelperAction.text = "GG WP!";
            CanvasHelperPlayerIndex.text = "Player "+ winIndex;
            CanvasEnd.SetActive(true);
        }
        if((nextState == GameState.GAME) ||  (nextState == GameState.OBJSELECT)){
            foreach (int value in Enumerable.Range(0, GameObject.Find("ObjectSelection").transform.childCount)){
                GameObject.Find("ObjectSelection").transform.GetChild(value).transform.SendMessage("ResetPos");
            }
            CanvasHelperPlayerIndex.text = "Player "+ playerIndex;
            Debug.Log("ENTREI");
        }
        if(gameState == GameState.GAME && nextState == GameState.OBJSELECT){
            multiplier+=0.2f;
        }
        if(gameState == GameState.PLAYERSELECT && nextState == GameState.OBJSELECT){
            string scoreBoard = "";
            for(int i = 1; i<=maxPlayers; i++){
                scoreBoard += "Player " + i + "\n";
                Debug.Log("ENTREI");
            }
            CanvasHelperLives.text = scoreBoard;
        }
        gameState = nextState;
    }
    public void ResetTemp(){
        tempObjCor.prefab = Block;
        tempObjCor.type = "";
        tempObjCor.XY = new Vector2(0f,0f);
        tempObjCor.Z = 0f;
        tempObjCor.rotate = new Quaternion(0f,0f,0f,0f);
    }
    public void PushBack(){
        ObjCorList.Add(tempObjCor);
        ResetTemp();
    }
    public void CreateLifeList(){
        for(int player = 0;player < maxPlayers;player++){
            LifeList.Add(lifes);
        }
    }
    public void ChangePlayer(){
        playerIndex += 1;
        startGame = false;
        checkLives();
        if(gameState == GameState.END){
            return;
        }

        if(playerIndex<=maxPlayers && LifeList[playerIndex-1]<0){
            ChangePlayer();
        }

        if(playerIndex > maxPlayers){
            if(gameState == GameState.GAME){
                playerIndex = 1;
                ChangeState(GameState.OBJSELECT);
            }
            else if(gameState != GameState.GAME){
                playerIndex = 1;
                ChangeState(GameState.GAME);
            }
        }
        else 
        {
            if(gameState != GameState.GAME){
                ChangeState(GameState.OBJSELECT);
            }
            else{
                ChangeState(GameState.GAME);
            }
        }

    }

    public void checkLives(){

        int dead = 0;
        int contador = 0;
        if(maxPlayers != 1){
            foreach(int player in LifeList){
                if(player==0){
                    dead++;
                }
                else{
                    winIndex = contador+1;
                }
                contador++;

            }
            if(dead==maxPlayers-1){
                Debug.Log("END GAME");
                ChangeState(GameManager.GameState.END);
            }
        }
        else{
            if(LifeList[0]==0){
                Debug.Log("END GAME");

                ChangeState(GameManager.GameState.END);
            }
        }

    }
    public void ResetGame(){
        CanvasHelperPlayerIndex = GameObject.FindWithTag("CanvasHelperText").GetComponent<Text>();
        CanvasHelperAction = GameObject.FindWithTag("CanvasHelperAction").GetComponent<Text>();
        CanvasHelperLives = GameObject.FindWithTag("CanvasLives").GetComponent<Text>();
        gameState = GameState.PLAYERSELECT;
        CanvasEnd = GameObject.Find("CanvasEnd");
        CanvasEnd.SetActive(false);
        CanvasHelperPlayerIndex.text = "GL";
        CanvasHelperAction.text = "HF";
        CanvasHelperLives.text = "";
        playerIndex = 1;
        maxPlayers = 1;
        startGame = false;
        resetGame = false;
        multiplier = 4f;
        LifeList = new List<int>();
        ObjCorList = new List<ObjCordinates>();
    }
    
    private GameManager()
    {
        Walls = GameObject.Find("Walls");
        CanvasHelperPlayerIndex = GameObject.FindWithTag("CanvasHelperText").GetComponent<Text>();
        CanvasHelperAction = GameObject.FindWithTag("CanvasHelperAction").GetComponent<Text>();
        CanvasHelperLives = GameObject.FindWithTag("CanvasLives").GetComponent<Text>();
        CanvasEnd = GameObject.Find("CanvasEnd");
        CanvasEnd.SetActive(false);
        gameState = GameState.PLAYERSELECT;
        // CanvasHelperPlayerIndex.text = "GL";
        // CanvasHelperAction.text = "HF";
        // CanvasHelperLives.text = "";
        playerIndex = 1;
        maxPlayers = 1;
        startGame = false;
        resetGame = false;
        multiplier = 4f;
        LifeList = new List<int>();
        ObjCorList = new List<ObjCordinates>();
    }

}