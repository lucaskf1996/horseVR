using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndCanvas : MonoBehaviour
{
    public Button Restart;
    public Scene SelfScene;
    // Start is called before the first frame update
    public void restartGame(){
        SceneManager.LoadScene("Planing", LoadSceneMode.Single);
    }    
}
