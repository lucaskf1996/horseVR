using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class playerselection : MonoBehaviour
{
    public Button menos, mais;
    public Text text;
    public int contador;

    void Start(){
        contador = 1;
        text.text = Convert.ToString(contador);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void increase(){
        contador++;
        print("teste");
        text.text = Convert.ToString(contador); 
    }    

    public void decrease(){
        if(contador!=1){
            contador--;
        }
        text.text = Convert.ToString(contador); 
    }    
}

