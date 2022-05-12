using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class barController : MonoBehaviour
{

    private bool right;
    private Vector3 startPosition;
    private float t;
    private float temp;
    GameManager gm;
    public SteamVR_ActionSet actionSet;
    public SteamVR_Input_Sources hand;

    public SteamVR_Action_Vector2 moveAction;
    private float startX, startY;
    private bool sendrotate = false;

    // Start is called before the first frame update
    void Start()
    {
        right = true;
        startPosition = gameObject.transform.position;
        t = 0;
        gm = GameManager.GetInstance();
        startX = transform.position.x;
        startY = transform.position.y;
        actionSet.Activate(hand);

    }
    void SendRotate(){
        sendrotate = true;
    }

    // Update is called once per frame
    void Update()
    {
        

        if(gm.move){
            gameObject.transform.position += new Vector3(0f, 0f, -1f * Time.deltaTime);

            if(right){
                gameObject.transform.position += gameObject.transform.right * Time.deltaTime;
                if(temp > 1f && t > 0.2f){
                    right = false;
                    t = 0;
                }
            }
            else if(!right){
                gameObject.transform.position -= gameObject.transform.right * Time.deltaTime;
                if(temp > 1f && t > 0.2f){
                    right = true;
                    t = 0;
                }
            }
            temp = Mathf.Sqrt(Mathf.Pow(gameObject.transform.position.x-startX, 2) + Mathf.Pow(gameObject.transform.position.y-startY, 2));
            t+=Time.deltaTime;
        }
        if(gm.gameState == GameManager.GameState.ROTATESELECT){
            Vector2 m = moveAction[hand].axis;
            transform.Rotate (new Vector3 (0, 0, m.y) * Time.deltaTime);

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
