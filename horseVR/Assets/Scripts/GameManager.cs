using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private static GameManager _instance;
    public int maxPlayers;
    public int playerIndex;

    public delegate void ChangeStateDelegate();
    public static ChangeStateDelegate changeStateDelegate;
    public bool move;
    public string selectedObjName;
    public enum GameState { OBJSELECT,XYSELECT,ZSELECT,ROTATESELECT, GAME};
    public GameState gameState { get; private set; }
    public struct ObjCordinates {
        public string type;
        public Vector2 XY;
        public float Z;
        public Quaternion rotate;

        public ObjCordinates(string type, Vector2 XY,float Z,Quaternion rotate) {
        this.type = type;
        this.XY = XY;
        this.Z = Z;
        this.rotate = rotate;
    }
    }
    public List<ObjCordinates> ObjCorList = new List<ObjCordinates>();

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
        gameState = nextState;
    }
    public void ResetTemp(){
        ObjCordinates tempObjCor = new ObjCordinates("", new Vector2(0f,0f), 0f,new Quaternion(0f,0f,0f,0f));
    }
    public void PushBack(){
        ObjCorList.Add(tempObjCor);
    }
    private GameManager()
    {
        gameState = GameState.ZSELECT;
        playerIndex = 1;
        maxPlayers = 1;
        move = false;

    }

}