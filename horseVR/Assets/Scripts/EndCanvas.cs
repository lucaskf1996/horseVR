using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndCanvas : MonoBehaviour
{
    public Button Restart;
    public Scene SelfScene;
    GameManager gm;
    // Start is called before the first frame update
    void Start(){
        gm = GameManager.GetInstance();
        if(gm.resetGame) gm.ResetGame();
    }
}
