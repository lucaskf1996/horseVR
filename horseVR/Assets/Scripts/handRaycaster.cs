using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR.Extras;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class handRaycaster : MonoBehaviour
{

    GameManager gm;
    private SteamVR_LaserPointer steamVrLaserPointer;
    private float t;
    private string objectName;public SteamVR_Action_Boolean botao = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("InteractUI");
    SteamVR_Behaviour_Pose trackedObj;

    private void Awake()
    {
        steamVrLaserPointer = gameObject.GetComponent<SteamVR_LaserPointer>();
        steamVrLaserPointer.PointerClick += OnPointerClick;
    }

    private void Start(){

        gm = GameManager.GetInstance();
        trackedObj = GetComponent<SteamVR_Behaviour_Pose>();
    }

    private void OnPointerClick(object sender, PointerEventArgs e)
    {
        if(t>1f)
        {
            Debug.Log("objeto clicado com o laser " + e.target.tag);
            t = 0f;
            if(e.target.gameObject.tag == "target"){
                Destroy(e.target.gameObject);
            }
        }
        if(gm.gameState == GameManager.GameState.XYSELECT){
            if(e.target.gameObject.tag == "selectxy"){
                print(e.point.x);
                print(e.point.y);
            }
        }
        if(gm.gameState == GameManager.GameState.OBJSELECT){
            if(e.target.tag == "ObjectSelection"){
                if(e.target.name == "Block"){
                    gm.ChangeState(GameManager.GameState.XYSELECT);
                    Debug.Log("Block");
                }
                if(e.target.name == "Target"){
                    gm.ChangeState(GameManager.GameState.XYSELECT);
                    Debug.Log("Target");
                }
                if(e.target.name == "Ceiling"){
                    gm.ChangeState(GameManager.GameState.ZSELECT);
                    Debug.Log("Ceiling");
                }
                if(e.target.name == "HalfWall"){
                    gm.ChangeState(GameManager.GameState.ZSELECT);
                    Debug.Log("HalfWall");
                }
                if(e.target.name == "Propeller"){
                    gm.ChangeState(GameManager.GameState.ZSELECT);
                    Debug.Log("Propeller");
                }
                if(e.target.name == "Cylinder"){
                    gm.ChangeState(GameManager.GameState.ZSELECT);
                    Debug.Log("Cylinder");
                }
                gm.selectedObjName = e.target.name;
            }
        }
    }

    private void Update()
    {
        t += Time.deltaTime;
        if(gm.gameState == GameManager.GameState.ZSELECT){
            if(botao.GetStateDown(trackedObj.inputSource)){
                GameObject.Find("Walls").transform.SendMessage("SendZ");
            }
        }
    }
}