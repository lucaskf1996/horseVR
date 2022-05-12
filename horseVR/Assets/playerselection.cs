using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class playerselection : MonoBehaviour
{
    public Button menos, mais;
    public Text text;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void increase(){
        int players = Int32.TryParse(text.text);
        players++;
        text.text = players; 
    }    

    public void decrease(){
        int players = Int32.TryParse(text.text);
        players--;
        text.text = players; 
    }    
}

