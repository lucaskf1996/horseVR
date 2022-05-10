using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private static GameManager _instance;

    public delegate void ChangeStateDelegate();
    public static ChangeStateDelegate changeStateDelegate;
    public bool move;
    public string selectedObjName;
    public enum GameState { OBJSELECT,XYSELECT,ZSELECT,ROTATESELECT, GAME};
    public GameState gameState { get; private set; }

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
        changeStateDelegate();
    }
    private GameManager()
    {
        gameState = GameState.ZSELECT;

        move = false;

    }

}