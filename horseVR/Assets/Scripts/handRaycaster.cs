using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR.Extras;
using Valve.VR;
using Valve.VR.InteractionSystem;
using System;
using System.Linq;

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


    private void InstantiateSelect(Vector2 xy, string type){
        // Vector3 oldPos = GameObject.Find(type+"Select").transform.position;
        GameObject.Find(type+"Select").transform.position = new Vector3(xy.x, xy.y, 5);
        // return oldPos;
    }

    private void OnPointerClick(object sender, PointerEventArgs e)
    {
        if(t>1f)
        {
            Debug.Log("objeto clicado com o laser " + e.target.name);
            t = 0f;
            if(e.target.gameObject.tag == "target"){
                Destroy(e.target.gameObject);
            }
        }
        if(gm.gameState == GameManager.GameState.PLAYERSELECT){
            if(e.target.gameObject.tag == ">"){
                GameObject.Find("Canvas").GetComponent<playerselection>().increase();
            }
            else if(e.target.gameObject.tag == "<"){
                GameObject.Find("Canvas").GetComponent<playerselection>().decrease();
            }
            else if(e.target.gameObject.tag == "Confirm"){
                gm.maxPlayers = GameObject.Find("Canvas").GetComponent<playerselection>().contador;
                GameObject.Find("Canvas").SetActive(false);
                gm.ChangeState(GameManager.GameState.OBJSELECT);
            }
        }
        if(gm.gameState == GameManager.GameState.XYSELECT){
            if(e.target.gameObject.tag == "selectxy"){
                print(e.point.x);
                print(e.point.y);
                gm.tempObjCor.XY = new Vector2(e.point.x,e.point.y);
                GameObject.Find("SelectXY").SetActive(false);
                gm.ChangeState(GameManager.GameState.ZSELECT);
                InstantiateSelect(gm.tempObjCor.XY, gm.tempObjCor.type);
            }
        }
        if(gm.gameState == GameManager.GameState.OBJSELECT){
            foreach (int value in Enumerable.Range(0, GameObject.Find("ObjectSelection").transform.childCount)){
                transform.GetChild(value).transform.SendMessage("ResetPos");
            }
            if(e.target.tag == "ObjectSelection"){
                if(e.target.name == "BlockSelect"){
                    gm.tempObjCor.type = "Block";
                    gm.ChangeState(GameManager.GameState.XYSELECT);
                    // Debug.Log("Block");
                    GameObject.Find("SelectXY").SetActive(true);
                }
                if(e.target.name == "TargetSelect"){
                    gm.tempObjCor.type = "Target";
                    gm.ChangeState(GameManager.GameState.XYSELECT);
                    // Debug.Log("Target");
                    GameObject.Find("SelectXY").SetActive(true);
                }
                if(e.target.name == "CeilingSelect"){
                    gm.tempObjCor.type = "Ceiling";
                    gm.tempObjCor.XY = new Vector2(0f, 1.5f);
                    gm.ChangeState(GameManager.GameState.ZSELECT);
                    InstantiateSelect(gm.tempObjCor.XY, gm.tempObjCor.type);
                    // Debug.Log("Ceiling");
                }
                if(e.target.name == "HalfWallSelect"){
                    gm.tempObjCor.type = "HalfWall";
                    gm.tempObjCor.XY = new Vector2(0.38f, 1f);
                    gm.ChangeState(GameManager.GameState.ZSELECT);
                    InstantiateSelect(gm.tempObjCor.XY, gm.tempObjCor.type);
                    // Debug.Log("HalfWall");
                }
                if(e.target.name == "PropellerSelect"){
                    gm.tempObjCor.type = "Propeller";
                    gm.tempObjCor.XY = new Vector2(0f, 1f);
                    gm.ChangeState(GameManager.GameState.ZSELECT);
                    InstantiateSelect(gm.tempObjCor.XY, gm.tempObjCor.type);
                    // Debug.Log("Propeller");
                }
                if(e.target.name == "CylinderSelect"){
                    gm.tempObjCor.type = "Cylinder";
                    gm.tempObjCor.XY = new Vector2(0f, 1f);
                    gm.ChangeState(GameManager.GameState.OBJSELECT);
                    InstantiateSelect(gm.tempObjCor.XY, gm.tempObjCor.type);
                    // Debug.Log("Cylinder");
                }

                gm.tempObjCor.type = e.target.name;
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
        if(gm.gameState == GameManager.GameState.ROTATESELECT){
            if(botao.GetStateDown(trackedObj.inputSource)){
                GameObject.Find("CylinderSelect").transform.SendMessage("SendRotate");
            }
        }
    }
}