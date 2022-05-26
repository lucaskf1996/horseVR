using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headController : MonoBehaviour
{
    GameManager gm;
    wallController walls;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.GetInstance();
        walls = GameObject.Find("Walls").GetComponent<wallController>();
    }

    void OnTriggerEnter(Collider col)
    {
        if(gm.gameState == GameManager.GameState.GAME){
            if(col.gameObject.tag == "obstacle"){
                gm.LifeList[gm.playerIndex-1]--;
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
    void Update()
    {
        
    }
}
