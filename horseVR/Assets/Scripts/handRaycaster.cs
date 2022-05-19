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
    public GameObject Canvas, SelectXY, Cylinder, HalfWall, Ceiling, Block, Propeller, Target;


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
        GameObject.Find(type+"Select").transform.position = new Vector3(xy.x, xy.y, 5);
        if(type != "Target"){
            GameObject.Find(type+"Select").transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        }
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
                Canvas.GetComponent<playerselection>().increase();
            }
            else if(e.target.gameObject.tag == "<"){
                Canvas.GetComponent<playerselection>().decrease();
            }
            else if(e.target.gameObject.tag == "Confirm"){
                gm.maxPlayers = Canvas.GetComponent<playerselection>().contador;
                Canvas.SetActive(false);
                gm.ChangeState(GameManager.GameState.OBJSELECT);
            }
        }
        if(gm.gameState == GameManager.GameState.XYSELECT){
            if(e.target.gameObject.tag == "selectxy"){
                gm.tempObjCor.XY = new Vector2(e.point.x,e.point.y);
                // print(gm.tempObjCor.XY);
                SelectXY.SetActive(false);
                gm.ChangeState(GameManager.GameState.ZSELECT);
                InstantiateSelect(gm.tempObjCor.XY, gm.tempObjCor.type);
            }
        }
        if(gm.gameState == GameManager.GameState.OBJSELECT){
            if(e.target.tag == "ObjectSelection"){
                if(e.target.name == "BlockSelect"){
                    gm.tempObjCor.prefab = Block;
                    gm.tempObjCor.type = "Block";
                    gm.ChangeState(GameManager.GameState.XYSELECT);
                    SelectXY.SetActive(true);
                    // Debug.Log("Block");
                }
                if(e.target.name == "TargetSelect"){
                    gm.tempObjCor.prefab = Target;
                    gm.tempObjCor.type = "Target";
                    gm.tempObjCor.rotate = Target.transform.rotation;
                    gm.ChangeState(GameManager.GameState.XYSELECT);
                    SelectXY.SetActive(true);
                    // Debug.Log("Target");
                }
                if(e.target.name == "CeilingSelect"){
                    gm.tempObjCor.prefab = Ceiling;
                    gm.tempObjCor.type = "Ceiling";
                    gm.tempObjCor.XY = new Vector2(0f, 1.5f);
                    gm.ChangeState(GameManager.GameState.ZSELECT);
                    InstantiateSelect(gm.tempObjCor.XY, gm.tempObjCor.type);
                    // Debug.Log("Ceiling");
                }
                if(e.target.name == "HalfWallSelect"){
                    gm.tempObjCor.prefab = HalfWall;
                    gm.tempObjCor.type = "HalfWall";
                    gm.tempObjCor.XY = new Vector2(0.38f, 1f);
                    gm.ChangeState(GameManager.GameState.ZSELECT);
                    InstantiateSelect(gm.tempObjCor.XY, gm.tempObjCor.type);
                    // Debug.Log("HalfWall");
                }
                if(e.target.name == "PropellerChild"){
                    gm.tempObjCor.prefab = Propeller;
                    gm.tempObjCor.type = "Propeller";
                    gm.tempObjCor.XY = new Vector2(0f, 1f);
                    gm.ChangeState(GameManager.GameState.ZSELECT);
                    InstantiateSelect(gm.tempObjCor.XY, gm.tempObjCor.type);
                    Debug.Log("Propeller");
                }
                if(e.target.name == "CylinderSelect"){
                    gm.tempObjCor.prefab = Cylinder;
                    gm.tempObjCor.type = "Cylinder";
                    gm.tempObjCor.XY = new Vector2(0f, 1f);
                    gm.ChangeState(GameManager.GameState.ZSELECT);
                    InstantiateSelect(gm.tempObjCor.XY, gm.tempObjCor.type);
                    // Debug.Log("Cylinder");
                }

                // gm.tempObjCor.type = e.target.name;
                // gm.selectedObjName = e.target.name;
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