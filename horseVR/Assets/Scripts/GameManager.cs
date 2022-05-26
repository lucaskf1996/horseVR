using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class GameManager
{
    private static GameManager _instance;
    public int maxPlayers;
    public int playerIndex;
    private int lifes = 1;
    private bool gameEnd;
    public delegate void ChangeStateDelegate();
    public static ChangeStateDelegate changeStateDelegate;
    public bool startGame;
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
    public List<ObjCordinates> ObjCorList = new List<ObjCordinates>();
    public List<int> LifeList = new List<int>();

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
        if((nextState == GameState.GAME)){
                playerIndex = 1;
        }
        if((nextState == GameState.GAME) ||  (nextState == GameState.OBJSELECT)){
            foreach (int value in Enumerable.Range(0, GameObject.Find("ObjectSelection").transform.childCount)){
                GameObject.Find("ObjectSelection").transform.GetChild(value).transform.SendMessage("ResetPos");
            }
        }
        gameState = nextState;
        Debug.Log(gameState);
    }
    public void ResetTemp(){
        tempObjCor.prefab = Block;
        tempObjCor.type = "";
        tempObjCor.XY = new Vector2(0f,0f);
        tempObjCor.Z = 0f;
        tempObjCor.rotate = new Quaternion(0f,0f,0f,0f);
    }
    public void PushBack(){
        Debug.Log(tempObjCor.Z);
        ObjCorList.Add(tempObjCor);
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
        if(playerIndex > maxPlayers && gameState == GameState.GAME){
            ChangeState(GameManager.GameState.OBJSELECT);
            playerIndex = 1;
        }
    }

    public void checkLives(){
        int contador = 0;
        if(maxPlayers != 1){
            foreach(int player in LifeList){
                if(player==-1){
                    contador++;
                }
            }
            if(contador==maxPlayers-1){
                ChangeState(GameManager.GameState.END);
            }
        }
        else{
            if(LifeList[0]==-1){
                ChangeState(GameManager.GameState.END);
            }
        }

    }

    private GameManager()
    {
        gameState = GameState.PLAYERSELECT;
        playerIndex = 1;
        maxPlayers = 1;
        startGame = false;
        gameEnd = false;
        startGame = false;
    }

}