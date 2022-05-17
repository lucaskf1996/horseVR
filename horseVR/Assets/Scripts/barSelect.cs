using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class barSelect : MonoBehaviour
{

    GameManager gm;
    public SteamVR_ActionSet actionSet;
    public SteamVR_Input_Sources hand;

    public SteamVR_Action_Vector2 moveAction;
    private bool sendrotate = false;

    // Start is called before the first frame update
    void Start()
    {
        
        gm = GameManager.GetInstance();
        actionSet.Activate(hand);

    }
    void SendRotate(){
        sendrotate = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(gm.gameState == GameManager.GameState.ROTATESELECT){
            Vector2 m = moveAction[hand].axis;
            print(m);
            transform.Rotate (new Vector3 (0, 0, m.y*60) * Time.deltaTime);

        }
        if(sendrotate){
            print(gameObject.transform.rotation);
            gm.tempObjCor.rotate = gameObject.transform.rotation;
            sendrotate = false;
            if(gm.playerIndex == gm.maxPlayers){
                gm.ChangeState(GameManager.GameState.GAME);
                gm.PushBack();
                gm.ResetTemp();
            }
            else{
                gm.ChangeState(GameManager.GameState.OBJSELECT);
            }
    
        }
    }
}
