using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ZSelect : MonoBehaviour
{
    GameManager gm;

    public SteamVR_ActionSet actionSet;
    public SteamVR_Action_Vector2 moveAction;
    public SteamVR_Input_Sources hand;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.GetInstance();
        actionSet.Activate(hand);



    }

    // Update is called once per frame
    void Update()
    {
        Vector2 m = moveAction[hand].axis;
        print(m.y);
        gameObject.transform.position += new Vector3(0f, 0f, m.y * Time.deltaTime);
        if(gm.gameState == GameManager.GameState.ZSELECT){
            print(gameObject.transform.position.z);
        }

    }
}
