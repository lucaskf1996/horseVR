using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class corridorController : MonoBehaviour
{
    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        if(gm.gameState == GameManager.GameState.GAME){
            gameObject.transform.position += new Vector3(0f, 0f, -1f * Time.deltaTime);
        }
    }
}
