using System.Collections;
using System.Collections.Generic;
using System;
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
    private float distanceZ;


    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.GetInstance();
        actionSet.Activate(hand);


    }

    void SendZ(){
        sendz = true;
    }
    public void ResetWallPos(){
        gameObject.transform.position = new Vector3(0f, 0f, 0f);

    }
    // Update is called once per frame
    void Update()
    {
        if((gm.gameState == GameManager.GameState.GAME) && (gm.startGame)){
            print("startGame: "+ gm.startGame);
            gameObject.transform.position += new Vector3(0f, 0f, -6f * Time.deltaTime);
        }
        else if(gm.gameState == GameManager.GameState.ZSELECT){
            Vector2 m = moveAction[hand].axis;
            gameObject.transform.position += new Vector3(0f, 0f, 5f* m.y * Time.deltaTime);

            if(gameObject.transform.position.z <-25f){
                gameObject.transform.position = new Vector3(0f, 0f, -25f);

            }
            else if(gameObject.transform.position.z >0.5f){
                ResetWallPos();

            }
            if(sendz){
                sendz = false;
                bool validCor  = CheckZIfCordValid(-gameObject.transform.position.z + 5f);
                if(validCor){
                    gm.tempObjCor.Z = -gameObject.transform.position.z + 5f;
                    if(gm.tempObjCor.type == "Cylinder"){
                        gm.ChangeState(GameManager.GameState.ROTATESELECT);
                    }
                    else{
                        Instantiate(gm.tempObjCor.prefab, new Vector3(gm.tempObjCor.XY.x, gm.tempObjCor.XY.y, gm.tempObjCor.Z), gm.tempObjCor.rotate, transform);
                        gm.PushBack();
                        gm.ResetTemp();
                        if(gm.playerIndex == gm.maxPlayers){
                            gm.ChangeState(GameManager.GameState.GAME);
                        }
                        else{
                            gm.playerIndex++;
                            gm.ChangeState(GameManager.GameState.OBJSELECT);
                        }
                    }
                    gameObject.transform.position = new Vector3(0f,0f,0f);
                }
            }
        }
    }
    public bool CheckZIfCordValid(float wantedCord){
        float offset = 1f;
        foreach(GameManager.ObjCordinates cordinate in gm.ObjCorList){
            if(Math.Abs(Math.Abs(cordinate.Z)- Math.Abs(wantedCord)) < offset){
                return false;
            }
        }
        return true;
    }
}