using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class wallController : MonoBehaviour
{

    GameManager gm;
    // public GameObject wall;
    public SteamVR_ActionSet actionSet;
    public SteamVR_Action_Vector2 moveAction;
    public SteamVR_Input_Sources hand;
    public SteamVR_Action_Boolean botao = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("InteractUI");
    SteamVR_Behaviour_Pose trackedObj;
    private bool sendz = false;



    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.GetInstance();
        actionSet.Activate(hand);
        trackedObj = GetComponent<SteamVR_Behaviour_Pose>();


    }

    void SendZ(){
        sendz = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(gm.move){
            gameObject.transform.position += new Vector3(0f, 0f, -1f * Time.deltaTime);
        }
        // if(botao.GetStateDown(trackedObj.inputSource)){
        //     print("salve");
        // }
        if(gm.gameState == GameManager.GameState.ZSELECT){
            Vector2 m = moveAction[hand].axis;
            gameObject.transform.position += new Vector3(0f, 0f, 5f* m.y * Time.deltaTime);

            if(gameObject.transform.position.z <-25f){
                gameObject.transform.position = new Vector3(0f, 0f, -25f);

            }
            else if(gameObject.transform.position.z >0.5f){
                gameObject.transform.position = new Vector3(0f, 0f, 0f);

            }
            if(sendz){
                print(gameObject.transform.position.z);
                sendz = false;
                if(gm.selectedObjName == "Cylinder"){
                    gm.ChangeState(GameManager.GameState.ROTATESELECT);
                }
                else{
                    if(gm.playerIndex == gm.maxPlayers){
                        gm.ChangeState(GameManager.GameState.GAME);
                    }
                    else{
                        gm.ChangeState(GameManager.GameState.OBJSELECT);
                    }
                }

            }

        }
    }
}
