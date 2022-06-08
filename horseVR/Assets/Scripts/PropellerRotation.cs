using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PropellerRotation : MonoBehaviour
{

    GameManager gm;
    public SteamVR_ActionSet actionSet;
    public SteamVR_Input_Sources hand;

    public SteamVR_Action_Vector2 moveAction;
    public GameObject Walls;

    void Start()
    {
        
        gm = GameManager.GetInstance();
        actionSet.Activate(hand);

    }
    void SendRotate(){
        gm.tempObjCor.rotate = gameObject.transform.rotation;

        gm.ChangePlayer();
        Instantiate(gm.tempObjCor.prefab, new Vector3(gm.tempObjCor.XY.x, gm.tempObjCor.XY.y, gm.tempObjCor.Z), gm.tempObjCor.rotate, Walls.transform);
        gm.PushBack();
    }

    void Update()
    {
        
        if(gm.gameState == GameManager.GameState.ROTATESELECT && gm.tempObjCor.type == "Propeller"){
            Vector2 m = moveAction[hand].axis;
            // print(m);
            transform.Rotate (new Vector3 (0, 0, m.y*60) * Time.deltaTime);

        }
    }
}
